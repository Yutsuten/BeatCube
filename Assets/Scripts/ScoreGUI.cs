using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreGUI : MonoBehaviour 
{
    BackgroundAnimation backgroundAnimation;
    Text scoreText;

	void Start () 
    {
        backgroundAnimation = GameObject.Find("Background").GetComponent<BackgroundAnimation>();
        scoreText = GetComponent<Text>();
	}
	
	void Update () 
    {
        scoreText.color = backgroundAnimation.DevolveCor();
	}

    public void AtualizaPontos(int p)
    {
        scoreText.text = (p).ToString();
    }
}
