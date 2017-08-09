using UnityEngine;
using System.Collections;

public class Sea : MonoBehaviour {

    private Vector3 _startPosition;

    void Start () {
        _startPosition = transform.position;
    }
	
	
	void Update () {
        transform.position = _startPosition + new Vector3(0.0f, Mathf.Sin(Time.time)/4, 0.0f);
    }
}
