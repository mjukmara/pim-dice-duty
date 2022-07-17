using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items;

    public enum ChangeType { ADD, REMOVE };

    public delegate void OnInventoryChange(ChangeType changeType, Item item);
    public static OnInventoryChange onInventoryChange;

    public void AddItem(Item item)
    {
        this.items.Add(item);
        onInventoryChange?.Invoke(ChangeType.ADD, item);
    }

    public void RemoveItem(Item item)
    {
        this.items.Remove(item);
        onInventoryChange?.Invoke(ChangeType.REMOVE, item);
    }

    public Item PopItem()
    {
        if (this.items.Count == 0) return null;
        Item temp = this.items[this.items.Count - 1];
        this.items.RemoveAt(this.items.Count - 1);
        onInventoryChange?.Invoke(ChangeType.REMOVE, temp);
        return temp;
    }

    public List<Item> GetItems()
    {
        return this.items;
    }
}
