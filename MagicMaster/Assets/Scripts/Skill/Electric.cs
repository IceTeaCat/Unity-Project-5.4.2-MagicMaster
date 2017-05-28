using UnityEngine;
using System.Collections;

public class Electric : MonoBehaviour {

    
    //增幅效果
    public bool isPowerUp = false;


    public GameObject Target;
    public int Team;
    int Damage=1;

    public LineRenderer LR;
    private float counter;
    private float dist;

    public GameObject origin;
    public GameObject destination;

    public float lineDrawSpeed;

    Vector3 pointAlongLine;

     float DieTime=1.0f;

    float MatOffset = 0;
    float MatOffsetSpeed = 8;

    void Start () {
        LR = GetComponent<LineRenderer>();
        //LR.SetWidth(0.45f, 0.45f);
        LR.SetWidth(2, 2);
        Destroy(gameObject, DieTime);

        Target.GetComponent<PlayerAbilityValue>().HEALTH -= Damage;
    }
	
	
	void Update () {

        if (origin != null && destination != null)
        {
            LR.SetPosition(0, origin.transform.position);
            LR.SetPosition(1, destination.transform.position);
            
            GetComponent<LineRenderer>().material.SetTextureOffset("_MainTex", new Vector2(MatOffset, 0));
            MatOffset -= Time.deltaTime* MatOffsetSpeed;
            
            if (isPowerUp)
            {
                if (!destination.GetComponent<DizzyEffect>())
                    destination.AddComponent<DizzyEffect>();
                else
                    destination.GetComponent<DizzyEffect>().DizzyTime = 2;
            }
            
        }
        /*
        LR.SetPosition(0, origin.position);
        
        dist = Vector3.Distance(origin.position, destination.position);

        if (counter<dist)
        {
            counter += 0.1f / lineDrawSpeed;

            float x = Mathf.Lerp(0, dist, counter);

            Vector3 pointA = origin.position;
            Vector3 pointB = destination.position;

            pointAlongLine = x * Vector3.Normalize(pointB - pointA) + pointA;
            LR.SetPosition(1, pointAlongLine);
        }
        
        else
            LR.SetPosition(1, destination.position);
        */
    }
    /*
   public  void CreateLR(Transform o,Transform d)
    {
        LR.SetPosition(0, o.position);
        LR.SetPosition(1, d.position);
    }
    */


    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(Team);

            stream.SendNext(Target);
            stream.SendNext(LR);

            stream.SendNext(origin);
            stream.SendNext(destination);
            stream.SendNext(Damage);
        }
        /*
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
        */
    }





}
