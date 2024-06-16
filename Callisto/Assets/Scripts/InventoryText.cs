using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class InventoryText : MonoBehaviour
{
    public Text itemAddedText;
    public void DisplayItemAddedMessage(Item item)
    {
        itemAddedText.text = $"Dodano {item.name} do ekwipunku!";
        StartCoroutine(ClearItemAddedMessage(4));
    }

    public void DisplayItemCreateMessage(Item item)
    {
        itemAddedText.text = $"Stworzono {item.name} przedmiot";
        DisplayItemAddedMessage(item);
    }

    public void DisplayItemNotCreateMessage(Item item)
    {
        itemAddedText.text = $"Item {item.name} nie zosta³ tworzony";
        StartCoroutine(ClearItemAddedMessage(4));
    }

    public void DisplayCounterMessage(Item item, int counter)
    {
        itemAddedText.text = $"Tworzony jest przedmiot {item.name} zostalo czasu: {counter}";
        StartCoroutine(ClearItemAddedMessage(1));
    }

    public void DisplayMessage(string str)
    {
        itemAddedText.text = str;
        StartCoroutine(ClearItemAddedMessage(10));
    }

    public void DisplaytSmeltItemMessage(InventoryItem item)
    {
        itemAddedText.text = $"Zosta³ stworzony {item.name}";
        StartCoroutine(ClearItemAddedMessage(2));
    }

    private IEnumerator ClearItemAddedMessage(int time)
    {
        yield return new WaitForSeconds(time);
        itemAddedText.text = "";
    }
}