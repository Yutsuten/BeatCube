using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMananger : MonoBehaviour {

    public GameObject Alvo;
    public GameObject Item;
    
    
    bool gameOn;
    bool Pause;
    public List<GameObject> ListaDeObjetos = new List<GameObject>();

    float tempoInstanciado;
    float tempoInstanciadoAnterior;

	// Use this for initialization
	void Start () 
    {
        gameOn = true;
        Pause = false;

        tempoInstanciadoAnterior = Time.time;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape))
        {
            Pause = !Pause;

            if (Pause)
            {
                PausaJogo();
            }
            else
            {
                ResumeJogo();
            }
        }
        CriaObjeto(Random.RandomRange(0.4f, 0.8f));
	}

    void OnGUI()
    {
        if (Pause)
        {
            GUI.Box(new Rect(0,0, Screen.width, Screen.height), "");
            GUI.Box(new Rect(Screen.width/4f, 10, Screen.width/2, Screen.height/1.3f), "Jogo Pausado");
            if (GUI.Button(new Rect(Screen.width/4f, 50, Screen.width/2f, 30), "Voltar"))
            {
                Pause = false;
            }
            if (GUI.Button(new Rect(Screen.width / 4f, 110, Screen.width / 2f, 30), "Reiniciar"))
            {
            }
            if (GUI.Button(new Rect(Screen.width / 4f, 170, Screen.width / 2f, 30), "Menu"))
            {
            }
        }
    }

    public void CriaObjeto(float intensidade)
    {
        if(gameOn)
        {
            tempoInstanciado = Time.time;

            if (tempoInstanciado - tempoInstanciadoAnterior > 1.5f)
            {
                if (Random.Range(0, 100) < 80)
                    CriaCubo(intensidade);
                else
                    CriaItem(intensidade);

                tempoInstanciadoAnterior = tempoInstanciado;
            }
        }
    }

    public void CriaCubo(float intensidade)
    {
        int corEscolhida = Random.Range(0, 100);

        GameObject obj = Instantiate(Alvo, new Vector3(Random.Range(-3.4f, 3.4f), 6.2f, -1.505835f), Quaternion.identity) as GameObject;
        ListaDeObjetos.Add(obj);
        
        if (corEscolhida < 33)
        {
            for (int i = 0; i < obj.transform.childCount; i += 1)
            {
                obj.transform.GetChild(i).GetComponent<ScriptCubo>().cubeColor = 1;
            }
            
            obj.transform.tag = "Target Azul";
        }
        else if (corEscolhida < 66)
        {
            for (var i2 = 0; i2 < obj.transform.childCount; i2 += 1)
            {
                obj.transform.GetChild(i2).GetComponent<ScriptCubo>().cubeColor = 2;
            }
            
            obj.transform.tag = "Target Vermelho";
        }
        else
        {
            for (var i3 = 0; i3 < obj.transform.childCount; i3 += 1)
            {
                obj.transform.GetChild(i3).GetComponent<ScriptCubo>().cubeColor = 3;
            }

            obj.transform.tag = "Target Amarelo";
        }

        obj.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-IntensidadeForca(0, 200, intensidade), IntensidadeForca(0, 200, intensidade)), -IntensidadeForca(100, 200, intensidade), 0));
    }

    private float IntensidadeForca(float min, float max, float intensidade)
    {
        float resultado;

        if (intensidade <= 3)
        {
            resultado = (intensidade * max) + min;
        }
        else
        {
            intensidade = 3;
            min = Random.Range(-min, min);

            resultado = (intensidade * max) + min;
        }

        return resultado;
    }

    public void CriaItem(float intensidade)
    {
        int corEscolhida = Random.Range(0, 300);

        GameObject obj = Instantiate(Item, new Vector3(Random.Range(-3.4f, 3.4f), 6.2f, -1.505835f), Quaternion.identity) as GameObject;
        ListaDeObjetos.Add(obj);

        if (corEscolhida < 100)
        {
            for (int i = 0; i < obj.transform.childCount; i += 1)
            {
                obj.transform.GetChild(i).GetComponent<ScriptCubo>().cubeColor = 5;
            }

            obj.transform.tag = "Item";
        }
        else if (corEscolhida < 200)
        {
            for (var i2 = 0; i2 < obj.transform.childCount; i2 += 1)
            {
                obj.transform.GetChild(i2).GetComponent<ScriptCubo>().cubeColor = 6;
            }

            obj.transform.tag = "Item";
        }
        else
        {
            for (var i3 = 0; i3 < obj.transform.childCount; i3 += 1)
            {
                obj.transform.GetChild(i3).GetComponent<ScriptCubo>().cubeColor = 7;
            }

            obj.transform.tag = "Item";
        }

        obj.GetComponent<Rigidbody>().AddForce(new Vector3(IntensidadeForca(0, 200, intensidade), -IntensidadeForca(100, 200, intensidade), 0));
    }

    public void AddObjLista(GameObject obj)
    {
        ListaDeObjetos.Add(obj);
    }

    public void RetiraObjLista(GameObject obj)
    {
        ListaDeObjetos.Remove(obj);
    }

    public void FimDeJogo()
    {
        for (int i = 0; i < ListaDeObjetos.Count; i++)
        {
            //ListaDeObjetos[i].SendMessage("ParalisaObj");
        }

        gameOn = false;
        GameObject.Find("BotaoAmarelo").GetComponent<Botao>().PausaJogo();
        GameObject.Find("BotaoAzul").GetComponent<Botao>().PausaJogo();
        GameObject.Find("BotaoVermelho").GetComponent<Botao>().PausaJogo();
    }

    public void ItemDestroiObj()
    {
        for (int i = 0; i < ListaDeObjetos.Count; i++)
        {
            //ListaDeObjetos[i].SendMessage("ChamaExplosao");
        }
    }

    private void PausaJogo()
    {
        for (int i = 0; i < ListaDeObjetos.Count; i++)
        {
            //ListaDeObjetos[i].SendMessage("ParalisaObj");
        }

        gameOn = false;

        GameObject.Find("BotaoAmarelo").GetComponent<Botao>().PausaJogo();
        GameObject.Find("BotaoAzul").GetComponent<Botao>().PausaJogo();
        GameObject.Find("BotaoVermelho").GetComponent<Botao>().PausaJogo();
        
        GameObject.Find("Acordes").GetComponent<DificuldadeMusica>().GamePause();
    }

    private void ResumeJogo()
    {
        gameOn = true;

        GameObject.Find("BotaoAmarelo").GetComponent<Botao>().VoltaAoJogo();
        GameObject.Find("BotaoAzul").GetComponent<Botao>().VoltaAoJogo();
        GameObject.Find("BotaoVermelho").GetComponent<Botao>().VoltaAoJogo();

        GameObject.Find("Acordes").GetComponent<DificuldadeMusica>().GameON();
    }

    private void ReiniciaJogo()
    {

    }

    private void RetornaMenuPrincipal()
    {

    }
}
