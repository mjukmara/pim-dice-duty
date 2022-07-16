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
        public DiceItem diceItem;
        public ColorItem colorItem;
    }

    public List<DiceItem> dices;
    public List<ColorItem> colors;
    public Belt lastBeltReference;

    public GameObject dicePrefab;
    public float spawnSpeed = 1f;
    public float diceSpacing = 1f;
    public float animatorSpeed = 2f;

    List<Dice> diceInstances = new List<Dice>();

    private void OnEnable()
    {
        Belt.onBeltDestroyResource += OnBeltDestroyResource;
    }

    private void OnDisable()
    {
        Belt.onBeltDestroyResource -= OnBeltDestroyResource;
    }

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartCoroutine(RollDice(1));
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StartCoroutine(RollDice(2));
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            StartCoroutine(RollDice(3));
        }
    }

    void OnBeltDestroyResource(Belt belt, Resource resource)
    {
        // Debug.Log("Resources was destroyed: " + resource.label);
        if (diceInstances.Count > 0)
        {
            Dice firstDice = diceInstances[0];
            if (firstDice.diceItem.resource == resource)
            {
                Debug.Log("Same dice");
                DestroyFirstDie();
            }
            if (firstDice.colorItem.resource == resource)
            {
                Debug.Log("Same color");
                DestroyFirstDie();
            }
        }
    }

    void SpawnDie(Vector3 position)
    {
        DiceItem diceItem = dices[Random.Range(0, dices.Count)];
        ColorItem colorItem = colors[Random.Range(0, colors.Count)];

        GameObject diceInstance = Instantiate(dicePrefab, position, Quaternion.identity);
        diceInstance.transform.SetParent(transform);
        diceInstance.transform.localPosition = position;

        diceInstance.GetComponent<DiceGoalScript>().Set(colorItem.color, diceItem.digit);
        diceInstance.GetComponent<Animator>().speed = animatorSpeed;

        Dice dice = new Dice();
        dice.diceInstance = diceInstance;
        dice.diceItem = diceItem;
        dice.colorItem = colorItem;

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
