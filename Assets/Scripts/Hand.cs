using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public Resource resource;
    private GameObject displayPrefab;
    
    AttachPoint attachPoint;

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
        attachPoint = GetComponent<AttachPoint>();
        if (resource)
        {
            attachPoint.AttachResource(resource);
        }
    }

    void OnInventoryChange(Inventory.ChangeType changeType, Resource item)
    {
        switch (changeType)
        {
            case Inventory.ChangeType.ADD: AddResource(item); break;
            case Inventory.ChangeType.REMOVE: RemoveResource(item); break;
            default: break;
        }
    }

    public void AddResource(Resource resource)
    {
        attachPoint.AttachResource(resource);
    }

    public Resource RemoveResource(Resource resource)
    {
        return attachPoint.DetachResource(resource);
    }

    public Resource PopResource()
    {
        return attachPoint.DetachLastResource();
    }

    public List<AttachPoint.Attachment> GetItems()
    {
        return attachPoint.GetAttachments();
    }
}
