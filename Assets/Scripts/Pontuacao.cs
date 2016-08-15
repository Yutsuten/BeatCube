using UnityEngine;
using System.Collections;

public class Pontuacao : MonoBehaviour 
{
    int pontos;
    int combo;

	// Use this for initialization
	void Start () 
    {
        pontos = 0;
        combo = 0;
	}

    public void AumentaPontos(int quantidade)
    {
        pontos += quantidade;
        GameObject.Find("Pontuacao").GetComponent<GUI_Pontos>().AtualizaPontos(pontos);
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
