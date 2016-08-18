﻿using UnityEngine;
using System.Collections;

public class ScriptEspecial : MonoBehaviour 
{
    public static float ESPECIAL_CHEIO = 30.0f;
    public static bool ESPECIAL_ATIVADO = false;

    public float tempoBurst = 15.0f;
    public float rateNoBurstWithWhite = 0.85f;
    public float rateBurstWithColors = 0.95f;
    public Transform botaoAzul;
    public Transform botaoAmarelo;
    public Transform botaoVermelho;
    public Transform fundo;
    public Material matAzul;
    public Material matAmarelo;
    public Material matVermelho;
    public Material matBranco;

    private float tempo;
    private float tempCorFundo = 0.0f;
    private float especial = 0.0f;
    private Color corAzul;
    private Color corAmarelo;
    private Color corVermelho;
    private Color corBranco;
    private ParticleAnimator animatorAzul;
    private ParticleAnimator animatorAmarelo;
    private ParticleAnimator animatorVermelho;
    private ParticleAnimator animatorFundo;
    private Color[] trueColorsAzul = new Color[5];
    private Color[] trueColorsAmarelo = new Color[5];
    private Color[] trueColorsVermelho = new Color[5];
    private Color[] vetorAzul = new Color[5];
    private Color[] vetorAmarelo = new Color[5];
    private Color[] vetorVermelho = new Color[5];
    private Color[] corFundo = new Color[5];
    

	// Use this for initialization
	void Start () 
    {
        animatorAzul = botaoAzul.GetComponent<ParticleAnimator>();
        animatorAmarelo = botaoAmarelo.GetComponent<ParticleAnimator>();
        animatorVermelho = botaoVermelho.GetComponent<ParticleAnimator>();

        corAzul = new Vector4(0f, 0f, 255.0f / 255, 255.0f / 255);
        corAmarelo = new Vector4(255.0f / 255, 255.0f / 255, 0f, 255.0f / 255);
        corVermelho = new Vector4(255.0f / 255, 0f, 0f, 255.0f / 255);
        corBranco = new Vector4(255.0f / 255, 255.0f / 255, 255.0f / 255, 255.0f / 255);

        matAzul.color = corAzul;
        matAmarelo.color = corAmarelo;
        matVermelho.color = corVermelho;

        for (var i = 0; i < 5; i += 1)
        {
            trueColorsAzul[i] = animatorAzul.colorAnimation[i];
            trueColorsAmarelo[i] = animatorAmarelo.colorAnimation[i];
            trueColorsVermelho[i] = animatorVermelho.colorAnimation[i];
            vetorAzul[i] = Color.white;
            vetorAmarelo[i] = Color.white;
            vetorVermelho[i] = Color.white;
        }

        animatorFundo = fundo.GetComponent<ParticleAnimator>();
        corFundo = animatorFundo.colorAnimation;
	}

    public void ResetaEspecial()
    {
        especial = 0;
        ESPECIAL_ATIVADO = false;

        // VOLTANDO OS BOTOES PARA AS CORES ORIGINAIS
        animatorAzul.colorAnimation = trueColorsAzul;
        animatorAmarelo.colorAnimation = trueColorsAmarelo;
        animatorVermelho.colorAnimation = trueColorsVermelho;

        // VOLTANDO AS BOLAS PARA AS CORES ORIGINAIS
        matAzul.color = corAzul;
        matAmarelo.color = corAmarelo;
        matVermelho.color = corVermelho;

        // DEIXANDO O FUNDO NORMAL
        corFundo[0] = new Color(0, 0, 0);
        corFundo[1] = new Color(0, 0, 0);
        corFundo[2] = new Color(0, 0, 0);
        corFundo[3] = new Color(0, 0, 0);
        corFundo[4] = new Color(0, 0, 0);
        animatorFundo.colorAnimation = corFundo;

        CancelInvoke();
    }

    public void MudaCorBola(string nomeBotao)
    {
        if (nomeBotao == "BotaoAzul")
        {
            matAzul.color = corAzul;
        }
        else if (nomeBotao == "BotaoAmarelo")
        {
            matAmarelo.color = corAmarelo;
        }
        else if (nomeBotao == "BotaoVermelho")
        {
            matVermelho.color = corVermelho;
        }
    }

