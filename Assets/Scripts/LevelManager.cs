using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour 
{
    private const float firstTargetTime = 2.0f;

    private float initialSpawnSpeed = 2.0f;

    private Spawn spawn;
    private GameTime gameTime;

    void Start()
    {
        spawn = this.gameObject.GetComponent<Spawn>();
        gameTime = GameObject.Find("UserInterface/Time").GetComponent<GameTime>();

        Invoke("NextTarget", firstTargetTime);
    }

    private void NextTarget()
    {
        spawn.SpawnObject(Direction(), Velocity());

        float timeNow = 2 * gameTime.TimeElapsed();
        float spawnSpeed;
        // Difficulties
        if (timeNow <= 90) // easy
            spawnSpeed = initialSpawnSpeed - (timeNow / 112.5f); // spawn from 2.0f to 1.2f
        /*else if (timeNow <= 120) // medium
            spawnSpeed = initialSpawnSpeed - 0.1f * (Mathf.Sqrt(timeNow) - 6f);*/ // spawn from 1.9f to 1.3f
        else if (timeNow <= 150) // medium
            spawnSpeed = 1.15f;
        else if (timeNow <= 240) // hard
            spawnSpeed = 1.00f;
        else if (timeNow <= 330) // very hard
            spawnSpeed = 0.90f;
        else if (timeNow <= 430) // insane
            spawnSpeed = 0.80f;
        else if (timeNow <= 600) // impossible
            spawnSpeed = 0.70f;
        else if (timeNow <= 720) // inhumane
            spawnSpeed = 0.60f;
        else // god
            spawnSpeed = 0.55f;
        Invoke("NextTarget", spawnSpeed);
    }

    private float Direction()
    {
        return Random.Range(-100, 100);
    }

    private float Velocity()
    {
        float velocity = 60 + 2.5f * Mathf.Sqrt(4 * gameTime.TimeElapsed()) + Random.Range(-5f, 10f);
        // if velocity gets too big, debuff it a little
        if (velocity > 150)
            velocity = 150 + ((velocity - 150) / 3);
        else if (velocity > 200)
            velocity = 200;
        return velocity;
    }
}
