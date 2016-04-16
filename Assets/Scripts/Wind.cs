using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Wind : MonoBehaviour {

    public float strength = 10.0f;

	// Use this for initialization
	void Start () {
        transform.forward = Vector3.right;
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Caterpillar c = other.GetComponent<Caterpillar>();
        if (c != null)
        {
            c.AddVelocity(transform.forward * strength);
        }
    }
}
