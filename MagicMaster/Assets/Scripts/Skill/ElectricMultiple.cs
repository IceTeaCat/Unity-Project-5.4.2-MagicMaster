using UnityEngine;
using System.Collections;

public class ElectricMultiple : Photon.MonoBehaviour
{
    public GameObject TargetEnemy;
    public GameObject Player;

    public int Team;

    void Update()
    {
        if (photonView.isMine)
        {
            if (GetComponent<ElectricLockRange>().Enemys.Count != 0)
            {
                for (int i = 0; i < GetComponent<ElectricLockRange>().Enemys.Count; i++)
                {

                    photonView.RPC("SetTargetEnemys", PhotonTargets.All, GetComponent<ElectricLockRange>().Enemys[i].GetComponent<PhotonView>().viewID);
                    //TargetEnemy = GetComponent<ElectricLockRange>().Enemys[i];
       


                    GameObject ElectricLR = PhotonNetwork.Instantiate("ElectricLR", TargetEnemy.transform.position, Quaternion.identity, 0) as GameObject;
                    GetComponent<PhotonView>().RPC("CreateMElectricLR", PhotonTargets.All, ElectricLR.GetComponent<PhotonView>().viewID);


                    /*
                    ElectricLR.GetComponent<Electric>().LR = ElectricLR.GetComponent<LineRenderer>();
                    ElectricLR.GetComponent<Electric>().origin = Player;
                    ElectricLR.GetComponent<Electric>().destination = TargetEnemy;
                    ElectricLR.GetComponent<Electric>().Target = TargetEnemy;
                    */

                    
                    if (Player.GetComponent<ElectricIncrease>())
                    {
                        ElectricLR.GetComponent<PhotonView>().RPC("SetIsPowerUp", PhotonTargets.All,true);
                        //ElectricLR.GetComponent<Electric>().isPowerUp = true;                      
                    }
                    

                }
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }





    [PunRPC]
    void CreateMElectricLR(int ElectricLR_ID)
    {
        PhotonView.Find(ElectricLR_ID).gameObject.GetComponent<Electric>().LR = PhotonView.Find(ElectricLR_ID).gameObject.GetComponent<LineRenderer>();
        PhotonView.Find(ElectricLR_ID).gameObject.GetComponent<Electric>().origin = Player;
        PhotonView.Find(ElectricLR_ID).gameObject.GetComponent<Electric>().destination = TargetEnemy;
        PhotonView.Find(ElectricLR_ID).gameObject.GetComponent<Electric>().Target = TargetEnemy;
        PhotonView.Find(ElectricLR_ID).gameObject.GetComponent<Electric>().Team = Team;
    }



    [PunRPC]
    void SetTargetEnemys(int Enemys_ID)
    {
        TargetEnemy = PhotonView.Find(Enemys_ID).gameObject;
        Player = GetComponent<ElectricLockRange>().PlayerOrEnemy;
    }



}
