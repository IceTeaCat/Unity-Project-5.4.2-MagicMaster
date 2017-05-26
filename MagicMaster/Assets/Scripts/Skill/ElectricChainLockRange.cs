using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ElectricChainLockRange : MonoBehaviour
{
    public GameObject ELR;

    float DamageTime = .1f;

    public int Team = -1;
    public List<GameObject> Enemys = new List<GameObject>();
    public List<float> D = new List<float>();
    int MinD;

    PlayerAbilityValue TargetPlayer_Data;

    public GameObject TargetEnemy;
    public GameObject OldEnemy;

    void Start()
    {

    }


    void Update()
    {
        DamageTime -= Time.deltaTime;

        if (DamageTime <= 0 && OldEnemy != null)
        {

                RemoveOldTarget();
                CaleDistance();

                GameObject ElectricLR = PhotonNetwork.Instantiate("ElectricLR", TargetEnemy.transform.position, Quaternion.identity, 0) as GameObject;
                ElectricLR.GetComponent<Electric>().LR = ElectricLR.GetComponent<LineRenderer>();
                ElectricLR.GetComponent<Electric>().LR.SetPosition(0, OldEnemy.transform.position);
                ElectricLR.GetComponent<Electric>().LR.SetPosition(1, TargetEnemy.transform.position);
                ElectricLR.GetComponent<Electric>().Target = TargetEnemy;
            /*
                ELR.transform.position = TargetEnemy.transform.position;
                Destroy(ELR.GetComponent<ElectricLockRange>());
                ELR.AddComponent<ElectricLockRange>();
                ELR.GetComponent<ElectricChain>().count -= 1;
                */
                Destroy(gameObject);
            
            

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
                D.Add(Vector3.Distance(TargetPlayer_Data.gameObject.transform.position, transform.position));
            }
        }


    }

    void RemoveOldTarget()
    {
        for (int i = 0; i < Enemys.Count; i++)
        {
            if(Enemys[i]== OldEnemy)
            {
                Enemys.RemoveAt(i);
                D.RemoveAt(i);
            }
        }
    }



    void CaleDistance()
    {
        if (D.Count != 0)
        {
            MinD = D.IndexOf(Mathf.Min(D.ToArray()));
            print(MinD);
            TargetEnemy = Enemys[MinD];
        }
    }


}
