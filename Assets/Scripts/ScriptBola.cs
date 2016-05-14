using UnityEngine;
using System.Collections;

public class ScriptBola : MonoBehaviour 
{
    void OnTriggerEnter(Collider Col)
    {
	    if (Col.gameObject.tag.Equals("Parede"))
        {
            Vector3 vetorAux = transform.GetComponent<Rigidbody>().velocity;
            vetorAux.x *= -1;
            transform.GetComponent<Rigidbody>().velocity = vetorAux;
        }
		    
	    else if (Col.gameObject.tag.Equals("Botao Jogar") && gameObject.tag.Equals("Esfera Azul"))
	    {
		    GameObject.Find("Scripts").GetComponent<ScriptMenu>().PlaySelected(Col);
		    Destroy(gameObject);
	    }
	    else if (Col.gameObject.tag.Equals("Botao Sobre") && gameObject.tag.Equals("Esfera Verde"))
	    {
		    GameObject.Find("Scripts").GetComponent<ScriptMenu>().AboutSelected(Col);
		    Destroy(gameObject);
	    }
	    else if (Col.gameObject.tag.Equals("Botao Sair") && gameObject.tag.Equals("Esfera Vermelha"))
	    {
		    GameObject.Find("Scripts").GetComponent<ScriptMenu>().ExitSelected(Col);
		    Destroy(gameObject);
	    }
	    else if (Col.gameObject.tag.Equals("Teto"))
	    {
		    Destroy(gameObject);
	    }
    }

	// Update is called once per frame
	void Update () 
    {
        if (ScriptAlvo.DESTROY_ALL_TARGETS)
            Destroy(gameObject);
	}
}
