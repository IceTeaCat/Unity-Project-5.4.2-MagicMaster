using UnityEngine;
using System.Collections;

public class Ice : MonoBehaviour {


    public int Team = -1;

    public float BornTime = 0;
    public float Lifetime = 5.0f;
    public int IceLevel = 1;
    public float Speed = 10;
    public int Power = 3;

    private Vector3 correctFireBallPos = Vector3.zero;
    private Quaternion correctFireBallRot = Quaternion.identity;
    private bool appliedInitialUpdate;

    void Start () {
        BornTime = Time.realtimeSinceStartup;
    }
	
	void Update () {
        transform.Translate(0, 0, -Speed * Time.deltaTime);
        Lifetime -= Time.deltaTime;
        if (Lifetime <= 0)
            Destroy(gameObject);
    }




    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
            //PhotonNetwork.Instantiate("Explode_big", transform.position, Quaternion.identity, 0);
        }

        //打到玩家
        if (other.gameObject.tag == "Player")
        {
            PlayerAbilityValue TargetPlayer_Data = other.transform.parent.GetComponent<PlayerAbilityValue>();
            //打到敵方
            if (TargetPlayer_Data.TEAM != Team)
            {
                //PhotonNetwork.Instantiate("Explode_big", transform.position, Quaternion.identity, 0);
                TargetPlayer_Data.HEALTH -= Power;
                Destroy(gameObject);
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


}
