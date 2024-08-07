using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour
{
    public Transform player; // Odniesienie do transformacji gracza
    public Vector3 offset = new Vector3(0, 2, 0); // Przesunięcie od gracza

    private Camera mainCamera;
    private Text textComponent;

    private void Start()
    {
        mainCamera = Camera.main;
        textComponent = GetComponent<Text>();
    }

    private void Update()
    {
        // Aktualizacja pozycji tekstu, aby był nad graczem
        Vector3 worldPosition = player.position + offset;
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(worldPosition);

        // Ustawienie pozycji elementu UI Text
        textComponent.rectTransform.position = screenPosition;
    }
}
