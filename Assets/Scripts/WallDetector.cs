using UnityEngine;
using System.Collections;

public class WallDetector : MonoBehaviour
{
    // The object's rigidbody
    private Rigidbody objectRigidbody;

    // Wall
    private float wallDistance = 2.8f; // From center
    private bool hitRightWall = false;
    private bool hitLeftWall = false;

    protected void Start()
    {
        objectRigidbody = transform.GetComponent<Rigidbody>();
    }

    protected void Update()
    {
        CheckWallCollision();
    }

    private void CheckWallCollision()
    {
        // Hit right wall
        if (transform.position.x > wallDistance && !hitRightWall)
        {
            hitRightWall = true;
            HitWall();
        }
        // Hit left wall
        else if (transform.position.x < -wallDistance && !hitLeftWall)
        {
            hitLeftWall = true;
            HitWall();
        }
        // Between walls
        else if (transform.position.x > -wallDistance && transform.position.x < wallDistance)
        {
            hitRightWall = false;
            hitLeftWall = false;
        }
    }

    private void HitWall()
    {
        Vector3 projectileVelocity = objectRigidbody.velocity;
        projectileVelocity.x *= -1;
        objectRigidbody.velocity = projectileVelocity;
    }
}
