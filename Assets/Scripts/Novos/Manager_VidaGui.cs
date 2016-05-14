using UnityEngine;
using System.Collections;

public class Manager_VidaGui : MonoBehaviour 
{
    int vidaAtual;
	// Use this for initialization
	void Start () 
    {
        /*for (int i = 0; i < transform.childCount; i++)
        {
            print(transform.GetChild(i));
        }*/
        vidaAtual = 10;
	}
	
	// Update is called once per frame
	void Update () 
    {

	}

    public void QuantidadeVida(int qntd)
    {
        if(qntd <= 10 && qntd >= 0)
        {
            if (qntd < vidaAtual)
            {
                transform.GetChild(qntd).GetComponent<GUI_Vidas>().Desativa();
            }
            else
            {
                transform.GetChild(qntd - 1).GetComponent<GUI_Vidas>().Ativa();
            }

            vidaAtual = qntd;
        }
    }
}
