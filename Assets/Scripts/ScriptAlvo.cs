using UnityEngine;
using System.Collections;

public class ScriptAlvo : MonoBehaviour 
{
    public static float T_EXPLOSAO;
    public static bool DESTROY_ALL_TARGETS = false;

    public GameObject quadradoMaior;
    public float tExplosao = 1.2f;
    public float minRotation = 50.0f;
    public float maxRotation = 150.0f;
    public float tempoColisao = 0.05f;
    
    private int corBola = 0;
    private float velocidadeRotacaoX;
    private float velocidadeRotacaoY;
    private float velocidadeRotacaoZ;
    private bool houveColisao = false;
    private GameObject scripts;

    void OnTriggerEnter(Collider Col)
    {
	    if (!houveColisao)
	    {
		    // BURST ATIVADO
		    if (ScriptEspecial.ESPECIAL_ATIVADO && (Col.gameObject.tag.Equals("Esfera Azul") || Col.gameObject.tag.Equals("Esfera Verde") || Col.gameObject.tag.Equals("Esfera Vermelha")))
		    {
			    Acerto(Col);
		    }
	        // COLISAO DAS BOLAS
	        else if (gameObject.tag.Equals("Target Azul"))
	        {
	            if (Col.gameObject.tag.Equals("Esfera Azul"))
	                Acerto(Col);
	            else if (Col.gameObject.tag.Equals("Esfera Verde"))
	        	    Incremento(quadradoMaior, Col);
	            else if (Col.gameObject.tag.Equals("Esfera Vermelha"))
	        	    Incremento(quadradoMaior, Col);
	        }
	        else if (gameObject.tag.Equals("Target Verde"))
	        {
	            if (Col.gameObject.tag.Equals("Esfera Verde"))
	                Acerto(Col);
	            else if (Col.gameObject.tag.Equals("Esfera Azul"))
	                Incremento(quadradoMaior, Col);
	            else if (Col.gameObject.tag.Equals("Esfera Vermelha"))
	                Incremento(quadradoMaior, Col);
	        }
	        else if (gameObject.tag.Equals("Target Vermelha"))
	        {
	            if (Col.gameObject.tag.Equals("Esfera Vermelha"))
	                Acerto(Col);
	            else if (Col.gameObject.tag.Equals("Esfera Azul"))
	                Incremento(quadradoMaior, Col);
	            else if (Col.gameObject.tag.Equals("Esfera Verde"))
	                Incremento(quadradoMaior, Col);
	        }
		    if (Col.gameObject.tag.Equals("Destroi Cubo"))
			    Destroy (gameObject);
		    else if (Col.gameObject.tag.Equals("Cubo Esquecido"))
			    CuboEsquecido();
	    }
    }

    private void DesabilitaColisao()
    {
        houveColisao = false;
    }

