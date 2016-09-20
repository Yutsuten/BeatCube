using UnityEngine;
using System.Collections;

public class ProjectileButton : MonoBehaviour 
{
    public GameObject shpere;

    private Transform spawnPoint;
    private float initialTime, finalTime;
    Vector2 initialTouchPosition, finalTouchPosition;
    private System.String[] sphereTags = { "BlueSphere", "RedSphere", "YellowSphere" };

    private static bool gameOn = true;
    private int buttonColor;
    

    void Start()
    {
        spawnPoint = transform.GetChild(0).transform;
        if (gameObject.name == "BlueButton")
            buttonColor = 1;
        else if(gameObject.name == "RedButton")
            buttonColor = 2;
        else //if (gameObject.name == "YellowButton")
            buttonColor = 3;
    }

    void OnMouseDown()
    {
        initialTime = Time.time;
        initialTouchPosition = Input.mousePosition;
    }

    void OnMouseUp()
    {
        // Check if it is not paused
        if (!gameOn)
            return;

        finalTime = Time.time;
        finalTouchPosition = Input.mousePosition;
        Vector2 touchPositionDifference = finalTouchPosition - initialTouchPosition;

        // Check if there were touch movement in a direction
        if (touchPositionDifference.y == 0 && touchPositionDifference.x == 0)
            return;

        float angle = CalculateAngle(touchPositionDifference);
        float distance = Mathf.Sqrt(Mathf.Pow(touchPositionDifference.x, 2) + Mathf.Pow(touchPositionDifference.y, 2));
        float timeDifference = finalTime - initialTime;

        // Check if the user moved enough pixels
        if (distance < 50)
            return;

        if (distance > 800)
            distance = 800;
        if (timeDifference < 0.15)
            timeDifference = 0.15f;

        float strength = distance / (timeDifference + 0.3f);

        if (strength < 550)
            strength = 550;
        else if (strength > 800)
            strength = 800;

        // Spawn point rotation to the desired direction
        spawnPoint.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        // Projectile instantiation
        GameObject projectile = Instantiate(shpere, spawnPoint.transform.position, Quaternion.identity) as GameObject;
        projectile.GetComponent<Projectile>().SphereColor(buttonColor);
        projectile.tag = sphereTags[buttonColor - 1];

        projectile.GetComponent<Rigidbody>().AddForce(spawnPoint.right * strength);
    }

    private float CalculateAngle(Vector2 touchPositionDifference)
    {
        float angle;
        if (touchPositionDifference.x == 0)
            angle = -90;
        else
        {
            angle = Mathf.Atan(touchPositionDifference.y / touchPositionDifference.x);
            angle = (180 * angle) / Mathf.PI;

            if (angle < 20 && angle >= 0)
                angle = 20;
            else if (angle > -20 && angle <= 0)
                angle = -20;
        }
        // If negative, let it positive
        if (angle < 0)
            angle += 180;

        return angle;
    }

    public static void DisableNewProjectiles(bool pause)
    {
        gameOn = !pause;
    }
}
