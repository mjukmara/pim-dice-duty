using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public Resource resource;
    private GameObject displayPrefab;

    private void OnEnable()
    {
        Inventory.onInventoryChange += OnInventoryChange;
    }

    private void OnDisable()
    {
        Inventory.onInventoryChange -= OnInventoryChange;
    }

    void Start()
    {
        if (this.HasResource())
        {
            this.InstantiateDisplayPrefab();
        }
    }

    void OnInventoryChange(Inventory.ChangeType changeType, Resource item)
    {
        Debug.Log("Handle inventory change");
        switch (changeType)
        {
            case Inventory.ChangeType.ADD: AddResource(item); break;
            case Inventory.ChangeType.REMOVE: RemoveResource(item); break;
            default: break;
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
        if (HasResource()) return false;

        this.resource = resource;
        this.InstantiateDisplayPrefab();

        return true;
    }

    public Resource RemoveResource(Resource resource)
    {
        if (resource != this.resource) return null;

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
