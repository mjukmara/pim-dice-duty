using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachPoint : MonoBehaviour
{
    public List<Item> attachments = new List<Item>();


    public void Attach(Item item)
    {
        if (!item) return;

        item.transform.SetParent(transform);
        item.transform.localPosition = Vector3.zero;
        item.transform.localScale = Vector3.one;
        item.transform.localRotation = Quaternion.identity;
        attachments.Add(item);
    }

    public Item Detach(Item item)
    {
        if (!item) return null;

        Item foundItem = this.attachments.Find(it => it == item);
        if (foundItem == null) return null;

        this.attachments.Remove(foundItem);

        return foundItem;
    }

    public Item DetachFirst()
    {
        if (this.attachments.Count == 0) return null;

        Item foundItem = this.attachments[0];
        if (foundItem == null) return null;

        this.attachments.RemoveAt(0);

        return foundItem;
    }

    public Item DetachLast()
    {
        if (this.attachments.Count == 0) return null;

        Item foundItem = this.attachments[this.attachments.Count - 1];
        if (foundItem == null) return null;

        this.attachments.Remove(foundItem);

        return foundItem;
    }

    public List<Item> GetAttachments()
    {
        return this.attachments;
    }
}
