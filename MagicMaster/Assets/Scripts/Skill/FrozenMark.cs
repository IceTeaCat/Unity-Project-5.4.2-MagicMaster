using UnityEngine;
using System.Collections;

//結凍標記
public class FrozenMark : MonoBehaviour {
    public GameObject FME;
    void Start () {
        //10秒取消標記
        FME =  PhotonNetwork.Instantiate("FrozenMarkEffect", transform.position, Quaternion.identity, 0);
        FME.transform.parent = gameObject.transform;
        Destroy(FME, 10);
        Destroy(gameObject.GetComponent<FrozenMark>(), 10);
    }


}
