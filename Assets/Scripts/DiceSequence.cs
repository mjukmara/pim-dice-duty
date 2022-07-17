using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSequence : MonoBehaviour
{
    [System.Serializable]
    public class DiceItem
    {
        public int digit;
        public Resource resource;
    }
    [System.Serializable]
    public class ColorItem
    {
        public Color color;
        public Resource resource;
    }

    [System.Serializable]
    public class Dice
    {
        public GameObject diceInstance;
        public Item.ItemNumber number;
        public Item.ItemColor color;
        public Color spriteColor;
    }

    public List<DiceItem> dices;
    public List<ColorItem> colors;
    public Belt lastBeltReference;

    public GameObject dicePrefab;
    public float spawnSpeed = 1f;
    public float diceSpacing = 1f;
    public float animatorSpeed = 2f;

    public GameObject beltExitEmpty;

    List<Dice> diceInstances = new List<Dice>();

    private void OnEnable()
    {
        Belt.onBeltDestroyItem += OnBeltDestroyItem;
    }

    private void OnDisable()
    {
        Belt.onBeltDestroyItem -= OnBeltDestroyItem;
    }

    void Start()
    {
        StartCoroutine(RollDice(3));
    }

    void OnBeltDestroyItem(Belt belt, Item item)
    {
        if (diceInstances.Count > 0)
        {
            Dice firstDice = diceInstances[0];
            if (item.type == Item.ItemType.DICE && firstDice.number == item.number && firstDice.color == item.color)
            {
                int score = 0;
                switch (item.number)
                {
                    case Item.ItemNumber.ONE: score = 200; break;
                    case Item.ItemNumber.TWO: score = 150; break;
                    case Item.ItemNumber.THREE: score = 150; break;
                    case Item.ItemNumber.FOUR: score = 100; break;
                    case Item.ItemNumber.FIVE: score = 100; break;
                    case Item.ItemNumber.SIX: score = 50; break;
                }

                switch (item.color)
                {
                    case Item.ItemColor.WHITE:
                    case Item.ItemColor.YELLOW:
                    case Item.ItemColor.RED:
                    case Item.ItemColor.BLUE:
                        score *= 1;
                        break;
                    case Item.ItemColor.ORANGE:
                    case Item.ItemColor.VIOLET:
                    case Item.ItemColor.GREEN:
                        score *= 2;
                        break;
                }


				AudioManager.Instance.PlaySfx("LoseMultiplier");
                AudioManager.Instance.PlaySfx("TurnIn");
                CameraManager.instance.Shake(0.1f, 0.2f);
				Game.multiplier += 1;

                ScoreSpawner.SpawnScore(score, beltExitEmpty.transform.position);

                DestroyFirstDie();
            } else
            {
				AudioManager.Instance.PlaySfx("LoseMultiplier");
				Game.multiplier = 1;

                ScoreSpawner.SpawnScore(-50, beltExitEmpty.transform.position);
            }
        }

        if (diceInstances.Count == 0)
        {
            Game.level += 1;
            StartCoroutine(RollDice(3));
            ScoreSpawner.SpawnScore(1250, transform.position);
			AudioManager.Instance.PlaySfx("ClearLevel");
        }
    }

    void SpawnDie(Vector3 position)
    {

        // DiceItem diceItem = dices[Random.Range(0, dices.Count)];
        // ColorItem colorItem = colors[Random.Range(0, colors.Count)];

        int numberIndex = Random.Range(0, 6);
        int colorIndex = Random.Range(0, 7);
        Item.ItemNumber itemNumber = (Item.ItemNumber)numberIndex;
        Item.ItemColor itemColor = (Item.ItemColor)colorIndex;
        Color spriteColor = Item.colorMap[(Item.ItemColor)colorIndex];

        GameObject diceInstance = Instantiate(dicePrefab, position, Quaternion.identity);
        diceInstance.transform.SetParent(transform);
        diceInstance.transform.localPosition = position;

        diceInstance.GetComponent<DiceGoalScript>().Set(spriteColor, numberIndex);
        diceInstance.GetComponent<Animator>().speed = animatorSpeed;

        Dice dice = new Dice();
        dice.diceInstance = diceInstance;
        dice.number = itemNumber;
        dice.color = itemColor;
        dice.spriteColor = spriteColor;

        diceInstances.Add(dice);
    }

    IEnumerator RollDice(int count)
    {
        DestroyAllDice();

        float startXOffset = -(float)(count-1) / 2f;
        for(int i=0; i<count; i++)
        {
            Vector3 spawnPos = new Vector3(startXOffset + (float)i * diceSpacing, 0, 0);
            SpawnDie(spawnPos);
            yield return new WaitForSeconds(1f/spawnSpeed);
        }
    }

    void DestroyAllDice()
    {
        while (diceInstances.Count > 0)
        {
            DestroyFirstDie();
        }
    }

    void DestroyFirstDie()
    {
        if (diceInstances.Count == 0) return;
        Destroy(diceInstances[0].diceInstance);
        diceInstances.RemoveAt(0);
    }
}
