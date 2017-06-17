using UnityEngine;
using System.Collections;

//冰凍效果
public class FrozenDamage : Photon.MonoBehaviour
{

    public float TargetOriginalSpeed = 0;
    PlayerAbilityValue TargetPlayer_Data;

    public float EndTime = 5;

    void Start()
    {
        TargetPlayer_Data = GetComponent<PlayerAbilityValue>();
        //TargetOriginalSpeed = TargetPlayer_Data.MOVE_SPEED;
        TargetOriginalSpeed = 5;
        TargetPlayer_Data.MOVE_SPEED = 0;

    }

    void Update()
    {
        EndTime -= Time.deltaTime;
        if (EndTime <= 0)
        {
            TargetPlayer_Data.MOVE_SPEED = TargetOriginalSpeed;
            Destroy(gameObject.GetComponent<FrozenDamage>());
        }
    }

}
