using UnityEngine;
using System.Collections;

public class TargetCube : MonoBehaviour {
    
    protected string metodoAcerto, metodoIncremento;

    private GameObject audioExplosao;

    public float tExplosao = 1.2f;
    public float minRotation = 50.0f;
    public float maxRotation = 150.0f;
    public float tempoColisao = 0.05f;
    int corBola;

    private float velocidadeRotacaoX;
    private float velocidadeRotacaoY;
    private float velocidadeRotacaoZ;

    protected Collider auxCol;
    protected GameObject auxObj;

    public GameObject QuadradoMaior;

    protected bool houveColisao = false;

    string color;

    void Start()
    {
        metodoAcerto = "chamaAcerto";
        metodoIncremento = "chamaIncremento";

        velocidadeRotacaoX = Random.Range(minRotation, maxRotation);
        velocidadeRotacaoY = Random.Range(minRotation, maxRotation);
        velocidadeRotacaoZ = Random.Range(minRotation, maxRotation);

        // Explosion audio
        audioExplosao = GameObject.Find("Sounds/Explosion");
    }

    void Update()
    {
        transform.Rotate(velocidadeRotacaoX * Time.deltaTime, velocidadeRotacaoY * Time.deltaTime, velocidadeRotacaoZ * Time.deltaTime);
    }

