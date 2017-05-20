using UnityEngine;
using System.Collections;

//融合
public class Fusion : MonoBehaviour
{
    GameObject OtherFireBall;
    GameObject MyFireBall;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fireball")
        {
            OtherFireBall = other.gameObject;
            MyFireBall = this.gameObject;

            if (OtherFireBall.GetComponent<FireBall>().Team == MyFireBall.GetComponent<FireBall>().Team)
            {
                //目標如果不是濺散火球
                if (OtherFireBall.GetComponent<Spatter>() == null)
                {
                    print("合體");
                    //目標如果也是合成火球
                    if (OtherFireBall.GetComponent<Fusion>() != null)
                    {
                        if (OtherFireBall.GetComponent<FireBall>().BornTime > MyFireBall.GetComponent<FireBall>().BornTime)
                        {
                            Destroy(OtherFireBall.GetComponent<Fusion>());
                            CreateFireBall();
                        }
                    }
                    else
                    {
                        CreateFireBall();
                    }
                    Destroy(OtherFireBall);
                    Destroy(MyFireBall);
                }
            }
        }
    }


    void CreateFireBall()
    {
        GameObject FB;
        float temp=0;
        float a = MyFireBall.transform.rotation.eulerAngles.y;
        float b= OtherFireBall.transform.rotation.eulerAngles.y;

        if (OtherFireBall.GetComponent<FireBall>().FireLevel == 1 && MyFireBall.GetComponent<FireBall>().FireLevel == 1)
        {
            print("合成中火球");
            FB = PhotonNetwork.Instantiate("FireBall_mid", transform.position, transform.rotation, 0);
        }
        else
        {
            print("合成大火球");
            FB = PhotonNetwork.Instantiate("FireBall_big", transform.position, transform.rotation, 0);
        }

        if (Mathf.Abs(a - b) >= 180)
            temp = (a + b) / 2 + 180;
        else
            temp = (a + b) / 2;

        print("a:" + a + " b:" + b + " temp:" + temp);
        FB.transform.rotation = Quaternion.Euler(0, temp, 0);
        FB.GetComponent<FireBall>().Team = MyFireBall.GetComponent<FireBall>().Team;
  
    }
    



}
