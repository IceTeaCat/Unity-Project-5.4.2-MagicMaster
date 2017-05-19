using UnityEngine;
using System.Collections;

public class Explode : MonoBehaviour {

	float Lifetime=1.8f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Lifetime -= Time.deltaTime;
		if (Lifetime <= 0)
			Destroy (gameObject);
	}
}
