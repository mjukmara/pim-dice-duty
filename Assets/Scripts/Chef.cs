using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chef : MonoBehaviour
{
    public GameObject itemPrefab;
    public List<CookBook> cookBooks;

    Inventory inventory;

    public delegate void OnCookedItem(Item item);
    public static OnCookedItem onCookedItem;

    public void Start()
    {
        this.inventory = gameObject.GetComponent<Inventory>();
    }

    public bool CanCookWithItems(Item item1, Item item2)
    {
        if (item1.type == Item.ItemType.DICE && item2.type == Item.ItemType.PLUS)
        {
            if (item1.color == item2.color)
            {
                int numberA = Item.numberMap[item1.number];
                if (numberA < 6)
                {
                    return true;
                }
            }
        }
        if (item1.type == Item.ItemType.PLUS && item2.type == Item.ItemType.DICE)
        {
            if (item1.color == item2.color)
            {
                int numberB = Item.numberMap[item2.number];
                if (numberB < 6)
                {
                    return true;
                }
            }
        }
        if (item1.type == Item.ItemType.PLUS && item2.type == Item.ItemType.DOT)
        {
            return true;
        }
        if (item1.type == Item.ItemType.DOT && item2.type == Item.ItemType.PLUS)
        {
            return true;
        }

        if (item1.type == Item.ItemType.DICE && item2.type == Item.ItemType.MINUS)
        {
            if (item1.color == item2.color)
            {
                int numberA = Item.numberMap[item1.number];
                if (numberA > 1)
                {
                    return true;
                }
            }
        }
        if (item1.type == Item.ItemType.MINUS && item2.type == Item.ItemType.DICE)
        {
            if (item1.color == item2.color)
            {
                int numberB = Item.numberMap[item2.number];
                if (numberB > 1)
                {
                    return true;
                }
            }
        }
        if (item1.type == Item.ItemType.MINUS && item2.type == Item.ItemType.DOT)
        {
            return true;
        }
        if (item1.type == Item.ItemType.DOT && item2.type == Item.ItemType.MINUS)
        {
            return true;
        }

        if (item1.type == Item.ItemType.DICE && item2.type == Item.ItemType.DICE)
        {
            if (item1.color == item2.color) {
                int numberA = Item.numberMap[item1.number];
                int numberB = Item.numberMap[item2.number];
                int sum = numberA + numberB;
                if (sum <= 6) return true;
            }
        }
        if (item1.type == Item.ItemType.DICE && item2.type == Item.ItemType.DOT)
        {
            return true;
        }
        if (item1.type == Item.ItemType.DOT && item2.type == Item.ItemType.DICE)
        {
            return true;
        }
        if (item1.type == Item.ItemType.DOT && item2.type == Item.ItemType.DOT)
        {
            if (item1.color == Item.ItemColor.WHITE) return true;
            if (item2.color == Item.ItemColor.WHITE) return true;

            if (item1.color == Item.ItemColor.YELLOW && item2.color == Item.ItemColor.RED) return true;
            if (item1.color == Item.ItemColor.RED && item2.color == Item.ItemColor.YELLOW) return true;

            if (item1.color == Item.ItemColor.YELLOW && item2.color == Item.ItemColor.BLUE) return true;
            if (item1.color == Item.ItemColor.BLUE && item2.color == Item.ItemColor.YELLOW) return true;

            if (item1.color == Item.ItemColor.RED && item2.color == Item.ItemColor.BLUE) return true;
            if (item1.color == Item.ItemColor.BLUE && item2.color == Item.ItemColor.RED) return true;
        }

        return false;
    }

    public GameObject TryCookWith(Item item1, Item item2)
    {
        if (item1.type == Item.ItemType.DICE && item2.type == Item.ItemType.PLUS)
        {
            if (item1.color == item2.color)
            {
                int numberA = Item.numberMap[item1.number];
                if (numberA < 6)
                {
                    GameObject itemObject = Instantiate(itemPrefab);
                    Item item = itemObject.GetComponent<Item>();
                    item.type = Item.ItemType.DICE;
                    item.number = (Item.ItemNumber)(numberA + 1 - 1);
                    item.color = item1.color;
                    onCookedItem?.Invoke(item);
                    return itemObject;
                }
            }
        }
        if (item1.type == Item.ItemType.PLUS && item2.type == Item.ItemType.DICE)
        {
            if (item1.color == item2.color)
            {
                int numberB = Item.numberMap[item2.number];
                if (numberB < 6)
                {
                    GameObject itemObject = Instantiate(itemPrefab);
                    Item item = itemObject.GetComponent<Item>();
                    item.type = Item.ItemType.DICE;
                    item.number = (Item.ItemNumber)(numberB + 1 - 1);
                    item.color = item2.color;
                    onCookedItem?.Invoke(item);
                    return itemObject;
                }
            }
        }
        if (item1.type == Item.ItemType.PLUS && item2.type == Item.ItemType.DOT)
        {
            GameObject itemObject = Instantiate(itemPrefab);
            Item item = itemObject.GetComponent<Item>();
            item.type = Item.ItemType.PLUS;
            item.number = item1.number;
            item.color = item2.color;
            onCookedItem?.Invoke(item);
            return itemObject;
        }
        if (item1.type == Item.ItemType.DOT && item2.type == Item.ItemType.PLUS)
        {
            GameObject itemObject = Instantiate(itemPrefab);
            Item item = itemObject.GetComponent<Item>();
            item.type = Item.ItemType.PLUS;
            item.number = item2.number;
            item.color = item1.color;
            onCookedItem?.Invoke(item);
            return itemObject;
        }

        if (item1.type == Item.ItemType.DICE && item2.type == Item.ItemType.MINUS)
        {
            if (item1.color == item2.color)
            {
                int numberA = Item.numberMap[item1.number];
                if (numberA > 1)
                {
                    GameObject itemObject = Instantiate(itemPrefab);
                    Item item = itemObject.GetComponent<Item>();
                    item.type = Item.ItemType.DICE;
                    item.number = (Item.ItemNumber)(numberA - 1 - 1);
                    item.color = item1.color;
                    onCookedItem?.Invoke(item);
                    return itemObject;
                }
            }
        }
        if (item1.type == Item.ItemType.MINUS && item2.type == Item.ItemType.DICE)
        {
            if (item1.color == item2.color)
            {
                int numberB = Item.numberMap[item2.number];
                if (numberB > 6)
                {
                    GameObject itemObject = Instantiate(itemPrefab);
                    Item item = itemObject.GetComponent<Item>();
                    item.type = Item.ItemType.DICE;
                    item.number = (Item.ItemNumber)(numberB - 1 - 1);
                    item.color = item2.color;
                    onCookedItem?.Invoke(item);
                    return itemObject;
                }
            }
        }
        if (item1.type == Item.ItemType.MINUS && item2.type == Item.ItemType.DOT)
        {
            GameObject itemObject = Instantiate(itemPrefab);
            Item item = itemObject.GetComponent<Item>();
            item.type = Item.ItemType.MINUS;
            item.number = item1.number;
            item.color = item2.color;
            onCookedItem?.Invoke(item);
            return itemObject;
        }
        if (item1.type == Item.ItemType.DOT && item2.type == Item.ItemType.MINUS)
        {
            GameObject itemObject = Instantiate(itemPrefab);
            Item item = itemObject.GetComponent<Item>();
            item.type = Item.ItemType.MINUS;
            item.number = item2.number;
            item.color = item1.color;
            onCookedItem?.Invoke(item);
            return itemObject;
        }

        if (item1.type == Item.ItemType.DICE && item2.type == Item.ItemType.DICE)
        {
            if (item1.color == item2.color)
            {
                int numberA = Item.numberMap[item1.number];
                int numberB = Item.numberMap[item2.number];
                int sum = numberA + numberB;
                if (sum <= 6)
                {
                    GameObject itemObject = Instantiate(itemPrefab);
                    Item item = itemObject.GetComponent<Item>();
                    item.type = Item.ItemType.DICE;
                    item.number = (Item.ItemNumber)(sum-1);
                    item.color = item1.color;
                    onCookedItem?.Invoke(item);
                    return itemObject;
                }
            }
        }
        if (item1.type == Item.ItemType.DICE && item2.type == Item.ItemType.DOT)
        {
            GameObject itemObject = Instantiate(itemPrefab);
            Item item = itemObject.GetComponent<Item>();
            item.type = Item.ItemType.DICE;
            item.number = item1.number;
            item.color = item2.color;
            onCookedItem?.Invoke(item);
            return itemObject;
        }
        if (item1.type == Item.ItemType.DOT && item2.type == Item.ItemType.DICE)
        {
            GameObject itemObject = Instantiate(itemPrefab);
            Item item = itemObject.GetComponent<Item>();
            item.type = Item.ItemType.DICE;
            item.number = item2.number;
            item.color = item1.color;
            onCookedItem?.Invoke(item);
            return itemObject;
        }
        if (item1.type == Item.ItemType.DOT && item2.type == Item.ItemType.DOT)
        {
            if (item1.color == Item.ItemColor.WHITE)
            {
                GameObject itemObject = Instantiate(itemPrefab);
                Item item = itemObject.GetComponent<Item>();
                item.type = Item.ItemType.DOT;
                item.number = item1.number;
                item.color = item2.color;
                onCookedItem?.Invoke(item);
                return itemObject;
            }
            if (item2.color == Item.ItemColor.WHITE)
            {
                GameObject itemObject = Instantiate(itemPrefab);
                Item item = itemObject.GetComponent<Item>();
                item.type = Item.ItemType.DOT;
                item.number = item2.number;
                item.color = item1.color;
                onCookedItem?.Invoke(item);
                return itemObject;
            }

            if (item1.color == Item.ItemColor.YELLOW && item2.color == Item.ItemColor.RED ||
                item1.color == Item.ItemColor.RED && item2.color == Item.ItemColor.YELLOW)
            {
                GameObject itemObject = Instantiate(itemPrefab);
                Item item = itemObject.GetComponent<Item>();
                item.type = Item.ItemType.DOT;
                item.number = item1.number;
                item.color = Item.ItemColor.ORANGE;
                onCookedItem?.Invoke(item);
                return itemObject;
            }

            if (item1.color == Item.ItemColor.YELLOW && item2.color == Item.ItemColor.BLUE ||
                item1.color == Item.ItemColor.BLUE && item2.color == Item.ItemColor.YELLOW)
            {
                GameObject itemObject = Instantiate(itemPrefab);
                Item item = itemObject.GetComponent<Item>();
                item.type = Item.ItemType.DOT;
                item.number = item1.number;
                item.color = Item.ItemColor.GREEN;
                onCookedItem?.Invoke(item);
                return itemObject;
            }

            if (item1.color == Item.ItemColor.RED && item2.color == Item.ItemColor.BLUE ||
                item1.color == Item.ItemColor.BLUE && item2.color == Item.ItemColor.RED)
            {
                GameObject itemObject = Instantiate(itemPrefab);
                Item item = itemObject.GetComponent<Item>();
                item.type = Item.ItemType.DOT;
                item.number = item1.number;
                item.color = Item.ItemColor.VIOLET;
                onCookedItem?.Invoke(item);
                return itemObject;
            }
        }

        return null;
    }
}
