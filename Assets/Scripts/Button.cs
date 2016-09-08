using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour 
{
    public GameObject shpere;
    public Transform spawnPoint;

    private float initialTime, finalTime;
    Vector2 initialTouchPosition, finalTouchPosition;
    private System.String[] sphereTags = { "BlueSphere", "RedSphere", "YellowSphere" };

    private bool gameOn = true;
    private int buttonColor;
    

    void Start()
    {
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
        if (gameOn)
        {
            finalTime = Time.time;

            finalTouchPosition = Input.mousePosition;

            Vector2 touchPositionDifference = finalTouchPosition - initialTouchPosition;
            if (touchPositionDifference.y != 0 || touchPositionDifference.x != 0)
            {
                float angle;
                if (touchPositionDifference.x == 0)
                {
                    angle = -90;
                }
                else
                {
                    angle = Mathf.Atan(touchPositionDifference.y / touchPositionDifference.x);
                    angle = (180 * angle) / Mathf.PI;

                    if (angle < 20 && angle >= 0)
                    {
                        angle = 20;
                    }
                    else if (angle > -20 && angle <= 0)
                    {
                        angle = -20;
                    }
                }

                float distancia = Mathf.Sqrt(Mathf.Pow(touchPositionDifference.x, 2) + Mathf.Pow(touchPositionDifference.y, 2));
                float tempo = finalTime - initialTime;

                if (distancia > 1500)
                {
                    distancia = 1500;
                }
                if (tempo < 0.08)
                {
                    tempo = 0.08f;
                }

                int forca = (int)(distancia / (tempo + 0.3f));

                if (forca < 600)
                {
                    forca = 600;
                }
                else if (forca > 2100)
                {
                    forca = 2100;
                }

                // Criacao da bola
                // Se angulo for positivo
                if (angle >= 0)
                {
                    spawnPoint.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                }
                // Se angulo for negativo
                else if (angle <= 0)
                {
                    spawnPoint.rotation = Quaternion.AngleAxis(180 + angle, Vector3.forward);
                }

                // Instanciar a bola
                GameObject bola = Instantiate(shpere, spawnPoint.transform.position, Quaternion.identity) as GameObject;
                bola.GetComponent<Projectile>().SphereColor(buttonColor);
                bola.tag = sphereTags[buttonColor - 1];
                GameObject.Find("GM").GetComponent<GameMananger>().AddObjectToList(bola);

                bola.GetComponent<Rigidbody>().AddForce(spawnPoint.right * forca);
            }
        }
    }

    public void PauseGame()
    {
        gameOn = false;
    }

    public void ResumeGame()
    {
        gameOn = true;
    }
}
