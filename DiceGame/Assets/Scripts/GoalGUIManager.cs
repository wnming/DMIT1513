using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public bool isClaimed;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            isClaimed = false;
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
        ProtectRollDiceButton();
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
        ReleaseRollDiceButton();
    }

    public void ProtectRollDiceButton()
    {
        rollTheDice.GetComponent<Button>().interactable = false;
    }

    public void ReleaseRollDiceButton()
    {
        rollTheDice.GetComponent<Button>().interactable = true;
    }

    public void CheckClaimButtons()
    {
        StartCoroutine("CheckAllClaimButtons");
    }

    private IEnumerator CheckAllClaimButtons()
    {
        int count = 0;
        for (int index = 0; index < goalButtons.Length; index++)
        {
            if (goalButtons[index].isClaim)
            {
                count++;
            }
        }
        if (count == goalButtons.Length)
        {
            yield return new WaitForSeconds(1.0f);
            SceneManager.LoadScene("End");
        }
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

    public void SetIsClaimedToFalse()
    {
        isClaimed = false;
    }

    #region -- CLAIMING COMBOS

    /// Create logic in each section that prevents you from claiming the combination before it's valid.  
    /// 

    public void TryClaimingThreeOfAKind()
    {
        goalButtons[(int)ComboTypes.ThreeKind].Claim();
        HideAllUnclaimButtons();
        isClaimed = true;
        CheckClaimButtons();
    }

    public void TryClaimingFourOfAKind()
    {
        goalButtons[(int)ComboTypes.FourKind].Claim();
        HideAllUnclaimButtons();
        isClaimed = true;
        CheckClaimButtons();
    }

    public void TryClaimingSmallStraight()
    {
        goalButtons[(int)ComboTypes.SmallStr].Claim();
        HideAllUnclaimButtons();
        isClaimed = true;
        CheckClaimButtons();
    }

    public void TryClaimingLargeStraight()
    {
        goalButtons[(int)ComboTypes.LargeStr].Claim();
        HideAllUnclaimButtons();
        isClaimed = true;
        CheckClaimButtons();
    }

    public void TryClaimingTwoPairs()
    {
        goalButtons[(int)ComboTypes.TwoPairs].Claim();
        HideAllUnclaimButtons();
        isClaimed = true;
        CheckClaimButtons();
    }

    public void TryClaimingFullHouse()
    {
        goalButtons[(int)ComboTypes.FullHouse].Claim();
        HideAllUnclaimButtons();
        isClaimed = true;
        CheckClaimButtons();
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
