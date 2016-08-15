using UnityEngine;
using System.Collections;

public class DificuldadeMusica : MonoBehaviour {

    bool OnGame, Pause;
    float incremento;
	// Use this for initialization
	void Start () 
    {
        GetComponent<AudioSource>().loop = false;
        OnGame = false;
        Pause = false;
        incremento = 0.1f / 60;
	}
	
	// Update is called once per frame
    void Update()
    {
        if (!Pause)
        {
            if (OnGame)
            {
                GetComponent<AudioSource>().pitch += incremento * Time.deltaTime;
            }
            if (!GetComponent<AudioSource>().isPlaying)
            {
                if (GetComponent<AudioSource>().time == 0)
                {
                    print("Ele acha que nao ta tocando");
                    GetComponent<AudioSource>().Play();
                    GetComponent<AudioSource>().loop = true;
                    OnGame = true;

                    //GameObject.Find("Fundo2").GetComponent<ScriptMusica>().RecalculaContador(0.2f);
                }
            }
        }
    }

    public void GameON()
    {
        //print("Ta deixando OnGame verdadeiro");
        //OnGame = true;
        Pause = false;
        GetComponent<AudioSource>().Play();
    }

    public void ONGame()
    {
        OnGame = true;
    }

    public void GamePause()
    {
        Pause = true;
        GetComponent<AudioSource>().Pause();
    }
}
