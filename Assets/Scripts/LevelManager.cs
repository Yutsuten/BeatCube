using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour 
{
    private const float firstTargetTime = 2.0f;
	private const float spawnSpeed = 1.4f;

    private float minIntensity = 100.0f;
    private float maxIntensity = 300.0f;

    private Spawn spawn;

    void Start()
    {
        spawn = this.gameObject.GetComponent<Spawn>();
        Invoke("NextTarget", firstTargetTime);
    }

    private void NextTarget()
    {
        spawn.SpawnObject(Direction(), Velocity());
        Invoke("NextTarget", spawnSpeed);
    }

    private float Direction()
    {
        return Random.Range(-100, 100);
        //float direction;
        //direction = Random.Range(0, difficulty);
        //if (Random.Range(0, 2) == 0)
        //    direction *= -1;
        //return direction;
    }

    private float Velocity()
    {
        return Random.Range(70, 140);
        //float velocity;
        //velocity = Random.Range(0, Mathf.Sqrt(difficulty));
        //return velocity;
    }

	public void AtualizaLevel()
	{
		//difficulty = 30.0f + GetComponent<ScriptPontuacao>().RetornaPontuacao() * 0.025f;
	}
}
