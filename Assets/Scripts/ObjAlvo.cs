using UnityEngine;
using System.Collections;

public abstract class ObjAlvo : MonoBehaviour {
    
    protected string metodoAcerto, metodoIncremento;

    private GameObject audioExplosao;

    public float tExplosao = 1.2f;
    public float minRotation = 50.0f;
    public float maxRotation = 150.0f;
    public float tempoColisao = 0.05f;

    private float velocidadeRotacaoX;
    private float velocidadeRotacaoY;
    private float velocidadeRotacaoZ;

    protected Collider auxCol;
    protected GameObject auxObj;

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
            if (ScriptEspecial.ESPECIAL_ATIVADO && (Col.gameObject.tag.Equals("Bola Azul") || Col.gameObject.tag.Equals("Bola Amarela") || Col.gameObject.tag.Equals("Bola Vermelha")) && Col.gameObject.GetComponent<Bola>().Especial())
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
                    Invoke(metodoAcerto, 0);
                    Explosao();
                    Destroy(Col.gameObject);
                }
                else if(Col.gameObject.tag.Equals("Bola Amarela"))
                {
                    //Incremento(quadradoMaior, Col);
                    Invoke(metodoIncremento, 0);
                }
                else if (Col.gameObject.tag.Equals("Bola Vermelha"))
                {
                    //Incremento(quadradoMaior, Col);
                    Invoke(metodoIncremento, 0);
                }
            }
            else if (gameObject.tag.Equals("Target Amarelo"))
            {
                if (Col.gameObject.tag.Equals("Bola Amarela"))
                {
                    //Acerto(Col);
                    Invoke(metodoAcerto, 0);
                    Explosao();
                    Destroy(Col.gameObject);
                }
                else if (Col.gameObject.tag.Equals("Bola Azul"))
                {
                    //Incremento(quadradoMaior, Col);
                    Invoke(metodoIncremento, 0);
                }
                else if (Col.gameObject.tag.Equals("Bola Vermelha"))
                {
                    //Incremento(quadradoMaior, Col);
                    Invoke(metodoIncremento, 0);
                }
            }
            else if (gameObject.tag.Equals("Target Vermelho"))
            {
                if (Col.gameObject.tag.Equals("Bola Vermelha"))
                {
                    //Acerto(Col);
                    Invoke(metodoAcerto, 0);
                    Explosao();
                    Destroy(Col.gameObject);
                }
                else if (Col.gameObject.tag.Equals("Bola Amarela"))
                {
                    //Incremento(quadradoMaior, Col);
                    Invoke(metodoIncremento, 0);
                }
                else if (Col.gameObject.tag.Equals("Bola Azul"))
                {
                    //Incremento(quadradoMaior, Col);
                    Invoke(metodoIncremento, 0);
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
        GameObject.Find("GM").GetComponent<Vida>().DiminuiVida();
        GameObject.Find("GM").GetComponent<Pontuacao>().ResetaCombo();
        /*scripts.GetComponent<ScriptPontuacao>().ResetaCombo();
        scripts.GetComponent<ScriptEspecial>().ResetaEspecial();
        scripts.GetComponent<ScriptVidas>().DecLife(1);*/
    }

    void Explosao()
    {
        GameObject.Find("GM").GetComponent<Pontuacao>().AumentaPontos(5);
        GameObject.Find("GM").GetComponent<Pontuacao>().AumentaCombo();

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
                transform.GetChild(j).GetComponent<ScriptExplosao>().Destroi(tExplosao, transform.GetChild(j).localScale.x * 0.5f);
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
                transform.GetChild(j).GetComponent<ScriptExplosao>().Destroi(tExplosao * 2, transform.GetChild(j).localScale.x);
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
