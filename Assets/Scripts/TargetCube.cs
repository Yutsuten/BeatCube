using UnityEngine;
using System.Collections;

public class TargetCube : CubeBehaviour
{
    // Movement and collisions
    public GameObject QuadradoMaior;

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
        if (!houveColisao)
        {
            // BURST ATIVADO
            if (SpecialManager.SpecialActivated && (Col.gameObject.tag.Equals("BlueSphere") || Col.gameObject.tag.Equals("YellowSphere") || Col.gameObject.tag.Equals("RedSphere")))
            {
                //Acerto(Col);
                Explosion();
                Destroy(Col.gameObject);
            }
            // COLISAO DAS BOLAS
            else if (gameObject.tag.Equals("BlueTarget"))
            {
                if (Col.gameObject.tag.Equals("BlueSphere"))
                {
                    //Acerto(Col);
                    Explosion();
                    Destroy(Col.gameObject);
                }
                else if(Col.gameObject.tag.Equals("YellowSphere"))
                {
                    //Incremento(quadradoMaior, Col);
                    Incremento(Col);
                }
                else if (Col.gameObject.tag.Equals("RedSphere"))
                {
                    //Incremento(quadradoMaior, Col);
                    Incremento(Col);
                }
            }
            else if (gameObject.tag.Equals("YellowTarget"))
            {
                if (Col.gameObject.tag.Equals("YellowSphere"))
                {
                    //Acerto(Col);
                    Explosion();
                    Destroy(Col.gameObject);
                }
                else if (Col.gameObject.tag.Equals("BlueSphere"))
                {
                    //Incremento(quadradoMaior, Col);
                    Incremento(Col);
                }
                else if (Col.gameObject.tag.Equals("RedSphere"))
                {
                    //Incremento(quadradoMaior, Col);
                    Incremento(Col);
                }
            }
            else if (gameObject.tag.Equals("RedTarget"))
            {
                if (Col.gameObject.tag.Equals("RedSphere"))
                {
                    //Acerto(Col);
                    Explosion();
                    Destroy(Col.gameObject);
                }
                else if (Col.gameObject.tag.Equals("YellowSphere"))
                {
                    //Incremento(quadradoMaior, Col);
                    Incremento(Col);
                }
                else if (Col.gameObject.tag.Equals("BlueSphere"))
                {
                    //Incremento(quadradoMaior, Col);
                    Incremento(Col);
                }
            }

        }
    }

    private void Incremento(Collider col)
    {
        scoreManager.ResetCombo();

        int corBola;
        houveColisao = true;
        Invoke("DesabilitaColisao", tempoColisao);
        if (col.gameObject.tag == "RedSphere")
            corBola = 2;
        else if (col.gameObject.tag == "BlueSphere")
            corBola = 1;
        else // if (col.gameObject.tag == "YellowSphere")
            corBola = 3;
        Destroy(col.gameObject);

        // SE O QUADRADO JA TA NO TAMANHO MAXIMO E O JOGADOR ERROU DENOVO
        if (QuadradoMaior == null)
        {
            Destroy(transform.GetChild(transform.childCount - 1).gameObject);
            targetColor = 4;
            Vector3 centro;
            Vector3[] coordFora = new Vector3[13];
            centro = transform.position; // Obtendo a posicao do centro
            for (var j = 0; j < transform.childCount - 1; j += 1)
            { // Obtendo a posicao dos outros quadrados
                coordFora[j] = transform.GetChild(j).position;
                transform.GetChild(j).GetComponent<Rigidbody>().isKinematic = false;
                transform.GetChild(j).GetComponent<Rigidbody>().AddForce((coordFora[j] - centro) * Random.Range(150, 200));
                Destroy(transform.GetChild(j).gameObject, tExplosao);
                transform.GetChild(j).GetComponent<Rigidbody>().constraints = 0; // Eliminando o "travamento" da posicao em z
                transform.GetChild(j).GetComponent<Rigidbody>().AddTorque(Random.Range(minRotation, maxRotation), Random.Range(minRotation, maxRotation), Random.Range(minRotation, maxRotation));
                transform.GetChild(j).GetComponent<TargetCubeFragment>().Destroi(tExplosao, transform.GetChild(j).localScale.x * 0.5f);
            }
            transform.DetachChildren();
            Destroy(transform.gameObject);
            return;
        }

        GameObject obj = Instantiate(QuadradoMaior, transform.position, transform.rotation) as GameObject;
        // ADICIONANDO A ROTACAO DO QUADRADO MAIOR
        obj.GetComponent<Rigidbody>().AddTorque(Random.Range(minRotation, maxRotation), Random.Range(minRotation, maxRotation), Random.Range(minRotation, maxRotation));
        // ajustar angulos dos quadrados e velocidade
        obj.GetComponent<Rigidbody>().velocity = transform.GetComponent<Rigidbody>().velocity;
        transform.parent = obj.transform;
        var aux = obj;
        while (aux.transform.childCount != 8) // MUDANDO A COR DE TODOS OS QUADRADOS DO MAIOR E MENOR
        {
            aux.GetComponent<TargetCube>().BiggerCube = true;
            aux.GetComponent<TargetCube>().Color = aux.transform.GetChild(aux.transform.childCount - 1).GetComponent<TargetCube>().Color;
            // cor de todos os quadrados do maior = cor dos quadrados menor
            aux.tag = aux.transform.GetChild(aux.transform.childCount - 1).tag;
            aux = aux.transform.GetChild(aux.transform.childCount - 1).gameObject;
        }
        // PINTANDO OS QUADRADOS DO MENOR
        aux.GetComponent<TargetCube>().Color = corBola;
        // ARRUMANDO A TAG DO MENOR
        if (corBola == 1) // AZUL
        {
            aux.tag = "BlueTarget";
        }
        else if (corBola == 2) // VERMELHO
        {
            aux.tag = "RedTarget";
        }
        else if (corBola == 3) // AMARELO
        {
            aux.tag = "YellowTarget";
        }
        transform.GetComponent<Rigidbody>().GetComponent<Collider>().enabled = false;
        transform.GetComponent<Rigidbody>().isKinematic = true;
    }

    private void ExplodeBigCube()
    {
        var bolaMenor = transform.GetChild(transform.childCount - 1);
        bolaMenor.transform.parent = null; // tirando o parentesco da bola menor
        bolaMenor.GetComponent<Rigidbody>().isKinematic = false; // deixa de ser "estatico"
        bolaMenor.GetComponent<Rigidbody>().velocity = transform.GetComponent<Rigidbody>().velocity; // recebendo a velocidade do que sera destruido
        // EXPLOSAO DOS QUADRADOS MAIS EXTERNOS
        Vector3 centro;
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
        audioExplosao.Play();
        Destroy(transform.gameObject);// destruindo o que foi acertado
        bolaMenor.GetComponent<Collider>().enabled = true; // ativando as colisoes da bola menor
    }

    protected void Explosion()
    {
        scoreManager.GetPoints();

        houveColisao = true;
        Invoke("DesabilitaColisao", tempoColisao);

        if (transform.childCount != 8) // Verifica se e o ultimo quadrado
            ExplodeBigCube(); // EH UM CUBO GRANDE
        else
            base.ExplodeSmallCube();
    }

    private void DesabilitaColisao()
    {
        houveColisao = false;
    }

    public void ChamaExplosao()
    {
        Explosion();
    }
}
