using UnityEngine;
    
public class OrthographicCamera : MonoBehaviour {

	public Transform target;
	public float smoothTime = 0.3f;

	private Vector3 velocity = Vector3.zero;

	void Update () {
		if (target != null) {
			Vector3 goalPos = new Vector3 (target.position.x, target.position.y + 13, target.position.z - 10);
			transform.position = Vector3.SmoothDamp (transform.position, goalPos, ref velocity, smoothTime);
		}
	}

}

