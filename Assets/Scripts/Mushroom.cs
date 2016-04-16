﻿
using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class Mushroom : MonoBehaviour {

    public float bouncyness = 0.8f;
    public float friction = 0.1f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        
        Caterpillar c = other.GetComponent<Caterpillar>();
        if (c != null)
        {
            Vector3 vel = c.curVelocity;
            if (vel.y > 0.0f) return;
            Vector3 velU = Vector3.Project(vel, Vector3.up);
            vel -= velU;

            c.SetVelocity(vel * (1.0f - friction) - velU * bouncyness);
        }
    }
}