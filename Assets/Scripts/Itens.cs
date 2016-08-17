using UnityEngine;
using System.Collections;

public class Itens : MonoBehaviour, I_Itens {

    private GameObject audioExplosao;
    //protected string metodoAcerto, metodoIncremento;

    public float minRotation = 50.0f;
    public float maxRotation = 150.0f;

    private float velocidadeRotacaoX;
    private float velocidadeRotacaoY;
    private float velocidadeRotacaoZ;

    //private Vector3 velocidadeAtual;

    public float tExplosao = 1.2f;
    int ID;

    protected Collider auxCol;
    protected GameObject auxObj;

    protected bool houveColisao = false;

    void Start()
    {
        velocidadeRotacaoX = Random.Range(minRotation, maxRotation);
        velocidadeRotacaoY = Random.Range(minRotation, maxRotation);
        velocidadeRotacaoZ = Random.Range(minRotation, maxRotation);

        ID = transform.GetChild(0).GetComponent<ScriptCubo>().cubeColor;

        // Explosion audio
        audioExplosao = GameObject.Find("Sounds/Explosion");
    }

    void Update()
    {
        transform.Rotate(velocidadeRotacaoX * Time.deltaTime, velocidadeRotacaoY * Time.deltaTime, velocidadeRotacaoZ * Time.deltaTime);
    }

    void OnTriggerEnter(Collider Col)
    {
        //print(gameObject.tag + " " + Col.gameObject.name);

        if (Col.gameObject.tag.Equals("Bola Azul") || Col.gameObject.tag.Equals("Bola Amarela") || Col.gameObject.tag.Equals("Bola Vermelha"))
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
        GameObject.Find("GM").GetComponent<Vida>().DiminuiVida();
        GameObject.Find("GM").GetComponent<Pontuacao>().ResetaCombo();
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
        audioExplosao.GetComponent<AudioSource>().Play();
        Destroy(transform.gameObject);// destruindo o que foi acertado
        //bolaMenor.collider.enabled = true; // ativando as colisoes da bola menor
        Destroy(bolaMenor.gameObject);
        Efeito();
    }

    void OnDestroy()
    {
        GameObject.Find("GM").GetComponent<GameMananger>().RetiraObjLista(this.gameObject);
        //print(gameObject.name + " foi destruido");
    }

    public void ParalisaObj()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        velocidadeRotacaoX = 0;
        velocidadeRotacaoY = 0;
        velocidadeRotacaoZ = 0;
    }

    public void Efeito()
    {
        if (ID == 5)
        {
            //print("Vai aumenta a vida");
            GameObject.Find("GM").GetComponent<Vida>().AumentaVida();
        }
        else if (ID == 6)
        {
            //print("Tem que ativa o especial");
            GameObject.Find("GM").GetComponent<ScriptEspecial>().ItemAtivaEspecial();
        }
        else if(ID == 7)
        {
            //print("Tem q destrui tudo");
            GameObject.Find("GM").GetComponent<GameMananger>().ItemDestroiObj();
        }
    }
    
    public void ChamaExplosao()
    {
        Explosao();
    }
}