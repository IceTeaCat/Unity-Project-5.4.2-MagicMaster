using UnityEngine;
using System.Collections;

public class CombustionDamage : Magic
{
    void Update()
    {
        if (GetComponent<PhotonView>().isMine)
        {
            if (!IsDestroy)
            {
                Target.gameObject.GetPhotonView().RPC("SetDamage", PhotonTargets.All, Power );
                LifeTime -= Time.deltaTime;
                if (LifeTime <= 0)
                {
                    IsDestroy = true;
                    PhotonNetwork.Destroy(gameObject);
                }
                print("燒");
            }
        }
    }

    private void OnDestroy()
    {
        IsDestroy = true;
    }

}
