using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPoint : MonoBehaviour
{
    public Resource resource;
    private GameObject displayPrefab;

    void Start()
    {
        this.InstantiateDisplayPrefab();
    }

    private void InstantiateDisplayPrefab()
    {
        if (!this.HasResource()) return;

        if (this.displayPrefab)
        {
            GameObject.Destroy(this.displayPrefab);
        }

        this.displayPrefab = Instantiate(this.resource.displayPrefab);
        this.displayPrefab.transform.SetParent(transform);
        this.displayPrefab.transform.localPosition = Vector3.zero;
    }

    public bool AddResource(Resource resource)
    {
        if (HasResource())
        {
            return false;
        }

        this.resource = resource;
        return true;
    }

    public bool HasResource()
    {
        return !!this.resource;
    }
}
