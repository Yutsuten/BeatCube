using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameTime : MonoBehaviour 
{
    float tempo;
    float tempoInicio;
    BackgroundAnimation scriptMusica;
    Text timeText;

	void Start () 
    {
        tempo = 0;
        tempoInicio = Time.time;
        scriptMusica = GameObject.Find("Background").GetComponent<BackgroundAnimation>();
        timeText = GetComponent<Text>();
	}
	
	void Update () 
    {
        timeText.color = scriptMusica.DevolveCor();
        tempo = Time.time - tempoInicio;
        timeText.text = Mathf.RoundToInt(tempo).ToString();
    }

    public float TimeElapsed()
    {
        return Mathf.RoundToInt(tempo);
    }
}
