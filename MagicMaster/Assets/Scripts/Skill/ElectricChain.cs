using UnityEngine;
using System.Collections;


public class ElectricChain : Photon.MonoBehaviour {

    public GameObject Player;

    public GameObject ECLR;
    public int Team = -1;

    public GameObject TargetEnemy;
    public GameObject PlayerOrEnemy;

    //GameObject ElectricChainLockRange;

    bool isCreate=false;
    /*
    private void Awake()
    {
        ElectricChainLockRange = Resources.Load("ElectricChainLockRange") as GameObject;
    }
    */
    void Update () {
        if (photonView.isMine)
        {
            if (GetComponent<ElectricLockRange>().TargetEnemy != null && !isCreate)
            {

                ECLR = PhotonNetwork.Instantiate("ElectricChainLockRange", GetComponent<ElectricLockRange>().TargetEnemy.transform.position, Quaternion.identity, 0);
                GetComponent<PhotonView>().RPC("CreateECLR", PhotonTargets.All, ECLR.GetComponent<PhotonView>().viewID);

                /*
                ECLR = Instantiate(ElectricChainLockRange, GetComponent<ElectricLockRange>().TargetEnemy.transform.position, Quaternion.identity) as GameObject;
                ECLR.GetComponent<ElectricChainLockRange>().Team = Team;
                ECLR.GetComponent<ElectricChainLockRange>().ELR = gameObject;
                ECLR.GetComponent<ElectricChainLockRange>().Player = Player;
                isCreate = true;


                */



                GetComponent<PhotonView>().RPC("SetECLRTargetEnemys", PhotonTargets.All, gameObject.GetComponent<PhotonView>().viewID);
                /*
                GameObject TargetEnemy = GetComponent<ElectricLockRange>().TargetEnemy;
                GameObject PlayerOrEnemy = GetComponent<ElectricLockRange>().PlayerOrEnemy;
                */

                //ECLR.GetComponent<ElectricChainLockRange>().TargetEnemy = TargetEnemy;
                /*
                ECLR.GetComponent<ElectricChainLockRange>().OldEnemy = TargetEnemy;
                */
                GetComponent<PhotonView>().RPC("SetNewECLRTargetEnemys", PhotonTargets.All, ECLR.GetComponent<PhotonView>().viewID);





                //產生閃電
                GameObject ElectricLR = PhotonNetwork.Instantiate("ElectricLR", TargetEnemy.transform.position, Quaternion.identity, 0) as GameObject;
                GetComponent<PhotonView>().RPC("CreateFirstElectricLR", PhotonTargets.All, ElectricLR.GetComponent<PhotonView>().viewID);

                /*
                ElectricLR.GetComponent<Electric>().LR = ElectricLR.GetComponent<LineRenderer>();

                ElectricLR.GetComponent<Electric>().origin = PlayerOrEnemy;
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
                */
                PhotonNetwork.Destroy(gameObject);

            }
        }

    }

    [PunRPC]
    void CreateFirstElectricLR(int ElectricLR_ID)
    {
        PhotonView.Find(ElectricLR_ID).GetComponent<Electric>().Team = Team;


        PhotonView.Find(ElectricLR_ID).GetComponent<Electric>().LR = PhotonView.Find(ElectricLR_ID).GetComponent<LineRenderer>();

        PhotonView.Find(ElectricLR_ID).GetComponent<Electric>().origin = PlayerOrEnemy;
        PhotonView.Find(ElectricLR_ID).GetComponent<Electric>().destination = TargetEnemy;


        PhotonView.Find(ElectricLR_ID).GetComponent<Electric>().Target = TargetEnemy;
    }




    [PunRPC]
    void CreateECLR(int ECLR_ID)
    {
        PhotonView.Find(ECLR_ID).GetComponent<ElectricChainLockRange>().Team = Team;
        PhotonView.Find(ECLR_ID).GetComponent<ElectricChainLockRange>().Player = Player;
        isCreate = true;
        PhotonView.Find(ECLR_ID).GetComponent<ElectricChainLockRange>().OldEnemy = TargetEnemy;

    }

    [PunRPC]
    void SetECLRTargetEnemys(int Enemys_ID)
    {
        TargetEnemy = PhotonView.Find(Enemys_ID).GetComponent<ElectricLockRange>().TargetEnemy;
        PlayerOrEnemy = PhotonView.Find(Enemys_ID).GetComponent<ElectricLockRange>().PlayerOrEnemy;
    }


    [PunRPC]
    void SetNewECLRTargetEnemys(int ECLR_ID)
    {
        //ECLR.GetComponent<ElectricChainLockRange>().TargetEnemy = TargetEnemy;
        PhotonView.Find(ECLR_ID).GetComponent<ElectricChainLockRange>().OldEnemy = TargetEnemy;
    }


}
