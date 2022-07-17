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

    void Start()
    {
        timer = Time.time;
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

    void SpawnRandomItemObject()
    {
		AudioManager.Instance.PlaySfx("Spawn");
        AttachPoint targetBeltAttachPoint = targetBelt.GetAttachPoint();
        if (targetBeltAttachPoint)
        {

            if (Random.Range(0,5) == 0)
            {
                Item.ItemType type;
                if (Random.Range(0, 1) == 0)
                {
                    type = Item.ItemType.PLUS;
                } else
                {
                    type = Item.ItemType.MINUS;
                }
                GameObject itemObject = Instantiate(itemPrefab);
                Item item = itemObject.GetComponent<Item>();
                item.type = type;
                item.number = randomNumbers[Random.Range(0, randomNumbers.Count)];
                item.color = randomColors[Random.Range(0, randomColors.Count)];

                targetBeltAttachPoint.Attach(item);
                onItemSpawned?.Invoke(this, item);
            } else
            {
                GameObject itemObject = Instantiate(itemPrefab);
                Item item = itemObject.GetComponent<Item>();
                item.type = (Item.ItemType)Random.Range(0, 2);
                item.number = randomNumbers[Random.Range(0, randomNumbers.Count)];
                item.color = randomColors[Random.Range(0, randomColors.Count)];

                targetBeltAttachPoint.Attach(item);
                onItemSpawned?.Invoke(this, item);
            }
        }
    }
}
