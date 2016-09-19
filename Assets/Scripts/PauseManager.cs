using UnityEngine;
using System.Collections;

public class PauseManager : MonoBehaviour 
{
    private GameObject buttonContinue;
    private GameObject buttonRestart;
    private GameObject buttonQuit;
    private AudioSource gameMusic;

    private Vector2 touchPosition;
    private float pauseArea = 0.3f; // From upper screen, in that percentage, if the user clicks he/she activates the pause

    void Start()
    {
        // Getting button objects
        buttonContinue = GameObject.Find("UserInterface/ButtonContinue");
        buttonRestart = GameObject.Find("UserInterface/ButtonRestart");
        buttonQuit = GameObject.Find("UserInterface/ButtonQuit");

        gameMusic = GameObject.Find("Sounds/Music").GetComponent<AudioSource>();

        // Hiding buttons by default
        ShowPauseButtons(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || (Input.GetMouseButtonDown(0) && Input.mousePosition.y / Screen.height > 1 - pauseArea)) // Left Click on upper screen, or esc
            PauseGame();
    }

    void OnApplicationPause(bool pause)
    {
        if (pause)
            PauseGame();
    }

    private void ShowPauseButtons(bool state)
    {
        buttonContinue.SetActive(state);
        buttonRestart.SetActive(state);
        buttonQuit.SetActive(state);
        if (state) // Pause
            gameMusic.Pause();
        else // Resume
            gameMusic.Play();
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        ShowPauseButtons(true);
        Button.PauseGame(true);
    }

    public void ButtonContinue_OnClick()
    {
        Time.timeScale = 1.0f;
        ShowPauseButtons(false);
        Button.PauseGame(false);
    }

    public void ButtonRestart_OnClick()
    {
        Debug.Log("ButtonRestart_OnClick");
    }

    public void ButtonQuit_OnClick()
    {
        Application.Quit();
    }
}
