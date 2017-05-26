using UnityEngine;
using System.Collections;

public class ElectricMultiple : MonoBehaviour
{
    void Update()
    {
        if (GetComponent<ElectricLockRange>().Enemys.Count != 0 )
        {
            for (int i = 0; i < GetComponent<ElectricLockRange>().Enemys.Count; i++)
            {
                GameObject TargetEnemy = GetComponent<ElectricLockRange>().Enemys[i];
                GameObject Player = GetComponent<ElectricLockRange>().Player;
                GameObject ELR = PhotonNetwork.Instantiate("ElectricLR", TargetEnemy.transform.position, Quaternion.identity, 0) as GameObject;
                ELR.GetComponent<Electric>().LR = ELR.GetComponent<LineRenderer>();
                ELR.GetComponent<Electric>().LR.SetPosition(0, Player.transform.position);
                ELR.GetComponent<Electric>().LR.SetPosition(1, TargetEnemy.transform.position);
                ELR.GetComponent<Electric>().Target = TargetEnemy;
                Destroy(gameObject);
            }
        }
    }
}
