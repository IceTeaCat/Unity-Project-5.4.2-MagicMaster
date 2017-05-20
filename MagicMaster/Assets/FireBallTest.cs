using UnityEngine;
using System.Collections;

public class FireBallTest : MonoBehaviour
{
    public GameObject FireBall_Obj;

    void Start()
    {

    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel("SkillTest");
            
        }
    }



    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fireball")
        {
            GameObject FBT = Instantiate(FireBall_Obj, transform.position, transform.rotation) as GameObject;
            float temp = (other.transform.rotation.y * 180 + transform.rotation.y * 180) / 2;
            FBT.transform.rotation=Quaternion.Euler(0, temp, 0);

            print("A:"+other.transform.rotation.y + " B:" + transform.rotation.y + " Temp:" + temp);
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }


}
