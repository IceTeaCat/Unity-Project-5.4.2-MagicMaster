using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DamageRangeCircle : MonoBehaviour {

    public GameObject Player;
    PlayerAbilityValue TargetPlayer_Data;

    public int Team = -1;
    public List<GameObject> Enemys=new List<GameObject>();


    void Start () {

        
    }
	
	
	void Update () {
    }

    void OnTriggerEnter(Collider other)
    {      
        //打到玩家
        if (other.gameObject.tag == "Player")
        {
            TargetPlayer_Data = other.transform.parent.GetComponent<PlayerAbilityValue>();
            //打到敵方
            if (TargetPlayer_Data.TEAM != Team)
            {
                    Enemys.Add(TargetPlayer_Data.gameObject);
            }
        }
        

    }



}
