using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Resource resource;

    void Start()
    {
        GameObject displayPrefab = Instantiate(this.resource.displayPrefab);
        displayPrefab.transform.SetParent(transform);
    }

    void Update()
    {
        
    }
}
