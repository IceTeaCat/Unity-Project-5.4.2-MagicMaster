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
                GameObject Player = GetComponent<ElectricLockRange>().PlayerOrEnemy;
                GameObject ElectricLR = PhotonNetwork.Instantiate("ElectricLR", TargetEnemy.transform.position, Quaternion.identity, 0) as GameObject;
                ElectricLR.GetComponent<Electric>().LR = ElectricLR.GetComponent<LineRenderer>();

                ElectricLR.GetComponent<Electric>().origin = Player;
                ElectricLR.GetComponent<Electric>().destination = TargetEnemy;

                if (Player.GetComponent<ElectricIncrease>())
                {
                    ElectricLR.GetComponent<Electric>().isPowerUp = true;
                }

                ElectricLR.GetComponent<Electric>().Target = TargetEnemy;
            }
            Destroy(gameObject);
        }
    }
}
