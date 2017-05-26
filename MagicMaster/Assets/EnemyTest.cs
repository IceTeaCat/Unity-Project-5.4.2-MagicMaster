using UnityEngine;
using System.Collections;

public class EnemyTest : MonoBehaviour
{
    int dir=1;

    void Start()
    {

    }


    void Update()
    {
        if (transform.position.z < -2 || transform.position.z >2)
            dir *= -1;

            transform.Translate(0, 0, Random.Range( 0.01f,0.1f)* dir);
    }
}
