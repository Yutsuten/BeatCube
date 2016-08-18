using UnityEngine;
using System.Collections;

public class GameTime : MonoBehaviour 
{
    float tempo;
    float tempoInicio;
    ScriptMusica scriptMusica;
    GUIText guiText;

	void Start () 
    {
        tempo = 0;
        tempoInicio = Time.time;
        scriptMusica = GameObject.Find("Fundo2").GetComponent<ScriptMusica>();
        guiText = GetComponent<GUIText>();
	}
	
	void Update () 
    {
        guiText.color = scriptMusica.DevolveCor();
        tempo = Time.time - tempoInicio;
        guiText.text = Mathf.RoundToInt(tempo).ToString();
    }

    public float TimeElapsed()
    {
        return Mathf.RoundToInt(tempo);
    }
}
