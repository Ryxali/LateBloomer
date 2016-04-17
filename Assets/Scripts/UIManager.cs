using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

    public static UIManager inst { get; private set; }

    public UISlider forceSlider;

	// Use this for initialization
	void Start () {
        inst = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
