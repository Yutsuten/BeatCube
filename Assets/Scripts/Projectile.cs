using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour 
{
    private Renderer projectileRenderer;
    private Color projectileColor, white;
    private Rigidbody rigidbody;

    // Wall
    private float wallDistance = 3.0f; // From center
    private bool hitRightWall = false;
    private bool hitLeftWall = false;

    void Start()
    {
        projectileRenderer = GetComponent<Renderer>();
        rigidbody = transform.GetComponent<Rigidbody>();
        white = new Color(1, 1, 1);
    }

    void Update()
    {
        CheckIfOnScreen();
        if (SpecialManager.SpecialActivated)
            PaintShpere(white);
        else
            PaintShpere(projectileColor);
    }

    private void CheckIfOnScreen()
    {
        // Hit right wall
        if (transform.position.x > wallDistance && !hitRightWall)
        {
            hitRightWall = true;
            hitLeftWall = false;
            HitWall();
        }
        // Hit left wall
        if (transform.position.x < -wallDistance && !hitLeftWall)
        {
            hitRightWall = false;
            hitLeftWall = true;
            HitWall();
        }
        // Did not hit a cube
        if (transform.position.y >= 8)
            Destroy(this.gameObject);
    }

    private void HitWall()
    {
        Vector3 projectileVelocity = rigidbody.velocity;
        projectileVelocity.x *= -1;
        rigidbody.velocity = projectileVelocity;
    }

    public void SphereColor(int colorId)
    {
        switch (colorId)
        {
            case 1:
                projectileColor = new Color(0.5f, 1, 1);
                break;
            case 2:
                projectileColor = new Color(1, 0.5f, 1);
                break;
            case 3:
                projectileColor = new Color(1, 1, 0.5f);
                break;
        }
    }

    private void PaintShpere(Color sphereColor)
    {
        projectileRenderer.material.color = sphereColor;
    }
}
