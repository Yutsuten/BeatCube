using UnityEngine;
using System.Collections;

public class BackgroundAnimation : MonoBehaviour
{
    public AudioSource audioSource;

    float[] spectrum;
    int qSamples = 1024, lossFactor = 4;
    float color, a, b, yo, sumX, sumY, sumX2, sumXY, counter;
    private float minC = 0.8f, maxC = 1.5f;

    float specialInfluence;
    Color backgroundColor;

	void Start()
    {
        spectrum = new float[qSamples];
        counter = Random.Range(minC,maxC);
	}

    // 'Math' functions
    private float NumberInInterval(float number, float min = 0f, float max = 1f)
    {
        number = (number < min) ? min : number;
        number = (number > max) ? max : number;
        return number;
    }
	
    // Updating background
	void Update()
    {
        ReadSpectrumData();

        ParticleAnimator particleAnimator = GetComponent<ParticleAnimator>();
        Color[] backgroundColors = particleAnimator.colorAnimation;
        yo = CorDoMeio();

        backgroundColor = ColorWithSpecial();

        backgroundColors[0] = backgroundColor;
        backgroundColors[1] = backgroundColor;
        backgroundColors[2] = backgroundColor;
        backgroundColors[3] = backgroundColor;
        backgroundColors[4] = backgroundColor;
        particleAnimator.colorAnimation = backgroundColors;

        counter -= color * Time.deltaTime;
        if (counter <= 0)
        {
            RecalculaContador();
        }
	}

    public void UpdateSpecialInfluence(float ratio)
    {
        specialInfluence = ratio;
    }

    private Color ColorWithSpecial()
    {
        return new Color(NumberInInterval(color + specialInfluence), NumberInInterval(yo + specialInfluence), NumberInInterval(1 - color + specialInfluence));
    }

    private void RecalculaContador()
    {
        counter = Random.Range(minC, maxC);
    }

    public Color DevolveCor()
    {
        return new Color(color, yo, 1 - color);
    }

    private float CorDoMeio()
    {
        if (color >= 0.5f)
        {
            return 1 - Mathf.Sqrt(color-0.5f);
        }
        else
        {
            return 1 - Mathf.Sqrt(0.5f + (0.5f - color));
        }
    }

    private void ReadSpectrumData()
    {
        audioSource.GetComponent<AudioSource>().GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);

        sumX = sumY = sumX2 = sumXY = 0;

        for (int i = 0; i < qSamples / lossFactor - 1; i++)
        {
            sumX += i * lossFactor;
            sumY += ((Mathf.Log(spectrum[i * lossFactor]) + 10) * 10);
            sumX2 += (i * lossFactor) * (i * lossFactor);
            sumXY += (i * lossFactor) * ((Mathf.Log(spectrum[i * lossFactor]) + 10) * 10);

            sumX += (i * lossFactor) + 1;
            sumY += ((Mathf.Log(spectrum[(i * lossFactor) + 1]) + 10) * 10);
            sumX2 += ((i * lossFactor) + 1) * ((i * lossFactor) + 1);
            sumXY += ((i * lossFactor) + 1) * ((Mathf.Log(spectrum[(i * lossFactor) + 1]) + 10) * 10);
        }

        //Line adjustment
        a = ((qSamples * 0.5f) * sumXY - sumX * sumY) / ((qSamples * 0.5f) * sumX2 - sumX * sumX);
        b = (sumY - a * sumX) / (qSamples * 0.5f);

        //fim: Min= -0.25; Max= 1.5
        color = ((-b / (a * qSamples)) + 0.25f) / 1.75f;// *1.2f;

        // DETAILS AND OTHER ALTERNATIVES
        //color = (-b / (a * qSamples));  //Base
        //fim: Min= -0.27; Max= 1.42
        //color = ((-b / (a * qSamples)) + 0.3f) / 1.72f;// *1.2f;
        //Penguin: Min= 0; Max= 1.2
        //color = ((-b / (a * qSamples)) + 0f) / 1.2f;
    }
}
