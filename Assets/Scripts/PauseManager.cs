using UnityEngine;
using System.Collections;

public class PauseManager : MonoBehaviour 
{
    private GameObject buttonContinue;
    private GameObject buttonRestart;
    private GameObject buttonQuit;

    private Vector2 touchPosition;
    private float pauseArea = 0.3f; // From upper screen, in that percentage, if the user clicks he/she activates the pause

    void Start()
    {
        // Getting button objects
        buttonContinue = GameObject.Find("UserInterface/ButtonContinue");
        buttonRestart = GameObject.Find("UserInterface/ButtonRestart");
        buttonQuit = GameObject.Find("UserInterface/ButtonQuit");

        // Hiding buttons by default
        ShowButtons(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left Click
        {
            touchPosition = Input.mousePosition;
            if (Input.mousePosition.y / Screen.height > 1 - pauseArea)
            {
                Time.timeScale = 0;
                ShowButtons(true);
            }
        }
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
