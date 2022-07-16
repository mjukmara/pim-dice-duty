using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPoint : MonoBehaviour
{
    public Resource startResource;
    public bool pickup = true;
    public bool dropoff = true;

    AttachPoint attachPoint;

    void Start()
    {
        attachPoint = GetComponent<AttachPoint>();
        if (startResource)
        {
            attachPoint.AttachResource(startResource);
        }
    }

    public virtual Resource PickupResource()
    {
        if (!pickup) return null;

        return attachPoint.DetachLastResource();
    }

    public virtual bool DropOffResource(Resource resource)
    {
        if (!dropoff) return false;

        attachPoint.AttachResource(resource);

        return true;
    }

    public virtual void AddResource(Resource resource)
    {
        attachPoint.AttachResource(resource);
    }

    public virtual Resource RemoveResource(Resource resource)
    {
        return attachPoint.DetachResource(resource);
    }

    public virtual Resource PopResource()
    {
        return attachPoint.DetachLastResource();
    }

    public virtual List<AttachPoint.Attachment> GetItems()
    {
        return attachPoint.GetAttachments();
    }

    public virtual List<Resource> GetResources()
    {
        return attachPoint.GetAttachmentsResources();
    }

    public virtual AttachPoint GetAttachPoint()
    {
        return attachPoint;
    }

    public virtual bool IsInteractable()
    {
        return true;
    }
}
