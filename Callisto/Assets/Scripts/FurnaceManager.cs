using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FurnaceManager : MonoBehaviour
{
    public InventorySlot fuelSlot;
    public InventorySlot oreSlot;
    public InventorySlot productSlot;
    public Item ironIngot;
    public Item goldIngot;
    public InventoryManager inventoryManager;
    public float smeltingTime = 10.0f;

    private int time;
    private int productAmount;
    private Item productItem;

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
        if (fuelSlot == null)
        {
            Debug.Log("Fuel slot is null");
            return false;
        }
        if (oreSlot == null)
        {
            Debug.Log("Ore slot is null");
            return false;
        }

        InventoryItem fuelSlotItem = fuelSlot.GetComponentInChildren<InventoryItem>();
        InventoryItem oreSlotItem = oreSlot.GetComponentInChildren<InventoryItem>();
        if (fuelSlotItem == null)
        {
            Debug.Log("Fuel slot item is missing");
        }
        if (oreSlotItem == null)
        {
            Debug.Log("Ore slot item is missing");
        }

        if (oreSlotItem != null && fuelSlotItem != null)
        {
            Debug.Log("Ore slot item name: " + oreSlotItem.item.itemName);
            Debug.Log("Fuel slot item name: " + fuelSlotItem.item.itemName);

            if ((oreSlotItem.item.itemName == "IronOre" || oreSlotItem.item.itemName == "GoldOre") && fuelSlotItem.item.itemName == "Stick")
            {
                if (fuelSlotItem.count >= 3)
                {
                    productAmount = (int)Mathf.Ceil(oreSlotItem.count * 0.5f);

                    if (oreSlotItem.count >= 3 && oreSlotItem.count < 7)
                    {
                        time = 5;
                    }
                    else if (oreSlotItem.count >= 7 && oreSlotItem.count < 10)
                    {
                        time = 10;
                    }
                    else if (oreSlotItem.count >= 10 && oreSlotItem.count < 20)
                    {
                        time = 15;
                    }
                    else if (oreSlotItem.count >= 20)
                    {
                        time = 20;
                    }

                    if (oreSlotItem.item.itemName == "IronOre")
                    {
                        productItem = ironIngot;
                    }
                    else if (oreSlotItem.item.itemName == "GoldOre")
                    {
                        productItem = goldIngot;
                    }

                    return true;
                }
                else
                {
                    Debug.Log("Not enough fuel");
                    return false;
                }
            }
            else
            {
                Debug.Log("Invalid items for smelting");
                return false;
            }
        }
        else
        {
            Debug.Log("Missing items");
            return false;
        }
    }

    IEnumerator Smelt()
    {
        InventoryItem fuelSlotItem = fuelSlot.GetComponentInChildren<InventoryItem>();
        InventoryItem oreSlotItem = oreSlot.GetComponentInChildren<InventoryItem>();
        if (fuelSlotItem == null || oreSlotItem == null)
        {
            Debug.Log("No fuel or ore");
            yield break;
        }

        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(time / 5);
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
            
            InventoryItem productSlotItem = productSlot.GetComponentInChildren<InventoryItem>();
            if (productSlotItem == null)
            {
                inventoryManager.SpawnNewItem(productItem, productSlot);
                productSlotItem = productSlot.GetComponentInChildren<InventoryItem>();
                productSlotItem.count = productAmount;
                productSlotItem.RefreshCount();
            }
            else
            {
                productSlotItem.count += productAmount;
                productSlotItem.RefreshCount();
            }
        }
    }

    public void CreateNewObject()
    {
        StartSmelting();
    }
}