    void OnTriggerEnter(Collider Col)
    {
        //print(gameObject.tag + " " + Col.gameObject.tag);
        auxCol = Col;
        if (!houveColisao)
        {
            // BURST ATIVADO
            if (SpecialManager.ESPECIAL_ATIVADO && (Col.gameObject.tag.Equals("Bola Azul") || Col.gameObject.tag.Equals("Bola Amarela") || Col.gameObject.tag.Equals("Bola Vermelha")))
            {
                //Acerto(Col);
                Invoke(metodoAcerto,0);
                Explosao();
                Destroy(Col.gameObject);
            }
            // COLISAO DAS BOLAS
            else if (gameObject.tag.Equals("Target Azul"))
            {
                if (Col.gameObject.tag.Equals("Bola Azul"))
                {
                    //Acerto(Col);
                    Explosao();
                    Destroy(Col.gameObject);
                }
                else if(Col.gameObject.tag.Equals("Bola Amarela"))
                {
                    //Incremento(quadradoMaior, Col);
                    Incremento(Col);
                }
                else if (Col.gameObject.tag.Equals("Bola Vermelha"))
                {
                    //Incremento(quadradoMaior, Col);
                    Incremento(Col);
                }
            }
            else if (gameObject.tag.Equals("Target Amarelo"))
            {
                if (Col.gameObject.tag.Equals("Bola Amarela"))
                {
                    //Acerto(Col);
                    Explosao();
                    Destroy(Col.gameObject);
                }
                else if (Col.gameObject.tag.Equals("Bola Azul"))
                {
                    //Incremento(quadradoMaior, Col);
                    Incremento(Col);
                }
                else if (Col.gameObject.tag.Equals("Bola Vermelha"))
                {
                    //Incremento(quadradoMaior, Col);
                    Incremento(Col);
                }
            }
            else if (gameObject.tag.Equals("Target Vermelho"))
            {
                if (Col.gameObject.tag.Equals("Bola Vermelha"))
                {
                    //Acerto(Col);
                    Explosao();
                    Destroy(Col.gameObject);
                }
                else if (Col.gameObject.tag.Equals("Bola Amarela"))
                {
                    //Incremento(quadradoMaior, Col);
                    Incremento(Col);
                }
                else if (Col.gameObject.tag.Equals("Bola Azul"))
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
        GameObject.Find("GM").GetComponent<ScoreManager>().ResetaCombo();
        /*scripts.GetComponent<ScriptPontuacao>().ResetaCombo();
        scripts.GetComponent<ScriptEspecial>().ResetaEspecial();
        scripts.GetComponent<ScriptVidas>().DecLife(1);*/
    }

    public void Incremento(Collider col)
    {
        //print("Erro imbecil");
        GameObject.Find("GM").GetComponent<ScoreManager>().ResetaCombo();


        houveColisao = true;
        Invoke("DesabilitaColisao", tempoColisao);
        if (col.gameObject.tag == "Bola Vermelha")
        {
            corBola = 2;
        }
        else if (col.gameObject.tag == "Bola Azul")
        {
            corBola = 1;
        }
        else if (col.gameObject.tag == "Bola Amarela")
        {
            corBola = 3;
        }
        Destroy(col.gameObject);
        // SE O QUADRADO JA TA NO TAMANHO MAXIMO E O JOGADOR ERROU DENOVO
        if (QuadradoMaior == null)
        {
            Destroy(transform.GetChild(transform.childCount - 1).gameObject);
            Vector3 centro;
            Vector3[] coordFora = new Vector3[13];
            centro = transform.position; // Obtendo a posicao do centro
            for (var j = 0; j < transform.childCount - 1; j += 1)
            { // Obtendo a posicao dos outros quadrados
                transform.GetChild(j).GetComponent<TargetCubeFragment>().cubeColor = 4;
                // TESTANDO CIMA
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
            //scripts.GetComponent<ScriptPontuacao>().ResetaCombo();
            //scripts.GetComponent<ScriptVidas>().DecLife(3);
            return;
        }
        GameObject obj = Instantiate(QuadradoMaior, transform.position, transform.rotation) as GameObject;
        // ADICIONANDO A ROTACAO DO QUADRADO MAIOR
        obj.GetComponent<Rigidbody>().AddTorque(Random.Range(minRotation, maxRotation), Random.Range(minRotation, maxRotation), Random.Range(minRotation, maxRotation));
        // ajustar angulos dos quadrados e velocidade
        obj.GetComponent<Rigidbody>().velocity = transform.GetComponent<Rigidbody>().velocity;
        transform.parent = obj.transform;
        //print("Transform Obj Inst: " + obj.transform.rotation + " - Transform: " + transform.rotation);
        var aux = obj.transform;
        while (aux.childCount != 8) // MUDANDO A COR DE TODOS OS QUADRADOS DO MAIOR E MENOR
        {
            for (var i = 0; i < aux.childCount - 1; i++)
            {
                aux.GetChild(i).GetComponent<TargetCubeFragment>().cubeColor = aux.GetChild(aux.childCount - 1).GetChild(0).GetComponent<TargetCubeFragment>().cubeColor;
            }
            // cor de todos os quadrados do maior = cor dos quadrados menor
            aux.tag = aux.GetChild(aux.childCount - 1).tag;
            aux = aux.GetChild(aux.childCount - 1);
        }
        // PINTANDO OS QUADRADOS DO MENOR
        for (var k = 0; k < aux.childCount; k++)
        {
            aux.GetChild(k).GetComponent<TargetCubeFragment>().cubeColor = corBola;
        }
        // ARRUMANDO A TAG DO MENOR
        if (corBola == 1) // AZUL
        {
            aux.tag = "Target Azul";
        }
        else if (corBola == 2) // VERMELHO
        {
            aux.tag = "Target Vermelho";
        }
        else if (corBola == 3) // AMARELO
        {
            aux.tag = "Target Amarelo";
        }
        transform.GetComponent<Rigidbody>().GetComponent<Collider>().enabled = false;
        transform.GetComponent<Rigidbody>().isKinematic = true;
        //scripts.GetComponent<ScriptPontuacao>().DecCombo();
    }

    void Explosao()
    {
        GameObject.Find("GM").GetComponent<ScoreManager>().AumentaPontos(5);
        GameObject.Find("GM").GetComponent<ScoreManager>().AumentaCombo();

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
            audioExplosao.GetComponent<AudioSource>().Play();
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
            audioExplosao.GetComponent<AudioSource>().Play();
            Destroy(transform.gameObject);// destruindo o que foi acertado
        }
    }

    void DesabilitaColisao()
    {
        houveColisao = false;
    }

    void OnDestroy()
    {
        GameObject.Find("GM").GetComponent<GameMananger>().RemoveObjectFromList(this.gameObject);
        //print(gameObject.name + " foi destruido");
    }

    public void ParalisaObj()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        velocidadeRotacaoX = 0;
        velocidadeRotacaoY = 0;
        velocidadeRotacaoZ = 0;
    }

    public void ChamaExplosao()
    {
        Explosao();
    }
}
