using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ElectricLockRange : Photon.MonoBehaviour
{
    float DamageTime = 0.05f;

    public GameObject Player;

    public GameObject PlayerOrEnemy;
    PlayerAbilityValue TargetPlayer_Data;

    public int Team = -1;
    public List<GameObject> Enemys = new List<GameObject>();
    public List<float> D = new List<float>();
    public int MinD;

    public GameObject TargetEnemy;

    int NowChainCount = 0;

    public bool IsDestroy = false;

    void Start()
    {
        //TargetEnemy = null;
    }


    void Update()
    {
        CaleDistance();
        if (photonView.isMine)
        {
            if (!IsDestroy)
            {

                DamageTime -= Time.deltaTime;

                //GetComponent<PhotonView>().RPC("CaleDistance", PhotonTargets.All);

                if (DamageTime <= 0)
                {
                    if (Enemys.Count != 0 && TargetEnemy!=null && !GetComponent<ElectricMultiple>() && !GetComponent<ElectricChain>())
                    {
                        GameObject ElectricLR = PhotonNetwork.Instantiate("ElectricLR", TargetEnemy.transform.position, Quaternion.identity, 0);
                        GetComponent<PhotonView>().RPC("CreateElectricLR", PhotonTargets.All, ElectricLR.GetComponent<PhotonView>().viewID);
                        print("YA");
                        /*
                        ElectricLR.GetComponent<Electric>().LR = ElectricLR.GetComponent<LineRenderer>();
                        ElectricLR.GetComponent<Electric>().origin = PlayerOrEnemy;
                        ElectricLR.GetComponent<Electric>().destination = TargetEnemy;
                        ElectricLR.GetComponent<Electric>().Target = TargetEnemy;
                        */


                        if (Player.GetComponent<ElectricIncrease>())
                        {
                            ElectricLR.GetComponent<PhotonView>().RPC("SetIsPowerUp", PhotonTargets.All, true);
                            //ElectricLR.GetComponent<Electric>().isPowerUp = true;                      
                        }


                        IsDestroy = true;
                        PhotonNetwork.Destroy(gameObject);
                    }

                    if(Enemys.Count == 0)
                    {
                        IsDestroy = true;
                        PhotonNetwork.Destroy(gameObject);
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
                //D.Add(Vector3.Distance(TargetPlayer_Data.gameObject.transform.position, PlayerOrEnemy.transform.position));
                D.Add(Vector3.Distance(TargetPlayer_Data.gameObject.transform.position, Player.transform.position));
                print("YA");
            }

            print("YA2");
        }
    }

    //[PunRPC]
    void CaleDistance()
    {
        if (D.Count != 0)
        {
            MinD = D.IndexOf(Mathf.Min(D.ToArray()));
            photonView.RPC("SetTargetEnemy", PhotonTargets.All, Enemys[MinD].GetComponent<PhotonView>().viewID);
            //TargetEnemy = Enemys[MinD];
        }
        /*
        else
            PhotonNetwork.Destroy(gameObject);
        */
            
    }

    [PunRPC]
    void SetTargetEnemy(int Enemys_ID)
    {
        TargetEnemy = PhotonView.Find(Enemys_ID).gameObject;
    }



    
    [PunRPC]
    void CreateElectricLR(int ElectricLR_ID)
    {
        PhotonView.Find(ElectricLR_ID).gameObject.GetComponent<Electric>().LR = PhotonView.Find(ElectricLR_ID).gameObject.GetComponent<LineRenderer>();
        PhotonView.Find(ElectricLR_ID).gameObject.GetComponent<Electric>().origin = PlayerOrEnemy;
        PhotonView.Find(ElectricLR_ID).gameObject.GetComponent<Electric>().destination = TargetEnemy;
        PhotonView.Find(ElectricLR_ID).gameObject.GetComponent<Electric>().Target = TargetEnemy;
        PhotonView.Find(ElectricLR_ID).gameObject.GetComponent<Electric>().Team = Team;
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