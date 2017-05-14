using UnityEngine;
using System.Collections;

public class Fusion : MonoBehaviour {

    public GameObject Fireball_big;

    void Start () {
	
	}
	
	
	void Update () {
	
	}



    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fireball")
        {
            if (other.GetComponent<FireBall>().FireLevel == GetComponent<FireBall>().FireLevel)
            {
                print("合體");
                if (other.GetComponent<FireBall>().BornTime > GetComponent<FireBall>().BornTime)
                {
                    Destroy(other.gameObject);
                    if (gameObject.transform.rotation.eulerAngles.y > 180 || other.transform.rotation.eulerAngles.y > 180)
                    {
                        float temp = 0;
                        if (gameObject.transform.rotation.eulerAngles.y > 180)
                        {
                            temp = gameObject.transform.rotation.eulerAngles.y - 360;
                            Instantiate(Fireball_big, transform.position, Quaternion.Euler(0, (other.transform.rotation.eulerAngles.y + temp) / 2, 0));
                        }
                        else if (other.transform.rotation.eulerAngles.y > 180)
                        {
                            temp = other.transform.rotation.eulerAngles.y - 360;
                            Instantiate(Fireball_big, transform.position, Quaternion.Euler(0, (gameObject.transform.rotation.eulerAngles.y + temp) / 2, 0));
                        }
                    }
                    else
                    {
                        Instantiate(Fireball_big, transform.position, Quaternion.Euler(0, (other.transform.rotation.eulerAngles.y + gameObject.transform.rotation.eulerAngles.y) / 2, 0));
                    }
                    Destroy(gameObject);
                }
            }
            /*
            else if (other.GetComponent<Fusion>().FireLevel < FireLevel)
            {
                Destroy(other.gameObject);
                Instantiate(Explode_big, transform.position, Quaternion.identity);
            }
            */
        }




    }



}
