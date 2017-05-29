using UnityEngine;
using System.Collections;

public class FireBall : Magic
{

    private Vector3 correctFireBallPos = Vector3.zero;
    private Quaternion correctFireBallRot = Quaternion.identity;
    private bool appliedInitialUpdate;

    void Start()
    {
        BornTime = Time.realtimeSinceStartup;
    }


    void Update()
    {
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
    /*
    [PunRPC]
    void DestroyFireBall()
    {
        PhotonNetwork.Destroy(gameObject);
    }
    */


}
