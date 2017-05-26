using UnityEngine;
using System.Collections;


public class ElectricChain : MonoBehaviour {

    public GameObject ECLR;

    public int count = 3;

    GameObject ElectricChainLockRange;
    bool isCreate=false;

    private void Awake()
    {
        ElectricChainLockRange = Resources.Load("ElectricChainLockRange") as GameObject;
    }

    void Start () {

    }
	
	
	void Update () {
        if (GetComponent<ElectricLockRange>().TargetEnemy != null && !isCreate)
        {
            ECLR = Instantiate(ElectricChainLockRange, GetComponent<ElectricLockRange>().TargetEnemy.transform.position, Quaternion.identity) as GameObject;
            ECLR.GetComponent<ElectricChainLockRange>().Team = gameObject.GetComponent<ElectricLockRange>().Team;
            ECLR.GetComponent<ElectricChainLockRange>().ELR = gameObject;
          //ECLR.GetComponent<ElectricChainLockRange>().OldEnemy = gameObject.GetComponent<ElectricLockRange>().TargetEnemy;
          isCreate = true;
        }

    }
}
