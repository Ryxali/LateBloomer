using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public TransformFollower cam;
    void Start()
    {
        OnRoundEnd();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            OnRoundEnd();
        }
    }
	
    public void OnRoundEnd()
    {
        BroadcastMessage("Reset");
    }

    public void OnShake(float intensity, float duration)
    {
        cam.GetComponentInChildren<CameraShaker>().Shake(intensity, duration);
    }
}
