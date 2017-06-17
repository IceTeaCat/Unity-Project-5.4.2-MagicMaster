using UnityEngine;
using System.Collections;

public class Electric : Photon.MonoBehaviour {

    
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
    float MatOffsetSpeed = 6;

    void Start () {
        if (photonView.isMine)
        {
            LR = GetComponent<LineRenderer>();
            LR.SetWidth(2, 2);
            //Destroy(gameObject, DieTime);

            Target.GetComponent<PhotonView>().RPC("SetDamage", PhotonTargets.All,Damage );
        }
    }


    void Update()
    {

        LR.SetPosition(0, origin.transform.position);
        LR.SetPosition(1, destination.transform.position);
        gameObject.GetComponent<LineRenderer>().enabled = true;

        GetComponent<LineRenderer>().material.SetTextureOffset("_MainTex", new Vector2(MatOffset, 0));
        MatOffset -= Time.deltaTime * MatOffsetSpeed;

        if (GetComponent<PhotonView>().isMine)
        {
            DieTime -= Time.deltaTime;
            if (DieTime <= 0)
            {
                PhotonNetwork.Destroy(gameObject);
            }

            if (origin != null && destination != null)
            {

           
                if (isPowerUp)
                {
                    GetComponent<PhotonView>().RPC("SetPowerUpEffect", PhotonTargets.All);
                    /*
                    if (!destination.GetComponent<DizzyEffect>())
                        destination.AddComponent<DizzyEffect>();
                    else
                        destination.GetComponent<DizzyEffect>().DizzyTime = 2;
                    */
                }



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

        /*
        //閃電多生一個BUG無法解決的最終解決辦法FUck!!!
        else
            Destroy(gameObject);
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
            /*
            stream.SendNext(origin.transform.position);
            stream.SendNext(destination.transform.position);
            */

            stream.SendNext(Damage);
        }

    }


    [PunRPC]
    void SetIsPowerUp(bool x)
    {
        isPowerUp = x;
    }

    [PunRPC]
    void SetPowerUpEffect()
    {
        if (!destination.GetComponent<DizzyEffect>())
            destination.AddComponent<DizzyEffect>();
        else
            destination.GetComponent<DizzyEffect>().DizzyTime = 2;
    }


}
