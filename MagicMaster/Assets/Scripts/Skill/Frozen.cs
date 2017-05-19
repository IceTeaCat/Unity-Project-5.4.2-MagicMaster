using UnityEngine;
using System.Collections;

//結凍
public class Frozen : MonoBehaviour {


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            PlayerAbilityValue TargetPlayer_Data = other.transform.parent.GetComponent<PlayerAbilityValue>();
            //打到敵方
            if (TargetPlayer_Data.TEAM != gameObject.GetComponent<Ice>().Team)
            {
                //未標記就標記
                if (TargetPlayer_Data.gameObject.GetComponent<FrozenMark>() == null)
                {
                    TargetPlayer_Data.gameObject.AddComponent<FrozenMark>();
                }
                else
                {
                    Destroy(TargetPlayer_Data.gameObject.GetComponent<FrozenMark>().FME);
                    Destroy(TargetPlayer_Data.gameObject.GetComponent<FrozenMark>());
                    TargetPlayer_Data.gameObject.AddComponent<FrozenDamage>();

                    //結凍特效
                    GameObject FE = PhotonNetwork.Instantiate("Explode_big", transform.position, Quaternion.identity, 0);
                    Destroy(FE, TargetPlayer_Data.GetComponent<FrozenDamage>().EndTime);
                }

            }
        }
    }


}
