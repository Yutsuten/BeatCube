using UnityEngine;
using System.Collections;

public class ScriptMusica : MonoBehaviour
{
    public AudioSource audioSource;
#if UNITY_EDITOR
    public bool on = true;
#endif

    float[] spectrum;
    int qSamples = 1024;
    float color;
    float a, b;
    float sumX, sumY, sumX2, sumXY;

    float min = float.MaxValue, max = float.MinValue;
    int lossFactor = 4;
    float yo;

    float contador;
    public float minC, maxC;

	void Start()
    {
        minC = 0.8f; 
        maxC = 1.5f;
        spectrum = new float[qSamples];
        contador = Random.Range(minC,maxC);
	}
	
	void Update()
    {
        Method();

        ParticleAnimator particleAnimator = GetComponent<ParticleAnimator>();
        Color[] cores = particleAnimator.colorAnimation;
        yo = CorDoMeio();
        
         
        //print("color: " + color);
        
        
        cores[0] = new Color(color, yo, 1-color);
        cores[1] = new Color(color, yo, 1-color);
        cores[2] = new Color(color, yo, 1-color);
        cores[3] = new Color(color, yo, 1-color);
        cores[4] = new Color(color, yo, 1-color);
        particleAnimator.colorAnimation = cores;

        //print(contador);
        contador -= color * Time.deltaTime;
        
        if (contador <= 0)
        {
            RecalculaContador();
        }
	}

    public void RecalculaContador()
    {
        //GameObject.Find("GM").GetComponent<GameMananger>().CriaObjeto(color);
        //print(color);

        contador = Random.Range(minC, maxC);
    }

    /*public void RecalculaContador(float c)
    {
        GameObject.Find("GM").GetComponent<GameMananger>().CriaObjeto(c);

        contador = Random.Range(minC, maxC);
    }*/

    public Color DevolveCor()
    {
        return new Color(color, yo, 1 - color);
    }

    float CorDoMeio()
    {
        //return 1 - Mathf.Abs(color - 1);
        if (color >= 0.5f)
        {
            return 1 - Mathf.Sqrt(color-0.5f);
        }
        else
        {
            return 1 - Mathf.Sqrt(0.5f + (0.5f - color));
        }
    }

    void Method()
    {
        audioSource.GetComponent<AudioSource>().GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);

        sumX = sumY = sumX2 = sumXY = 0;

        for (int i = 0; i < qSamples / lossFactor - 1; i++)
        {
            sumX += i * lossFactor;// / 2;
            sumY += ((Mathf.Log(spectrum[i * lossFactor]) + 10) * 10);
            sumX2 += (i * lossFactor /*/ 2*/) * (i * lossFactor /*/ 2*/);
            sumXY += (i * lossFactor /*/ 2*/) * ((Mathf.Log(spectrum[i * lossFactor]) + 10) * 10);

            sumX += (i * lossFactor) + 1;// / 2;
            sumY += ((Mathf.Log(spectrum[(i * lossFactor) + 1]) + 10) * 10);
            sumX2 += ((i * lossFactor) + 1 /*/ 2*/) * ((i * lossFactor) + 1 /*/ 2*/);
            sumXY += ((i * lossFactor) + 1 /*/ 2*/) * ((Mathf.Log(spectrum[(i * lossFactor) + 1]) + 10) * 10);
        }

        //Line adjustment
        a = ((qSamples * 0.5f) * sumXY - sumX * sumY) / ((qSamples * 0.5f) * sumX2 - sumX * sumX);
        b = (sumY - a * sumX) / (qSamples * 0.5f);

#if UNITY_EDITOR
        if (on)
        {
            //color = (-b / (a * qSamples));  //Base
            
            //fim: Min= -0.27; Max= 1.42
            //color = ((-b / (a * qSamples)) + 0.3f) / 1.72f;// *1.2f;

            //Penguin: Min= 0; Max= 1.2
            //color = ((-b / (a * qSamples)) + 0f) / 1.2f;

            //fim: Min= -0.25; Max= 1.5
            color = ((-b / (a * qSamples)) + 0.25f) / 1.75f;// *1.2f;
        }
        else
            color = 0;
#elif UNITY_WP8
        color = ((-b / (a * qSamples)) + 0.3f) / 1.72f;// *1.2f;
#endif
    }
}
