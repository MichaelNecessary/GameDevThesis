using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FurnaceManager : MonoBehaviour
{
    public InventorySlot fuelSlot;
    public InventorySlot oreSlot;
    public InventorySlot productSlot;
    public Item ironBarItem;
    public Item goldBarItem;
    public InventoryManager inventoryManager;
    public float smeltingTime = 10.0f;
    public InventoryText inventoryText;

    private int time;
    private int productAmount;
    private Item currentProductItem;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) 
        {
            StartSmelting();
        }
    }

    void StartSmelting()
    {
        string inventoryContents;
        if (CheckOreAndFuel(out inventoryContents))
        {
            StartCoroutine(Smelt());
        }
    }

    bool CheckOreAndFuel(out string inventoryContents)
    {
        inventoryContents = "";
        
        InventoryItem fuelSlotItem = fuelSlot.GetComponentInChildren<InventoryItem>();
        InventoryItem oreSlotItem = oreSlot.GetComponentInChildren<InventoryItem>();
        if (fuelSlotItem == null)
        {
            inventoryText.DisplayMessage("Brak opa³u");
        }
        if (oreSlotItem == null)
        {
            inventoryText.DisplayMessage("Brak ród  metalu");
        }

        if (oreSlotItem != null && fuelSlotItem != null)
        {
           
            if ((oreSlotItem.item.itemName == "IronOre" || oreSlotItem.item.itemName == "GoldOre") && fuelSlotItem.item.itemName == "Stick")
            {
                if (fuelSlotItem.count >= 3)
                {
                    productAmount = oreSlotItem.count;

                    if (oreSlotItem.count == 1)
                    {
                        time = 1;
                    }
                    else if (oreSlotItem.count == 2)
                    {
                        time = 2;
                    }
                    else if (oreSlotItem.count == 3)
                    {
                        time = 3;
                    }
                    else if (oreSlotItem.count == 4)
                    {
                        time = 4;
                    }

                    currentProductItem = oreSlotItem.item.itemName == "IronOre" ? ironBarItem : goldBarItem;

                    return true;
                }
                else
                {
                    inventoryText.DisplayMessage("Brak paliwa");
                    return false;
                }
            }
            else
            {
                inventoryText.DisplayMessage("Przedmiot nienadaje siê do przetopienia");
                return false;
            }
        }
        else
        {
            inventoryText.DisplayMessage("Brak przedmiotu");
            return false;
        }
    }

    IEnumerator Smelt()
    {
        InventoryItem fuelSlotItem = fuelSlot.GetComponentInChildren<InventoryItem>();
        InventoryItem oreSlotItem = oreSlot.GetComponentInChildren<InventoryItem>();
        if (fuelSlotItem == null || oreSlotItem == null)
        {
            inventoryText.DisplayMessage("Brak sk³adników");
            yield break;
        }

        for (int i = 0; i < time; i++)
        {
            yield return new WaitForSeconds(1);
            if (fuelSlotItem != null)
            {
                fuelSlotItem.count--;
                if (fuelSlotItem.count <= 0)
                {
                    Destroy(fuelSlotItem.gameObject);
                    fuelSlotItem = null; 
                }
                else
                {
                    fuelSlotItem.RefreshCount();
                }
            }
            if (oreSlotItem != null)
            {
                oreSlotItem.count--;
                if (oreSlotItem.count <= 0)
                {
                    Destroy(oreSlotItem.gameObject);
                    oreSlotItem = null; 
                }
                else
                {
                    oreSlotItem.RefreshCount();
                }
            }

            InventoryItem productItem = productSlot.GetComponentInChildren<InventoryItem>();
            if (productItem == null)
            {
                inventoryManager.SpawnNewItem(currentProductItem, productSlot);
                productItem = productSlot.GetComponentInChildren<InventoryItem>();
                productItem.count = 1;
                productItem.RefreshCount();
            }
            else
            {
                productItem.count++;
                productItem.RefreshCount();
                inventoryText.DisplaytSmeltItemMessage(productItem);
            }
        }
    }

    public void CreateNewObject()
    {
        StartSmelting();
    }
}
