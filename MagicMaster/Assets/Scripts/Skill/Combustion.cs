using UnityEngine;
using System.Collections;

//燃燒
public class Combustion : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerAbilityValue TargetPlayer_Data = other.transform.parent.GetComponent<PlayerAbilityValue>();
            //打到敵方
            if (TargetPlayer_Data.TEAM != gameObject.GetComponent<FireBall>().Team)
            {
                GameObject CE = PhotonNetwork.Instantiate("CombustionEffect", other.transform.position, Quaternion.identity, 0) as GameObject;
                CE.GetComponent<CombustionDamage>().Target = other.gameObject.transform.parent.gameObject;
            }
        }
    }


}