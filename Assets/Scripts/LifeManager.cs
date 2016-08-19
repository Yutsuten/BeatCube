using UnityEngine;
using System.Collections;

public class LifeManager : MonoBehaviour {

    int lifes = 10;

    private void UpdateGUI()
    {
        if (lifes >= 1) // Will only have an activated light if has at least a life
            transform.GetChild(lifes - 1).GetComponent<LifeGUI>().Ativa();
        if (lifes <= 9) // Won't disable a light if life is complete
            transform.GetChild(lifes).GetComponent<LifeGUI>().Desativa();
    }

    public void DiminuiVida()
    {
        print("Lost life.");
        lifes--;
        if (lifes < 0)
            lifes = 0;

        UpdateGUI();

        //GetComponent<GameMananger>().CancelInvoke();
        //GetComponent<GameMananger>().FimDeJogo();
    }

    public void AumentaVida()
    {
        print("Won life.");
        lifes++;
        if (lifes > 10)
            lifes = 10;

        UpdateGUI();
    }
}
