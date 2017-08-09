using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour {

    private Vector3 _startPosition;
    float rand;

    void Start()
    {
        _startPosition = transform.position;
        rand = Random.Range(1, 4);
    }


    void Update()
    {
        transform.position = _startPosition + new Vector3(0.0f, Mathf.Sin(Time.time*rand), 0.0f);
    }
}
