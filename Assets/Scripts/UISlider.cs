using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UISlider : MonoBehaviour {

    [SerializeField]
    private RectTransform sliderRect;
    private RectTransform selfRect;
    public float value
    {
        get
        {
            return sliderRect.rect.width / selfRect.rect.width;
        }
        set
        {
            //Rect r = sliderRect.rect;
            //r.width = selfRect.rect.width * Mathf.Clamp01(value);
            //sliderRect.rect.Set(r.x, r.y, r.width, r.height);
            sliderRect.sizeDelta = new Vector2(selfRect.rect.width * Mathf.Clamp01(value), sliderRect.sizeDelta.y);
        }
    }
	// Use this for initialization
	void Start () {
        selfRect = GetComponent<RectTransform>();

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
