using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chef : MonoBehaviour
{
    public List<CookBook> cookBooks;

    Inventory inventory;

    public delegate void OnCookedRecipe(Recipe recipe);
    public static OnCookedRecipe onCookedRecipe;

    public void Start()
    {
        this.inventory = gameObject.GetComponent<Inventory>();
    }

    public bool CanCookWithExtraResources(List<Resource> resources)
    {
        List<Resource> available = new List<Resource>(this.inventory.GetItems());
        available.AddRange(resources);
        List<Recipe> possibleRecipes = new List<Recipe>();
        foreach(CookBook cookBook in cookBooks)
        {
            possibleRecipes.AddRange(CookBook.FindAllPossibleRecipes(cookBook, available));
        }
        return possibleRecipes.Count > 0;
    }

    public Recipe TryCookAnyRecipe()
    {
        List<Recipe> possibleRecipes = new List<Recipe>();
        foreach (CookBook cookBook in cookBooks)
        {
            possibleRecipes.AddRange(CookBook.FindAllPossibleRecipes(cookBook, this.inventory.GetItems()));
        }

        if (possibleRecipes.Count == 0)
        {
            return null;
        }

        Recipe recipe = possibleRecipes[Random.Range(0, possibleRecipes.Count)];

        foreach (Resource resource in recipe.consumes)
        {
            this.inventory.RemoveItem(resource);
        }

        foreach (Resource resource in recipe.produces)
        {
            this.inventory.AddItem(resource);
        }

        onCookedRecipe?.Invoke(recipe);

        return recipe;
    }
}
