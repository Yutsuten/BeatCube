using UnityEngine;
using System.Collections;

public class ScriptSpawn : MonoBehaviour 
{
    public GameObject quadrado;
    public float minIntensity = 100.0f;
    public float maxIntensity = 300.0f;
    public float rotacaoGraus = 80.0f;

    private float timing;
    private float rotation;
    private Vector3 direcao;

	// Use this for initialization
	void Start () 
    {
        timing = 0;
	}
	
	// Update is called once per frame
	public void Spawn () 
    {
	    int rnd = Random.Range(0, 99);
        int corCubo;
	    GameObject gameObj = Instantiate(quadrado, new Vector3(Random.Range(-3.4f, 3.4f), 6.2f, 0), Quaternion.identity) as GameObject;
	    	
        if (rnd < 33)
        {
    	    for (var i = 0; i < gameObj.transform.childCount; i += 1)
    	    {
                gameObj.transform.GetChild(i).GetComponent<ScriptCubo>().cubeColor = 1;
		    }
		    gameObj.transform.tag = "Target Azul";
        }
        else if (rnd < 66)
        {
    	    for (var i2 = 0; i2 < gameObj.transform.childCount; i2 += 1)
    	    {
                gameObj.transform.GetChild(i2).GetComponent<ScriptCubo>().cubeColor = 2;
            }
		    gameObj.transform.tag = "Target Vermelha";
        }
        else
        {	for (var i3 = 0; i3 < gameObj.transform.childCount; i3 += 1)
    	    {
                gameObj.transform.GetChild(i3).GetComponent<ScriptCubo>().cubeColor = 3;
            }
		    gameObj.transform.tag = "Target Verde";
        }
        // MUDANDO A DIRECAO DO CUBO
        rotation = GetComponent<ScriptNiveis>().CalculaRotacao(rotacaoGraus);
        gameObj.transform.Rotate(Vector3.back, rotation);
        // ADICIONANDO FORCA
        direcao = Vector3.down * GetComponent<ScriptNiveis>().CalculaIntensidade(rotacaoGraus, rotation, minIntensity, maxIntensity);
	    gameObj.transform.GetComponent<Rigidbody>().AddRelativeForce(direcao, ForceMode.Force);
	
	    // VERIFICANDO SE VAI SER UM CUBO MAIOR
        if (Random.Range(0, 100) < GetComponent<ScriptNiveis>().RetornaDificuldade() / 30)
	    {
		    var obj = Instantiate(gameObj.GetComponent<ScriptAlvo>().quadradoMaior, gameObj.transform.position, Quaternion.identity) as GameObject;
	        // ADICIONANDO A ROTACAO DO QUADRADO MAIOR
	        //obj.rigidbody.AddTorque(Random.Range(gameObj.GetComponent(ScriptTarget).minRotation, gameObj.GetComponent(ScriptTarget).maxRotation), Random.Range(gameObj.GetComponent(ScriptTarget).minRotation, gameObj.GetComponent(ScriptTarget).maxRotation), Random.Range(gameObj.GetComponent(ScriptTarget).minRotation, gameObj.GetComponent(ScriptTarget).maxRotation));
	        // ajustar angulos dos quadrados e velocidade
	        obj.GetComponent<Rigidbody>().transform.Rotate(Vector3.back, rotation);
	        obj.GetComponent<Rigidbody>().AddRelativeForce(direcao, ForceMode.Force);
	        obj.transform.rotation = gameObj.transform.rotation;
	        gameObj.transform.parent = obj.transform;
	        var aux = obj.transform;
	        while (aux.childCount != 8) // MUDANDO A COR DE TODOS OS QUADRADOS DO MAIOR E MENOR
	        {
	    	    for (var i4 = 0; i4 < aux.childCount - 1; i4++)
	    	    {
                    aux.GetChild(i4).GetComponent<ScriptCubo>().cubeColor = aux.GetChild(aux.childCount - 1).GetChild(0).GetComponent<ScriptCubo>().cubeColor;
	    	    }
	    	    // cor de todos os quadrados do maior = cor dos quadrados menor
	    	    aux.tag = aux.GetChild(aux.childCount - 1).tag;
	    	    aux = aux.GetChild(aux.childCount - 1);
	        }
	        // PINTANDO OS QUADRADOS DO MENOR
	        corCubo = Random.Range(0, 3) + 1;
	        for (var k = 0; k < aux.childCount; k++)
                aux.GetChild(k).GetComponent<ScriptCubo>().cubeColor = corCubo;
	        // ARRUMANDO A TAG DO MENOR
	        if (corCubo == 1) // AZUL
	        {
	    	    aux.tag = "Target Azul";
	        }
	        else if (corCubo == 2) // VERMELHO
	        {
	    	    aux.tag = "Target Vermelha";
	        }
	        else if (corCubo == 3) // AMARELO
	        {
	    	    aux.tag = "Target Verde";
	        }
	        gameObj.transform.GetComponent<Rigidbody>().GetComponent<Collider>().enabled = false;
	        gameObj.transform.GetComponent<Rigidbody>().isKinematic = true;
	    }
	
	    Invoke("Spawn", 2 - ScriptNiveis.SPAWNSPEED * 0.006f);
	}
}
