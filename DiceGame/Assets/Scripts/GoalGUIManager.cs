using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalGUIManager : MonoBehaviour
{
    public enum ComboTypes
    {
        ThreeKind,
        FourKind,
        SmallStr,
        LargeStr,
        TwoPairs,
        FullHouse
    }
    //[SerializeField]
    public ClaimButton[] goalButtons;
    public static GoalGUIManager Instance;
    [SerializeField] Button rollTheDice;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ProtectButtons()
    {
        foreach (ClaimButton button in goalButtons)
        {
            button.GetComponent<Button>().interactable = false;
        }
        rollTheDice.GetComponent<Button>().interactable = false;
    }
    public void ReleaseButtons()
    {
        foreach (ClaimButton button in goalButtons)
        {
            if (!button.isClaim) 
            {
                button.GetComponent<Button>().interactable = true;
            }
        }
        rollTheDice.GetComponent<Button>().interactable = true;
    }

    public void HideAllUnclaimButtons()
    {
        for(int index = 0; index < goalButtons.Length; index++)
        {
            if (!goalButtons[index].isClaim)
            {
                goalButtons[index].HideClaimButton();
            }
        }
    }

    #region -- CLAIMING COMBOS

    /// Create logic in each section that prevents you from claiming the combination before it's valid.  
    /// 

    public void TryClaimingThreeOfAKind()
    {
        goalButtons[(int)ComboTypes.ThreeKind].Claim();
        HideAllUnclaimButtons();
    }

    public void TryClaimingFourOfAKind()
    {
        goalButtons[(int)ComboTypes.FourKind].Claim();
        HideAllUnclaimButtons();
    }

    public void TryClaimingSmallStraight()
    {
        goalButtons[(int)ComboTypes.SmallStr].Claim();
        HideAllUnclaimButtons();
    }

    public void TryClaimingLargeStraight()
    {
        goalButtons[(int)ComboTypes.LargeStr].Claim();
        HideAllUnclaimButtons();
    }

    public void TryClaimingTwoPairs()
    {
        goalButtons[(int)ComboTypes.TwoPairs].Claim();
        HideAllUnclaimButtons();
    }

    public void TryClaimingFullHouse()
    {
        goalButtons[(int)ComboTypes.FullHouse].Claim();
        HideAllUnclaimButtons();
    }
    #endregion

    #region --SHOWING MATCH COMBO BUTTONS
    public void EnableClaimingThreeOfAKind()
    {
        goalButtons[(int)ComboTypes.ThreeKind].ShowClaimButton();
    }

    public void EnableClaimingFourOfAKind()
    {
        goalButtons[(int)ComboTypes.FourKind].ShowClaimButton();
    }

    public void EnableClaimingSmallStraight()
    {
        goalButtons[(int)ComboTypes.SmallStr].ShowClaimButton();
    }

    public void EnableClaimingLargeStraight()
    {
        goalButtons[(int)ComboTypes.LargeStr].ShowClaimButton();
    }

    public void EnableClaimingTwoPairs()
    {
        goalButtons[(int)ComboTypes.TwoPairs].ShowClaimButton();
    }

    public void EnableClaimingFullHouse()
    {
        goalButtons[(int)ComboTypes.FullHouse].ShowClaimButton();
    }
    #endregion
}
