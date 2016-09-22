using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour 
{
    public GameObject cube;
    public GameObject item;

    private System.String[] cubeTags = { "BlueTarget", "RedTarget", "YellowTarget", "Untagged", "Item", "Item" };

    public void SpawnObject(float direction, float intensity)
    {
        if (Random.Range(0, 100) < 95)
            CreateTarget(cube, CubeColor(), direction, intensity);
        else
            CreateTarget(item, Random.Range(5, 7), direction, intensity);
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

    private void CreateTarget(GameObject targetType, int cubeColor, float direction, float intensity)
    {
        GameObject newTarget = Instantiate(targetType, new Vector3(Random.Range(-2.6f, 2.6f), 5.9f, -1f), Quaternion.identity) as GameObject;

        // Setting color properties
        newTarget.GetComponent<CubeBehaviour>().Color = cubeColor;
        newTarget.transform.tag = cubeTags[cubeColor - 1]; // Set the tag

        // Adding the velocity and instantiate
        //print("Direction: " + direction + "; Intensity: " + intensity);
        newTarget.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(direction, -intensity, 0));
    }
}
