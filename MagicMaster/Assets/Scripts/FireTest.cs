using UnityEngine;
using System.Collections;

public class FireTest : MonoBehaviour {

	public GameObject FireBall;
	float CD=0.5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (CD <= 0) {
			CD = 0.5f;
			Instantiate (FireBall, transform.position, new Quaternion(transform.rotation.x,transform.rotation.y,transform.rotation.z,1));
		} else {
			CD -= Time.deltaTime;
		}


	}
}
