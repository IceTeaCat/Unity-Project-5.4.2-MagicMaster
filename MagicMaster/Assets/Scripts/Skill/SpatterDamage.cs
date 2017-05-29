using UnityEngine;
using System.Collections;

public class SpatterDamage : MonoBehaviour
{
    //範圍內的對象
    public int Team;
    public int Power=100;


    void OnTriggerEnter(Collider other)
    {
        if (GetComponent<PhotonView>().isMine)
        {
            if (other.tag == "Player")
            {
                PlayerAbilityValue Target = other.transform.parent.GetComponent<PlayerAbilityValue>();
                if (Target.TEAM != Team)
                {
                    int range = (int)GetComponent<SphereCollider>().radius;
                    int temp = (range - (int)Vector3.Distance(other.gameObject.transform.position, this.gameObject.transform.position)) * Power;
                    //print(temp);
                    other.gameObject.transform.parent.gameObject.GetPhotonView().RPC("SetDamage", PhotonTargets.All, temp);

                }
            }
        }

    }


}
