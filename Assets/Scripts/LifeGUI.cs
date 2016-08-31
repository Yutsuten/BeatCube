using UnityEngine;
using System.Collections;

public class LifeGUI : MonoBehaviour 
{
    bool ativa;

    BackgroundAnimation backgroundAnimation;

	// Use this for initialization
	void Start () 
    {
        ativa = true;

        backgroundAnimation = GameObject.Find("Background").GetComponent<BackgroundAnimation>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        GetComponent<GUITexture>().color = backgroundAnimation.DevolveCor();
        if(!ativa)
        {
            GetComponent<GUITexture>().color = new Color(GetComponent<GUITexture>().color.r, GetComponent<GUITexture>().color.g, GetComponent<GUITexture>().color.b, 0.3f);
        }
	}

    public void Desativa()
    {
        ativa = false;
    }

    public void Ativa()
    {
        ativa = true;
    }
}
