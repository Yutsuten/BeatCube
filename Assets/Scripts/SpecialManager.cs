using UnityEngine;
using System.Collections;

public class SpecialManager : MonoBehaviour 
{
    public static bool SpecialActivated = false;

    private const float fullSpecial = 30.0f;
    private float tempoBurst = 10.0f;
    private float rateNoBurstWithWhite = 0.85f;
    private float rateBurstWithColors = 0.95f;
    public Transform botaoAzul;
    public Transform botaoAmarelo;
    public Transform botaoVermelho;
    public Transform fundo;
    public Material matAzul;
    public Material matAmarelo;
    public Material matVermelho;
    public Material matBranco;

    private float specialBeginTime;
    private float tempCorFundo = 0.0f;
    private float special = 0.0f;
    /*private Color corAzul;
    private Color corAmarelo;
    private Color corVermelho;
    private Color corBranco;*/
    private ParticleAnimator blueButton;
    private ParticleAnimator yellowButton;
    private ParticleAnimator redButton;
    private Color[] blue = new Color[5];
    private Color[] yellow = new Color[5];
    private Color[] red = new Color[5];
    private Color[] white = new Color[5];
    

	// Use this for initialization
	void Start () 
    {
        blueButton = botaoAzul.GetComponent<ParticleAnimator>();
        yellowButton = botaoAmarelo.GetComponent<ParticleAnimator>();
        redButton = botaoVermelho.GetComponent<ParticleAnimator>();

        /*corAzul = new Vector4(0f, 0f, 1f, 1f);
        corAmarelo = new Vector4(1f,1f, 0f, 1f);
        corVermelho = new Vector4(1f, 0f, 0f, 1f);
        corBranco = new Vector4(1f, 1f, 1f, 1f);

        matAzul.color = corAzul;
        matAmarelo.color = corAmarelo;
        matVermelho.color = corVermelho;*/

        for (var i = 0; i < 5; i += 1)
        {
            blue[i] = blueButton.colorAnimation[i];
            yellow[i] = yellowButton.colorAnimation[i];
            red[i] = redButton.colorAnimation[i];
            white[i] = Color.white;
        }

        //animatorFundo = fundo.GetComponent<ParticleAnimator>();
        //corFundo = animatorFundo.colorAnimation;
	}

    private void ButtonsWhite()
    {
        blueButton.colorAnimation = white;
        yellowButton.colorAnimation = white;
        redButton.colorAnimation = white;
    }

    private void ButtonsColored()
    {
        blueButton.colorAnimation = blue;
        yellowButton.colorAnimation = yellow;
        redButton.colorAnimation = red;
    }

    public void ResetSpecial()
    {
        special = 0;
        SpecialActivated = false;

        ButtonsColored();

        // VOLTANDO AS BOLAS PARA AS CORES ORIGINAIS
        /*matAzul.color = corAzul;
        matAmarelo.color = corAmarelo;
        matVermelho.color = corVermelho;*/

        // DEIXANDO O FUNDO NORMAL
        /*corFundo[0] = new Color(0, 0, 0);
        corFundo[1] = new Color(0, 0, 0);
        corFundo[2] = new Color(0, 0, 0);
        corFundo[3] = new Color(0, 0, 0);
        corFundo[4] = new Color(0, 0, 0);
        animatorFundo.colorAnimation = corFundo;*/

        CancelInvoke();
    }

