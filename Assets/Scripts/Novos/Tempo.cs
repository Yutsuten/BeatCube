using UnityEngine;
using System.Collections;

public class Tempo : MonoBehaviour 
{
    float tempo;
    float tempoInicio;
	// Use this for initialization
	void Start () 
    {
        tempo = 0;
        tempoInicio = Time.time;
        //StartCoroutine(ContaTempo());
	}
	
	// Update is called once per frame
	void Update () 
    {
        GetComponent<GUIText>().color = GameObject.Find("Fundo2").GetComponent<ScriptMusica>().DevolveCor();

        tempo = Time.time - tempoInicio;

        GetComponent<GUIText>().text = Mathf.RoundToInt(tempo).ToString();
    }
}
