using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class ScoreKeeper : MonoBehaviour {

    List<int> scores = new List<int>();
    [SerializeField]
    private Text scoreText;
	public void AddScore(int score)
    {
        scores.Add(score);
        scores.Sort();
        scores.Reverse();
        
        if (scores.Count > 10)
        {
            scores.RemoveAt(scores.Count - 1);
        }
        string s = "";
        for(int i = 0; i < scores.Count; i++)
        {
            Debug.Log(scores[i]);
            s += (i+1) + ". " + (scores[i]  / 2) + "dm" + System.Environment.NewLine;
        }
        
        scoreText.text = s;

    }
}
