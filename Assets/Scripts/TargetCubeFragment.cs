using UnityEngine;
using System.Collections;

public class TargetCubeFragment : MonoBehaviour 
{
    public int cubeColor;

    // Colors
    private float velMudaPreto = 1.2f;
    private int incrementaCor;
    private float cor;
    private float fatorIncremento; 
    private Color corRGB;
    private int corMax;

    // Explosion
    private int minRotacao = 50;
    private int maxRotacao = 120;
    private float tempo;
    private float tempoExplosao = 1.5f;
    private float tamCuboInicial = 0.5f;
    private bool destruido = false;

	void Start () 
    {
        fatorIncremento = Random.Range(0.01f, 0.04f);
        incrementaCor = 1;
        corMax = Random.Range(4, 7);
        cor = Random.Range(100, corMax * 100); // SE FOR QUADRADOS NORMAIS
        cor /= 1000;
	}

    public void Destroi(float tempoExp, float tamCubo)
    {
        tempoExplosao = tempoExp;
        tamCuboInicial = tamCubo;
        destruido = true;
        tempo = Time.time;
    }

    private void DestroyAnimation()
    {
        // CALCULANDO O QUANTO IRA REDUZIR OS QUADRADOS MENORES
        float reducao = (Time.time - tempo) / (tempoExplosao * 2);
        // CASO VA DIMINUIR MAIS DO QUE O TAMANHO QUE JA TEM, TRATAR (DARIA NEGATIVO, MAS NAO SE PODE DEIXAR DAR NEGATIVO)
        if (reducao >= tamCuboInicial)
            reducao = tamCuboInicial;
        // REDUZINDO OS QUADRADOS MENORES
        float scale = tamCuboInicial - reducao;
        transform.localScale = new Vector3(scale, scale, scale);
        transform.GetComponent<Rigidbody>().AddTorque(Random.Range(minRotacao, maxRotacao), Random.Range(minRotacao, maxRotacao), Random.Range(minRotacao, maxRotacao));
    }

    private void CubeAnimationColor()
    {
        if (cubeColor != 4) // not black
        {
            if ((cor >= (float)corMax / 10) || (cor <= 0.1))
            {
                incrementaCor *= -1;
            }
            cor += incrementaCor * fatorIncremento;

            switch (cubeColor)
            {
                case 1:
                    corRGB = new Color(cor, 1, 1);
                    break;
                case 2:
                    corRGB = new Color(1, cor, 1);
                    break;
                case 3:
                    corRGB = new Color(1, 1, cor);
                    break;
                case 5:
                    corRGB = new Color(1, cor, cor);
                    break;
                case 6:
                    corRGB = new Color(cor, cor, cor);
                    break;
                case 7:
                    corRGB = new Color(1, cor, 0.3f);
                    break;
            }
        }
        else // black cube
        {
            float corDecremento = Time.deltaTime * velMudaPreto;
            if (corRGB[0] > 0.1)
                corRGB[0] -= corDecremento;
            if (corRGB[1] > 0.1)
                corRGB[1] -= corDecremento;
            if (corRGB[2] > 0.1)
                corRGB[2] -= corDecremento;
            corRGB = new Color(corRGB[0], corRGB[1], corRGB[2]);
        }
    }

    private void PaintCube()
    {
        this.GetComponent<Renderer>().material.color = corRGB;
    }
	
	// Update is called once per frame
	void Update () 
    {
        CubeAnimationColor();
        PaintCube();
        if (destruido)
            DestroyAnimation();
	}
}
