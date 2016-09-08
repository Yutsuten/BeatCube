using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour 
{
    int color;

    public void PaintShpere(int color)
    {
        this.color = color;
        if (color == 1)
        {
            GetComponent<Renderer>().material.color = new Color(0.5f, 1, 1);
        }
        else if(color == 2)
        {
            GetComponent<Renderer>().material.color = new Color(1, 0.5f, 1);
        }
        else //if (color == 3)
        {
            GetComponent<Renderer>().material.color = new Color(1, 1, 0.5f);
        }
    }

    void Update()
    {
        if (!SpecialManager.SpecialActivated)
        {
            if (color == 1)
            {
                GetComponent<Renderer>().material.color = new Color(GameObject.Find("Background").GetComponent<BackgroundAnimation>().DevolveCor().g * 1.3f, 1, 1);
            }
            else if (color == 2)
            {
                GetComponent<Renderer>().material.color = new Color(1, GameObject.Find("Background").GetComponent<BackgroundAnimation>().DevolveCor().g * 0.3f, 1);
            }
            else if (color == 3)
            {
                GetComponent<Renderer>().material.color = new Color(1, 1, GameObject.Find("Background").GetComponent<BackgroundAnimation>().DevolveCor().g * 0.3f);
            }
        }
    }

    void OnTriggerEnter(Collider Col)
    {
        // When hitting on the wall
        if (Col.gameObject.tag.Equals("Parede"))
        {
            Vector3 vetorAux = transform.GetComponent<Rigidbody>().velocity;
            vetorAux.x *= -1;
            transform.GetComponent<Rigidbody>().velocity = vetorAux;
        }
        // If, form some reason, the cube is going up instead of down 
        else if (Col.gameObject.tag.Equals("Teto"))
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        GameObject.Find("GM").GetComponent<GameMananger>().RemoveObjectFromList(this.gameObject);
    }
}
