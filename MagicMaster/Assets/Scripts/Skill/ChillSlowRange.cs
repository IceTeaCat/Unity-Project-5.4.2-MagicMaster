using UnityEngine;
using System.Collections;

public class ChillSlowRange : Photon.MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        //打到玩家
        if (other.gameObject.tag == "Player")
        {
            PlayerAbilityValue TargetPlayer_Data = other.transform.parent.GetComponent<PlayerAbilityValue>();
            //打到敵方
            if (TargetPlayer_Data.TEAM != gameObject.transform.parent.GetComponent<Ice>().Team)
            {
                if (TargetPlayer_Data.gameObject.GetComponent<ChillSlowEffect>() == null)
                {
                    //photonView.RPC("AddChillSlowEffect", PhotonTargets.AllBufferedViaServer, TargetPlayer_Data.gameObject.GetComponent<PhotonView>().viewID);
                    TargetPlayer_Data.gameObject.AddComponent<ChillSlowEffect>();

                }
            }
        }
    }


    [PunRPC]
    void AddChillSlowEffect(int Target_ID)
    {

        PhotonView.Find(Target_ID).gameObject.AddComponent<ChillSlowEffect>();

    }

}
