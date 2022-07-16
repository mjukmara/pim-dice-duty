using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chef : MonoBehaviour
{
    public CookBook cookBook;

    Inventory inventory;

    public void Start()
    {
        this.inventory = gameObject.GetComponent<Inventory>();
    }

    public Recipe TryCookAnyRecipe()
    {
        List<Recipe> possibleRecipes = CookBook.FindAllPossibleRecipes(this.cookBook, this.inventory.GetItems());
        if (possibleRecipes.Count == 0)
        {
            return null;
        }

        Recipe recipe = possibleRecipes[Random.Range(0, possibleRecipes.Count)];
        Debug.Log("Making recipe: " + recipe.label);

        foreach (Resource resource in recipe.consumes)
        {
            this.inventory.RemoveItem(resource);
        }

        foreach (Resource resource in recipe.produces)
        {
            this.inventory.AddItem(resource);
        }

		CameraManager.instance.Shake(0.1f, 0.2f);

        return recipe;
    }
}
