﻿using UnityEngine;
using System.Collections;
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Bubble : MonoBehaviour {
    public BubbleEffect bubbleEffectPrefab;

    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Caterpillar c = other.GetComponent<Caterpillar>();
        if(c != null)
        {
            BubbleEffect bEff = c.GetComponentInChildren<BubbleEffect>();
            if(bEff != null)
            {
                bEff.ApplyStack();
            } else
            {
                BubbleEffect effect = Instantiate<BubbleEffect>(bubbleEffectPrefab);
                effect.transform.parent = c.transform;
                effect.transform.localPosition = Vector3.zero;
                effect.transform.localRotation = Quaternion.Euler(Vector3.zero);
            }
            
        }
        Destroy(gameObject);
    }
}
