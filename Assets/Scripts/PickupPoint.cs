using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPoint : MonoBehaviour
{
    public Resource startResource;

    AttachPoint attachPoint;

    void Start()
    {
        attachPoint = GetComponent<AttachPoint>();
        if (startResource)
        {
            attachPoint.AttachResource(startResource);
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

    public List<Resource> GetResources()
    {
        return attachPoint.GetAttachmentsResources();
    }
}
