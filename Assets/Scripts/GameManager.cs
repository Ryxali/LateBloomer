using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public TransformFollower cam;

    public AudioSource introUnlooped;
    public AudioSource introLooped;
    public AudioSource flyMusic;

    void Start()
    {
        OnRoundEnd();

        //GetComponent<AudioSource>().Stop();
        //GetComponent<AudioSource>().PlayOneShot(introUnlooped);
        //GetComponent<AudioSource>().clip = introLooped;
        //GetComponent<AudioSource>().PlayDelayed(introUnlooped.length);
        //GetComponent<AudioSource>().PlayScheduled(Time.time + introUnlooped.length);
        //GetComponent<AudioSource>().Play(((ulong)introLooped.samples * (ulong)44100));

        //StartCoroutine(IntroMusic());
        
    }

    private void PlayIntro()
    {
        flyMusic.Stop();
        introUnlooped.Play();
        introLooped.PlayDelayed(introUnlooped.clip.length - introUnlooped.time);
    }

    private void PlayFly()
    {
        introUnlooped.Stop();
        introLooped.Stop();
        flyMusic.Play();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            OnRoundEnd();
        }
    }

    public void OnStart()
    {
        PlayFly();
    }
	
    public void OnRoundEnd()
    {
        BroadcastMessage("Reset");
        PlayIntro();
    }

    public void OnShake(float intensity, float duration)
    {
        cam.GetComponentInChildren<CameraShaker>().Shake(intensity, duration);
    }
}
