using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSequence : MonoBehaviour
{
    public GameObject dicePrefab;

    List<GameObject> diceInstances = new List<GameObject>();

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartCoroutine(RollDice(1));
        }    
    }

    void SpawnDie()
    {
        GameObject diceInstance = Instantiate(dicePrefab, transform.position, Quaternion.identity);

        diceInstance.GetComponent<DiceGoalScript>().Set(new Color(Random.value, Random.value, Random.value), Random.Range(1, 6));

        diceInstances.Add(diceInstance);
    }

    IEnumerator RollDice(int count)
    {
        for(int i=0; i<count; i++)
        {
            SpawnDie();
            yield return new WaitForSeconds(1);
        }
    }
}
