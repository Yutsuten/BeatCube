using UnityEngine;
using System.Collections;

public class ScoreGUI : MonoBehaviour 
{
    BackgroundAnimation backgroundAnimation;

	void Start () 
    {
        backgroundAnimation = GameObject.Find("Background").GetComponent<BackgroundAnimation>();
	}
	
	void Update () 
    {
        GetComponent<GUIText>().color = backgroundAnimation.DevolveCor();
	}

    public void AtualizaPontos(int p)
    {
        GetComponent<GUIText>().text = (p).ToString();
    }
}
