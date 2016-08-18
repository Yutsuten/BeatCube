using UnityEngine;
using System.Collections;

public class LifeManager : MonoBehaviour 
{
    int vidaAtual;
	// Use this for initialization
	void Start () 
    {
        vidaAtual = 10;
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
