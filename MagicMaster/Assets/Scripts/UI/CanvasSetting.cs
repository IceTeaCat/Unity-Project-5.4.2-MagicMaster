using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanvasSetting : Photon.MonoBehaviour {
    
	void Awake(){

		CanvasScaler canvasScaler = GetComponent<CanvasScaler>();

		float screenWidthScale = Screen.width / canvasScaler.referenceResolution.x;
		float screenHeightScale = Screen.height / canvasScaler.referenceResolution.y;

		canvasScaler.matchWidthOrHeight = screenWidthScale > screenHeightScale ? 1 : 0;
	}
    






}
