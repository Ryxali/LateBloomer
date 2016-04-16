using UnityEngine;
using System.Collections;
[AddComponentMenu("Scripts/Player/Caterpillar")]
[RequireComponent(typeof(CircleCollider2D))]
public class Caterpillar : MonoBehaviour {
    
    private Vector3 lastPosition = Vector3.zero;
    public Vector3 acceleration = Vector3.zero;
    public static readonly Vector3 GRAVITY = Vector3.down * 9.8f;
    public Vector3 sumAcceleration { get { return acceleration + (useGravity ? GRAVITY : Vector3.zero); } }
    public bool useGravity = true;

    private float maxVelocity = 0.5f;
    //xi+1 = xi + (xi - xi-1) + a * dt * dt
    // Use this for initialization
    void Start () {
        
        lastPosition += transform.position;
	}

    public void AddVelocity(Vector3 velocity)
    {
        lastPosition -= velocity * Time.deltaTime;
    }

    #region Physics
    void FixedUpdate () {

        TickPhysics();
	}

    private void TickPhysics()
    {
        Vector3 lPos = transform.position;
        transform.position = transform.position + (transform.position - lastPosition) + sumAcceleration * Time.fixedDeltaTime * Time.fixedDeltaTime;
        lastPosition = lPos;    
        //if ((transform.position - lPos).magnitude > maxVelocity / Time.fixedDeltaTime) transform.position = lPos + (transform.position - lPos).normalized * (maxVelocity / Time.fixedDeltaTime);
    }

    #endregion
}
