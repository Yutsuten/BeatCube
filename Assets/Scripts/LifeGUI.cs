using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LifeGUI : MonoBehaviour 
{
    bool ativa;

    BackgroundAnimation backgroundAnimation;
    RawImage lifeTexture;

	// Use this for initialization
	void Start () 
    {
        ativa = true;

        backgroundAnimation = GameObject.Find("Background").GetComponent<BackgroundAnimation>();
        lifeTexture = GetComponent<RawImage>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        lifeTexture.color = backgroundAnimation.DevolveCor();
        if(!ativa)
        {
            lifeTexture.color = new Color(lifeTexture.color.r, lifeTexture.color.g, lifeTexture.color.b, 0.3f);
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
