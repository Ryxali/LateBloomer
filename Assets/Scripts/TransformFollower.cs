using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera/TransformFollower")]
public class TransformFollower : MonoBehaviour {

    public Transform target;
    public float FollowSpeed = 1.0f;
	// Use this for initialization
	void Start () {
	    if(target == null)
        {
            Debug.LogWarning("target is null. Camera won't move.");
        }
	}
	

	void FixedUpdate () {
	    if(target != null)
        {
            float dist = (transform.position - target.position).magnitude;
            transform.position = Vector3.MoveTowards(transform.position, target.position, dist * dist * Time.deltaTime * FollowSpeed);
            transform.position += Vector3.back * target.position.y / 100.0f;
        }
	}
}
