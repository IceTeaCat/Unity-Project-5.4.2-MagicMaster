using UnityEngine;
using System.Collections;

public class FireBall : MonoBehaviour
{
    public int Team=-1;

    public float BornTime = 0;
     float Lifetime = 5.0f;
    public int FireLevel = 1;
     float Speed = 10;


    private Vector3 correctFireBallPos = Vector3.zero;
    private Quaternion correctFireBallRot = Quaternion.identity;
    private bool appliedInitialUpdate;


    public GameObject Explode_big;

    void Start()
    {
        BornTime = Time.realtimeSinceStartup;
    }


    void Update()
    {
        transform.Translate(0, 0, -Speed * Time.deltaTime);
        Lifetime -= Time.deltaTime;
        if (Lifetime <= 0)
            Destroy(gameObject);

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            print(other.gameObject.name);
            Destroy(gameObject);
            Instantiate(Explode_big, transform.position, Quaternion.identity);
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
            Team=(int)stream.ReceiveNext();

            if (!appliedInitialUpdate)
            {
                appliedInitialUpdate = true;
                transform.position = correctFireBallPos;
                transform.rotation = correctFireBallRot;
            }
        }

    }
    



}
