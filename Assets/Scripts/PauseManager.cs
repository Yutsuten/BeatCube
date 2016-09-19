using UnityEngine;
using System.Collections;

public class PauseManager : MonoBehaviour 
{
    private GUIStyle styleAbout = new GUIStyle();
    private bool onPause;
    private string textoAbout = "LTIA";
    private string lbAbout;

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
