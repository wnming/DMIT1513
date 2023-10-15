using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class DiceGameManager : MonoBehaviour
{
    public List<Dice> Dicelist;
    public DiceButton[] KeepDiceButtons;

    [SerializeField] TMP_Dropdown[] specificResultDropdown;

    public AIActivationButton AIActivationButton;
    public SpecificResult SpecificResultButton;

    public List<CountDice> countDiceList;

    public bool isRolling;
    public bool isFirstTurn;
    public bool isSpecificResult;

    public static DiceGameManager Instance;

    public int rollCount = 0;
    public int score = 0;
    public int rollsLeft = 0;
    private int rollsMax = 3;

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
            HideInteractButton();
            HideSpecificResultDropdown();
            for (int index = 0; index < specificResultDropdown.Length; index++)
            {
                specificResultDropdown[index].options.Add(new TMP_Dropdown.OptionData() { text = "1" });
                specificResultDropdown[index].options.Add(new TMP_Dropdown.OptionData() { text = "2" });
                specificResultDropdown[index].options.Add(new TMP_Dropdown.OptionData() { text = "3" });
                specificResultDropdown[index].options.Add(new TMP_Dropdown.OptionData() { text = "4" });
                specificResultDropdown[index].options.Add(new TMP_Dropdown.OptionData() { text = "5" });
                specificResultDropdown[index].options.Add(new TMP_Dropdown.OptionData() { text = "6" });
            }
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
            StartCoroutine(RollAllDice());
        }
    }

    IEnumerator RollAllDice()
    {
        Debug.Log(GoalGUIManager.Instance.isClaimed);
        ShowInteractButton();
        isRolling = true;
        GoalGUIManager.Instance.HideAllUnclaimButtons();
        CheckRollsLeft();
        GoalGUIManager.Instance.SetIsClaimedToFalse();
        GoalGUIManager.Instance.ProtectButtons();
        DisableInteractButton();
        AIActivationButton.ProtectButton();
        SpecificResultButton.ProtectButton();
        if (SpecificResultButton.isSpecificResultOn && !AIActivationButton.isAIOn)
        {
            //produce specific result
            for (int index = 0; index < Dicelist.Count; index++)
            {
                if (!KeepDiceButtons[index].keepDice)
                {
                    Dicelist[index].ManualSpecificValue(specificResultDropdown[index].value + 1);
                }
            }
        }
        else
        {
            if (AIActivationButton.isAIOn)
            {
                DisableInteractButton();
            }
            for (int index = 0; index < Dicelist.Count; index++)
            {
                if (!KeepDiceButtons[index].keepDice)
                {
                    Dicelist[index].RollToRandomSide();
                }
            }
        }
        yield return new WaitForSeconds(0.65f);
        //check all combos
        CountTheDice();
        EvaluateDiceAndCombo();
        AIActivationButton.ReleaseButton();
        GoalGUIManager.Instance.ReleaseButtons();
        if (!AIActivationButton.isAIOn)
        {
            EnableInteractButton();
            SpecificResultButton.ReleaseButton();
        }
        isRolling = false;
        rollCount += 1;
        StatsGUI.Instance.UpdateStatsGUI();
    }

    void CheckRollsLeft()
    {
        rollsLeft -= 1;
        if (rollsLeft < 0 || (GoalGUIManager.Instance.isClaimed && !AIActivationButton.isAIOn))
        {
            foreach (var keep in KeepDiceButtons)
            {
                keep.ResetDice();
            }
            rollsLeft = rollsMax;
        }
    }

    public void ShowInteractButton()
    {
        for (int index = 0; index < KeepDiceButtons.Length; index++)
        {
            KeepDiceButtons[index].ShowButton();
        }
    }

    public void HideInteractButton()
    {
        for (int index = 0; index < KeepDiceButtons.Length; index++)
        {
            KeepDiceButtons[index].HideButton();
        }
    }

    public void DisableInteractButton()
    {
        for (int index = 0; index < KeepDiceButtons.Length; index++)
        {
            KeepDiceButtons[index].DisableInteractButton();
        }
    }

    public void EnableInteractButton()
    {
        for (int index = 0; index < KeepDiceButtons.Length; index++)
        {
            KeepDiceButtons[index].EnableInteractButton();
        }
    }

    public void TurnAIOnOff()
    {
        AIActivationButton.TurnAIOnOff();
        if (AIActivationButton.isAIOn)
        {
            DisableInteractButton();
            //disable specific result
            //ResetSpecificResult();
            SpecificResultButton.TurnSpecificResultOff();
            HideSpecificResultDropdown();
            SpecificResultButton.ProtectButton();
        }
        else
        {
            EnableInteractButton();
            //ShowSpecificResultDropdown();
            SpecificResultButton.ReleaseButton();
        }
    }

    public void TurnSpecificResultOnOff()
    {
        SpecificResultButton.TurnSpecificResultOnOff();
        if (SpecificResultButton.isSpecificResultOn)
        {
            ShowSpecificResultDropdown();
        }
        else
        {
            HideSpecificResultDropdown();
        }
    }

    public void ResetSpecificResult()
    {
        for(int index = 0; index < specificResultDropdown.Length; index++)
        {
            specificResultDropdown[index].value = 0;
        }
    }

    public void HideSpecificResultDropdown()
    {
        ResetSpecificResult();
        for (int index = 0; index < specificResultDropdown.Length; index++)
        {
            specificResultDropdown[index].gameObject.SetActive(false);
        }
    }

    public void ShowSpecificResultDropdown()
    {
        for (int index = 0; index < specificResultDropdown.Length; index++)
        {
            specificResultDropdown[index].gameObject.SetActive(true);
        }
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
                    Debug.Log("four");
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
            ResetAllKeepDice();
            //decide what to keep
            if (pairsList.Count == 2 && (
                !GoalGUIManager.Instance.goalButtons[(int)GoalGUIManager.ComboTypes.FullHouse].isClaim
                ))
            {
                for (int index = 0; index < Dicelist.Count; index++)
                {
                    if (Dicelist[index].diceValue == pairsList[0] || Dicelist[index].diceValue == pairsList[1])
                    {
                        KeepDiceButtons[index].ToggleDice();
                    }
                }
            }else if (maxRun > 2 && (
                !GoalGUIManager.Instance.goalButtons[(int)GoalGUIManager.ComboTypes.SmallStr].isClaim ||
                !GoalGUIManager.Instance.goalButtons[(int)GoalGUIManager.ComboTypes.LargeStr].isClaim
                ))
            {
                int countRun = startRun + maxRun;
                while (startRun < countRun)
                {
                    int index = Dicelist.FindIndex(x => x.diceValue == startRun);
                    KeepDiceButtons[index].ToggleDice();
                    startRun++;
                }
            }else if(threeKind != 0 && !GoalGUIManager.Instance.goalButtons[(int)GoalGUIManager.ComboTypes.FourKind].isClaim)
            {
                for (int index = 0; index < Dicelist.Count; index++)
                {
                    if (Dicelist[index].diceValue == threeKind)
                    {
                        KeepDiceButtons[index].ToggleDice();
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
                        KeepDiceButtons[index].ToggleDice();
                    }
                }
            }
            else
            {
                //reset
                rollsLeft = rollsMax;
                ResetAllKeepDice();
            }
        }
        else
        {
            //reset
            if(stopChecking && AIActivationButton.isAIOn)
            {
                //reset keep
                rollsLeft = rollsMax;
                ResetAllKeepDice();
            }
        }
    }

    void ResetAllKeepDice()
    {
        for (int index = 0; index < KeepDiceButtons.Length; index++)
        {
            KeepDiceButtons[index].ResetDice();
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
