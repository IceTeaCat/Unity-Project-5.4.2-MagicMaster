using UnityEngine;
using System.Collections;


public class ElectricChain : MonoBehaviour {

    public GameObject ECLR;
    public int Team = -1;

    GameObject ElectricChainLockRange;

    bool isCreate=false;

    private void Awake()
    {
        ElectricChainLockRange = Resources.Load("ElectricChainLockRange") as GameObject;
    }

    void Update () {

        if (GetComponent<ElectricLockRange>().TargetEnemy != null && !isCreate)
        {
            ECLR = Instantiate(ElectricChainLockRange, GetComponent<ElectricLockRange>().TargetEnemy.transform.position, Quaternion.identity) as GameObject;
            ECLR.GetComponent<ElectricChainLockRange>().Team = Team;
            ECLR.GetComponent<ElectricChainLockRange>().ELR = gameObject;

            isCreate = true;





            GameObject TargetEnemy = GetComponent<ElectricLockRange>().TargetEnemy;
            GameObject Player = GetComponent<ElectricLockRange>().PlayerOrEnemy;
            //ECLR.GetComponent<ElectricChainLockRange>().TargetEnemy = TargetEnemy;
            ECLR.GetComponent<ElectricChainLockRange>().OldEnemy = TargetEnemy;

            //產生閃電
            GameObject ElectricLR = PhotonNetwork.Instantiate("ElectricLR", TargetEnemy.transform.position, Quaternion.identity, 0) as GameObject;
            ElectricLR.GetComponent<Electric>().LR = ElectricLR.GetComponent<LineRenderer>();
            ElectricLR.GetComponent<Electric>().LR.SetPosition(0, Player.transform.position);
            ElectricLR.GetComponent<Electric>().LR.SetPosition(1, TargetEnemy.transform.position);
            ElectricLR.GetComponent<Electric>().Target = TargetEnemy;
            Destroy(gameObject);
            
        }


    }


}
