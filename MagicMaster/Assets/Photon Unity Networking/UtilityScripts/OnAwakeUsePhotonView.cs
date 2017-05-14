using ExitGames.Client.Photon;
using UnityEngine;
using System.Collections;


[RequireComponent(typeof(PhotonView))]
public class OnAwakeUsePhotonView : Photon.MonoBehaviour {

    // tries to send an RPC as soon as this script awakes (e.g. immediately when instantiated)
	void Awake() 
    {
        if (!this.photonView.isMine)
        {
            return;
        }

        // UnityEngine.Debug.Log("OnAwakeSendRPC.Awake() of " + this + " photonView: " + this.photonView + " this.photonView.instantiationData: " + this.photonView.instantiationData);
	    this.photonView.RPC("OnAwakeRPC", PhotonTargets.All);
	}

    // tries to send an RPC as soon as this script starts (e.g. immediately when instantiated)
    void Start()
    {
        if (!this.photonView.isMine)
        {
            return;
        }

        // UnityEngine.Debug.Log("OnAwakeSendRPC.Start() of " + this + " photonView: " + this.photonView);
        this.photonView.RPC("OnAwakeRPC", PhotonTargets.All, (byte)1);
    }
	
    [PunRPC]
    public void OnAwakeRPC()
    {
        UnityEngine.Debug.Log("RPC: 'OnAwakeRPC' PhotonView: " + this.photonView);
    }

    [PunRPC]
    public void OnAwakeRPC(byte myParameter)
    {
        UnityEngine.Debug.Log("RPC: 'OnAwakeRPC' Parameter: " + myParameter + " PhotonView: " + this.photonView);
    }
}
