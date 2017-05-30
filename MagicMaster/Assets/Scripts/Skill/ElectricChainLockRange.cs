using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ElectricChainLockRange : Photon.MonoBehaviour
{
    public GameObject Player;

    float JumpTime = 0.1f;

   public int JumpCount = 10;

    float DamageTime = 0.01f;

    public int Team = -1;
    public List<GameObject> Enemys = new List<GameObject>();
    public List<float> D = new List<float>();
    int MinD;

    PlayerAbilityValue TargetPlayer_Data;

    public GameObject TargetEnemy;
    public GameObject OldEnemy;

    public bool IsDestroy = false;

    void Update()
    {
        print(JumpCount);
        DamageTime -= Time.deltaTime;

        if (DamageTime <= 0 )
        {
            if( OldEnemy != null)
            { 
            JumpTime -= Time.deltaTime;

                //時間到就電過去
                if (JumpTime <= 0)
                {
                    RemoveOldTarget();
                    CaleDistance();
                    if (photonView.isMine)
                    {
                        if (!IsDestroy)
                        {
                            if (JumpCount > 0)
                            {
                                if (TargetEnemy != null)
                                {
                                    //print(OldEnemy.name +":" + TargetEnemy.name);
                                    GameObject ElectricLR = PhotonNetwork.Instantiate("ElectricLR", TargetEnemy.transform.position, Quaternion.identity, 0) as GameObject;
                                    GetComponent<PhotonView>().RPC("CreateECLRElectricLR", PhotonTargets.All, ElectricLR.GetComponent<PhotonView>().viewID);



                                    /*
                                    ElectricLR.GetComponent<Electric>().LR = ElectricLR.GetComponent<LineRenderer>();

                                    ElectricLR.GetComponent<Electric>().origin = OldEnemy;
                                    ElectricLR.GetComponent<Electric>().destination = TargetEnemy;
                                    */
                                    /*
                                    if (Player.GetComponent<ElectricIncrease>())
                                    {
                                        ElectricLR.GetComponent<Electric>().isPowerUp = true;
                                    }
                                    */
                                    /*
                                    ElectricLR.GetComponent<Electric>().Target = TargetEnemy;

                                    GetComponent<SphereCollider>().enabled = false;

                                    JumpCount--;
                                    JumpTime = 0.1f;
                                    //清空資料
                                    Enemys.Clear();
                                    D.Clear();
                                    //移動位置
                                    gameObject.transform.position = TargetEnemy.transform.position;
                                    GetComponent<SphereCollider>().enabled = true;
                                    OldEnemy = TargetEnemy;
                                    */
                                }
                                else
                                {
                                    IsDestroy = true;
                                    PhotonNetwork.Destroy(gameObject);
                                }
                            }
                            else
                            {
                                IsDestroy = true;
                                PhotonNetwork.Destroy(gameObject);
                            }


                        }

                    }
                }
            }



        }
        

        


    }

    void OnTriggerEnter(Collider other)
    {
        //打到玩家
        if (other.gameObject.tag == "Player")
        {
            TargetPlayer_Data = other.transform.parent.GetComponent<PlayerAbilityValue>();
            //打到敵方
            if (TargetPlayer_Data.TEAM != Team)
            {
                Enemys.Add(TargetPlayer_Data.gameObject);
                D.Add(Vector3.Distance(TargetPlayer_Data.gameObject.transform.position, transform.position));
            }
            /*
            else
                PhotonNetwork.Destroy(gameObject);
            */
        }
    }

    void RemoveOldTarget()
    {
        for (int i = 0; i < Enemys.Count; i++)
        {
            if(Enemys[i]== OldEnemy)
            {
                Enemys.RemoveAt(i);
                D.RemoveAt(i);
            }
        }
    }



    void CaleDistance()
    {
        if (D.Count != 0)
        {
            MinD = D.IndexOf(Mathf.Min(D.ToArray()));
            photonView.RPC("SetECLRTargetEnemy", PhotonTargets.All, Enemys[MinD].GetComponent<PhotonView>().viewID);
            //TargetEnemy = Enemys[MinD];
        }
    }



    [PunRPC]
    void SetECLRTargetEnemy(int Enemys_ID)
    {
        TargetEnemy = PhotonView.Find(Enemys_ID).gameObject;
    }



    [PunRPC]
    void CreateECLRElectricLR(int ElectricLR_ID)
    {
        PhotonView.Find(ElectricLR_ID).GetComponent<Electric>().LR = PhotonView.Find(ElectricLR_ID).GetComponent<LineRenderer>();

        PhotonView.Find(ElectricLR_ID).GetComponent<Electric>().origin = OldEnemy;
        PhotonView.Find(ElectricLR_ID).GetComponent<Electric>().destination = TargetEnemy;






        PhotonView.Find(ElectricLR_ID).GetComponent<Electric>().Target = TargetEnemy;

        GetComponent<SphereCollider>().enabled = false;

        JumpCount--;
        JumpTime = 0.1f;
        //清空資料
        Enemys.Clear();
        D.Clear();
        //移動位置
        gameObject.transform.position = TargetEnemy.transform.position;
        GetComponent<SphereCollider>().enabled = true;
        OldEnemy = TargetEnemy;

    }



    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
    }






}
