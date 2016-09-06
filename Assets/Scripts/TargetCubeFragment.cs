using UnityEngine;
using System.Collections;

public class TargetCubeFragment : MonoBehaviour 
{
    // Explosion
    private int minRotacao = 50;
    private int maxRotacao = 120;
    private float tempo;
    private float tempoExplosao = 1.5f;
    private float tamCuboInicial = 0.5f;
    private bool destruido = false;

    public void Destroi(float tempoExp, float tamCubo)
    {
        tempoExplosao = tempoExp;
        tamCuboInicial = tamCubo;
        destruido = true;
        tempo = Time.time;
    }

    private void DestroyAnimation()
    {
        // CALCULANDO O QUANTO IRA REDUZIR OS QUADRADOS MENORES
        float reducao = (Time.time - tempo) / (tempoExplosao * 2);
        // CASO VA DIMINUIR MAIS DO QUE O TAMANHO QUE JA TEM, TRATAR (DARIA NEGATIVO, MAS NAO SE PODE DEIXAR DAR NEGATIVO)
        if (reducao >= tamCuboInicial)
            reducao = tamCuboInicial;
        // REDUZINDO OS QUADRADOS MENORES
        float scale = tamCuboInicial - reducao;
        transform.localScale = new Vector3(scale, scale, scale);
        transform.GetComponent<Rigidbody>().AddTorque(Random.Range(minRotacao, maxRotacao), Random.Range(minRotacao, maxRotacao), Random.Range(minRotacao, maxRotacao));
    }

    public void PaintCube(Color cubeColor)
    {
        this.GetComponent<Renderer>().material.color = cubeColor;
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (destruido)
            DestroyAnimation();
	}
}
