using UnityEngine;
using System.Collections;

//燃燒
public class Combustion : Photon.MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (GetComponent<PhotonView>().isMine)
        {
            if (other.tag == "Player")
            {
                PlayerAbilityValue TargetPlayer_Data = other.transform.parent.GetComponent<PlayerAbilityValue>();
                //打到敵方
                if (TargetPlayer_Data.TEAM != gameObject.GetComponent<FireBall>().Team)
                {
                    GameObject CE = PhotonNetwork.Instantiate("CombustionEffect", other.transform.position, Quaternion.identity, 0) as GameObject;
                    GetComponent<PhotonView>().RPC("GetTarget", PhotonTargets.All, new object[] { CE.GetComponent<PhotonView>().viewID, other.gameObject.transform.parent.GetComponent<PhotonView>().viewID });
                }
            }
        }
    }



    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            transform.position = (Vector3)stream.ReceiveNext();
            transform.rotation = (Quaternion)stream.ReceiveNext();
        }
    }


    [PunRPC]
    void GetTarget(int Target_ID, int other_ID)
    {
        //print(Target_ID + " : " + other_ID);
        PhotonView.Find(Target_ID).gameObject.GetComponent<CombustionDamage>().Target = PhotonView.Find(other_ID).gameObject;
    }


}