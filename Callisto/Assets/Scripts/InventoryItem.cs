using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public Image image;
    public Text countText;

    [HideInInspector]
    public Item item;

    [HideInInspector]
    public int count = 1;

    [HideInInspector]
    public Transform parentAfterDrag;

    private void Start()
    {
        parentAfterDrag = transform.parent;
    }

    public void InitialiseItem(Item newItem)
    {
        item = newItem;
        image.sprite = newItem.image;
        RefreshCount();
    }

    public void RefreshCount()
    {
        countText.text = count.ToString();
        countText.gameObject.SetActive(count > 1);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
        transform.localPosition = Vector3.zero;
    }

    public void OnDrop(PointerEventData eventData)
    {
        InventoryItem droppedItem = eventData.pointerDrag.GetComponent<InventoryItem>();
        if (droppedItem != null && droppedItem != this)
        {
            if (item.stackable && item.itemID == droppedItem.item.itemID)
            {
                // If both items are stackable and of the same type, combine them
                CombineItems(droppedItem);
            }
            else
            {
                // If items are not stackable or different types, swap them
                SwapItems(droppedItem);
            }
        }
    }

    private void CombineItems(InventoryItem droppedItem)
    {
        int totalItemCount = count + droppedItem.count;
        if (totalItemCount <= 4)
        {
            count = totalItemCount;
            RefreshCount();
            Destroy(droppedItem.gameObject);
        }
        else
        {
            count = 4;
            droppedItem.count = totalItemCount - 4;
            RefreshCount();
            droppedItem.RefreshCount();
        }
    }

    private void SwapItems(InventoryItem droppedItem)
    {
        // Swap item data
        Item tempItem = item;
        int tempCount = count;

        InitialiseItem(droppedItem.item);
        droppedItem.InitialiseItem(tempItem);

        count = droppedItem.count;
        droppedItem.count = tempCount;

        RefreshCount();
        droppedItem.RefreshCount();
    }
}
