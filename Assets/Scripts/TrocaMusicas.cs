using UnityEngine;
using System.Collections;

public class TrocaMusicas : MonoBehaviour 
{
    public AudioClip audioMenu;
    public AudioClip audioPlay;
    //public AudioClip audioMenu;

	// Use this for initialization
	void Start () 
    {
        TrocaAudio(2);
	}

    public void TrocaAudio(int id)
    {
        if (id == 1)
        {
            GetComponent<AudioSource>().clip = audioMenu;
            GetComponent<AudioSource>().Play();
        }
        else if (id == 2)
        {
            GetComponent<AudioSource>().clip = audioPlay;
            GetComponent<AudioSource>().Play();
        }
        else if (id == 3)
        {
            GetComponent<AudioSource>().clip = audioPlay;
            GetComponent<AudioSource>().pitch = 0.8f;
            GetComponent<AudioSource>().Play();
        }
    }
}
