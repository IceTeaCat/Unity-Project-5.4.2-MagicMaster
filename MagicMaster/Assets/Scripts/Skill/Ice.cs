using UnityEngine;
using System.Collections;

public class Ice : Magic {

    /*
    public int Team = -1;

    public float BornTime = 0;
    public float Lifetime = 5.0f;
    public int IceLevel = 1;
    public float Speed = 10;
    public int Power = 3;
    */



    private Vector3 correctFireBallPos = Vector3.zero;
    private Quaternion correctFireBallRot = Quaternion.identity;
    private bool appliedInitialUpdate;

    void Start () {
        BornTime = Time.realtimeSinceStartup;
    }
	
	void Update () {
        if (!IsDestroy)
        {
            transform.Translate(0, 0, -MoveSpeed * Time.deltaTime);
            LifeTime -= Time.deltaTime;
            if (LifeTime <= 0)
            {
                IsDestroy = true;
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }




    void OnTriggerEnter(Collider other)
    {
        if (GetComponent<PhotonView>().isMine)
        {
            if (!IsDestroy)
            {

                //打到牆壁or障礙物
                if (other.gameObject.tag == "Wall")
                {
                    photonView.RPC("HitFX_Function", PhotonTargets.All, new object[] { 0, transform.position });
                    IsDestroy = true;
                    PhotonNetwork.Destroy(gameObject);
                }

                //打到玩家
                if (other.gameObject.tag == "Player")
                {
                    PlayerAbilityValue TargetPlayer_Data = other.gameObject.transform.parent.GetComponent<PlayerAbilityValue>();
                    //打到敵方
                    if (TargetPlayer_Data.TEAM != Team)
                    {
                        photonView.RPC("HitFX_Function", PhotonTargets.All, new object[] { 0, transform.position });
                        other.gameObject.transform.parent.gameObject.GetPhotonView().RPC("SetDamage", PhotonTargets.All, Power);
                        IsDestroy = true;
                        PhotonNetwork.Destroy(gameObject);

                        //如果有冰凍標記
                        if (TargetPlayer_Data.gameObject.GetComponent<FrozenMark>() != null && TargetPlayer_Data.gameObject.GetComponent<FrozenDamage>() == null && GetComponent<Frozen>() == null)
                        {
                            photonView.RPC("RemoveFrozenMark2", PhotonTargets.All, TargetPlayer_Data.gameObject.GetComponent<PhotonView>().viewID);
                            photonView.RPC("AddFrozenDamage2", PhotonTargets.All, TargetPlayer_Data.gameObject.GetComponent<PhotonView>().viewID);

                            //結凍特效
                            GameObject FE = PhotonNetwork.Instantiate("FireBall_Explode", transform.position, Quaternion.identity, 0);
                            FE.transform.localScale = new Vector3(5, 5, 5);
                        }
                    }
                }

            }
        }

    }




    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(Team);
        }
        else
        {
            correctFireBallPos = (Vector3)stream.ReceiveNext();
            correctFireBallRot = (Quaternion)stream.ReceiveNext();
            Team = (int)stream.ReceiveNext();

            if (!appliedInitialUpdate)
            {
                appliedInitialUpdate = true;
                transform.position = correctFireBallPos;
                transform.rotation = correctFireBallRot;
            }
        }

    }



    [PunRPC]
    void RemoveFrozenMark2(int Target_ID)
    {
        Destroy(PhotonView.Find(Target_ID).gameObject.GetComponent<FrozenMark>().FME);
        Destroy(PhotonView.Find(Target_ID).gameObject.GetComponent<FrozenMark>());
    }

    [PunRPC]
    void AddFrozenDamage2(int Target_ID)
    {
        PhotonView.Find(Target_ID).gameObject.AddComponent<FrozenDamage>();
    }

}
