using UnityEngine;
using System.Collections;

public class Botao : MonoBehaviour 
{
    public GameObject Bola;
    public Transform referencia;
    
    GameObject bola;

    float tempoInicial, tempoFinal;
    float angulo;
    
    Vector2 inicioToque, fimToque, diferenca;

    bool congelado = false;
    bool foiCongelado = false;

    bool gameOn = true;

    int contador = 0;
    int Id;

    Color[] CorCongelada = { new Color(0.537f, 0.906f, 0.906f, 0.039f), new Color(0.565f, 0.796f, 0.765f, 0.706f), new Color(0.788f, 0.827f, 1f, 1f), new Color(0.827f, 0.906f, 0.965f, 0.706f), new Color(1f, 1f, 1f, 0.039f) };
    Color[] CorPropria;


    void Start()
    {
        CorPropria = GetComponent<ParticleAnimator>().colorAnimation;
        if (gameObject.name == "BotaoAzul")
        {
            Id = 1;
        }
        else if(gameObject.name == "BotaoVermelho")
        {
            Id = 2;
        }
        else if (gameObject.name == "BotaoAmarelo")
        {
            Id = 3;
        }
    }

    void OnMouseDown()
    {
        if (!congelado)
        {
            tempoInicial = Time.time;

            inicioToque = Input.mousePosition;
        }

        /*for (int i = 0; i < 5; i++ )
        {
            print(GetComponent<ParticleAnimator>().colorAnimation[i]);
        }*/
    }

    void OnMouseUp()
    {
        if (gameOn)
        {
            if (!congelado)
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

                    /*if (distancia < 40)
                    {
                        inicioToque = new Vector2(Screen.width, Screen.height);
                        return;
                    }*/
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
                        referencia.rotation = Quaternion.AngleAxis(angulo, Vector3.forward);
                    }
                    // Se angulo for negativo
                    else if (angulo <= 0)
                    {
                        referencia.rotation = Quaternion.AngleAxis(180 + angulo, Vector3.forward);
                    }

                    // Instanciar a bola
                    bola = Instantiate(Bola, transform.position, Quaternion.identity) as GameObject;
                    bola.GetComponent<Bola>().DefineCor(Id);
                    GameObject.Find("GM").GetComponent<GameMananger>().AddObjectToList(bola);

                    if (!foiCongelado)
                    {
                        bola.GetComponent<Bola>().AtivadoEspecial();
                    }


                    bola.GetComponent<Rigidbody>().AddForce(referencia.right * forca);
                }
            }
        }
    }

    public void CongelaBotao()
    {
        contador++;

        congelado = true;
        
        Invoke("DescongelaBotao", 3f);

        GetComponent<ParticleAnimator>().colorAnimation = CorCongelada;

        if (ScriptEspecial.ESPECIAL_ATIVADO)
        {
            GameObject.Find("GM").GetComponent<ScriptEspecial>().MudaCorBola(gameObject.name);
        }
    }

    void DescongelaBotao()
    {
        contador--;

        if (contador == 0)
        {
            congelado = false;

            GetComponent<ParticleAnimator>().colorAnimation = CorPropria;

            foiCongelado = true;
        }
    }

    public void ContCongela()
    {
        foiCongelado = false;
        //print("Ta funfando");
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
