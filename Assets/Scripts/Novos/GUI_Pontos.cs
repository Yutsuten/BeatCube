using UnityEngine;
using System.Collections;

public class GUI_Pontos : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
    {
        
	}
	
	// Update is called once per frame
	void Update () 
    {
        GetComponent<GUIText>().color = GameObject.Find("Fundo2").GetComponent<ScriptMusica>().DevolveCor();
	}

    public void AtualizaPontos(int p)
    {
        GetComponent<GUIText>().text = (p * 10).ToString();
    }
}
