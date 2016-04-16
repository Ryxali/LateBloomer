using UnityEngine;
using System.Collections;

public class BubbleEffect : MonoBehaviour {
    public float effectMagnitude = 15.0f;
    public float duration = 2.0f;
	// Use this for initialization
	void Start () {
        StartCoroutine(EffectSequence());
	}

    IEnumerator EffectSequence()
    {
        Caterpillar caterpillar = GetComponentInParent<Caterpillar>();
        caterpillar.acceleration += Vector3.up * effectMagnitude;
        yield return new WaitForSeconds(duration);
        caterpillar.acceleration -= Vector3.up * effectMagnitude;
        Destroy(gameObject);
    }
}
