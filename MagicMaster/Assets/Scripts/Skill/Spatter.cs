using UnityEngine;
using System.Collections;

//濺散
public class Spatter : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (GetComponent<PhotonView>().isMine)
        {
            if (other.tag == "Fireball")
            {
                GetComponent<PhotonView>().RPC("DestoryFireball", PhotonTargets.All, other.gameObject.GetComponent<PhotonView>().viewID);
                PhotonNetwork.Destroy(gameObject);

                GameObject SE = PhotonNetwork.Instantiate("SpatterEffect", transform.position, Quaternion.identity, 0);


                GetComponent<PhotonView>().RPC("SetSETeam", PhotonTargets.All, SE.GetComponent<PhotonView>().viewID);
                //SE.GetComponent<SpatterDamage>().Team = GetComponent<FireBall>().Team;

                print(SE.GetComponent<SpatterDamage>().Team + ":" + GetComponent<FireBall>().Team);
            }
        }
    }

    [PunRPC]
    void DestoryFireball(int Fireball_ID)
    {
        Destroy(PhotonView.Find(Fireball_ID).gameObject);
    }

    
    [PunRPC]
    void SetSETeam(int SE_ID)
    {
        PhotonView.Find(SE_ID).gameObject.GetComponent<SpatterDamage>().Team = GetComponent<FireBall>().Team;
    }
    


}
