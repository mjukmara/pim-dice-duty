using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPoint : MonoBehaviour
{
    public class Item
    {
        public Resource resource;
        public GameObject displayPrefab;

        public Item(Resource resource, GameObject displayPrefab)
        {
            this.resource = resource;
            this.displayPrefab = displayPrefab;
        }
    }

    public Resource startResource;
    public List<Item> items = new List<Item>();

    void Start()
    {
        if (startResource)
        {
            items.Add(new Item(startResource, this.InstantiateDisplayPrefab(startResource)));
        }
    }

    private GameObject InstantiateDisplayPrefab(Resource resource)
    {
        GameObject prefab = Instantiate(resource.displayPrefab);
        prefab.transform.SetParent(transform);
        prefab.transform.localPosition = Vector3.zero;
        return prefab;
    }

    public bool AddResource(Resource resource)
    {
        if (!resource) return false;

        items.Add(new Item(resource, this.InstantiateDisplayPrefab(resource)));

        return true;
    }

    public Resource RemoveResource(Resource resource)
    {
        Item foundItem = this.items.Find(item => item.resource == resource);
        if (foundItem == null) return null;

        GameObject.Destroy(foundItem.displayPrefab);
        this.items.Remove(foundItem);

        return foundItem.resource;
    }

    public Resource PopResource()
    {
        if (this.items.Count == 0) return null;

        Item foundItem = this.items[this.items.Count - 1];
        GameObject.Destroy(foundItem.displayPrefab);
        this.items.Remove(foundItem);

        return foundItem.resource;
    }

    public List<Item> GetItems()
    {
        return this.items;
    }
}
