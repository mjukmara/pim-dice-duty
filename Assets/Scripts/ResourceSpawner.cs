using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    public GameObject itemPrefab;
    public List<Resource> resources;
    public float spawnInterval = 5f;
    public Belt targetBelt;
    public List<Item.ItemNumber> randomNumbers;
    public List<Item.ItemColor> randomColors;

    float timer;
    public bool isSpawning = false;

    public delegate void OnItemSpawned(ResourceSpawner resourceSpawner, Item item);
    public static OnItemSpawned onItemSpawned;

    [System.Serializable]
    public class SetItem
    {
        public Item.ItemType type;
        public Item.ItemNumber number;
        public Item.ItemColor color;
    }

    public List<SetItem> currentSet = new List<SetItem>();

    void Start()
    {
        timer = Time.time;
        currentSet = NewSet();
    }

    void Update()
    {
        if (!isSpawning) return;

        timer += Time.deltaTime;
        if (timer > spawnInterval)
        {
            timer = 0;
            if (targetBelt)
            {
                SpawnRandomItemObject();
            }
        }
    }
    List<SetItem> LazySet()
    {
        if (currentSet.Count == 0)
        {
            currentSet = NewSet();
        }

        return currentSet;
    }

    List<SetItem> NewSet()
    {
        List<SetItem> set = new List<SetItem>();

        List<Item.ItemColor> diceColors = new List<Item.ItemColor>();
        diceColors.Add(Item.ItemColor.WHITE);
        diceColors.Add(Item.ItemColor.WHITE);
        diceColors.Add(Item.ItemColor.YELLOW);
        diceColors.Add(Item.ItemColor.YELLOW);
        diceColors.Add(Item.ItemColor.RED);
        diceColors.Add(Item.ItemColor.RED);
        diceColors.Add(Item.ItemColor.BLUE);
        diceColors.Add(Item.ItemColor.BLUE);

        for (int i=0; i<8; i++)
        {
            SetItem setItem = new SetItem();
            setItem.type = Item.ItemType.DICE;
            setItem.number = (Item.ItemNumber)Random.Range(0, 6);
            setItem.color = diceColors[i];
            set.Add(setItem);
        }

        List<Item.ItemColor> dotColors = new List<Item.ItemColor>();
        dotColors.Add(Item.ItemColor.WHITE);
        dotColors.Add(Item.ItemColor.YELLOW);
        dotColors.Add(Item.ItemColor.RED);
        dotColors.Add(Item.ItemColor.BLUE);

        for (int i = 0; i < dotColors.Count; i++)
        {
            SetItem setItem = new SetItem();
            setItem.type = Item.ItemType.DOT;
            setItem.number = Item.ItemNumber.ONE;
            setItem.color = dotColors[i];
            set.Add(setItem);
        }

        List<Item.ItemColor> effectColors = new List<Item.ItemColor>();
        dotColors.Add(Item.ItemColor.WHITE);
        dotColors.Add(Item.ItemColor.YELLOW);
        dotColors.Add(Item.ItemColor.RED);
        dotColors.Add(Item.ItemColor.BLUE);

        {
            SetItem setItem = new SetItem();
            setItem.type = Item.ItemType.PLUS;
            setItem.number = Item.ItemNumber.ONE;
            setItem.color = effectColors[Random.Range(0, effectColors.Count)];
            set.Add(setItem);
        }

        {
            SetItem setItem = new SetItem();
            setItem.type = Item.ItemType.MINUS;
            setItem.number = Item.ItemNumber.ONE;
            setItem.color = effectColors[Random.Range(0, effectColors.Count)];
            set.Add(setItem);
        }

        return set;
    }

    void SpawnRandomItemObject()
    {
		AudioManager.Instance.PlaySfx("Spawn");
        AttachPoint targetBeltAttachPoint = targetBelt.GetAttachPoint();
        if (targetBeltAttachPoint)
        {
            LazySet();
            int randomIndex = Random.Range(0, currentSet.Count);
            SetItem setItem = currentSet[randomIndex];
            currentSet.RemoveAt(randomIndex);

            GameObject itemObject = Instantiate(itemPrefab);
            Item item = itemObject.GetComponent<Item>();
            item.type = setItem.type;
            item.number = setItem.number;
            item.color = setItem.color;

            targetBeltAttachPoint.Attach(item);
            onItemSpawned?.Invoke(this, item);
        }
    }
}
