using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour 
{
    private const float firstTargetTime = 2.0f;
	private const float spawnSpeed = 1.4f;

    private float minIntensity = 100.0f;
    private float maxIntensity = 300.0f;

    private Spawn spawn;
    private GameTime gameTime;

    void Start()
    {
        spawn = this.gameObject.GetComponent<Spawn>();
        gameTime = GameObject.Find("PainelCompleto/Tempo").GetComponent<GameTime>();

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
    }

    private float Velocity()
    {
        float velocity = 60 + Mathf.Sqrt(gameTime.TimeElapsed()) + Random.RandomRange(0f, 10f);
        // if velocity gets too big, debuff it a little
        if (velocity > 150)
            velocity = 150 + ((velocity - 150) / 3);
        return Random.Range(70, 140);
    }
}
