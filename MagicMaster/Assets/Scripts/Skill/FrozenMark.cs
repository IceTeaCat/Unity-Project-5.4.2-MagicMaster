using UnityEngine;
using System.Collections;

//結凍標記
public class FrozenMark : Photon.MonoBehaviour {

    public GameObject FME;
    public float LiftTime = 10;

    void Start () {
        //10秒取消標記
        /*
        FME =  PhotonNetwork.Instantiate("FrozenMarkEffect", transform.position, Quaternion.identity, 0);
        FME.transform.parent = gameObject.transform;
        */
        /*
        Destroy(FME, 10);
        Destroy(gameObject.GetComponent<FrozenMark>(), 10);
        */

    }

    void Update()
    {
        LiftTime -= Time.deltaTime;
        if(LiftTime<=0)
        {
            photonView.RPC("RemoveFrozenMark", PhotonTargets.All, gameObject.GetComponent<PhotonView>().viewID);
        }

    }

    [PunRPC]
    void RemoveFrozenMark(int Target_ID)
    {
        Destroy(PhotonView.Find(Target_ID).gameObject.GetComponent<FrozenMark>().FME);
        Destroy(PhotonView.Find(Target_ID).gameObject.GetComponent<FrozenMark>());
    }



}
