﻿using UnityEngine;
using System.Collections;

public class BubbleEffect : MonoBehaviour {
    public float effectMagnitude = 15.0f;
    public float duration = 2.0f;
    private int stacks = 0;
    public int maxStacks = 3;
    public AudioClip popSound;
    // Use this for initialization
    void Start () {
        ApplyStack();
        StartCoroutine(EffectSequence());
       
	}

    public void ApplyStack()
    {
        stacks++;
        Caterpillar caterpillar = GetComponentInParent<Caterpillar>();
        if (stacks > maxStacks)
        {
            while(stacks > 1)
            {
                
                caterpillar.acceleration -= Vector3.up * effectMagnitude;
                stacks--;
                StopAllCoroutines();
                
                
            }
            GetComponent<AudioSource>().pitch = 1.0f;
            caterpillar.GetComponent<AudioSource>().PlayOneShot(popSound);
            Destroy(gameObject);
            return;
        }
        GetComponent<AudioSource>().pitch =  0.66f + (float)stacks / 3.0f;
        GetComponent<AudioSource>().Play();
        caterpillar.acceleration += Vector3.up * effectMagnitude;
        UpdateSize();
    }

    private void UpdateSize()
    {
        transform.localScale = Vector3.one * (1.0f + (float)stacks / 4.0f);
    }

    IEnumerator EffectSequence()
    {
        Caterpillar caterpillar = GetComponentInParent<Caterpillar>();
        while(stacks > 0)
        {
            yield return new WaitForSeconds(duration);
            caterpillar.acceleration -= Vector3.up * effectMagnitude;
            GetComponent<AudioSource>().pitch = 0.66f + (float)stacks / 3.0f;
            caterpillar.GetComponent<AudioSource>().PlayOneShot(popSound);
            stacks--;
            UpdateSize();
        }
        
        Destroy(gameObject);
    }
}
