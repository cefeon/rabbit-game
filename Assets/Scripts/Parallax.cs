using UnityEngine;
using UnityEngine.UI;

public class Parallax : MonoBehaviour
{
    private float length;
    private float startingPosition;

    private GameObject gameCamera;

    public float parallaxEffect;

    private void Start()
    {
        startingPosition = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        gameCamera = GameObject.Find("Main Camera");
    }

    private void Update()
    {
        float distance = gameCamera.transform.position.x * parallaxEffect;
        
        Transform transform1 = transform;
        Vector3 position = transform1.position;
        position = new Vector3(startingPosition + distance, position.y, position.z);
        transform1.position = position;
        startingPosition = RecalculatePositionForInfinity(startingPosition);
    }

    private float RecalculatePositionForInfinity(float position)
    {
        if (gameCamera.transform.position.x * (1 - parallaxEffect) >= position + length)
        {
            return position = position + length;
        }
        
        if (gameCamera.transform.position.x * (1 - parallaxEffect) < position - length)
        {
            return position = position - length;
        }
        
        return startingPosition;
    }
}
