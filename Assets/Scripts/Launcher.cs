using UnityEngine;
using System.Collections;

public class Launcher : MonoBehaviour {
    public Caterpillar caterpillarPrefab;
    public TransformFollower cameraTransformFollower;
    public Transform launchTransform;
    public WorldBuilder worldBuilder;
    public Transform cannonAxis;
    private bool canFire = false;

    [Range(0.0f, 1000.0f)]
    public float maxLaunchVelocity = 20.0f;

    private float launchVelocity = 20.0f;
	// Use this for initialization
	void Start () {
        Debug.Assert(caterpillarPrefab != null);
        Debug.Assert(cameraTransformFollower != null);
	}
	
	// Update is called once per frame
	void Update () {
        UpdateFiringFluctuation();
        if (Input.GetKeyDown(KeyCode.Space) && canFire)
        {
            Caterpillar caterpillar = Instantiate<Caterpillar>(caterpillarPrefab);
            caterpillar.transform.parent = transform.parent;
            caterpillar.transform.position = launchTransform.position;
            caterpillar.transform.rotation = launchTransform.rotation;
            caterpillar.AddVelocity(launchTransform.forward * launchVelocity);
            cameraTransformFollower.target = caterpillar.transform;
            worldBuilder.target = caterpillar.transform;
            canFire = false;
            //caterpillar.acc
        }
	}

    private void UpdateFiringFluctuation()
    {
        cannonAxis.transform.rotation = Quaternion.AngleAxis(Mathf.Sin(Time.time) * 30, Vector3.forward);
        launchVelocity = Mathf.Abs(Mathf.Cos(Time.time * 1.5f)) * maxLaunchVelocity;
    }


    // Called from GameManager
    void Reset()
    {
        cameraTransformFollower.target = transform;
        canFire = true;
        worldBuilder.target = transform;
    }
}
