using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour 
{
    public GameObject alvo;
    public GameObject item;

    private System.String[] cubeTags = { "Target Azul", "Target Vermelho", "Target Amarelo" };

    private GameMananger gameManager;

    private float timing;

	// Use this for initialization
	void Start () 
    {
        timing = 0;
        // Getting access to other scripts
        gameManager = this.gameObject.GetComponent<GameMananger>();
	}

    public void SpawnObject(float direction, float intensity)
    {
        if (Random.Range(0, 100) < 80)
            CreateCube(direction, intensity);
        else
            CreateItem(direction, intensity);
    }

    private int CubeColor()
    {
        int roulette = Random.Range(0, 100);
        if (roulette < 33)
            return 1;
        else if (roulette < 66)
            return 2;
        else
            return 3;
    }

    private void CreateCube(float direction, float intensity)
    {
        int cubeColor = CubeColor(); // Randomly determine the cube color

        GameObject newCube = Instantiate(alvo, new Vector3(Random.Range(-3.4f, 3.4f), 6.2f, 0), Quaternion.identity) as GameObject;
        gameManager.AddObjectToList(newCube);

        // Setting color properties
        for (int i = 0; i < newCube.transform.childCount; i += 1)
        {
            newCube.transform.GetChild(i).GetComponent<ScriptCubo>().cubeColor = cubeColor; // Attribute the color to child cubes
        }
        newCube.transform.tag = cubeTags[cubeColor - 1]; // Set the tag

        // Adding the velocity and instantiate
        print("Direction: " + direction + "; Intensity: " + intensity);
        newCube.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(direction, -intensity, 0));

        //obj.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-IntensidadeForca(0, 200, intensidade), IntensidadeForca(0, 200, intensidade)), -IntensidadeForca(100, 200, intensidade), 0));

        //gameObj.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-100, 100), -100, 0)); // <- Edit here!!!
    }

    public void CreateItem(float direction, float intensity)
    {
        GameObject newItem = Instantiate(item, new Vector3(Random.Range(-3.4f, 3.4f), 6.2f, 0), Quaternion.identity) as GameObject;
        gameManager.AddObjectToList(newItem);

        // Setting color properties (5, 6 or 7)
        for (int i = 0; i < newItem.transform.childCount; i += 1)
        {
            newItem.transform.GetChild(i).GetComponent<ScriptCubo>().cubeColor = 5;
        }
        newItem.transform.tag = "Item";

        // Adding the velocity and instantiate
        print("Direction: " + direction + "; Intensity: " + intensity);
        newItem.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(direction, -intensity, 0));
    }
}
