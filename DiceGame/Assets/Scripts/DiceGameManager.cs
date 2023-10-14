using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class DiceGameManager : MonoBehaviour
{
    public List<Dice> Dicelist;
    public DiceButton[] KeepDiceButtons;

    public AIActivationButton AIActivationButton;

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
        GoalGUIManager.Instance.HideAllUnclaimButtons();
        CheckRollsLeft();
        GoalGUIManager.Instance.ProtectButtons();
        AIActivationButton.ProtectButton();
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
        AIActivationButton.ReleaseButton();
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

    public void TurnAIOnOff()
    {
        AIActivationButton.TurnAIOnOff();
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
        bool stopChecking = false;

        //large straight
        if ((!stopChecking || !AIActivationButton.isAIOn) && !GoalGUIManager.Instance.goalButtons[(int)GoalGUIManager.ComboTypes.LargeStr].isClaim)
        {
            stopChecking = LargeStr(maxRun);
            if (stopChecking)
            {
                if (AIActivationButton.isAIOn)
                {
                    GoalGUIManager.Instance.TryClaimingLargeStraight();
                }
                else
                {
                    GoalGUIManager.Instance.EnableClaimingLargeStraight();
                }
            }
        }
        //small straight
        if ((!stopChecking || !AIActivationButton.isAIOn) && !GoalGUIManager.Instance.goalButtons[(int)GoalGUIManager.ComboTypes.SmallStr].isClaim)
        {
            stopChecking = SmallStr(maxRun);
            if (stopChecking)
            {
                if (AIActivationButton.isAIOn)
                {
                    GoalGUIManager.Instance.TryClaimingSmallStraight();
                }
                else
                {
                    GoalGUIManager.Instance.EnableClaimingSmallStraight();
                }
            }
        }
        //full house
        if ((!stopChecking || !AIActivationButton.isAIOn) && !GoalGUIManager.Instance.goalButtons[(int)GoalGUIManager.ComboTypes.FullHouse].isClaim)
        {
            stopChecking = FullHouse(threeKind, pairsList);
            if (stopChecking)
            {
                if (AIActivationButton.isAIOn)
                {
                    GoalGUIManager.Instance.TryClaimingFullHouse();
                }
                else
                {
                    GoalGUIManager.Instance.EnableClaimingFullHouse();
                }
            }
        }
        //four kind
        if ((!stopChecking || !AIActivationButton.isAIOn) && !GoalGUIManager.Instance.goalButtons[(int)GoalGUIManager.ComboTypes.FourKind].isClaim)
        {
            stopChecking = FourKind(fourKind);
            if (stopChecking)
            {
                if (AIActivationButton.isAIOn)
                {
                    GoalGUIManager.Instance.TryClaimingFourOfAKind();
                }
                else
                {
                    GoalGUIManager.Instance.EnableClaimingFourOfAKind();
                }
            }
        }
        //three kind
        if ((!stopChecking || !AIActivationButton.isAIOn) && !GoalGUIManager.Instance.goalButtons[(int)GoalGUIManager.ComboTypes.ThreeKind].isClaim)
        {
            stopChecking = ThreeKind(threeKind);
            if (stopChecking)
            {
                if (AIActivationButton.isAIOn)
                {
                    GoalGUIManager.Instance.TryClaimingThreeOfAKind();
                }
                else
                {
                    GoalGUIManager.Instance.EnableClaimingThreeOfAKind();
                }
            }
        }
        //two paris
        if ((!stopChecking || !AIActivationButton.isAIOn) && !GoalGUIManager.Instance.goalButtons[(int)GoalGUIManager.ComboTypes.TwoPairs].isClaim)
        {
            stopChecking = TwoPairs(pairsList);
            if (stopChecking)
            {
                if (AIActivationButton.isAIOn)
                {
                    GoalGUIManager.Instance.TryClaimingTwoPairs();
                }
                else
                {
                    GoalGUIManager.Instance.EnableClaimingTwoPairs();
                }
            }
        }

        if (!stopChecking && AIActivationButton.isAIOn)
        {
            //decide what to keep
            if(pairsList.Count == 2 && (
                //!GoalGUIManager.Instance.goalButtons[(int)GoalGUIManager.ComboTypes.TwoPairs].isClaim ||
                //!GoalGUIManager.Instance.goalButtons[(int)GoalGUIManager.ComboTypes.ThreeKind].isClaim ||
                //!GoalGUIManager.Instance.goalButtons[(int)GoalGUIManager.ComboTypes.FourKind].isClaim ||
                !GoalGUIManager.Instance.goalButtons[(int)GoalGUIManager.ComboTypes.FullHouse].isClaim
                ))
            {
                for (int index = 0; index < Dicelist.Count; index++)
                {
                    if (Dicelist[index].diceValue == pairsList[0] || Dicelist[index].diceValue == pairsList[1])
                    {
                        KeepDiceButtons[index].keepDice = true;
                    }
                }
            }else if (maxRun > 2 && (
                !GoalGUIManager.Instance.goalButtons[(int)GoalGUIManager.ComboTypes.SmallStr].isClaim ||
                !GoalGUIManager.Instance.goalButtons[(int)GoalGUIManager.ComboTypes.LargeStr].isClaim
                ))
            {
                while(startRun <= maxRun)
                {
                    int index = Dicelist.FindIndex(x => x.diceValue == startRun);
                    KeepDiceButtons[index].keepDice = true;
                    startRun++;
                }
            }else if(threeKind != 0 && !GoalGUIManager.Instance.goalButtons[(int)GoalGUIManager.ComboTypes.FourKind].isClaim)
            {
                for (int index = 0; index < Dicelist.Count; index++)
                {
                    if (Dicelist[index].diceValue == threeKind)
                    {
                        KeepDiceButtons[index].keepDice = true;
                    }
                }
            //The AI will keep pairs when at least one of the following combos are available. Two Pair, Three of a Kind, Four of a Kind, Full House.
            }
            else if (pairsList.Count == 1 && (
                !GoalGUIManager.Instance.goalButtons[(int)GoalGUIManager.ComboTypes.TwoPairs].isClaim ||
                !GoalGUIManager.Instance.goalButtons[(int)GoalGUIManager.ComboTypes.ThreeKind].isClaim ||
                !GoalGUIManager.Instance.goalButtons[(int)GoalGUIManager.ComboTypes.FourKind].isClaim ||
                !GoalGUIManager.Instance.goalButtons[(int)GoalGUIManager.ComboTypes.FullHouse].isClaim
                ))
            {
                for (int index = 0; index < Dicelist.Count; index++)
                {
                    if (Dicelist[index].diceValue == pairsList[0])
                    {
                        KeepDiceButtons[index].keepDice = true;
                    }
                }
            }
            else
            {
                //reset
            }
        }
        else
        {
            //reset
        }
    }

    bool LargeStr(int maxRun)
    {
        if(maxRun == 5)
        {
            return true;
        }
        return false;
    }

    bool SmallStr(int maxRun)
    {
        if (maxRun >= 4)
        {
            return true;
        }
        return false;
    }

    bool FullHouse(int threeKind, List<int> pairsList)
    {
        if (threeKind != 0 && pairsList.Count > 0)
        {
            return true;
        }
        return false;
    }

    bool FourKind(int threeKind)
    {
        if (threeKind != 0)
        {
            return true;
        }
        return false;
    }

    bool ThreeKind(int fourKind)
    {
        if (fourKind != 0)
        {
            return true;
        }
        return false;
    }

    bool TwoPairs(List<int> pairsList)
    {
        if (pairsList.Count >= 2)
        {
            return true;
        }
        return false;
    }
}
