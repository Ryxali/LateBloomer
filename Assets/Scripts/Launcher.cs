using UnityEngine;
using System.Collections;

public class Launcher : MonoBehaviour {
    public Caterpillar caterpillarPrefab;
    public TransformFollower cameraTransformFollower;
    public Transform launchTransform;
    public WorldBuilder worldBuilder;

    [Range(0.0f, 1000.0f)]
    public float launchVelocity = 20.0f;
	// Use this for initialization
	void Start () {
        Debug.Assert(caterpillarPrefab != null);
        Debug.Assert(cameraTransformFollower != null);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Caterpillar caterpillar = Instantiate<Caterpillar>(caterpillarPrefab);
            caterpillar.transform.position = launchTransform.position;
            caterpillar.transform.rotation = launchTransform.rotation;
            caterpillar.AddVelocity(launchTransform.forward * launchVelocity);
            cameraTransformFollower.target = caterpillar.transform;
            worldBuilder.target = caterpillar.transform;
            worldBuilder.Reseed();
            //caterpillar.acc
        }
	}


    // Called from GameManager
    void Reset()
    {
        cameraTransformFollower.target = transform;
    }
}
