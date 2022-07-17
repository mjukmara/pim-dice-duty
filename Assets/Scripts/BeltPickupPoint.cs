using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltPickupPoint : PickupPoint
{
    public Belt belt;

    override public Item PickupItem()
    {
        if (belt.IsMoving()) return null;
        if (!pickup) return null;

        AttachPoint attachPoint = belt.GetAttachPoint();
        return attachPoint.DetachLast();
    }

    override public bool DropOffItem(Item item)
    {
        if (belt.IsMoving()) return false;
        if (!dropoff) return false;

        AttachPoint attachPoint = belt.GetAttachPoint();
        attachPoint.Attach(item);

        return true;
    }

    override public void AddItem(Item item)
    {
        AttachPoint attachPoint = belt.GetAttachPoint();
        attachPoint.Attach(item);
    }

    override public Item RemoveItem(Item item)
    {
        AttachPoint attachPoint = belt.GetAttachPoint();
        return attachPoint.Detach(item);
    }

    override public Item PopItem()
    {
        AttachPoint attachPoint = belt.GetAttachPoint();
        return attachPoint.DetachLast();
    }

    override public List<Item> GetItems()
    {
        AttachPoint attachPoint = belt.GetAttachPoint();
        return attachPoint.GetAttachments();
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
