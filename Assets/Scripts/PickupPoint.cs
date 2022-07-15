using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPoint : MonoBehaviour
{
    public Resource resource;
    private GameObject displayPrefab;

    void Start()
    {
        if (this.HasResource()) {
            this.InstantiateDisplayPrefab();
        }
    }

    private void InstantiateDisplayPrefab()
    {
        if (!this.HasResource()) return;

        this.DestroyDisplayPrefab();

        this.displayPrefab = Instantiate(this.resource.displayPrefab);
        this.displayPrefab.transform.SetParent(transform);
        this.displayPrefab.transform.localPosition = Vector3.zero;
    }

    private void DestroyDisplayPrefab()
    {
        if (this.displayPrefab)
        {
            GameObject.Destroy(this.displayPrefab);
        }
    }

    public bool AddResource(Resource resource)
    {
        if (HasResource())  return false;

        this.resource = resource;
        this.InstantiateDisplayPrefab();

        return true;
    }

    public Resource RemoveResource()
    {
        Resource temp = this.resource;
        this.DestroyDisplayPrefab();
        this.resource = null;
        return temp;
    }

    public bool HasResource()
    {
        return !!this.resource;
    }
}
