using UnityEngine;
using System.Collections;

public class TargetItem : CubeBehaviour
{
    public GameObject effectTextMesh;

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
        {
            lifeManager.AumentaVida();
            ShowEffectText("+life");
        }
        else if (Color == 6)
        {
            specialManager.ItemSpecialBonus();
            ShowEffectText("+special");
        }
    }

    private void ShowEffectText(string effectText)
    {
        GameObject objectTextMesh = Instantiate(effectTextMesh, new Vector3(transform.position.x, transform.position.y + 0.7f, transform.position.z), Quaternion.identity) as GameObject;
        objectTextMesh.GetComponent<ItemTextAnimation>().SetText(effectText);
    }

    public void ChamaExplosao()
    {
        Explosion();
    }
}