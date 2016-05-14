using UnityEngine;
using System.Collections;

public class CuboDeGelo : MonoBehaviour
{
    public GameObject AudioExplosao;

    protected string metodoAcerto, metodoIncremento;

    public float minRotation = 50.0f;
    public float maxRotation = 150.0f;

    private float velocidadeRotacaoX;
    private float velocidadeRotacaoY;
    private float velocidadeRotacaoZ;

    //private Vector3 velocidadeAtual;

    public float tExplosao = 1.2f;

    protected Collider auxCol;
    protected GameObject auxObj;

    protected bool houveColisao = false;

    void Start()
    {
        velocidadeRotacaoX = Random.Range(minRotation, maxRotation);
        velocidadeRotacaoY = Random.Range(minRotation, maxRotation);
        velocidadeRotacaoZ = Random.Range(minRotation, maxRotation);
    }

    void Update()
    {
        //transform.Rotate(velocidadeRotacaoX * Time.deltaTime, velocidadeRotacaoY * Time.deltaTime, velocidadeRotacaoZ * Time.deltaTime);
    }

    void OnTriggerEnter(Collider Col)
    {
        //print(gameObject.tag + " " + Col.gameObject.name);

        if (Col.gameObject.name.Equals("BotaoAzul") || Col.gameObject.name.Equals("BotaoAmarelo") || Col.gameObject.name.Equals("BotaoVermelho"))
        {
            //print("AAAAAAAAAAAAAAAAAA");
            Explosao();
            Col.GetComponent<Botao>().CongelaBotao();
            //GameObject.Find("GM").GetComponent<Pontuacao>().ResetaCombo();
            
            //Destroy(this.gameObject);
        }
        else if(Col.gameObject.tag.Equals("Bola Azul") || Col.gameObject.tag.Equals("Bola Amarela") || Col.gameObject.tag.Equals("Bola Vermelha"))
        {
            Destroy(Col.gameObject);
            Explosao();
            //Destroy(this.gameObject);
        }
        else if(Col.gameObject.tag.Equals("Parede"))
        {
            Vector3 aux = transform.GetComponent<Rigidbody>().velocity;
            aux.x *= -1;
            transform.GetComponent<Rigidbody>().velocity = aux;
        }
    }

    private void CuboEsquecido()
    {
        Destroy(gameObject);
        /*scripts.GetComponent<ScriptPontuacao>().ResetaCombo();
        scripts.GetComponent<ScriptEspecial>().ResetaEspecial();
        scripts.GetComponent<ScriptVidas>().DecLife(1);*/
    }

    void Explosao()
    {
        GameObject.Find("GM").GetComponent<Pontuacao>().AumentaCombo();

        houveColisao = true;
        //Invoke("DesabilitaColisao", tempoColisao);
        //Destroy(col.gameObject);
        
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
        Instantiate(AudioExplosao);
        Destroy(transform.gameObject);// destruindo o que foi acertado
        //bolaMenor.collider.enabled = true; // ativando as colisoes da bola menor
        Destroy(bolaMenor.gameObject);
    }

    void OnDestroy()
    {
        GameObject.Find("GM").GetComponent<GameMananger>().RetiraObjLista(this.gameObject);
        //print(gameObject.name + " foi destruido");
    }

    public void ParalisaObj()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
        velocidadeRotacaoX = 0;
        velocidadeRotacaoY = 0;
        velocidadeRotacaoZ = 0;
    }

    public void ChamaExplosao()
    {
        Explosao();
    }
}