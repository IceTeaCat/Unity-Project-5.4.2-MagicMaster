using UnityEngine;
using System.Collections;

public class DizzyEffect : MonoBehaviour {

    public float DizzyTime=2;
	
    public float OriginSpeed;

	void Start () {
        OriginSpeed = GetComponent<PlayerAbilityValue>().MOVE_SPEED;
        GetComponent<PlayerAbilityValue>().MOVE_SPEED=0;

        //Test
        GetComponent<EnemyTest>().enabled = false;
    }
	
	
	void Update () {
        DizzyTime -= Time.deltaTime;
        if(DizzyTime<=0)
        {
            GetComponent<PlayerAbilityValue>().MOVE_SPEED = OriginSpeed;
            //Test
            GetComponent<EnemyTest>().enabled = true;
            Destroy(GetComponent<DizzyEffect>());
        }

	}
}
