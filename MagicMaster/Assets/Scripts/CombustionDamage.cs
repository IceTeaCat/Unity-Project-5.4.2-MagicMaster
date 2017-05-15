using UnityEngine;
using System.Collections;

public class CombustionDamage : MonoBehaviour
{
    //燃燒的對象
    public GameObject Target;

    void Start()
    {
        Destroy(gameObject, 2);
    }

    void Update()
    {
        transform.position = Target.transform.position;
        Target.gameObject.GetComponent<PlayerAbilityValue>().HEALTH -= 1;
        print("燒");
    }
}
