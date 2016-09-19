using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameTime : MonoBehaviour 
{
    float tempo = 0;
    float tempoInicio;
    BackgroundAnimation scriptMusica;
    Text timeText;

	void Start () 
    {
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

    public void ResetTime()
    {
        tempoInicio = Time.time;
        tempo = 0;
        timeText.text = "0";
    }
}
