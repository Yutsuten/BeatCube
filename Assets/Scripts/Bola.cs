using UnityEngine;
using System.Collections;

public class Bola : MonoBehaviour 
{
    bool especial = false;
    int Cor;

    public void DefineCor(int cor)
    {
        Cor = cor;
        if (cor == 1)
        {
            GetComponent<Renderer>().material.color = new Color(0.5f, 3, 5);
        }
        else if(cor == 2)
        {
            GetComponent<Renderer>().material.color = new Color(5, 0.5f, 3);
        }
        else if(cor == 3)
        {
            GetComponent<Renderer>().material.color = new Color(5, 4, 0.5f);
        }
    }

    void Update()
    {
        if (!ScriptEspecial.ESPECIAL_ATIVADO)
        {
            if (Cor == 1)
            {
                GetComponent<Renderer>().material.color = new Color(GameObject.Find("Fundo2").GetComponent<ScriptMusica>().DevolveCor().g * 1.3f, 3, 5);
            }
            else if (Cor == 2)
            {
                GetComponent<Renderer>().material.color = new Color(5, GameObject.Find("Fundo2").GetComponent<ScriptMusica>().DevolveCor().g * 1.3f, 3);
            }
            else if (Cor == 3)
            {
                GetComponent<Renderer>().material.color = new Color(5, 4, GameObject.Find("Fundo2").GetComponent<ScriptMusica>().DevolveCor().g * 1.3f);
            }
        }
    }

    void OnTriggerEnter(Collider Col)
    {
        //print("ta entrando " + Col.gameObject.name);
        if (Col.gameObject.tag.Equals("Parede"))
        {
            Vector3 vetorAux = transform.GetComponent<Rigidbody>().velocity;
            vetorAux.x *= -1;
            transform.GetComponent<Rigidbody>().velocity = vetorAux;
        }
        else if (Col.gameObject.tag.Equals("Botao Jogar"))
        {
            if (gameObject.tag.Equals("Bola Azul"))
            {
                Destroy(GameObject.Find("BtMenu"));
                //GameObject.Find("GM").GetComponent<GameMananger>().CriaCubo();
                GameObject.Find("Fundo2").GetComponent<ScriptMusica>().enabled = true;
                GameObject.Find("Fundo2").GetComponent<ScriptMusica>().Invoke("RecalculaContador", 2f);
                GameObject.Find("Acordes").GetComponent<DificuldadeMusica>().enabled = true;
                GameObject.Find("Acordes").GetComponent<TrocaMusicas>().TrocaAudio(2);

                for (int i = 0; i < GameObject.Find("PainelCompleto").transform.childCount; i++)
                {
                    GameObject.Find("PainelCompleto").transform.GetChild(i).gameObject.SetActive(true);
                }
                GameObject.Find("Acordes").GetComponent<DificuldadeMusica>().GameON();
            }
            Destroy(gameObject);
        }
        else if (Col.gameObject.tag.Equals("Botao Sobre"))
        {
            if (gameObject.tag.Equals("Bola Amarela"))
            {
                Destroy(GameObject.Find("BtMenu"));
                GameObject.Find("Acordes").GetComponent<TrocaMusicas>().TrocaAudio(3);
            }
            Destroy(gameObject);
        }
        else if (Col.gameObject.tag.Equals("Botao Sair"))
        {
            if (gameObject.tag.Equals("Bola Vermelha"))
            {
                Destroy(GameObject.Find("BtMenu"));
            }
            Destroy(gameObject);
        }
        else if (Col.gameObject.tag.Equals("Teto"))
        {
            Destroy(gameObject);
            //GameObject.Find("GM").GetComponent<Pontuacao>().ResetaCombo();
        }
    }

    public void AtivadoEspecial()
    {
        especial = true;
    }

    public void DesativadoEspecial()
    {
        especial = false;
    }

    public bool Especial()
    {
        return especial;
    }

    public void ParalisaObj()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
    }

    void OnDestroy()
    {
        GameObject.Find("GM").GetComponent<GameMananger>().RetiraObjLista(this.gameObject);
    }
}
