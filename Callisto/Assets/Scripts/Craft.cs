using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item item;
    public Chest chest;

    private Recipe pickaxeRockRecipe = new Recipe();
    private Recipe pickaxeIronRecipe = new Recipe();
    private Recipe axeRockRecipe = new Recipe();
    private Recipe axeIronRecipe = new Recipe();
    private Recipe goldFigureRecipe = new Recipe();
    private Recipe furnaceRecipe = new Recipe();

    public GameObject loadingSpinner;
    public float creationTime = 2f;

    void Start()
    {
        loadingSpinner.SetActive(false);
        InitializeRecipes();
    }

    private void InitializeRecipes()
    {
        pickaxeRockRecipe.ingredients = new List<Ingredient>
        {
            new Ingredient { name = "Stick", quantity = 2 },
            new Ingredient { name = "Rock", quantity = 2 }
        };

        pickaxeIronRecipe.ingredients = new List<Ingredient>
        {
            new Ingredient { name = "Stick", quantity = 2 },
            new Ingredient { name = "Iron", quantity = 3 }
        };

        axeRockRecipe.ingredients = new List<Ingredient>
        {
            new Ingredient { name = "Stick", quantity = 2 },
            new Ingredient { name = "Rock", quantity = 3 }
        };

        axeIronRecipe.ingredients = new List<Ingredient>
        {
            new Ingredient { name = "Stick", quantity = 2 },
            new Ingredient { name = "Iron", quantity = 3 }
        };

        goldFigureRecipe.ingredients = new List<Ingredient>
        {
            new Ingredient { name = "Gold", quantity = 5 }
        };

        furnaceRecipe.ingredients = new List<Ingredient>
        {
            new Ingredient { name = "Iron", quantity = 8 }
        };
    }

    public void StartCreatingItem()
    {
        StartCoroutine(CreateItem());
    }

    private IEnumerator CreateItem()
    {
        loadingSpinner.SetActive(true);
        yield return new WaitForSeconds(creationTime);
        loadingSpinner.SetActive(false);
        CompleteCrafting();
    }

    private void CompleteCrafting()
    {
        if (inventoryManager.IsInventoryFull() && chest.Openchest())
        {
            chest.AddItemToChest(item);
            Debug.Log($"{item.name} został stworzony w skrzynce");
        }
        else
        {
            inventoryManager.AddItem(item);
            Debug.Log($"{item.name} został stworzony");
        }
    }

    public void CraftItem(Recipe recipe)
    {
        bool success = inventoryManager.TryCraftItem(recipe);
        if (success)
        {
            StartCreatingItem();
        }
        else
        {
            Debug.Log($"{recipe.result.name} nie został stworzony");
        }
    }

    public void CraftFurnace()
    {
        CraftItem(furnaceRecipe);
    }

    public void CraftGoldFigure()
    {
        CraftItem(goldFigureRecipe);
    }

    public void CraftRockAxe()
    {
        CraftItem(axeRockRecipe);
    }

    public void CraftIronAxe()
    {
        CraftItem(axeIronRecipe);
    }

    public void CraftIronPickaxe()
    {
        CraftItem(pickaxeIronRecipe);
    }

    public void CraftRockPickaxe()
    {
        CraftItem(pickaxeRockRecipe);
    }
}
