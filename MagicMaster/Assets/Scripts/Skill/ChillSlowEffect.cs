using UnityEngine;
using System.Collections;

public class ChillSlowEffect : MonoBehaviour {

    float TargetOriginalSpeed = 0;
    PlayerAbilityValue TargetPlayer_Data;

    public float EndTime = 5;
    public float SlowPower=1;

    void Start () {
        TargetPlayer_Data = GetComponent<PlayerAbilityValue>();
        TargetOriginalSpeed = TargetPlayer_Data.MOVE_SPEED;
        TargetPlayer_Data.MOVE_SPEED *= 0.5f;
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
