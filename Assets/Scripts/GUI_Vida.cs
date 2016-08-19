using UnityEngine;
using System.Collections;

public class GUI_Vida : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {	
	}
	
	// Update is called once per frame
	void Update () 
    {
        GetComponent<GUIText>().color = GameObject.Find("Fundo2").GetComponent<BackgroundAnimation>().DevolveCor();

        if (transform.position.y <= 1.5)
        {
            transform.Translate(new Vector3(0, 0.2f * Time.deltaTime, 0));
        }
        else
        {
            Destroy(gameObject);
        }
	}
}
