using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[AddComponentMenu("Scripts/Player/Caterpillar")]
[RequireComponent(typeof(CircleCollider2D))]
public class Caterpillar : MonoBehaviour {
    
    private Vector3 lastPosition = Vector3.zero;
    public Vector3 acceleration = Vector3.zero;
    public static readonly Vector3 GRAVITY = Vector3.down * 9.8f;
    public Vector3 sumAcceleration { get { return acceleration + (useGravity ? GRAVITY : Vector3.zero); } }
    public bool useGravity = true;
    public Vector3 curVelocity { get { return (transform.position - lastPosition) / Time.fixedDeltaTime; } }

    public int leavesNeededUntilKakuna = 10;

    public int nJumps = 3;
    public float jumpVelocity = 10;

    public bool canNomOnLeaf { get; private set; }
    public int leavesNommed { get; private set; }

    public float groundBouncyness = 0.6f;
    public float groundFriction = 0.6f;


    public Renderer larvaRenderer;
    public Renderer cocoonRenderer;
    public AudioClip startSquee;
    public AudioClip nomLeafClip;
    //private float maxVelocity = 0.5f;
    //xi+1 = xi + (xi - xi-1) + a * dt * dt
    // Use this for initialization
    void Start () {
        
        lastPosition += transform.position;
        canNomOnLeaf = true;
        GetComponent<AudioSource>().PlayOneShot(startSquee);
	}

    public void NomOnLeaf()
    {
        if (!canNomOnLeaf) return;
        canNomOnLeaf = false;
        GetComponent<AudioSource>().PlayOneShot(nomLeafClip);
        StartCoroutine(OmNomNom());

        

    }

    IEnumerator OmNomNom()
    {
        Debug.Log("NOM");
        yield return new WaitForSeconds(0.9f);
        leavesNommed++;
        if(leavesNommed >= leavesNeededUntilKakuna)
        {
            yield return StartCoroutine(KakunaMatata());
        }
        else
        {
            canNomOnLeaf = true;
        }
    }

    IEnumerator KakunaMatata()
    {
        larvaRenderer.enabled = false;
        cocoonRenderer.enabled = true;
        yield return new WaitForSeconds(5.0f);
        Debug.Log("What a wonderful phrase!");
    }

    // Called by GameManager
    void Reset()
    {
        Destroy(gameObject);
    }

    public void AddVelocity(Vector3 velocity)
    {
        lastPosition -= velocity * Time.fixedDeltaTime;
    }

    public void SetVelocity(Vector3 velocity)
    {
        lastPosition = transform.position - velocity * Time.fixedDeltaTime;
    }

    #region Physics
    void FixedUpdate () {

        if(nJumps > 0 && Input.GetKeyDown(KeyCode.Space))
        {
            AddVelocity(Vector3.up * jumpVelocity);
            nJumps--;
        }

        TickPhysics();
        if(transform.position.y < 0.5f)
        {
            
            
            Vector3 vel = curVelocity;
            if (vel.y > 0.0f) return;
            Vector3 velU = Vector3.Project(vel, Vector3.up);
            vel -= velU;
            transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
            SetVelocity(vel * (1.0f - groundFriction) - velU * groundBouncyness);
            if (velU.magnitude > 1.0f)
            {
                GetComponentInParent<GameManager>().OnShake(velU.magnitude, 0.5f);
                GetComponent<AudioSource>().pitch = Mathf.Lerp(1.2f, 0.8f, velU.magnitude / 10.0f);
                GetComponent<AudioSource>().Play();
            }

        }
        transform.forward = (transform.position - lastPosition).normalized;

        larvaRenderer.transform.parent.GetComponent<Animator>().SetFloat("Velocity",  Mathf.Clamp01((curVelocity.sqrMagnitude / 1000.0f)));

        
	}

    private void TickPhysics()
    {
        Vector3 lPos = transform.position;
        transform.position = transform.position + (transform.position - lastPosition) * 0.999f + sumAcceleration * Time.fixedDeltaTime * Time.fixedDeltaTime;
        lastPosition = lPos;    
        //if ((transform.position - lPos).magnitude > maxVelocity / Time.fixedDeltaTime) transform.position = lPos + (transform.position - lPos).normalized * (maxVelocity / Time.fixedDeltaTime);
    }

    #endregion
}
