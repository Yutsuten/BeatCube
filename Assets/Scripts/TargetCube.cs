using UnityEngine;
using System.Collections;

public class TargetCube : TargetColor
{
    // Audio
    private AudioSource audioExplosao;

    // Movement and collisions
    protected bool houveColisao = false;
    private float tExplosao = 1.2f;
    private float minRotation = 50.0f;
    private float maxRotation = 150.0f;
    private float tempoColisao = 0.05f;
    public GameObject QuadradoMaior;

    private float velocidadeRotacaoX;
    private float velocidadeRotacaoY;
    private float velocidadeRotacaoZ;

    void Start()
    {
        base.Start();

        velocidadeRotacaoX = Random.Range(minRotation, maxRotation);
        velocidadeRotacaoY = Random.Range(minRotation, maxRotation);
        velocidadeRotacaoZ = Random.Range(minRotation, maxRotation);

        // Explosion audio
        audioExplosao = GameObject.Find("Sounds/Explosion").GetComponent<AudioSource>();
    }

    void Update()
    {
        transform.Rotate(velocidadeRotacaoX * Time.deltaTime, velocidadeRotacaoY * Time.deltaTime, velocidadeRotacaoZ * Time.deltaTime);
        UpdateChildColors();
    }

    void OnTriggerEnter(Collider Col)
    {
        if (!houveColisao)
        {
            // BURST ATIVADO
            if (SpecialManager.SpecialActivated && (Col.gameObject.tag.Equals("BlueSphere") || Col.gameObject.tag.Equals("YellowSphere") || Col.gameObject.tag.Equals("RedSphere")))
            {
                //Acerto(Col);
                Explosao();
                Destroy(Col.gameObject);
            }
            // COLISAO DAS BOLAS
            else if (gameObject.tag.Equals("BlueTarget"))
            {
                if (Col.gameObject.tag.Equals("BlueSphere"))
                {
                    //Acerto(Col);
                    Explosao();
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
                    Explosao();
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
                    Explosao();
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
                
            if (Col.gameObject.tag.Equals("Destroi Cubo"))
            {
                Destroy(gameObject);
            }
            else if (Col.gameObject.tag.Equals("Cubo Esquecido"))
            {
                CuboEsquecido();
            }
        }
    }

    private void CuboEsquecido()
    {
        Destroy(gameObject);
        GameObject.Find("Panel/Lifes").GetComponent<LifeManager>().DiminuiVida();
        GameObject.Find("GM").GetComponent<ScoreManager>().ResetCombo();
    }

    public void Incremento(Collider col)
    {
        //print("Erro imbecil");
        GameObject.Find("GM").GetComponent<ScoreManager>().ResetCombo();

        int corBola;
        houveColisao = true;
        Invoke("DesabilitaColisao", tempoColisao);
        if (col.gameObject.tag == "RedSphere")
        {
            corBola = 2;
        }
        else if (col.gameObject.tag == "BlueSphere")
        {
            corBola = 1;
        }
        else // if (col.gameObject.tag == "YellowSphere")
        {
            corBola = 3;
        }
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
        //print("Transform Obj Inst: " + obj.transform.rotation + " - Transform: " + transform.rotation);
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
            aux.tag = "YellowTarget";
        }
        else if (corBola == 3) // AMARELO
        {
            aux.tag = "RedTarget";
        }
        transform.GetComponent<Rigidbody>().GetComponent<Collider>().enabled = false;
        transform.GetComponent<Rigidbody>().isKinematic = true;
    }

    private void Explosao()
    {
        GameObject.Find("GM").GetComponent<ScoreManager>().GetPoints();

        houveColisao = true;
        Invoke("DesabilitaColisao", tempoColisao);
        
        if (transform.childCount != 8) // Verifica se e o ultimo quadrado
        { // EH UM CUBO GRANDE
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
        else
        { // EH O CUBO MENOR
            Vector3 centro2;
            Vector3[] coordFora2 = new Vector3[transform.childCount];
            centro2 = transform.position; // Obtendo a posicao do centro
            for (int j = 0; j < transform.childCount; j++)
            { // Obtendo a posicao dos outros quadrados
                coordFora2[j] = transform.GetChild(j).position;
                transform.GetChild(j).GetComponent<Rigidbody>().isKinematic = false;
                transform.GetChild(j).GetComponent<Rigidbody>().AddForce((coordFora2[j] - centro2) * Random.Range(150, 200) * 2);
                transform.GetChild(j).GetComponent<Rigidbody>().constraints = 0; // Eliminando o "travamento" da posicao em z
                transform.GetChild(j).GetComponent<Rigidbody>().AddTorque(Random.Range(minRotation, maxRotation), Random.Range(minRotation, maxRotation), Random.Range(minRotation, maxRotation));
                transform.GetChild(j).GetComponent<TargetCubeFragment>().Destroi(tExplosao * 2, transform.GetChild(j).localScale.x);
                Destroy(transform.GetChild(j).gameObject, tExplosao);
            }
            transform.DetachChildren();
            audioExplosao.Play();
            Destroy(transform.gameObject);// destruindo o que foi acertado
        }
    }

    private void DesabilitaColisao()
    {
        houveColisao = false;
    }

    private void OnDestroy()
    {
        GameObject.Find("GM").GetComponent<GameMananger>().RemoveObjectFromList(this.gameObject);
    }

    public void ChamaExplosao()
    {
        Explosao();
    }
}
