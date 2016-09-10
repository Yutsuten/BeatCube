using UnityEngine;
using System.Collections;

public class TargetItem : CubeBehaviour
{
    private Collider auxCol;
    private GameObject auxObj;

    // Scripts
    ScoreManager scoreManager;
    SpecialManager specialManager;
    GameMananger gameManager;

    void Start()
    {
        base.Start();

        // Load scripts
        scoreManager = GameObject.Find("GameManager").GetComponent<ScoreManager>();
        specialManager = GameObject.Find("GameManager").GetComponent<SpecialManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameMananger>();
    }

    void Update()
    {
        base.Update();
    }

    void OnTriggerEnter(Collider Col)
    {
        if (Col.gameObject.tag.Equals("BlueSphere") || Col.gameObject.tag.Equals("YellowSphere") || Col.gameObject.tag.Equals("RedSphere"))
        {
            Destroy(Col.gameObject);
            Explosion();
        }
        else if (Col.gameObject.tag.Equals("Parede"))
        {
            Vector3 aux = transform.GetComponent<Rigidbody>().velocity;
            aux.x *= -1;
            transform.GetComponent<Rigidbody>().velocity = aux;
        }
    }

    private void CuboEsquecido()
    {
        Destroy(gameObject);
        lifeManager.DiminuiVida();
        scoreManager.ResetCombo();
    }

    protected void Explosion()
    {
        scoreManager.GetPoints();

        houveColisao = true;

        ExplodeSmallCube();
        Efeito();
    }

    public void Efeito()
    {
        if (Color == 5)
            lifeManager.AumentaVida();
        else if (Color == 6)
            specialManager.ItemSpecialBonus();
        else if (Color == 7)
            gameManager.ItemDestroiObj();
    }

    public void ChamaExplosao()
    {
        Explosion();
    }
}