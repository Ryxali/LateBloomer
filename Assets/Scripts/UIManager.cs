using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

    public enum Panels
    {
        NONE = 0,
        LAUNCHER_SLIDER_PANEL = 1 << 1,
        SCORE_PANEL = 1 << 2,
        TUTORIAL_PANEL = 1 << 3
    }
    public static UIManager inst { get; private set; }

    public UISlider forceSlider;
    [SerializeField]
    private Transform tutorialPanel;
    [SerializeField]
    private Transform scorePanel;
	// Use this for initialization
	void Start () {
        inst = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ShowPanels(Panels panels)
    {
        forceSlider.gameObject.SetActive((panels & Panels.LAUNCHER_SLIDER_PANEL) == Panels.LAUNCHER_SLIDER_PANEL);
        scorePanel.gameObject.SetActive((panels & Panels.SCORE_PANEL) == Panels.SCORE_PANEL);
        tutorialPanel.gameObject.SetActive((panels & Panels.TUTORIAL_PANEL) == Panels.TUTORIAL_PANEL);
    }

    public void AddScore(int score)
    {
        scorePanel.GetComponentInChildren<ScoreKeeper>().AddScore(score);
    }
}
