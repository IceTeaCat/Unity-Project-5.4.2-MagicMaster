using UnityEngine;
using System.Collections;

public class CombustionDamage : MonoBehaviour
{
    //燃燒的對象
    public GameObject Target;
    private bool appliedInitialUpdate;

    private Vector3 correctFireBallPos = Vector3.zero;

    void Start()
    {
        Destroy(gameObject, 2);
    }

    void Update()
    {
        //GetComponent<PhotonView>().RPC("Damage", PhotonTargets.All, Target.GetComponent<PhotonView>().viewID);
        
        if (Target.gameObject.GetComponent<PlayerAbilityValue>().HEALTH >= 0)
        {
            transform.position = Target.transform.position;
            Target.gameObject.GetComponent<PlayerAbilityValue>().HEALTH -= 1;
            print("燒");
        }
        else
        {
           PhotonNetwork.Destroy(gameObject);
        }
        
    }


    [PunRPC]
    void Damage(int Target_ID)
    {
        if(PhotonView.Find(Target_ID).gameObject.GetComponent<PlayerAbilityValue>().HEALTH >= 0)
        {
            transform.position = PhotonView.Find(Target_ID).gameObject.transform.position;
            PhotonView.Find(Target_ID).gameObject.GetComponent<PlayerAbilityValue>().HEALTH -= 1;
            print("燒");
        }
        else
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }


    
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(Target.gameObject.GetComponent<PlayerAbilityValue>().HEALTH);
        }
        else
        {
            transform.position = (Vector3)stream.ReceiveNext();
            //Target.gameObject.GetComponent<PlayerAbilityValue>().HEALTH= (int)stream.ReceiveNext();
        }
        
    }
    



}
