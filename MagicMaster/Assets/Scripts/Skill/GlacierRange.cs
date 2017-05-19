using UnityEngine;
using System.Collections;

public class GlacierRange : MonoBehaviour {

    void OnTriggerStay(Collider other)
    {
        //打到玩家
        if (other.gameObject.tag == "Player")
        {
            PlayerAbilityValue TargetPlayer_Data = other.transform.parent.GetComponent<PlayerAbilityValue>();
            //打到敵方
            if (TargetPlayer_Data.TEAM != gameObject.transform.parent.GetComponent<PlayerAbilityValue>().TEAM)
            {
                if (TargetPlayer_Data.gameObject.GetComponent<GlacierEffect>() == null)
                {
                    TargetPlayer_Data.gameObject.AddComponent<GlacierEffect>();
                }
            }
        }
    }
}
