using UnityEngine;
using System.Collections;

public class ChillSlowEffect : MonoBehaviour {

    float TargetOriginalSpeed = 0;
    PlayerAbilityValue TargetPlayer_Data;

    public float EndTime = 5;
    public float SlowPower=1;

    void Start () {
        TargetPlayer_Data = GetComponent<PlayerAbilityValue>();
        //TargetOriginalSpeed = TargetPlayer_Data.MOVE_SPEED;
        TargetOriginalSpeed = 5;
        TargetPlayer_Data.MOVE_SPEED *= 0.5f;
        
        
        //緩速特效
        //GameObject SE = PhotonNetwork.Instantiate("ChillSlowEffect", transform.position, Quaternion.identity, 0);
        GameObject SE =Instantiate(Resources.Load("ChillSlowEffect"), transform.position, Quaternion.identity) as GameObject;
        SE.transform.parent = gameObject.transform;
        Destroy(SE, EndTime);
        
    }
	
	
	void Update () {
        EndTime -= Time.deltaTime;
        if (EndTime <= 0)
        {
            TargetPlayer_Data.MOVE_SPEED = TargetOriginalSpeed;
            Destroy(gameObject.GetComponent<ChillSlowEffect>());
        }
    }
}
