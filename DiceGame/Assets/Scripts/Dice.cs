using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.SearchService;

public class Dice : MonoBehaviour
{
    //[HideInInspector]
    [SerializeField] Sprite[] DiceSprites;

    [SerializeField, Tooltip("The face value of the die.")]
    public int diceValue { get; set; }

    public void RollToRandomSide()
    {
        StartCoroutine("RandomSide");
    }


    private IEnumerator RandomSide()
    {
        int newValue = Random.Range(1, 6);
        for (int i = 0; i < 10; i++)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = DiceSprites[Random.Range(1, 6) - 1];
            yield return new WaitForSeconds(0.05f);
        }
        gameObject.GetComponent<SpriteRenderer>().sprite = DiceSprites[newValue - 1];
        diceValue = newValue;
    }

    public void ManualSpecificValue(int value)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = DiceSprites[value - 1];
        diceValue = value;
    }
}
