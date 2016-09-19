using UnityEngine;
using System.Collections;

public class PauseManager : MonoBehaviour 
{
    private GameObject buttonContinue;
    private GameObject buttonRestart;
    private GameObject buttonQuit;

    void Start()
    {
        buttonContinue = GameObject.Find("UserInterface/ButtonContinue");
        buttonRestart = GameObject.Find("UserInterface/ButtonRestart");
        buttonQuit = GameObject.Find("UserInterface/ButtonQuit");
    }

    public void ButtonContinue_OnClick()
    {
        Debug.Log("ButtonContinue_OnClick");
    }

    public void ButtonRestart_OnClick()
    {
        Debug.Log("ButtonRestart_OnClick");
    }

    public void ButtonQuit_OnClick()
    {
        Debug.Log("ButtonQuit_OnClick");
    }
}
