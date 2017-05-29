using UnityEngine;
using System.Collections;

//結凍
public class Frozen : Photon.MonoBehaviour {

    GameObject FME;

    void OnTriggerEnter(Collider other)
    {
        if (photonView.isMine)
        {
            if (other.tag == "Player")
            {
                PlayerAbilityValue TargetPlayer_Data = other.transform.parent.GetComponent<PlayerAbilityValue>();
                //打到敵方
                if (TargetPlayer_Data.TEAM != gameObject.GetComponent<Ice>().Team)
                {
                    //未標記就標記
                    if (TargetPlayer_Data.gameObject.GetComponent<FrozenMark>() == null)
                    {

                        FME = PhotonNetwork.Instantiate("FrozenMarkEffect", transform.position, Quaternion.identity, 0);
                        photonView.RPC("AddFrozenMark", PhotonTargets.All,new object[] { TargetPlayer_Data.gameObject.GetComponent<PhotonView>().viewID, FME.GetComponent<PhotonView>().viewID });
                        //TargetPlayer_Data.gameObject.AddComponent<FrozenMark>();
                    }
                    else
                    {
                        photonView.RPC("RemoveFrozenMark", PhotonTargets.All, TargetPlayer_Data.gameObject.GetComponent<PhotonView>().viewID);
                        photonView.RPC("AddFrozenDamage", PhotonTargets.All, TargetPlayer_Data.gameObject.GetComponent<PhotonView>().viewID);

                        /*
                        Destroy(TargetPlayer_Data.gameObject.GetComponent<FrozenMark>().FME);
                        Destroy(TargetPlayer_Data.gameObject.GetComponent<FrozenMark>());
                        TargetPlayer_Data.gameObject.AddComponent<FrozenDamage>();
                        */

                        //結凍特效
                        GameObject FE = PhotonNetwork.Instantiate("FireBall_Explode", transform.position, Quaternion.identity, 0);
                        //Destroy(FE, TargetPlayer_Data.GetComponent<FrozenDamage>().EndTime);
                    }

                }
            }
        }
    }

    [PunRPC]
    void AddFrozenMark(int Target_ID, int FME_ID)
    {
        PhotonView.Find(Target_ID).gameObject.AddComponent<FrozenMark>();

        PhotonView.Find(FME_ID).gameObject.transform.parent = PhotonView.Find(Target_ID).gameObject.transform;
        PhotonView.Find(FME_ID).gameObject.transform.localPosition = Vector3.zero;
        PhotonView.Find(Target_ID).gameObject.GetComponent<FrozenMark>().FME = PhotonView.Find(FME_ID).gameObject;
    }

    [PunRPC]
    void RemoveFrozenMark(int Target_ID)
    {
        Destroy(PhotonView.Find(Target_ID).gameObject.GetComponent<FrozenMark>().FME);
        Destroy(PhotonView.Find(Target_ID).gameObject.GetComponent<FrozenMark>());
    }

    [PunRPC]
    void AddFrozenDamage(int Target_ID)
    {
        PhotonView.Find(Target_ID).gameObject.AddComponent<FrozenDamage>();
    }


}