    private void EspecialAtivado()
    {
        // TEMPO DO BURST
        special = fullSpecial - ((Time.time - specialBeginTime) / tempoBurst) * fullSpecial;

        // AS CORES VAO VOLTANDO CONFORME GASTA O BURST
        tempCorFundo = (1 - rateBurstWithColors) + (special / fullSpecial) * rateBurstWithColors;
        /*corFundo[0] = new Color(tempCorFundo, tempCorFundo, tempCorFundo);
        corFundo[1] = new Color(tempCorFundo, tempCorFundo, tempCorFundo);
        corFundo[2] = new Color(tempCorFundo, tempCorFundo, tempCorFundo);
        corFundo[3] = new Color(tempCorFundo, tempCorFundo, tempCorFundo);
        corFundo[4] = new Color(tempCorFundo, tempCorFundo, tempCorFundo);
        animatorFundo.colorAnimation = corFundo;*/

        if (Time.time - specialBeginTime > tempoBurst)
        { // BURST ACABOU
            special = 0;
            SpecialActivated = false;

            // VOLTANDO OS BOTOES PARA AS CORES ORIGINAIS
            blueButton.colorAnimation = blue;
            yellowButton.colorAnimation = yellow;
            redButton.colorAnimation = red;

            // VOLTANDO AS BOLAS PARA AS CORES ORIGINAIS
            /*matAzul.color = corAzul;
            matAmarelo.color = corAmarelo;
            matVermelho.color = corVermelho;*/

            // DEIXANDO O FUNDO NORMAL
            /*corFundo[0] = new Color(0, 0, 0);
            corFundo[1] = new Color(0, 0, 0);
            corFundo[2] = new Color(0, 0, 0);
            corFundo[3] = new Color(0, 0, 0);
            corFundo[4] = new Color(0, 0, 0);
            animatorFundo.colorAnimation = corFundo;*/

            CancelInvoke();
        }
    }

    public void RecalculaFundo()
    {
	    if (!SpecialActivated)
	    {
		    special++;
            //print(special + "/" + fullSpecial + " ~ Cor fundo = " + tempCorFundo);

		    // VAI FICANDO BRANCO CONFORME AUMENTA O BURST
            tempCorFundo = Mathf.Pow((special / fullSpecial) * rateNoBurstWithWhite, 2);
		    /*corFundo[0] = new Color(tempCorFundo, tempCorFundo, tempCorFundo);
            corFundo[1] = new Color(tempCorFundo, tempCorFundo, tempCorFundo);
            corFundo[2] = new Color(tempCorFundo, tempCorFundo, tempCorFundo);
            corFundo[3] = new Color(tempCorFundo, tempCorFundo, tempCorFundo);
            corFundo[4] = new Color(tempCorFundo, tempCorFundo, tempCorFundo);
		    animatorFundo.colorAnimation = corFundo;*/

            if (special == fullSpecial)
            {
                AtivaEspecial();
                ButtonsWhite();
            }
			    
	    }
    }

    private void AtivaEspecial()
    {
        SpecialActivated = true;
        specialBeginTime = Time.time;

        // DEIXANDO AS BOLAS BRANCAS
        /*matAzul.color = corBranco;
        matAmarelo.color = corBranco;
        matVermelho.color = corBranco;*/

        // DEIXANDO OS BOTOES BRANCOS
        blueButton.colorAnimation = white;
        yellowButton.colorAnimation = white;
        redButton.colorAnimation = white;

        // DEIXANDO O FUNDO BRANCO
        /*corFundo[0] = new Color(rateNoBurstWithWhite, rateNoBurstWithWhite, rateNoBurstWithWhite);
        corFundo[1] = new Color(rateNoBurstWithWhite, rateNoBurstWithWhite, rateNoBurstWithWhite);
        corFundo[2] = new Color(rateNoBurstWithWhite, rateNoBurstWithWhite, rateNoBurstWithWhite);
        corFundo[3] = new Color(rateNoBurstWithWhite, rateNoBurstWithWhite, rateNoBurstWithWhite);
        corFundo[4] = new Color(rateNoBurstWithWhite, rateNoBurstWithWhite, rateNoBurstWithWhite);
        animatorFundo.colorAnimation = corFundo;*/

        InvokeRepeating("EspecialAtivado", 0.2f, 0.2f);
    }

    public void ItemAtivaEspecial()
    {
        AtivaEspecial();
    }
}
