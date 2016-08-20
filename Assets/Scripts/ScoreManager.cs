using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour 
{
    int pontos = 0;
    int combo = 0;

    ScoreGUI scoreGUI;

    void Start()
    {
        scoreGUI = GameObject.Find("Panel/Score").GetComponent<ScoreGUI>();
    }

    public void AumentaPontos(int quantidade)
    {
        pontos += quantidade;
        scoreGUI.AtualizaPontos(pontos);
    }

    public void AumentaCombo()
    {
        combo++;
        GetComponent<ScriptEspecial>().RecalculaFundo(3);

        if (combo >= 10)
        {
            //GetComponent<ScriptEspecial>().RecalculaFundo(30);
            //print("Especial");
            combo = 0;
        }
    }

    public void ResetaCombo()
    {
        combo = 0;
        GetComponent<ScriptEspecial>().ResetaEspecial();
    }
}
