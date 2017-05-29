using UnityEngine;
using System.Collections;

//寒風
public class Chill : Photon.MonoBehaviour {

	void Start () {
        //創造緩速範圍物件
        if (photonView.isMine)
        {
            GameObject CSR = PhotonNetwork.Instantiate("ChillSlowRange", transform.position, Quaternion.identity, 0);
            photonView.RPC("SetCSRParent", PhotonTargets.All, CSR.GetComponent<PhotonView>().viewID, gameObject.GetComponent<PhotonView>().viewID);
            //CSR.transform.parent = gameObject.transform;
        }
    }
    
    [PunRPC]
    void SetCSRParent(int CSR_ID,int obj)
    {
        PhotonView.Find(CSR_ID).gameObject.transform.parent = PhotonView.Find(obj).gameObject.transform;
    }
    
}
