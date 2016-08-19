using UnityEngine;
using System.Collections;

public class LifeGUI : MonoBehaviour 
{
    bool ativa;
	// Use this for initialization
	void Start () 
    {
        ativa = true;
	}
	
	// Update is called once per frame
	void Update () 
    {
        GetComponent<GUITexture>().color = GameObject.Find("Fundo2").GetComponent<ScriptMusica>().DevolveCor();
        if(!ativa)
        {
            GetComponent<GUITexture>().color = new Color(GetComponent<GUITexture>().color.r, GetComponent<GUITexture>().color.g, GetComponent<GUITexture>().color.b, 0.3f);
        }
	}

    public void Desativa()
    {
        ativa = false;

        //GetComponent<GUITexture>().color = new Color(0.3f, 0.3f, 184);
        //GetComponent<GUITexture>().color = new Color(GetComponent<GUITexture>().color.r, GetComponent<GUITexture>().color.g, GetComponent<GUITexture>().color.b, 70);
    }

    public void Ativa()
    {
        ativa = true;
    }
}
