using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public Item item;
    private GameObject displayPrefab;
    
    AttachPoint attachPoint;

    private void OnEnable()
    {
        Inventory.onInventoryChange += OnInventoryChange;
    }

    private void OnDisable()
    {
        Inventory.onInventoryChange -= OnInventoryChange;
    }

    void Start()
    {
        attachPoint = GetComponent<AttachPoint>();
        if (item)
        {
            attachPoint.Attach(item);
        }
    }

    void OnInventoryChange(Inventory.ChangeType changeType, Item item)
    {
        switch (changeType)
        {
            case Inventory.ChangeType.ADD: AddItem(item); break;
            case Inventory.ChangeType.REMOVE: RemoveItem(item); break;
            default: break;
        }
    }

    public void AddItem(Item item)
    {
        this.item = item;
        attachPoint.Attach(item);
		SetHoldingSortingOrder(6);
    }

    public Item RemoveItem(Item item)
    {
        SetHoldingSortingOrder(3);
        this.item = null;
        return attachPoint.Detach(item);
    }

	GameObject GetItemGameObject()
	{
		return item ? item.gameObject : null;
	}

	void SetHoldingSortingOrder(int sortingOrder)
	{
        if (item)
        {
            SpriteRenderer sprite = GetItemGameObject()?.GetComponent<SpriteRenderer>();
            if (sprite)
            {
                sprite.sortingOrder = sortingOrder;
            }
        }
	}

    public Item PopItem()
    {
        this.item = null;
        return attachPoint.DetachLast();
    }

    public List<Item> GetItems()
    {
        return attachPoint.GetAttachments();
    }
}
