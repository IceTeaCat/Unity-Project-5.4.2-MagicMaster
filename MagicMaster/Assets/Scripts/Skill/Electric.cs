using UnityEngine;
using System.Collections;

public class Electric : MonoBehaviour {

    public GameObject Target;
    public int Team;
    int Damage=1;

    public LineRenderer LR;
    private float counter;
    private float dist;

    public Transform origin;
    public Transform destination;

    public float lineDrawSpeed;

    Vector3 pointAlongLine;

    public float DieTime=2;

    void Start () {
        LR = GetComponent<LineRenderer>();
        LR.SetWidth(0.45f, 0.45f);
        Destroy(gameObject, DieTime);

        Target.GetComponent<PlayerAbilityValue>().HEALTH -= Damage;
    }
	
	
	void Update () {
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

}
