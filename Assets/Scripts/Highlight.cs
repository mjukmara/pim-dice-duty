using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    public GameObject highlightObject;
    
    Inventory inventory;
    Chef chef;
    Item itemScript;

    void Start()
    {
        itemScript = GetComponent<Item>();

        highlightObject.SetActive(false);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
            Inventory inventory = player.GetComponent<Inventory>();
            if (inventory)
            {
                this.inventory = inventory;
            }
            Chef chef = player.GetComponent<Chef>();
            if (chef)
            {
                this.chef = chef;
            }
        }
    }

    void Update()
    {
        List<Item> items = inventory.GetItems();
        if (items.Count > 0)
        {
            Item otherItem = items[0];

            if (itemScript != otherItem) {
                bool canCookWithOtherItem = chef.CanCookWithItems(itemScript, otherItem);

                if (canCookWithOtherItem)
                {
                    highlightObject.SetActive(true);
                } else
                {
                    highlightObject.SetActive(false);
                }
            }
            else
            {
                highlightObject.SetActive(false);
            }
        }
        else
        {
            highlightObject.SetActive(false);
        }
    }
}
