using UnityEngine;
using System.Collections;

public class TargetCubeFragment : MonoBehaviour 
{
    public int cubeColor;

    private float velMudaPreto = 1.2f;
    private int incrementaCor, incrementaCor2; 
    private float Cor, CorItem1, CorItem2;
    private float fatorIncremento; 
    private Color corRGB;

    int corMax;

	// Use this for initialization
	void Start () 
    {
        fatorIncremento = Random.Range(0.01f, 0.04f);
        incrementaCor = 1;
        corMax = Random.Range(4, 7);
        Cor = Random.Range(100, corMax * 100); // SE FOR QUADRADOS NORMAIS
        Cor /= 1000;

        if (cubeColor == 5)
        {
            CorItem1 = 7;
            CorItem2 = 2.5f;
        }
        else if (cubeColor == 6)
        {
            CorItem1 = 2;
        }
        else if (cubeColor == 7)
        {
            CorItem1 = 0.7f;
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
        if ((Cor >= (float)corMax/10) || (Cor <= 0.1))
        {
            incrementaCor *= -1;
        }
        
        Cor += incrementaCor * fatorIncremento;

        if (ScriptEspecial.ESPECIAL_ATIVADO)
            corRGB = new Color(1, 1, 1);
        else if (cubeColor == 1) // AZUL
        {
            corRGB = new Color(Cor, 1, 1);
        }
        else if (cubeColor == 2) // VERMELHO
        {
            corRGB = new Color(1, Cor, 1);
        }
        else if (cubeColor == 3) // AMARELO
        {
            corRGB = new Color(1, 1, Cor);
        }

        else if (cubeColor == 4) // PRETO
        {
            var corDecremento = Time.deltaTime * velMudaPreto;
            if (corRGB[0] > 0.1)
                corRGB[0] -= corDecremento;
            if (corRGB[1] > 0.1)
                corRGB[1] -= corDecremento;
            if (corRGB[2] > 0.1)
                corRGB[2] -= corDecremento;
            corRGB = new Color(corRGB[0], corRGB[1], corRGB[2]);
        }

        else if (cubeColor == 5)
        {
            if (CorItem1 >= 10 || CorItem1 <= 5)
            {
                incrementaCor *= -1;
            }

            if (CorItem2 >= 3 || CorItem2 <= 2)
            {
                incrementaCor2 *= -1;
            }

            CorItem1 += fatorIncremento * incrementaCor;
            CorItem2 += fatorIncremento * incrementaCor2;

            corRGB = new Color(1, CorItem1, CorItem2);
        }
        else if (cubeColor == 6)
        {
            if (CorItem1 >= 6 || CorItem1 <= 0.8)
            {
                incrementaCor *= -1;
            }

            CorItem1 += fatorIncremento * incrementaCor;

            corRGB = new Color(CorItem1, CorItem1, CorItem1);
        }
        else if (cubeColor == 7)
        {
            if (CorItem1 >= 0.9 || CorItem1 <= 0.6)
            {
                incrementaCor *= -1;
            }

            CorItem1 += fatorIncremento/3 * incrementaCor;

            corRGB = new Color(5, CorItem1, 0.3f);
        }

        this.GetComponent<Renderer>().material.color = corRGB;
	}
}
