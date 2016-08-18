using UnityEngine;
using System.Collections;

public class ScriptPontuacao : MonoBehaviour 
{/*
	const float taxaAtualizacao = 0.02f;

	private int incrementar;
	private int totalInc;
	private int numPontuacao;
	private int valorInc;
    private int pontuacao;
    private int combo;
	private float tempo;
	private GUIStyle styleScore = new GUIStyle();

	public int pontuacaoAcerto = 10;
	public float comboMultiplier = 0.005f;
	//public float tempoVermelho = 0.2f;

    public int RetornaPontuacao()
    {
        return pontuacao;
    }

	public void ResetaContadores()
	{
		pontuacao = 0;
		numPontuacao = 0;
		incrementar = 0;
		totalInc = 0;
		tempo = 0;
		valorInc = 0;
		combo = 0;
	}

	public void ResetaCombo()
	{
		combo = 0;
		//styleScore.normal.textColor = Color.red;
		//Invoke("fimVermelho", tempoVermelho);
	}

	/*public void fimVermelho()
	{
		//styleScore.normal.textColor = Color.white;
	}*//*

    public void IncCombo()
    {
        combo++;
    }

    public void DecCombo()
    {
        if (--combo < 0)
            combo = 0;
    }

	public void AumentaPontuacao()
	{
		// VALOR A SER INCREMENTADO NA PONTUACAO
		incrementar = pontuacaoAcerto + (int)((combo * comboMultiplier + GetComponent<LevelManager>().RetornaDificuldade()) * pontuacaoAcerto);
		// INCREMENTANDO A PONTUACAO REAL
        pontuacao += incrementar;
		totalInc += incrementar * 100;
		GetComponent<LevelManager>().AtualizaLevel();
	}

	void OnGUI()
	{
		GUI.Label(new Rect(60*Screen.width/100, Screen.height/100, Screen.width/10, Screen.height/30), "Score: " + numPontuacao/100 + "\nCombo: " + combo, styleScore);
	}

	// Use this for initialization
	void Start () 
	{
		ResetaContadores();
		styleScore.fontSize = Screen.width/20;
		styleScore.normal.textColor = Color.white;
	}
	
	// Update is called once per frame
	void Update () 
	{
		tempo += Time.deltaTime;
		while (tempo > taxaAtualizacao)
		{
			// DECREMENTANDO O VALOR DESSA ITERACAO
			tempo -= taxaAtualizacao;
			// ATUALIZANDO A PONTUACAO
			if (totalInc > 0)
			{
				// INCREMENTO DE POUCO EM POUCO .. + 100 PARA ACELERAR QUANDO ESTIVER FALTANDO 1 PONTO
				valorInc = (int)(0.05f * (totalInc + 100));
				if (valorInc > totalInc)
					valorInc = totalInc;
				numPontuacao += valorInc;
				totalInc -= valorInc;
			}
		}
	}*/
}
