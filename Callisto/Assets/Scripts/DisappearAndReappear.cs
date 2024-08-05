using UnityEngine;

public class DisappearAndReappear : MonoBehaviour
{
    public float disappearDistance = 5f;
    private Transform player;
    private Renderer objectRenderer;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        objectRenderer = GetComponent<Renderer>();

    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        Debug.Log("Distance to player: " + distance);

        if (distance <= disappearDistance)
        {
            if (objectRenderer.enabled)
            {
                Debug.Log("Object is disappearing");
                objectRenderer.enabled = false;
            }
        }
        else
        {
            if (!objectRenderer.enabled)
            {
                Debug.Log("Object is reappearing");
                objectRenderer.enabled = true;
            }
        }
    }
}
