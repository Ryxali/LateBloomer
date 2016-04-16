using UnityEngine;
using System.Collections;

public class Grass : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.localScale = Vector3.one * Random.Range(0.7f, 1.1f);
        transform.rotation = Quaternion.AngleAxis(Random.Range(0.0f, 360f), Vector3.up);
	}
	
}
