using UnityEngine;
using System.Collections;

public class LifeManager : MonoBehaviour
{
    private PauseManager pauseManager;
    private int lifes = 10;

    void Start()
    {
        pauseManager = GameObject.Find("GameManager").GetComponent<PauseManager>();
    }

    private void UpdateGUI()
    {
        if (lifes >= 1) // Will only have an activated light if has at least a life
            transform.GetChild(lifes).GetComponent<LifeGUI>().Ativa();
        if (lifes <= 9) // Won't disable a light if life is complete
            transform.GetChild(lifes + 1).GetComponent<LifeGUI>().Desativa();
    }

    public void DiminuiVida()
    {
        lifes--;
        if (lifes < 0)
            lifes = 0;

        UpdateGUI();

        if (lifes == 0)
            pauseManager.GameOver();
    }

    public void AumentaVida()
    {
        lifes++;
        if (lifes > 10)
            lifes = 10;

        UpdateGUI();
    }

    public void ResetLifes()
    {
        lifes = 10;
        for (int i = 1; i < 11; i++)
            transform.GetChild(i).GetComponent<LifeGUI>().Ativa();
    }
}
