using UnityEngine;
using TMPro;
using System.Collections;

public class PlayerItemTag : MonoBehaviour
{
    [Header("Tag Settings")]
    public string playerName = "Player"; // Initial player name to be displayed
    public Vector3 offset = new Vector3(0, 2, 0); // Position offset of the text relative to the player
    public float textSize = 1.0f; // Text size

    private Canvas canvas;
    private TextMeshProUGUI textMeshPro;

    void Start()
    {
        // Create a new Canvas object
        GameObject canvasObject = new GameObject("NameTagCanvas");
        canvasObject.transform.SetParent(transform); // Set as child of the player

        // Add a Canvas component
        canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;

        // Set the RectTransform for the Canvas
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        canvasRect.sizeDelta = new Vector2(200, 50); // Canvas size
        canvasRect.localPosition = offset;

        // Add a TextMeshPro component for displaying text
        GameObject textObject = new GameObject("TagText");
        textObject.transform.SetParent(canvasObject.transform);

        textMeshPro = textObject.AddComponent<TextMeshProUGUI>();
        textMeshPro.text = playerName; // Initial text is the player's name
        textMeshPro.fontSize = textSize;
        textMeshPro.alignment = TextAlignmentOptions.Center;

        // Set the RectTransform for the text
        RectTransform textRect = textObject.GetComponent<RectTransform>();
        textRect.sizeDelta = new Vector2(200, 50);
        textRect.localPosition = Vector3.zero;
    }

    void LateUpdate()
    {
        // Orient the text to face the camera
        if (Camera.main != null)
        {
            canvas.transform.LookAt(canvas.transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
        }
    }

    // Method to set the text to display the player's name
    public void ShowPlayerName()
    {
        textMeshPro.text = playerName;
    }

    // Method to display item information temporarily
    public void DisplayItemInfo(string itemName, float displayTime = 4f)
    {
        StartCoroutine(ShowTemporaryText(itemName, displayTime));
    }

    // Method to update the player's name
    public void UpdatePlayerName(string newName)
    {
        playerName = newName;
        ShowPlayerName(); // Immediately show the updated name
    }

    // Method to update the text with any string immediately
    public void UpdateText(string text)
    {
        textMeshPro.text = text;
    }

    // Coroutine to show item information for a specified duration
    private IEnumerator ShowTemporaryText(string itemName, float displayTime)
    {
        textMeshPro.text = itemName;
        yield return new WaitForSeconds(displayTime);
        ShowPlayerName(); // Revert to the player's name after the delay
    }
}
