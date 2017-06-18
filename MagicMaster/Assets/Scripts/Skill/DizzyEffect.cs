using UnityEngine;
using System.Collections;

public class DizzyEffect : MonoBehaviour {

    public float DizzyTime=2;
	
    public float OriginSpeed;

    GameObject DE;

    void Start () {
        OriginSpeed = GetComponent<PlayerAbilityValue>().MOVE_SPEED;
        GetComponent<PlayerAbilityValue>().MOVE_SPEED=0;

        GetComponent<PlayerController>().enabled = false;
        GetComponent<PlayerSkill>().enabled = false;

        if (GetComponent<PhotonView>().isMine)
            //暈眩特效
            DE = PhotonNetwork.Instantiate("DizzyEffect", transform.position, Quaternion.Euler(90,0,0), 0);
        
    }
	
	
	void Update () {
        DizzyTime -= Time.deltaTime;
        if(DizzyTime<=0)
        {
            GetComponent<PlayerController>().enabled = true;
            GetComponent<PlayerSkill>().enabled = true;

            GetComponent<PlayerAbilityValue>().MOVE_SPEED = OriginSpeed;

            if (GetComponent<PhotonView>().isMine)
                PhotonNetwork.Destroy(PhotonView.Find(DE.GetComponent<PhotonView>().viewID));
            
            Destroy(GetComponent<DizzyEffect>());
        }

	}
}
