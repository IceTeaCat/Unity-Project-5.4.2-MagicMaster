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
            PhotonNetwork.Instantiate("Explode_big", transform.position, Quaternion.identity,0);

        }
    }



}
