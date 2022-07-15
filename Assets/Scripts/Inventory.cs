using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Resource> items;

    public void AddItem(Resource item)
    {
        this.items.Add(item);
    }

    public void RemoveItem(Resource item)
    {
        this.items.Remove(item);
    }

    public List<Resource> GetItems()
    {
        return this.items;
    }
}
