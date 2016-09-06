using UnityEngine;
using System.Collections;

public class TargetColor : MonoBehaviour
{
    // Main color
    protected int targetColor;

    // Auxiliaries
    private float velMudaPreto = 1.2f;
    private int incrementaCor;
    private float cor;
    private float fatorIncremento;
    private Color corRGB;
    private int corMax;

    void Start()
    {
        fatorIncremento = Random.Range(0.01f, 0.04f);
        incrementaCor = 1;
        corMax = Random.Range(4, 7);
        cor = Random.Range(100, corMax * 100); // SE FOR QUADRADOS NORMAIS
        cor /= 1000;
    }

    protected void UpdateChildColors()
    {
        CubeAnimationColor();
        for (int childIndex = 0; childIndex < transform.childCount; childIndex++)
            transform.GetChild(childIndex).GetComponent<TargetCubeFragment>().PaintCube(corRGB);
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
