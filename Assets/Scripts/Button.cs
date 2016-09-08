using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour 
{
    public GameObject shpere;
    public Transform spawnPoint;

    float tempoInicial, tempoFinal;
    float angulo;
    
    Vector2 inicioToque, fimToque, diferenca;

    bool gameOn = true;

    int buttonColor;

    void Start()
    {
        if (gameObject.name == "BlueButton")
        {
            buttonColor = 1;
        }
        else if(gameObject.name == "RedButton")
        {
            buttonColor = 2;
        }
        else if (gameObject.name == "YellowButton")
        {
            buttonColor = 3;
        }
    }

    void OnMouseDown()
    {
        tempoInicial = Time.time;
        inicioToque = Input.mousePosition;
    }

    void OnMouseUp()
    {
        if (gameOn)
        {
            tempoFinal = Time.time;

            fimToque = Input.mousePosition;

            diferenca = fimToque - inicioToque;
            if (diferenca.y != 0 || diferenca.x != 0)
            {
                if (diferenca.x == 0)
                {
                    angulo = -90;
                }
                else
                {
                    angulo = Mathf.Atan(diferenca.y / diferenca.x);
                    angulo = (180 * angulo) / Mathf.PI;

                    if (angulo < 20 && angulo >= 0)
                    {
                        angulo = 20;
                    }
                    else if (angulo > -20 && angulo <= 0)
                    {
                        angulo = -20;
                    }
                }

                float distancia = Mathf.Sqrt(Mathf.Pow(diferenca.x, 2) + Mathf.Pow(diferenca.y, 2));
                float tempo = tempoFinal - tempoInicial;

                if (distancia > 1500)
                {
                    distancia = 1500;
                }
                if (tempo < 0.08)
                {
                    tempo = 0.08f;
                }

                int forca = (int)(distancia / (tempo + 0.3f));

                if (forca < 600)
                {
                    forca = 600;
                }
                else if (forca > 2100)
                {
                    forca = 2100;
                }

                // Criacao da bola
                // Se angulo for positivo
                if (angulo >= 0)
                {
                    spawnPoint.rotation = Quaternion.AngleAxis(angulo, Vector3.forward);
                }
                // Se angulo for negativo
                else if (angulo <= 0)
                {
                    spawnPoint.rotation = Quaternion.AngleAxis(180 + angulo, Vector3.forward);
                }

                // Instanciar a bola
                GameObject bola = Instantiate(shpere, spawnPoint.transform.position, Quaternion.identity) as GameObject;
                bola.GetComponent<Projectile>().DefineCor(buttonColor);
                GameObject.Find("GM").GetComponent<GameMananger>().AddObjectToList(bola);

                bola.GetComponent<Rigidbody>().AddForce(spawnPoint.right * forca);
            }
        }
    }

    public void PausaJogo()
    {
        gameOn = false;
    }

    public void VoltaAoJogo()
    {
        gameOn = true;
    }
}
