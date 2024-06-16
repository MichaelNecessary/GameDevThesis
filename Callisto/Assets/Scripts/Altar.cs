using System;
using System.Collections.Generic;
using UnityEngine;
public class Altar : MonoBehaviour
{
    public InventorySlot chestSlot;

    public InventoryText inventoryText;
    void Update()
    {
        CheckForGoldenFigure();
    }

    private void CheckForGoldenFigure()
    {
        
        InventoryItem itemInSlot = chestSlot.GetComponentInChildren<InventoryItem>();

        if (itemInSlot != null && itemInSlot.item != null)
        {
            if (itemInSlot.item.itemName == "Trophy")
            {

                inventoryText.DisplayMessage("Figurka jest w slocie");
            }
            else
            {
                inventoryText.DisplayMessage("Figurka nie znajduje się w slocie.");
            }
        }
        else
        {
            inventoryText.DisplayMessage("Slot jest pusty lub nie zawiera przedmiotu.");
        }
    }
}