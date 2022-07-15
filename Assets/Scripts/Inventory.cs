using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Resource> items;

    public enum ChangeType { ADD, REMOVE };

    public delegate void OnInventoryChange(ChangeType changeType, Resource item);
    public static OnInventoryChange onInventoryChange;

    public void AddItem(Resource item)
    {
        this.items.Add(item);
        Debug.Log("Invoke inventory change: ADD");
        onInventoryChange?.Invoke(ChangeType.ADD, item);
    }

    public void RemoveItem(Resource item)
    {
        this.items.Remove(item);
        Debug.Log("Invoke inventory change: REMOVE");
        onInventoryChange?.Invoke(ChangeType.REMOVE, item);
    }

    public Resource PopItem()
    {
        if (this.items.Count == 0) return null;
        Resource temp = this.items[this.items.Count - 1];
        this.items.RemoveAt(this.items.Count - 1);
        onInventoryChange?.Invoke(ChangeType.REMOVE, temp);
        return temp;
    }

    public List<Resource> GetItems()
    {
        return this.items;
    }
}
