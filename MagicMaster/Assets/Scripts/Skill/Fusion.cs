using UnityEngine;
using System.Collections;

//融合
public class Fusion : MonoBehaviour
{
    GameObject OtherFireBall;
    GameObject MyFireBall;

    void OnTriggerEnter(Collider other)
    {
        if (GetComponent<PhotonView>().isMine)
        {
            if (other.tag == "Fireball")
            {
                print("YA");

                FireBall OtherFireball_Data = other.gameObject.GetComponent<FireBall>();


                if (OtherFireball_Data.Team == gameObject.GetComponent<FireBall>().Team)
                {
                    GetComponent<PhotonView>().RPC("Damage", PhotonTargets.All, new object[] { other.gameObject.GetComponent<PhotonView>().viewID, this.gameObject.GetComponent<PhotonView>().viewID });
                    CreateFireBall();
                    //GameObject FB = PhotonNetwork.Instantiate("FireBall_mid", transform.position, transform.rotation, 0);

                    GetComponent<PhotonView>().RPC("DestoryFireball", PhotonTargets.All, other.gameObject.GetComponent<PhotonView>().viewID);
                    PhotonNetwork.Destroy(gameObject);
                }
            }

        }






        /*
        if (GetComponent<PhotonView>().isMine)
        {
            print("YA5");
            GetComponent<PhotonView>().RPC("Damage", PhotonTargets.All, new object[] { other.gameObject.GetComponent<PhotonView>().viewID, this.gameObject.GetComponent<PhotonView>().viewID });

            if (OtherFireBall.GetComponent<FireBall>().Team == MyFireBall.GetComponent<FireBall>().Team)
            {
                print("YA4");
                //目標如果不是濺散火球
                if (OtherFireBall.GetComponent<Spatter>() == null)
                {
                    print("合體");
                    //目標如果也是合成火球
                    if (OtherFireBall.GetComponent<Fusion>() != null)
                    {
                        print("YA3"+ OtherFireBall.GetComponent<FireBall>().BornTime +":" + MyFireBall.GetComponent<FireBall>().BornTime);

                        if (OtherFireBall.GetComponent<FireBall>().BornTime > MyFireBall.GetComponent<FireBall>().BornTime)
                        {
                            print("YA2");
                            GetComponent<PhotonView>().RPC("DestoryComponent", PhotonTargets.All, OtherFireBall.gameObject.GetComponent<PhotonView>().viewID);
                            //Destroy(OtherFireBall.GetComponent<Fusion>());
                            StartCoroutine(CreateFireBall());
                            //CreateFireBall();
                        }
                        else
                        {
                            print("YA22");
                            //GetComponent<PhotonView>().RPC("DestoryComponent", PhotonTargets.All, MyFireBall.gameObject.GetComponent<PhotonView>().viewID);
                            StartCoroutine(CreateFireBall());
                        }


                    }
                    else
                    {
                        StartCoroutine(CreateFireBall());
                        //CreateFireBall();
                    }

                    //print(OtherFireBall.GetComponent<PhotonView>().viewID);
                    //print(MyFireBall.GetComponent<PhotonView>().viewID);

                    GetComponent<PhotonView>().RPC("DestoryFireball", PhotonTargets.All, OtherFireBall.gameObject.GetComponent<PhotonView>().viewID);
                    PhotonNetwork.Destroy(MyFireBall);

                }

            }
        }
        */


    }

    

    
     void CreateFireBall()
    {
        print("YA");
        GameObject FB;
        float temp=0;
        float a = gameObject.transform.rotation.eulerAngles.y;
        float b= OtherFireBall.transform.rotation.eulerAngles.y;

        if (OtherFireBall.GetComponent<FireBall>().MagicLevel == 1 && MyFireBall.GetComponent<FireBall>().MagicLevel == 1)
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

        //print("a:" + a + " b:" + b + " temp:" + temp);
        FB.transform.rotation = Quaternion.Euler(0, temp, 0);
        FB.GetComponent<FireBall>().Team = MyFireBall.GetComponent<FireBall>().Team;
    }
    

        /*
    [PunRPC]
    void CreateFireBall()
    {
        print("YA");
        GameObject FB;
        float temp = 0;
        float a = MyFireBall.transform.rotation.eulerAngles.y;
        float b = OtherFireBall.transform.rotation.eulerAngles.y;

        if (OtherFireBall.GetComponent<FireBall>().MagicLevel == 1 && MyFireBall.GetComponent<FireBall>().MagicLevel == 1)
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

        //print("a:" + a + " b:" + b + " temp:" + temp);
        FB.transform.rotation = Quaternion.Euler(0, temp, 0);
        FB.GetComponent<FireBall>().Team = MyFireBall.GetComponent<FireBall>().Team;

    }
    */


    [PunRPC]
    void Damage(int other_ID, int this_ID)
    {
        OtherFireBall = PhotonView.Find(other_ID).gameObject;
        MyFireBall = PhotonView.Find(this_ID).gameObject;
    }

    [PunRPC]
    void DestoryComponent(int Fireball_ID)
    {
        Destroy(PhotonView.Find(Fireball_ID).gameObject.GetComponent<Fusion>());
    }

    [PunRPC]
    void DestoryFireball(int Fireball_ID)
    {
        Destroy(PhotonView.Find(Fireball_ID).gameObject);
    }


}
