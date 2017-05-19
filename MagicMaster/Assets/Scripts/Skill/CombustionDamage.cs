using UnityEngine;
using System.Collections;

public class CombustionDamage : MonoBehaviour
{
    //燃燒的對象
    public GameObject Target;
    private bool appliedInitialUpdate;

    private Vector3 correctFireBallPos = Vector3.zero;

    void Start()
    {
        Destroy(gameObject, 2);
    }

    void Update()
    {
        if (Target.gameObject.GetComponent<PlayerAbilityValue>().HEALTH >= 0)
        {
            transform.position = Target.transform.position;
            Target.gameObject.GetComponent<PlayerAbilityValue>().HEALTH -= 1;
            print("燒");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
        }
        else
        {
            correctFireBallPos = (Vector3)stream.ReceiveNext();

            if (!appliedInitialUpdate)
            {
                appliedInitialUpdate = true;
                transform.position = correctFireBallPos;
            }
        }

    }




}
