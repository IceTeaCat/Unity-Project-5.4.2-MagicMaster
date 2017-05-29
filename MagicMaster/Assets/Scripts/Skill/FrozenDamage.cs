using UnityEngine;
using System.Collections;

//冰凍效果
public class FrozenDamage : Photon.MonoBehaviour {

    public float TargetOriginalSpeed = 0;
    PlayerAbilityValue TargetPlayer_Data;

    public float EndTime=5;

    void Start () {

        //photonView.RPC("FrozenEffect", PhotonTargets.All,gameObject.GetComponent<PhotonView>().viewID);
        
        TargetPlayer_Data= GetComponent<PlayerAbilityValue>();
        TargetOriginalSpeed = TargetPlayer_Data.MOVE_SPEED;
        TargetPlayer_Data.MOVE_SPEED = 0;
        
    }	
	
	void Update () {
        EndTime -= Time.deltaTime;
        if(EndTime<=0)
        {
            //photonView.RPC("FrozenFree", PhotonTargets.All, gameObject.GetComponent<PhotonView>().viewID);
            
            TargetPlayer_Data.MOVE_SPEED = TargetOriginalSpeed;
            Destroy(gameObject.GetComponent<FrozenDamage>());
            
        }
    }

    /*
    [PunRPC]
    void FrozenEffect(int obj_ID)
    {
        TargetOriginalSpeed = PhotonView.Find(obj_ID).gameObject.GetComponent<PlayerAbilityValue>().MOVE_SPEED;
        PhotonView.Find(obj_ID).gameObject.GetComponent<PlayerAbilityValue>().MOVE_SPEED = 0;
    }

    [PunRPC]
    void FrozenFree(int obj_ID)
    {
        PhotonView.Find(obj_ID).gameObject.GetComponent<PlayerAbilityValue>().MOVE_SPEED = TargetOriginalSpeed;
        Destroy(PhotonView.Find(obj_ID).gameObject.GetComponent<FrozenDamage>());
    }
    */

    }
