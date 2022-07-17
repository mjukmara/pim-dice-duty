using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPoint : MonoBehaviour
{
    public bool pickup = true;
    public bool dropoff = true;

    AttachPoint attachPoint;

    void Start()
    {
        attachPoint = GetComponent<AttachPoint>();
    }

    public virtual Item PickupItem()
    {
        if (!pickup) return null;

        return attachPoint.DetachLast();
    }

    public virtual bool DropOffItem(Item item)
    {
        if (!dropoff) return false;

        attachPoint.Attach(item);

        return true;
    }

    public virtual void AddItem(Item item)
    {
        attachPoint.Attach(item);
    }

    public virtual Item RemoveItem(Item item)
    {
        return attachPoint.Detach(item);
    }

    public virtual Item PopItem()
    {
        return attachPoint.DetachLast();
    }

    public virtual List<Item> GetItems()
    {
        return attachPoint.GetAttachments();
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
