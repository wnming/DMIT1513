using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class DiceGameManager : MonoBehaviour
{
    public List<Dice> Dicelist;
    public DiceButton[] KeepDiceButtons;

    public List<CountDice> countDiceList;
    
    public bool isRolling;
    public bool isFirstTurn;

    public static DiceGameManager Instance;

    public int rollCount = 0;
    public int score = 0;
    public int rollsLeft = 0;
    private int rollsMax = 1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            rollCount = 0;
            score = 0;
            rollsLeft = rollsMax;
            isFirstTurn = true;
            countDiceList = new List<CountDice>()
            {
                new CountDice() { diceNumber = 1, count = 0},
                new CountDice() { diceNumber = 2, count = 0},
                new CountDice() { diceNumber = 3, count = 0},
                new CountDice() { diceNumber = 4, count = 0},
                new CountDice() { diceNumber = 5, count = 0},
                new CountDice() { diceNumber = 6, count = 0}
            };
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Roll()
    {
        if (!isRolling)
        {
            //RollAllDice();
            StartCoroutine(RollAllDice());
        }
    }

    IEnumerator RollAllDice()
    {
        isRolling = true;
        CheckRollsLeft();
        GoalGUIManager.Instance.ProtectButtons();
        for(int index = 0; index < Dicelist.Count; index++)
        {
            if (!KeepDiceButtons[index].keepDice)
            {
                Dicelist[index].RollToRandomSide();
            }
        }
        yield return new WaitForSeconds(0.55f);
        //check all combos
        CountTheDice();
        EvaluateDiceAndCombo();
        isRolling = false;
        GoalGUIManager.Instance.ReleaseButtons();
        rollCount += 1;
        StatsGUI.Instance.UpdateStatsGUI();
    }

    void CheckRollsLeft()
    {
        //rollsLeft -= 1;
        //if (rollsLeft < 0)
        //{
        //    foreach (var d in KeepDiceButtons)
        //    {
        //        d.ResetDice();
        //    }
        //    rollsLeft = rollsMax;
        //}
    }

    void CountTheDice()
    {
        //Debug.Log(string.Join(", ", countDiceList));
        foreach (CountDice cd in countDiceList)
        {
            //Debug.Log(string.Join(", ", cd.count));
            cd.count = Dicelist.Where(x => x.diceValue == cd.diceNumber)
            .Select(y => y).Count();
        }
    }

    void EvaluateDiceAndCombo()
    {
        //Straight
        int maxRun = 1;
        int startRun = 0;
        List<int> sequentialNumber = Dicelist.Select(x => x.diceValue).OrderBy(y => y).ToList();
        for (int index = 0; index < sequentialNumber.Count - 1; index++)
        {
            if (sequentialNumber[index + 1] - sequentialNumber[index] == 1)
            {
                startRun = maxRun == 1 ? sequentialNumber[index] : startRun;
                maxRun++;
            }
            else
            {
                //if the next to number is not the same, set maxRun = 0
                if(sequentialNumber[index + 1] - sequentialNumber[index] != 0)
                {
                    maxRun = 1;
                }
            }
        }
        //Debug.Log(string.Join(", ", Dicelist[0].diceValue));
        //Debug.Log(string.Join(", ", sequentialNumber));
        Debug.Log("maxRun: " + maxRun + " startRun: " + startRun);

        //pairs
        List<int> pairsList = countDiceList.Where(y => y.count == 2).Select(x => x.diceNumber).ToList();
        Debug.Log("pairs: " + pairsList.Count);

        //3 of a kind
        int threeKind = countDiceList.Where(y => y.count == 3).Select(x => x.diceNumber).FirstOrDefault();
        Debug.Log("threeKind: " + threeKind);

        //4 of a kind
        int fourKind = countDiceList.Where(y => y.count == 4).Select(x => x.diceNumber).FirstOrDefault();
        Debug.Log("fourKind: " + fourKind);

        //evaluate combo
        bool keepChecking = true;
        //large straight
        if (keepChecking && !GoalGUIManager.Instance.goalButtons[3].isClaim)
        {
            keepChecking = LargeStr(maxRun);
            if (!keepChecking)
            {
                GoalGUIManager.Instance.TryClaimingLargeStraight();
            }
        }
    }

    bool LargeStr(int maxRun)
    {
        if(maxRun == 5)
        {
            return false;
        }
        return true;
    }
}
