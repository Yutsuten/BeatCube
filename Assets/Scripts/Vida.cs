using UnityEngine;
using System.Collections;

public class Vida : MonoBehaviour {


    public GameObject GUIVida;

    int vidas = 10;
	// Use this for initialization
	void Start () 
    {	
	}
	
	// Update is called once per frame
	void Update () 
    {	
	}

    public int Vidas()
    {
        return vidas;
    }

    public void DiminuiVida()
    {
        if (vidas >= 1)
        {
            vidas--;
            print(vidas);

            GameObject.Find("Vidas").GetComponent<LifeManager>().QuantidadeVida(vidas);
        }
        else
        {
            GetComponent<GameMananger>().CancelInvoke();
            GetComponent<GameMananger>().FimDeJogo();
        }
    }

    public void AumentaVida()
    {
        vidas++;
        Instantiate(GUIVida);
        GameObject.Find("Vidas").GetComponent<LifeManager>().QuantidadeVida(vidas);
    }
}