    private void Incremento (GameObject aumento, Collider Col)
    {
	    houveColisao = true;
	    Invoke("DesabilitaColisao", tempoColisao);
	    if (Col.gameObject.tag == "Esfera Vermelha")
	    {
		    corBola = 2;
	    }
	    else if (Col.gameObject.tag == "Esfera Azul")
	    {
		    corBola = 1;
	    }
	    else if (Col.gameObject.tag == "Esfera Verde")
	    {
		    corBola = 3;
	    }
        Destroy(Col.gameObject);
        // SE O QUADRADO JA TA NO TAMANHO MAXIMO E O JOGADOR ERROU DENOVO
        if (aumento == null)
        {
    	    Destroy(transform.GetChild(transform.childCount - 1).gameObject);
    	    Vector3 centro;
    	    Vector3[] coordFora = new Vector3[13];
    	    centro = transform.position; // Obtendo a posicao do centro
    	    for (var j = 0; j < transform.childCount - 1; j += 1)
    	    { // Obtendo a posicao dos outros quadrados
    		    transform.GetChild(j).GetComponent<ScriptCubo>().cubeColor = 4;
    		    // TESTANDO CIMA
    		    coordFora[j] = transform.GetChild(j).position;
    		    transform.GetChild(j).GetComponent<Rigidbody>().isKinematic = false;
    		    transform.GetChild(j).GetComponent<Rigidbody>().AddForce((coordFora[j] - centro) * Random.Range(150,200));
    		    Destroy(transform.GetChild(j).gameObject, tExplosao);
    		    transform.GetChild(j).GetComponent<Rigidbody>().constraints = 0; // Eliminando o "travamento" da posicao em z
    		    transform.GetChild(j).GetComponent<Rigidbody>().AddTorque(Random.Range(minRotation, maxRotation), Random.Range(minRotation, maxRotation), Random.Range(minRotation, maxRotation));
                transform.GetChild(j).GetComponent<ScriptExplosao>().Destroi(tExplosao, transform.GetChild(j).localScale.x * 0.5f);
    	    }
    	    transform.DetachChildren();
    	    Destroy(transform.gameObject);
    	    scripts.GetComponent<ScriptPontuacao>().ResetaCombo();
    	    scripts.GetComponent<ScriptVidas>().DecLife(3);
    	    return;
        }
        GameObject obj = Instantiate(aumento, transform.position, transform.rotation) as GameObject;
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
                aux.GetChild(i).GetComponent<ScriptCubo>().cubeColor = aux.GetChild(aux.childCount - 1).GetChild(0).GetComponent<ScriptCubo>().cubeColor;
    	    }
    	    // cor de todos os quadrados do maior = cor dos quadrados menor
    	    aux.tag = aux.GetChild(aux.childCount - 1).tag;
    	    aux = aux.GetChild(aux.childCount - 1);
        }
        // PINTANDO OS QUADRADOS DO MENOR
        for (var k = 0; k < aux.childCount; k++)
        {
            aux.GetChild(k).GetComponent<ScriptCubo>().cubeColor = corBola;
        }
        // ARRUMANDO A TAG DO MENOR
        if (corBola == 1) // AZUL
        {
            aux.tag = "Target Azul";
        }
        else if (corBola == 2) // VERMELHO
        {
    	    aux.tag = "Target Vermelha";
        }
        else if (corBola == 3) // AMARELO
        {
    	    aux.tag = "Target Verde";
        }
        transform.GetComponent<Rigidbody>().GetComponent<Collider>().enabled = false;
        transform.GetComponent<Rigidbody>().isKinematic = true;
        scripts.GetComponent<ScriptPontuacao>().DecCombo();
    }

    private void CuboEsquecido()
    {
        Destroy(gameObject);
        scripts.GetComponent<ScriptPontuacao>().ResetaCombo();
        scripts.GetComponent<ScriptEspecial>().ResetaEspecial();
        scripts.GetComponent<ScriptVidas>().DecLife(1);
    }

    private void Acerto (Collider Col)
    {
	    houveColisao = true;
	    Invoke("DesabilitaColisao", tempoColisao);
	    Destroy(Col.gameObject);
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
    	    for (var j = 0; j < transform.childCount; j += 1)
    	    { // Obtendo a posicao dos outros quadrados
    		    coordFora[j] = transform.GetChild(j).position;
    		    transform.GetChild(j).GetComponent<Rigidbody>().isKinematic = false;
    		    transform.GetChild(j).GetComponent<Rigidbody>().AddForce((coordFora[j] - centro) * Random.Range(150,200));
    		    transform.GetChild(j).GetComponent<Rigidbody>().constraints = 0; // Eliminando o "travamento" da posicao em z
    		    transform.GetChild(j).GetComponent<Rigidbody>().AddTorque(Random.Range(minRotation, maxRotation), Random.Range(minRotation, maxRotation), Random.Range(minRotation, maxRotation));
                transform.GetChild(j).GetComponent<ScriptExplosao>().Destroi(tExplosao, transform.GetChild(j).localScale.x * 0.5f);
                Destroy(transform.GetChild(j).gameObject, tExplosao);
    	    }
    	    transform.DetachChildren();
		    Destroy(transform.gameObject);// destruindo o que foi acertado
		    bolaMenor.GetComponent<Collider>().enabled = true; // ativando as colisoes da bola menor
	    }
	    else 
	    { // EH O CUBO MENOR
		    Vector3 centro2;
    	    Vector3[] coordFora2 = new Vector3[transform.childCount];
    	    centro2 = transform.position; // Obtendo a posicao do centro
    	    for (var j2 = 0; j2 < transform.childCount; j2 += 1)
    	    { // Obtendo a posicao dos outros quadrados
    		    coordFora2[j2] = transform.GetChild(j2).position;
    		    transform.GetChild(j2).GetComponent<Rigidbody>().isKinematic = false;
    		    transform.GetChild(j2).GetComponent<Rigidbody>().AddForce((coordFora2[j2] - centro2) * Random.Range(150,200) * 2);
    		    transform.GetChild(j2).GetComponent<Rigidbody>().constraints = 0; // Eliminando o "travamento" da posicao em z
    		    transform.GetChild(j2).GetComponent<Rigidbody>().AddTorque(Random.Range(minRotation, maxRotation), Random.Range(minRotation, maxRotation), Random.Range(minRotation, maxRotation));
                transform.GetChild(j2).GetComponent<ScriptExplosao>().Destroi(tExplosao * 2, transform.GetChild(j2).localScale.x);
                Destroy(transform.GetChild(j2).gameObject, tExplosao);
    	    }
    	    transform.DetachChildren();
		    Destroy(transform.gameObject);// destruindo o que foi acertado
	    }
	    scripts.GetComponent<ScriptPontuacao>().AumentaPontuacao();
	    scripts.GetComponent<ScriptVidas>().IncLife();
        scripts.GetComponent<ScriptPontuacao>().IncCombo();
        scripts.GetComponent<ScriptEspecial>().RecalculaFundo(1);
    }

	// Use this for initialization
	void Start () 
    {
        T_EXPLOSAO = tExplosao;
        // VALORES PARA ROTACAO
        velocidadeRotacaoX = Random.Range(minRotation, maxRotation);
        velocidadeRotacaoY = Random.Range(minRotation, maxRotation);
        velocidadeRotacaoZ = Random.Range(minRotation, maxRotation);
        scripts = GameObject.Find("Scripts");
	}
	
	// Update is called once per frame
	void Update () 
    {
        // ROTACAO DO QUADRADO
        transform.Rotate(velocidadeRotacaoX * Time.deltaTime, velocidadeRotacaoY * Time.deltaTime, velocidadeRotacaoZ * Time.deltaTime);

        if (DESTROY_ALL_TARGETS == true)
            Destroy(gameObject);
	}
}
