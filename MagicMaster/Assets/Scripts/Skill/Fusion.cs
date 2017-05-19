using UnityEngine;
using System.Collections;

//融合
public class Fusion : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fireball")
        {
            GameObject OtherFireBall = other.gameObject;
            GameObject MyFireBall = this.gameObject;

            //目標如果不是濺散火球
            if (OtherFireBall.GetComponent<Spatter>() == null)
            {
                print("合體");
                if (OtherFireBall.GetComponent<Fusion>() != null)
                {
                    if (OtherFireBall.GetComponent<FireBall>().BornTime > MyFireBall.GetComponent<FireBall>().BornTime)
                    {
                        Destroy(OtherFireBall.GetComponent<Fusion>());
                        print("移除融合腳本");
                        //計算平均角度
                        if (MyFireBall.transform.rotation.eulerAngles.y > 180 || OtherFireBall.transform.rotation.eulerAngles.y > 180)
                        {
                            float temp = 0;
                            if (MyFireBall.transform.rotation.eulerAngles.y > 180)
                            {
                                temp = MyFireBall.transform.rotation.eulerAngles.y - 360;

                                if (OtherFireBall.GetComponent<FireBall>().FireLevel == 1 && MyFireBall.GetComponent<FireBall>().FireLevel == 1)
                                {
                                    print("合成中火球");
                                    PhotonNetwork.Instantiate("Fireball_mid", transform.position, Quaternion.Euler(0, (OtherFireBall.transform.rotation.eulerAngles.y + temp) / 2, 0), 0);
                                }
                                else
                                {
                                    print("合成大火球");
                                    PhotonNetwork.Instantiate("Fireball_big", transform.position, Quaternion.Euler(0, (OtherFireBall.transform.rotation.eulerAngles.y + temp) / 2, 0), 0);
                                }

                            }
                            else if (OtherFireBall.transform.rotation.eulerAngles.y > 180)
                            {
                                temp = OtherFireBall.transform.rotation.eulerAngles.y - 360;
                                if (OtherFireBall.GetComponent<FireBall>().FireLevel == 1 && MyFireBall.GetComponent<FireBall>().FireLevel == 1)
                                {
                                    print("合成中火球");
                                    PhotonNetwork.Instantiate("Fireball_mid", transform.position, Quaternion.Euler(0, (MyFireBall.transform.rotation.eulerAngles.y + temp) / 2, 0), 0);
                                }
                                else
                                {
                                    print("合成大火球");
                                    PhotonNetwork.Instantiate("Fireball_big", transform.position, Quaternion.Euler(0, (MyFireBall.transform.rotation.eulerAngles.y + temp) / 2, 0), 0);
                                }
                            }
                        }
                    }
                }
                else
                {
                    print("00");
                    //計算平均角度
                    if (MyFireBall.transform.rotation.eulerAngles.y > 180 || OtherFireBall.transform.rotation.eulerAngles.y > 180)
                    {
                        print("11");
                        float temp = 0;
                        if (MyFireBall.transform.rotation.eulerAngles.y > 180)
                        {
                            temp = MyFireBall.transform.rotation.eulerAngles.y - 360;
                            print("AA");
                            if (OtherFireBall.GetComponent<FireBall>().FireLevel == 1 && MyFireBall.GetComponent<FireBall>().FireLevel == 1)
                            {
                                print("合成中火球");
                                PhotonNetwork.Instantiate("Fireball_mid", transform.position, Quaternion.Euler(0, (OtherFireBall.transform.rotation.eulerAngles.y + temp) / 2, 0), 0);
                            }
                            else
                            {
                                print("合成大火球");
                                PhotonNetwork.Instantiate("Fireball_big", transform.position, Quaternion.Euler(0, (OtherFireBall.transform.rotation.eulerAngles.y + temp) / 2, 0), 0);
                            }

                        }
                        else if (OtherFireBall.transform.rotation.eulerAngles.y > 180)
                        {
                            print("BB");
                            temp = OtherFireBall.transform.rotation.eulerAngles.y - 360;
                            if (OtherFireBall.GetComponent<FireBall>().FireLevel == 1 && MyFireBall.GetComponent<FireBall>().FireLevel == 1)
                            {
                                print("合成中火球");
                                PhotonNetwork.Instantiate("Fireball_mid", transform.position, Quaternion.Euler(0, (MyFireBall.transform.rotation.eulerAngles.y + temp) / 2, 0), 0);
                            }
                            else
                            {
                                print("合成大火球");
                                PhotonNetwork.Instantiate("Fireball_big", transform.position, Quaternion.Euler(0, (MyFireBall.transform.rotation.eulerAngles.y + temp) / 2, 0), 0);
                            }
                        }
                    }
                    else
                        print("22");
                }


                Destroy(OtherFireBall);
                Destroy(MyFireBall);
                print("CC");
            }
        }












        /*
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
        }
        */




    }



}