    private void EspecialAtivado()
    {
        // TEMPO DO BURST
        especial = ESPECIAL_CHEIO - ((Time.time - tempo) / tempoBurst) * ESPECIAL_CHEIO;

        // AS CORES VAO VOLTANDO CONFORME GASTA O BURST
        tempCorFundo = (1 - rateBurstWithColors) + (especial / ESPECIAL_CHEIO) * rateBurstWithColors;
        corFundo[0] = new Color(tempCorFundo, tempCorFundo, tempCorFundo);
        corFundo[1] = new Color(tempCorFundo, tempCorFundo, tempCorFundo);
        corFundo[2] = new Color(tempCorFundo, tempCorFundo, tempCorFundo);
        corFundo[3] = new Color(tempCorFundo, tempCorFundo, tempCorFundo);
        corFundo[4] = new Color(tempCorFundo, tempCorFundo, tempCorFundo);
        animatorFundo.colorAnimation = corFundo;

        if (Time.time - tempo > tempoBurst)
        { // BURST ACABOU
            especial = 0;
            ESPECIAL_ATIVADO = false;

            // VOLTANDO OS BOTOES PARA AS CORES ORIGINAIS
            animatorAzul.colorAnimation = trueColorsAzul;
            animatorAmarelo.colorAnimation = trueColorsAmarelo;
            animatorVermelho.colorAnimation = trueColorsVermelho;

            // VOLTANDO AS BOLAS PARA AS CORES ORIGINAIS
            matAzul.color = corAzul;
            matAmarelo.color = corAmarelo;
            matVermelho.color = corVermelho;

            // DEIXANDO O FUNDO NORMAL
            corFundo[0] = new Color(0, 0, 0);
            corFundo[1] = new Color(0, 0, 0);
            corFundo[2] = new Color(0, 0, 0);
            corFundo[3] = new Color(0, 0, 0);
            corFundo[4] = new Color(0, 0, 0);
            animatorFundo.colorAnimation = corFundo;

            CancelInvoke();
        }
    }

    public void RecalculaFundo(int inc)
    {
        
	    if (!ESPECIAL_ATIVADO)
	    {
		    especial += inc;
            //print(especial + " " + ESPECIAL_CHEIO);
		    if (especial < 0)
			    especial = 0;

		    // VAI FICANDO BRANCO CONFORME AUMENTA O BURST
		    tempCorFundo = Mathf.Pow((especial / ESPECIAL_CHEIO) * rateNoBurstWithWhite, 2);
		    corFundo[0] = new Color(tempCorFundo, tempCorFundo, tempCorFundo);
            corFundo[1] = new Color(tempCorFundo, tempCorFundo, tempCorFundo);
            corFundo[2] = new Color(tempCorFundo, tempCorFundo, tempCorFundo);
            corFundo[3] = new Color(tempCorFundo, tempCorFundo, tempCorFundo);
            corFundo[4] = new Color(tempCorFundo, tempCorFundo, tempCorFundo);
		    animatorFundo.colorAnimation = corFundo;
		
		    if (especial == ESPECIAL_CHEIO)
			    AtivaEspecial();
	    }
    }

    private void AtivaEspecial()
    {
        ESPECIAL_ATIVADO = true;
        tempo = Time.time;

        // DEIXANDO AS BOLAS BRANCAS
        matAzul.color = corBranco;
        matAmarelo.color = corBranco;
        matVermelho.color = corBranco;

        // DEIXANDO OS BOTOES BRANCOS
        animatorAzul.colorAnimation = vetorAzul;
        animatorAmarelo.colorAnimation = vetorAmarelo;
        animatorVermelho.colorAnimation = vetorVermelho;

        // DEIXANDO O FUNDO BRANCO
        corFundo[0] = new Color(rateNoBurstWithWhite, rateNoBurstWithWhite, rateNoBurstWithWhite);
        corFundo[1] = new Color(rateNoBurstWithWhite, rateNoBurstWithWhite, rateNoBurstWithWhite);
        corFundo[2] = new Color(rateNoBurstWithWhite, rateNoBurstWithWhite, rateNoBurstWithWhite);
        corFundo[3] = new Color(rateNoBurstWithWhite, rateNoBurstWithWhite, rateNoBurstWithWhite);
        corFundo[4] = new Color(rateNoBurstWithWhite, rateNoBurstWithWhite, rateNoBurstWithWhite);
        animatorFundo.colorAnimation = corFundo;

        InvokeRepeating("EspecialAtivado", 0.2f, 0.2f);
    }

    public void ItemAtivaEspecial()
    {
        AtivaEspecial();
    }
}
