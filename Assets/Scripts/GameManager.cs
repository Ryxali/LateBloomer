using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

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
}
