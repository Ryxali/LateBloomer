using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class Leaf : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {

        Caterpillar c = other.GetComponent<Caterpillar>();
        if (c != null)
        {
            c.NomOnLeaf();
        }

        Destroy(gameObject);
    }
}
