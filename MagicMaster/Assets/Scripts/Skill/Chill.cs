using UnityEngine;
using System.Collections;

//寒風
public class Chill : MonoBehaviour {

	void Start () {
        //創造緩速範圍物件
        GameObject CSR = PhotonNetwork.Instantiate("ChillSlowRange", transform.position, Quaternion.identity, 0);
        CSR.transform.parent = gameObject.transform;
    }

}
