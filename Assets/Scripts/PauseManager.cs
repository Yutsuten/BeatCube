using UnityEngine;
using System.Collections;

public class PauseManager : MonoBehaviour 
{
    private GUIStyle styleAbout = new GUIStyle();
    private bool onPause;
    private string textoAbout = "LTIA";
    private string lbAbout;

    public void Pause()
    {
        Time.timeScale = 0;
        onPause = true;
    }

    public void ResetaLbAbout()
    {
        lbAbout = "About";
        styleAbout.normal.textColor = Color.white;
    }

    public void BeginGame()
    {
        lbAbout = "Click here to pause...";
        Invoke("styleEscuro", 0.4f);
        Invoke("styleClaro", 0.8f);
        Invoke("styleEscuro", 1.2f);
    }

    private void styleEscuro()
    {
        lbAbout = "";
    }

    private void styleClaro()
    {
        lbAbout = "Click here to pause...";
    }

    private void EnableTargets()
    { // DUCPLICATA DE SCRIPTVIDAS
        //ScriptAlvo.DESTROY_ALL_TARGETS = false;
    }

    void OnGUI()
    {/*
        //GUI.Box(new Rect(0, 0, Screen.width, 0.12*Screen.height), ""); // 1 - 380.0/Screen.height
        if (!ScriptMenu.menuEnabled)
            GUI.Label(new Rect(20 * Screen.width / 100, 5 * Screen.height / 100, Screen.width / 10, Screen.height / 30), "" + lbAbout, styleAbout);
        else
            GUI.Label(new Rect(40 * Screen.width / 100, 5 * Screen.height / 100, Screen.width / 10, Screen.height / 30), "" + lbAbout, styleAbout);
        if (onPause)
        {
            // SE ESTIVER EM JOGO
            if (!ScriptMenu.menuEnabled)
            {
                GUI.Box(new Rect((Screen.width - (Screen.width / 2)) * 0.5f, 33 * Screen.height / 100, Screen.width / 2, Screen.height / 3.25f), "");
                if (GUI.Button(new Rect((Screen.width - (Screen.width / 4)) * 0.5f, 35 * Screen.height / 100, Screen.width / 4, Screen.height / 15), "Resume"))
                {
                    onPause = false;
                    Time.timeScale = 1.0f;
                }
                if (GUI.Button(new Rect((Screen.width - (Screen.width / 4)) * 0.5f, 45 * Screen.height / 100, Screen.width / 4, Screen.height / 15), "Restart"))
                {  // DUPLICATA DE SCRIPT LIVES
                    GetComponent<ScriptPontuacao>().ResetaContadores();
                    GetComponent<LevelManager>().AtualizaLevel();
                    GetComponent<ScriptVidas>().ResetLives();
                    GetComponent<ScriptEspecial>().ResetaEspecial();
                    GetComponent<ScriptJogador>().enabled = true;
                    ScriptAlvo.DESTROY_ALL_TARGETS = true;
                    Invoke("EnableTargets", 0.05f);
                    Time.timeScale = 1.0f;
                    onPause = false;
                }
                if (GUI.Button(new Rect((Screen.width - (Screen.width / 3.5f)) * 0.5f, 55 * Screen.height / 100, Screen.width / 3.5f, Screen.height / 15), "Main Menu"))
                {  // DUPLICATA DE SCRIPT LIVES
                    GetComponent<ScriptPontuacao>().ResetaContadores();
                    GetComponent<ScriptMenu>().EnableMenu();
                    GetComponent<ScriptVidas>().ResetLives();
                    GetComponent<ScriptEspecial>().ResetaEspecial();
                    ScriptAlvo.DESTROY_ALL_TARGETS = true;
                    GetComponent<ScriptJogador>().enabled = true;
                    Invoke("EnableTargets", 0.05f);
                    Time.timeScale = 1.0f;
                    onPause = false;
                }
            }
            else // ESTA NO MENU PRINCIPAL AINDA
            {
                GUI.Box(new Rect((Screen.width - (Screen.width / 2)) * 0.5f, 33 * Screen.height / 100, Screen.width / 2, Screen.height / 3.25f), "");
                GUI.Label(new Rect(43 * Screen.width / 100, 40 * Screen.height / 100, Screen.width / 2, Screen.height / 2), textoAbout, styleAbout);
                if (GUI.Button(new Rect((Screen.width - (Screen.width / 3.5f)) * 0.5f, 55 * Screen.height / 100, Screen.width / 3.5f, Screen.height / 15), "OK"))
                {
                    onPause = false;
                    Time.timeScale = 1.0f;
                }
            }
        }*/
    }

	// Use this for initialization
	void Start () 
    {
        styleAbout.fontSize = Screen.width / 15;
        styleAbout.normal.textColor = Color.white;
        lbAbout = "About";
        onPause = false;
	}
}
