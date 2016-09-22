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
        if (timeNow <= 49) // easy
        {
            spawnSpeed = initialSpawnSpeed - (timeNow / 490); // spawn from 2.0f to 1.9f
        }
        else if (timeNow <= 169) // medium
        {
            spawnSpeed = initialSpawnSpeed - 0.1f * (Mathf.Sqrt(timeNow) - 6f); // spawn from 1.9f to 1.3f
        }
        else if (timeNow <= 1122) // hard
        {
            spawnSpeed = initialSpawnSpeed - (0.56f + 0.02f * (Mathf.Sqrt(timeNow) - 6f)); // spawn from 1.3f to 1.1f
        }
        else // insane
        {
            spawnSpeed = 1.05f;
        }
        Invoke("NextTarget", spawnSpeed);
    }

    private float Direction()
    {
        return Random.Range(-100, 100);
    }

    private float Velocity()
    {
        float velocity = 60 + 2.0f * Mathf.Sqrt(gameTime.TimeElapsed()) + Random.RandomRange(-5f, 10f);
        // if velocity gets too big, debuff it a little
        if (velocity > 150)
            velocity = 150 + ((velocity - 150) / 3);
        else if (velocity > 200)
            velocity = 200;
        return velocity;
    }
}
