using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chef : MonoBehaviour
{
    public CookBook cookBook;

    public void TryCookAnyRecipe(List<Resource> resources)
    {
        List<Recipe> possibleRecipes = CookBook.FindAllPossibleRecipes(this.cookBook, resources);
        foreach(Recipe recipe in possibleRecipes)
        {
            Debug.Log("I can make recipe: " + recipe.label);
        }
    }
}
