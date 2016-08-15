using UnityEngine;
using System.Collections;

public class ScriptNiveis : MonoBehaviour 
{
	public static float SPAWNSPEED = 0.0f;

	private float difficulty;

    public float RetornaDificuldade()
    {
        return 0.01f * difficulty;
    }

	public void AtualizaLevel()
	{
		difficulty = 30.0f + GetComponent<ScriptPontuacao>().RetornaPontuacao() * 0.025f;
		if (SPAWNSPEED < 100.0f)
            SPAWNSPEED = GetComponent<ScriptPontuacao>().RetornaPontuacao() * 0.015f;
		else SPAWNSPEED = 100.0f;
	}

	public float CalculaRotacao(float rotacao)
	{
		float direcao;
		do
		{
			direcao = Random.Range(0, rotacao);
		} while (direcao * direcao > difficulty * 50);
		direcao -= rotacao;
		if (Random.Range(0, 2) == 0)
			direcao *= -1;
		return direcao;
	}

	public float CalculaIntensidade(float rotacao, float direcao, float minIntensity, float maxIntensity)
	{
		float intensidade;
		intensidade = Mathf.Sqrt(difficulty * 50.0f - (Mathf.Abs(direcao) - rotacao) * (Mathf.Abs(direcao) - rotacao)) + minIntensity;
		if (intensidade > maxIntensity)
			intensidade = maxIntensity;
		//print("Dificuldade: " + (((Mathf.Abs(direcao) - rotacao) * (Mathf.Abs(direcao) - rotacao) + (intensidade - minIntensity) * (intensidade - minIntensity))/50) + " - SpawnFrequency: " + (2 - SPAWNSPEED * 0.01));
		return intensidade;
	}
}
