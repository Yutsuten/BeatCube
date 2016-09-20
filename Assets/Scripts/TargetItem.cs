using UnityEngine;
using System.Collections;

public class TargetItem : CubeBehaviour
{
    private Collider auxCol;
    private GameObject auxObj;

    // Scripts
    SpecialManager specialManager;

    void Start()
    {
        base.Start();

        // Load scripts
        specialManager = GameObject.Find("GameManager").GetComponent<SpecialManager>();
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
        //else if (Color == 7)
            //gameManager.ItemDestroiObj();
    }

    public void ChamaExplosao()
    {
        Explosion();
    }
}