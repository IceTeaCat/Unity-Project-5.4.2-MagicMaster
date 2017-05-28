using UnityEngine;
using System.Collections;

public class ElectricIncreaseRange : MonoBehaviour {

    public int Team;

	void Start () {
	
	}
	
	
	void Update () {
	
	}


    void OnTriggerStay(Collider other)
    {
        //打到玩家
        if (other.gameObject.tag == "Player")
        {
            PlayerAbilityValue TargetPlayer_Data = other.transform.parent.GetComponent<PlayerAbilityValue>();
            //碰到友方
            if (TargetPlayer_Data.TEAM == gameObject.transform.parent.GetComponent<PlayerAbilityValue>().TEAM)
            {
                if (TargetPlayer_Data.gameObject.GetComponent<ElectricIncrease>() == null)
                {
                    TargetPlayer_Data.gameObject.AddComponent<ElectricIncrease>();
                }
            }
        }
    }


    void OnTriggerExit(Collider other)
    {
        //打到玩家
        if (other.gameObject.tag == "Player")
        {
            PlayerAbilityValue TargetPlayer_Data = other.transform.parent.GetComponent<PlayerAbilityValue>();
            //碰到友方
            if (TargetPlayer_Data.TEAM == gameObject.transform.parent.GetComponent<PlayerAbilityValue>().TEAM)
            {
                if (TargetPlayer_Data.gameObject.GetComponent<ElectricIncrease>() != null)
                {
                    Destroy(TargetPlayer_Data.gameObject.GetComponent<ElectricIncrease>());
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
            stream.SendNext(Team);
        }
        /*
        else
        {
            correctFireBallPos = (Vector3)stream.ReceiveNext();
            correctFireBallRot = (Quaternion)stream.ReceiveNext();
            Team = (int)stream.ReceiveNext();

            if (!appliedInitialUpdate)
            {
                appliedInitialUpdate = true;
                transform.position = correctFireBallPos;
                transform.rotation = correctFireBallRot;
            }
        }
        */
    }



}
