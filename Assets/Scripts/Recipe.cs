using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "ScriptableObjects/Recipe", order = 1)]
public class Recipe : ScriptableObject
{
    public string label;
    public List<Resource> consumes;
    public List<Resource> produces;

    public static bool IsCompositionOf(Recipe recipe, List<Resource> resources)
    {
        List<Resource> resourcesCopy = new List<Resource>(resources);
        bool isComposition = true;
        foreach (Resource resource in recipe.consumes)
        {
            Resource found = resourcesCopy.Find(r => r.label == resource.label);
            if (!found)
            {
                isComposition = false;
                break;
            }
            resourcesCopy.Remove(found);
        }
        return isComposition;
    }
}
