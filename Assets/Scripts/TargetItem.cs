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
        UpdateChildColors();
    }

    void OnTriggerEnter(Collider Col)
    {
        //print(gameObject.tag + " " + Col.gameObject.name);

        if (Col.gameObject.tag.Equals("BlueSphere") || Col.gameObject.tag.Equals("YellowSphere") || Col.gameObject.tag.Equals("RedSphere"))
        {
            Destroy(Col.gameObject);
            Explosao();
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

    void Explosao()
    {
        GameObject.Find("GameManager").GetComponent<ScoreManager>().GetPoints();

        houveColisao = true;

        ExplodeSmallCube();
        // EXPLOSAO DOS QUADRADOS MAIS EXTERNOS
        /*Vector3 centro;
        Vector3[] coordFora = new Vector3[transform.childCount];
        centro = transform.position; // Obtendo a posicao do centro
        for (int j = 0; j < transform.childCount; j++)
        { // Obtendo a posicao dos outros quadrados
            coordFora[j] = transform.GetChild(j).position;
            transform.GetChild(j).GetComponent<Rigidbody>().isKinematic = false;
            transform.GetChild(j).GetComponent<Rigidbody>().AddForce((coordFora[j] - centro) * Random.Range(150, 200));
            transform.GetChild(j).GetComponent<Rigidbody>().constraints = 0; // Eliminando o "travamento" da posicao em z
            transform.GetChild(j).GetComponent<Rigidbody>().AddTorque(Random.Range(minRotation, maxRotation), Random.Range(minRotation, maxRotation), Random.Range(minRotation, maxRotation));
            transform.GetChild(j).GetComponent<TargetCubeFragment>().Destroi(tExplosao, transform.GetChild(j).localScale.x * 0.5f);
            Destroy(transform.GetChild(j).gameObject, tExplosao);
        }
        transform.DetachChildren();
        audioExplosao.GetComponent<AudioSource>().Play();
        Destroy(transform.gameObject);*/ // destruindo o que foi acertado
        Efeito();
    }

    public void ParalisaObj()
    {
        /*
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        velocidadeRotacaoX = 0;
        velocidadeRotacaoY = 0;
        velocidadeRotacaoZ = 0;*/
    }

    public void Efeito()
    {
        if (Color == 5)
        {
            //print("Vai aumenta a vida");
            GameObject.Find("Panel/Lifes").GetComponent<LifeManager>().AumentaVida();
        }
        else if (Color == 6)
        {
            //print("Tem que ativa o especial");
            GameObject.Find("GameManager").GetComponent<SpecialManager>().ItemSpecialBonus();
        }
        else if (Color == 7)
        {
            //print("Tem q destrui tudo");
            GameObject.Find("GameManager").GetComponent<GameMananger>().ItemDestroiObj();
        }
    }
    
    public void ChamaExplosao()
    {
        Explosao();
    }
}