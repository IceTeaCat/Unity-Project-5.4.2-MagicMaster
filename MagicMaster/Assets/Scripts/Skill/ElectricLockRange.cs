using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ElectricLockRange : MonoBehaviour
{
    float DamageTime = 0.01f;

    public GameObject Player;

    public GameObject PlayerOrEnemy;
    PlayerAbilityValue TargetPlayer_Data;

    public int Team = -1;
    public List<GameObject> Enemys = new List<GameObject>();
    public List<float> D = new List<float>();
    int MinD;

    public GameObject TargetEnemy;

    int NowChainCount = 0;

    void Start()
    {
        TargetEnemy = null;
    }


    void Update()
    {
        DamageTime -= Time.deltaTime;
        CaleDistance();
        if (DamageTime <= 0 )
        {
            if (Enemys.Count != 0 && !GetComponent<ElectricMultiple>() && !GetComponent<ElectricChain>())
            {
                GameObject ElectricLR = PhotonNetwork.Instantiate("ElectricLR", TargetEnemy.transform.position, Quaternion.identity, 0) as GameObject;
                ElectricLR.GetComponent<Electric>().LR = ElectricLR.GetComponent<LineRenderer>();
                ElectricLR.GetComponent<Electric>().origin = PlayerOrEnemy;
                ElectricLR.GetComponent<Electric>().destination = TargetEnemy;

                ElectricLR.GetComponent<Electric>().Target = TargetEnemy;

                if(Player.GetComponent<ElectricIncrease>())
                {
                    ElectricLR.GetComponent<Electric>().isPowerUp = true;
                }
                print(Player.name);
                Destroy(gameObject);
            }

        }




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
                D.Add(Vector3.Distance(TargetPlayer_Data.gameObject.transform.position, PlayerOrEnemy.transform.position));
            }
        }
    }

    void CaleDistance()
    {
        if (D.Count != 0)
        {
            MinD = D.IndexOf(Mathf.Min(D.ToArray()));
            TargetEnemy = Enemys[MinD];
        }
        else
            Destroy(gameObject);
    }

}
