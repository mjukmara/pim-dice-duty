using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour
{
    public List<Resource> resources;
    public Resource combined;

    private Chef chef;

    void Start()
    {
        this.chef = GetComponent<Chef>();

        this.chef.TryCookAnyRecipe(resources);
    }
}
