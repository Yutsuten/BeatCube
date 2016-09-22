using UnityEngine;
using System.Collections;

public class ItemTextAnimation : MonoBehaviour
{
    private float birthTime;
    private float initialYposition;
    private const float textLifeSpan = 0.5f;
    private const float animationMovement = 0.8f;

	void Start ()
    {
        birthTime = Time.time;
        initialYposition = transform.position.y;
	}
	
	void Update ()
    {
        float lifeTime = Time.time - birthTime;
        if (lifeTime > textLifeSpan)
            Destroy(gameObject);
        transform.position = new Vector3(transform.position.x, initialYposition + (lifeTime / textLifeSpan) * animationMovement, transform.position.z);
	}

    public void SetText(string effectText)
    {
        GetComponent<TextMesh>().text = effectText;
    }
}
