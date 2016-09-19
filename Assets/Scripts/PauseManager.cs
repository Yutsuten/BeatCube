using UnityEngine;
using System.Collections;

public class PauseManager : MonoBehaviour 
{
    private GameObject buttonContinue;
    private GameObject buttonRestart;
    private GameObject buttonQuit;

    void Start()
    {
        // Getting button objects
        buttonContinue = GameObject.Find("UserInterface/ButtonContinue");
        buttonRestart = GameObject.Find("UserInterface/ButtonRestart");
        buttonQuit = GameObject.Find("UserInterface/ButtonQuit");

        // Hiding buttons by default
        ShowButtons(false);
    }

    private void ShowButtons(bool state)
    {
        buttonContinue.SetActive(state);
        buttonRestart.SetActive(state);
        buttonQuit.SetActive(state);
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
