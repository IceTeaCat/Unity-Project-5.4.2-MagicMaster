using UnityEngine;
using System.Collections;

public class FireBall : MonoBehaviour
{

    public int Team = -1;

    public float BornTime = 0;
    public float Lifetime = 5.0f;
    public int FireLevel = 1;
    public float Speed = 10;
    public int Power = 10;

    private Vector3 correctFireBallPos = Vector3.zero;
    private Quaternion correctFireBallRot = Quaternion.identity;
    private bool appliedInitialUpdate;

    void Start()
    {
        BornTime = Time.realtimeSinceStartup;
    }


    void Update()
    {
        
        transform.Translate(0, 0, -Speed * Time.deltaTime);
        Lifetime -= Time.deltaTime;
        if (Lifetime <= 0)
            PhotonNetwork.Destroy(gameObject);

    }
    
    void OnTriggerEnter(Collider other)
    {
        if (GetComponent<PhotonView>().isMine)
        {
            if (other.gameObject.tag == "Wall")
            {
                PhotonNetwork.Destroy(gameObject);

                //GetComponent<PhotonView>().RPC("T", PhotonTargets.All);
                PhotonNetwork.Instantiate("FireBall_Explode", transform.position, Quaternion.identity, 0);

            }

            //打到玩家
            if (other.gameObject.tag == "Player")
            {
                PlayerAbilityValue TargetPlayer_Data = other.transform.parent.GetComponent<PlayerAbilityValue>();
                //打到敵方
                if (TargetPlayer_Data.TEAM != Team)
                {
                    PhotonNetwork.Destroy(gameObject);
                    PhotonNetwork.Instantiate("FireBall_Explode", transform.position, Quaternion.identity, 0);
                    print("YA");
                    TargetPlayer_Data.HEALTH -= Power;
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
    void DestoryFireball(int Fireball_ID)
    {
        print("Fireball_ID:"+ Fireball_ID);
        Destroy(PhotonView.Find(Fireball_ID).gameObject);
    }


    [PunRPC]
    void T()
    {
        print("Test");
    }



}
