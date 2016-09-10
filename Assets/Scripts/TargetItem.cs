using UnityEngine;
using System.Collections;

public class TargetItem : CubeBehaviour
{
    private Collider auxCol;
    private GameObject auxObj;

    void Start()
    {
        base.Start();
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
        GameObject.Find("Panel/Lifes").GetComponent<LifeManager>().DiminuiVida();
        GameObject.Find("GameManager").GetComponent<ScoreManager>().ResetCombo();
    }

    protected void Explosion()
    {
        GameObject.Find("GameManager").GetComponent<ScoreManager>().GetPoints();

        houveColisao = true;

        ExplodeSmallCube();
        Efeito();
    }

    public void Efeito()
    {
        if (Color == 5)
            GameObject.Find("Panel/Lifes").GetComponent<LifeManager>().AumentaVida();
        else if (Color == 6)
            GameObject.Find("GameManager").GetComponent<SpecialManager>().ItemSpecialBonus();
        else if (Color == 7)
            GameObject.Find("GameManager").GetComponent<GameMananger>().ItemDestroiObj();
    }

    public void ChamaExplosao()
    {
        Explosion();
    }
}