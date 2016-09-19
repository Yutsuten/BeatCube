using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseManager : MonoBehaviour 
{
    private GameObject buttonContinue;
    private GameObject buttonRestart;
    private GameObject textGameOver;

    private AudioSource gameMusic;
    private ScoreManager scoreManager;
    private GameTime gameTime;
    private LifeManager lifeManager;

    private Vector2 touchPosition;
    private float pauseArea = 0.3f; // From upper screen, in that percentage, if the user clicks he/she activates the pause

    void Start()
    {
        // Getting button objects
        buttonContinue = GameObject.Find("UserInterface/ButtonContinue");
        buttonRestart = GameObject.Find("UserInterface/ButtonRestart");
        // Getting game over text
        textGameOver = GameObject.Find("UserInterface/TextGameOver");

        gameMusic = GameObject.Find("Sounds/Music").GetComponent<AudioSource>();
        scoreManager = GameObject.Find("GameManager").GetComponent<ScoreManager>();
        gameTime = GameObject.Find("UserInterface/Time").GetComponent<GameTime>();
        lifeManager = GameObject.Find("UserInterface").GetComponent<LifeManager>();

        // Hiding buttons by default
        ShowPauseButtons(false);

        // Hiding game over text by default
        textGameOver.SetActive(false);
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
        buttonContinue.GetComponent<Button>().interactable = true;
        buttonContinue.SetActive(state);
        buttonRestart.SetActive(state);
        if (state) // Pause
            gameMusic.Pause();
        else // Resume
            gameMusic.UnPause();
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        ShowPauseButtons(true);
        ProjectileButton.PauseGame(true);
    }

    private void DestroyGameObjects(GameObject[] clones)
    {
        for (int i = 0; i < clones.Length; i++)
            Destroy(clones[i]);
    }

    public void ButtonContinue_OnClick()
    {
        Time.timeScale = 1.0f;
        ShowPauseButtons(false);
        ProjectileButton.PauseGame(false);
    }

    public void ButtonRestart_OnClick()
    {
        // Destry all cubes
        DestroyGameObjects(GameObject.FindGameObjectsWithTag("BlueTarget"));
        DestroyGameObjects(GameObject.FindGameObjectsWithTag("YellowTarget"));
        DestroyGameObjects(GameObject.FindGameObjectsWithTag("RedTarget"));
        DestroyGameObjects(GameObject.FindGameObjectsWithTag("Item"));
        // Destroy all projectiles
        DestroyGameObjects(GameObject.FindGameObjectsWithTag("BlueSphere"));
        DestroyGameObjects(GameObject.FindGameObjectsWithTag("YellowSphere"));
        DestroyGameObjects(GameObject.FindGameObjectsWithTag("RedSphere"));

        scoreManager.ResetScore();
        gameTime.ResetTime();
        lifeManager.ResetLifes();
        gameMusic.Stop();
        gameMusic.Play();

        // Resume game
        ButtonContinue_OnClick();
    }
}
