using UnityEngine;
using System.Collections;

public class PlayerOutArea : MonoBehaviour
{

    public PlayerAbilityValue _pav;

    void Start()
    {

    }


    void Update()
    {

    }

    /*
    void OnCollisionStay(Collision collision)
    {
        print(collision.gameObject.tag);
    }
    */

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Floor")
        {
            print("出界");
            _pav.HEALTH = 0;
        }
    }

}