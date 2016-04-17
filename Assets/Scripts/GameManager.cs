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
	
    public void OnRoundEnd(int score = 0)
    {
        if(score == 0)
        {
            Restart();
        }
        else
        {
            UIManager.inst.AddScore(score);
            UIManager.inst.ShowPanels(UIManager.Panels.SCORE_PANEL);
        }
    }

    void Restart()
    {
        BroadcastMessage("Reset");
        PlayIntro();
        UIManager.inst.ShowPanels(UIManager.Panels.LAUNCHER_SLIDER_PANEL | UIManager.Panels.TUTORIAL_PANEL);
    }

    public void OnShake(float intensity, float duration)
    {
        cam.GetComponentInChildren<CameraShaker>().Shake(intensity, duration);
    }
}
