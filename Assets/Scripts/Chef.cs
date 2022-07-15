using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chef : MonoBehaviour
{
    public CookBook cookBook;

    public void TryCookAnyRecipe(List<Resource> resources)
    {
        List<Recipe> possibleRecipes = CookBook.FindAllPossibleRecipes(this.cookBook, resources);
        if (possibleRecipes.Count == 0)
        {
            Debug.Log("No recipes could be made!");
            return;
        }

        Recipe recipe = possibleRecipes[Random.Range(0, possibleRecipes.Count)];
        Debug.Log("I can make recipe: " + recipe.label);
    }
}
