using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMananger : MonoBehaviour {
    bool gameOn;
    bool Pause;

    float tempoInstanciado;
    float tempoInstanciadoAnterior;

	// Use this for initialization
	void Start () 
    {
        gameOn = true;
        Pause = false;

        tempoInstanciadoAnterior = Time.time;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape))
        {
            Pause = !Pause;

            if (Pause)
            {
                PausaJogo();
            }
            else
            {
                ResumeJogo();
            }
        }
        //CriaObjeto(Random.RandomRange(0.4f, 0.8f));
	}

    void OnGUI()
    {
        if (Pause)
        {
            GUI.Box(new Rect(0,0, Screen.width, Screen.height), "");
            GUI.Box(new Rect(Screen.width/4f, 10, Screen.width/2, Screen.height/1.3f), "Jogo Pausado");
            if (GUI.Button(new Rect(Screen.width/4f, 50, Screen.width/2f, 30), "Voltar"))
            {
                Pause = false;
            }
            if (GUI.Button(new Rect(Screen.width / 4f, 110, Screen.width / 2f, 30), "Reiniciar"))
            {
            }
            if (GUI.Button(new Rect(Screen.width / 4f, 170, Screen.width / 2f, 30), "Menu"))
            {
            }
        }
    }

    public void ItemDestroiObj()
    {

    }

    public void FimDeJogo()
    {
        gameOn = false;
        /*GameObject.Find("BotaoAmarelo").GetComponent<Button>().PauseGame();
        GameObject.Find("BotaoAzul").GetComponent<Button>().PauseGame();
        GameObject.Find("BotaoVermelho").GetComponent<Button>().PauseGame();*/
    }

    private void PausaJogo()
    {
        gameOn = false;

        /*GameObject.Find("BotaoAmarelo").GetComponent<Button>().PauseGame();
        GameObject.Find("BotaoAzul").GetComponent<Button>().PauseGame();
        GameObject.Find("BotaoVermelho").GetComponent<Button>().PauseGame();*/
    }

    private void ResumeJogo()
    {
        gameOn = true;

        /*GameObject.Find("BotaoAmarelo").GetComponent<Button>().ResumeGame();
        GameObject.Find("BotaoAzul").GetComponent<Button>().ResumeGame();
        GameObject.Find("BotaoVermelho").GetComponent<Button>().ResumeGame();*/
    }

    private void ReiniciaJogo()
    {

    }

    private void RetornaMenuPrincipal()
    {

    }
}
