using UnityEngine;
using System.Collections;

public class CubeBehaviour : MonoBehaviour
{
    // Audio
    protected AudioSource audioExplosao;

    // Cube movement
    protected float minRotation = 50.0f;
    protected float maxRotation = 150.0f;
    private float velocidadeRotacaoX;
    private float velocidadeRotacaoY;
    private float velocidadeRotacaoZ;
    protected float tExplosao = 1.2f;

    // Main color
    protected int targetColor;

    // Auxiliaries
    private float velMudaPreto = 1.2f;
    private int incrementaCor;
    private float cor;
    private float fatorIncremento;
    private Color corRGB;
    private Color white;
    private int corMax;
    private int biggerCube = 0;

    protected void Start()
    {
        // Color
        white = new Color(1f, 1f, 1f);

        fatorIncremento = Random.Range(0.01f, 0.04f);
        incrementaCor = 1;
        corMax = Random.Range(4, 7);
        cor = Random.Range(100, corMax * 100); // SE FOR QUADRADOS NORMAIS
        cor /= 1000;

        // Rotation
        velocidadeRotacaoX = Random.Range(minRotation, maxRotation);
        velocidadeRotacaoY = Random.Range(minRotation, maxRotation);
        velocidadeRotacaoZ = Random.Range(minRotation, maxRotation);
    }

    protected void Update() 
    {
        transform.Rotate(velocidadeRotacaoX * Time.deltaTime, velocidadeRotacaoY * Time.deltaTime, velocidadeRotacaoZ * Time.deltaTime);
    }

    protected void ExplodeSmallCube()
    {
        Vector3 centro2;
        Vector3[] coordFora2 = new Vector3[transform.childCount];
        centro2 = transform.position; // Obtendo a posicao do centro
        for (int j = 0; j < transform.childCount; j++)
        { // Obtendo a posicao dos outros quadrados
            coordFora2[j] = transform.GetChild(j).position;
            transform.GetChild(j).GetComponent<Rigidbody>().isKinematic = false;
            transform.GetChild(j).GetComponent<Rigidbody>().AddForce((coordFora2[j] - centro2) * Random.Range(150, 200) * 2);
            transform.GetChild(j).GetComponent<Rigidbody>().constraints = 0; // Eliminando o "travamento" da posicao em z
            transform.GetChild(j).GetComponent<Rigidbody>().AddTorque(Random.Range(minRotation, maxRotation), Random.Range(minRotation, maxRotation), Random.Range(minRotation, maxRotation));
            transform.GetChild(j).GetComponent<TargetCubeFragment>().Destroi(tExplosao * 2, transform.GetChild(j).localScale.x);
            Destroy(transform.GetChild(j).gameObject, tExplosao);
        }
        transform.DetachChildren();
        audioExplosao.Play();
        Destroy(transform.gameObject);// destruindo o que foi acertado
    }

    protected void UpdateChildColors()
    {
        CubeAnimationColor();
        for (int childIndex = 0; childIndex < transform.childCount - biggerCube; childIndex++)
        {
            if (SpecialManager.SpecialActivated)
                transform.GetChild(childIndex).GetComponent<TargetCubeFragment>().PaintCube(white);
            else
                transform.GetChild(childIndex).GetComponent<TargetCubeFragment>().PaintCube(corRGB);
        }
    }

    public int Color
    {
        get
        {
            return targetColor;
        }
        set
        {
            targetColor = value;
        }
    }

    protected bool BiggerCube
    {
        set
        {
            biggerCube = value ? 1 : 0;
        }
    }

    private void CubeAnimationColor()
    {
        if (targetColor != 4) // not black
        {
            if ((cor >= (float)corMax / 10) || (cor <= 0.1))
            {
                incrementaCor *= -1;
            }
            cor += incrementaCor * fatorIncremento;

            switch (targetColor)
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
}
