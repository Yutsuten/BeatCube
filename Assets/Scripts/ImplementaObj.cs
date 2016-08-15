using UnityEngine;
using System.Collections;

public class ImplementaObj : ObjAlvo, I_objAlvo {

    public GameObject QuadradoMaior;

    int corBola;
	// Use this for initialization
	/*void Start () 
    {
        metodoAcerto = "chamaAcerto";
        metodoIncremento = "chamaIncremento";
	}*/

    void chamaAcerto()
    {
        Acerto(auxCol);
    }

    void chamaIncremento()
    {
        Incremento(auxCol);
    }

    public void Acerto(Collider col)
    {
        //print("Acerto kraio");
        //Destroy(col.gameObject);
        //Destroy(this.gameObject);

        
        /*scripts.GetComponent<ScriptPontuacao>().AumentaPontuacao();
        scripts.GetComponent<ScriptVidas>().IncLife();
        scripts.GetComponent<ScriptPontuacao>().IncCombo();
        scripts.GetComponent<ScriptEspecial>().RecalculaFundo(1);*/
    }

    public void Incremento(Collider col)
    {
        //print("Erro imbecil");
        GameObject.Find("GM").GetComponent<Pontuacao>().ResetaCombo();


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
                transform.GetChild(j).GetComponent<ScriptCubo>().cubeColor = 4;
                // TESTANDO CIMA
                coordFora[j] = transform.GetChild(j).position;
                transform.GetChild(j).GetComponent<Rigidbody>().isKinematic = false;
                transform.GetChild(j).GetComponent<Rigidbody>().AddForce((coordFora[j] - centro) * Random.Range(150, 200));
                Destroy(transform.GetChild(j).gameObject, tExplosao);
                transform.GetChild(j).GetComponent<Rigidbody>().constraints = 0; // Eliminando o "travamento" da posicao em z
                transform.GetChild(j).GetComponent<Rigidbody>().AddTorque(Random.Range(minRotation, maxRotation), Random.Range(minRotation, maxRotation), Random.Range(minRotation, maxRotation));
                transform.GetChild(j).GetComponent<ScriptExplosao>().Destroi(tExplosao, transform.GetChild(j).localScale.x * 0.5f);
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
}
