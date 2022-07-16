using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltPickupPoint : PickupPoint
{
    public Belt belt;

    override public Resource PickupResource()
    {
        if (belt.IsMoving()) return null;
        if (!pickup) return null;

        AttachPoint attachPoint = belt.GetAttachPoint();
        return attachPoint.DetachLastResource();
    }

    override public bool DropOffResource(Resource resource)
    {
        if (belt.IsMoving()) return false;
        if (!dropoff) return false;

        AttachPoint attachPoint = belt.GetAttachPoint();
        attachPoint.AttachResource(resource);

        return true;
    }

    override public void AddResource(Resource resource)
    {
        AttachPoint attachPoint = belt.GetAttachPoint();
        attachPoint.AttachResource(resource);
    }

    override public Resource RemoveResource(Resource resource)
    {
        AttachPoint attachPoint = belt.GetAttachPoint();
        return attachPoint.DetachResource(resource);
    }

    override public Resource PopResource()
    {
        AttachPoint attachPoint = belt.GetAttachPoint();
        return attachPoint.DetachLastResource();
    }

    override public List<AttachPoint.Attachment> GetItems()
    {
        AttachPoint attachPoint = belt.GetAttachPoint();
        return attachPoint.GetAttachments();
    }

    override public List<Resource> GetResources()
    {
        AttachPoint attachPoint = belt.GetAttachPoint();
        return attachPoint.GetAttachmentsResources();
    }

    public override AttachPoint GetAttachPoint()
    {
        return belt.GetAttachPoint();
    }
    public override bool IsInteractable()
    {
        return belt.IsInteractable();
    }
}
