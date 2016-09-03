using UnityEngine;
using System.Collections;

public class SpecialManager : MonoBehaviour 
{
    public static bool SpecialActivated = false;

    private const float fullSpecial = 30.0f;
    private const float ratioBeforeBurst = 0.55f;
    private const float ratioAfterBurst = 0.80f;
    private float tempoBurst = 10.0f;
    public Material matAzul;
    public Material matAmarelo;
    public Material matVermelho;
    public Material matBranco;

    private float specialBeginTime;
    private float specialColorRatio = 0.0f;
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

    BackgroundAnimation backgroundAnimation;

	// Use this for initialization
	void Start () 
    {
        blueButton = GameObject.Find("Buttons/BlueButton").GetComponent<ParticleAnimator>();
        yellowButton = GameObject.Find("Buttons/YellowButton").GetComponent<ParticleAnimator>();
        redButton = GameObject.Find("Buttons/RedButton").GetComponent<ParticleAnimator>();

        backgroundAnimation = GameObject.Find("Background").GetComponent<BackgroundAnimation>();

        /*corAzul = new Vector4(0f, 0f, 1f, 1f);
        corAmarelo = new Vector4(1f,1f, 0f, 1f);
        corVermelho = new Vector4(1f, 0f, 0f, 1f);
        corBranco = new Vector4(1f, 1f, 1f, 1f);*/

        for (var i = 0; i < 5; i += 1)
        {
            blue[i] = blueButton.colorAnimation[i];
            yellow[i] = yellowButton.colorAnimation[i];
            red[i] = redButton.colorAnimation[i];
            white[i] = Color.white;
        }
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
        backgroundAnimation.UpdateSpecialInfluence(0);

        CancelInvoke();
    }

    public void UpdateSpecialGauge()
    {
        if (!SpecialActivated)
        {
            special++;

            // VAI FICANDO BRANCO CONFORME AUMENTA O BURST
            specialColorRatio = ratioBeforeBurst * Mathf.Pow((special / fullSpecial), 2);
            backgroundAnimation.UpdateSpecialInfluence(specialColorRatio);

            print(special + "/" + fullSpecial + " ~ Cor fundo = " + specialColorRatio);

            if (special >= fullSpecial)
            {
                special = fullSpecial;
                ActivateSpecial();
                ButtonsWhite();
            }

        }
    }

    private void ActivateSpecial()
    {
        SpecialActivated = true;
        specialBeginTime = Time.time;

        // DEIXANDO OS BOTOES BRANCOS
        blueButton.colorAnimation = white;
        yellowButton.colorAnimation = white;
        redButton.colorAnimation = white;

        InvokeRepeating("ActivatedSpecial", 0.2f, 0.2f);
    }

    private void ActivatedSpecial()
    {
        // TEMPO DO BURST
        special = fullSpecial - ((Time.time - specialBeginTime) / tempoBurst) * fullSpecial;
        special = (special < 0) ? 0 : special;

        // AS CORES VAO VOLTANDO CONFORME GASTA O BURST
        specialColorRatio = ratioAfterBurst * Mathf.Pow((special / fullSpecial), 2) + 0.3f;
        backgroundAnimation.UpdateSpecialInfluence(specialColorRatio);

        print(special + "/" + fullSpecial + " ~ Cor fundo = " + specialColorRatio);

        if (Time.time - specialBeginTime > tempoBurst)
            ResetSpecial();
    }

    public void ItemSpecialBonus()
    {
        special += 3;
        UpdateSpecialGauge();
    }
}
