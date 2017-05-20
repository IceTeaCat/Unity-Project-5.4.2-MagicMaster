using UnityEngine;
using System.Collections;

//濺散
public class Spatter : MonoBehaviour
{


    void Start()
    {

    }


    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fireball")
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
            GameObject SE = PhotonNetwork.Instantiate("SpatterEffect", transform.position, Quaternion.identity,0);
            SE.GetComponent<SpatterDamage>().Team = GetComponent<FireBall>().Team;
        }
    }



}
