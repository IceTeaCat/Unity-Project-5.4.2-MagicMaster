using UnityEngine;
using System.Collections;

//燃燒
public class Combustion : Photon.MonoBehaviour
{
    public  GameObject Host;
    public int Team;
    GameObject CE;
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
                    CE = PhotonNetwork.Instantiate("CombustionEffect", other.transform.position, Quaternion.identity, 0) as GameObject;
                    GetComponent<PhotonView>().RPC("GetTarget", PhotonTargets.All, new object[] { CE.GetComponent<PhotonView>().viewID, other.gameObject.transform.parent.GetComponent<PhotonView>().viewID });
                }
            }
        }
    }

   

    [PunRPC]
    void GetTarget(int Target_ID, int other_ID)
    {
        PhotonView.Find(Target_ID).gameObject.GetComponent<CombustionDamage>().Target = PhotonView.Find(other_ID).gameObject;
        PhotonView.Find(Target_ID).gameObject.transform.parent = PhotonView.Find(other_ID).gameObject.transform;

        PhotonView.Find(Target_ID).GetComponent<CombustionDamage>().Host = Host;
        PhotonView.Find(Target_ID).GetComponent<CombustionDamage>().Team = Team;
    }

  




}