using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class Leaf : MonoBehaviour {
    public Mesh[] meshes;


    void Start()
    {
        if(meshes.Length > 0)
        {
            GetComponentInChildren<MeshFilter>().mesh = meshes[Random.Range(0, meshes.Length)];
        }
        transform.forward = Vector3.back;
        transform.Rotate(Vector3.forward * Random.Range(0.0f, 360.0f));
    }
    void OnTriggerEnter2D(Collider2D other)
    {

        Caterpillar c = other.GetComponent<Caterpillar>();
        if (c != null)
        {
            if (!c.canNomOnLeaf) return;
            c.NomOnLeaf();
        }

        Destroy(gameObject);
    }
}
