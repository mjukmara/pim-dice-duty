using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "CookBook", menuName = "ScriptableObjects/CookBook", order = 1)]
public class CookBook : ScriptableObject
{
    public string label;
    public List<Recipe> recipes;

    public static bool ContainsRecipe(CookBook cookBook, Recipe recipe)
    {
        return cookBook.recipes.FindAll(r => r.label == recipe.label).Count > 0;
    }

    public static List<Recipe> FindRecipesByLabel(CookBook cookBook, string label)
    {
        return cookBook.recipes.FindAll(recipe => recipe.label == label);
    }

    public static List<Recipe> FindAllPossibleRecipes(CookBook cookBook, List<Resource> resources)
    {
        List<Recipe> possibleRecipes = new List<Recipe>();

        foreach (Recipe cookBookRecipe in cookBook.recipes)
        {
            List<Resource> resourcesCopy = new List<Resource>(resources);
            bool earlyExit = false;
            foreach (Resource cookBookRecipeResource in cookBookRecipe.consumes)
            {
                Resource found = resourcesCopy.Find(resource => resource.label == cookBookRecipeResource.label);
                if (!found)
                {
                    earlyExit = true;
                    break;
                }
                resourcesCopy.Remove(found);
            }
            if (!earlyExit) {
                possibleRecipes.Add(cookBookRecipe);
            }
        }
        return possibleRecipes;
    }
}
