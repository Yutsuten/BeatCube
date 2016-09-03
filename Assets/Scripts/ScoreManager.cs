using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour 
{
    private int pointIncrement = 5;

    int score = 0;

    ScoreGUI scoreGUI;

    void Start()
    {
        scoreGUI = GameObject.Find("Panel/Score").GetComponent<ScoreGUI>();
    }

    public void GetPoints()
    {
        score += pointIncrement;
        scoreGUI.AtualizaPontos(score);

        GetComponent<SpecialManager>().RecalculaFundo();
    }

    public void ResetCombo()
    {
        GetComponent<SpecialManager>().ResetaEspecial();
    }
}
