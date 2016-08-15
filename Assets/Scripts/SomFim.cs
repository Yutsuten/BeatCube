using UnityEngine;
using System.Collections;

public class SomFim : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {	
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(!GetComponent<AudioSource>().isPlaying)
        {
            Destroy(gameObject);
        }
	}
}
