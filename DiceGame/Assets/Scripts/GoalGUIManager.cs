using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalGUIManager : MonoBehaviour
{
    
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

    #region -- CLAIMING COMBOS

    /// Create logic in each section that prevents you from claiming the combination before it's valid.  
    /// 

    public void TryClaimingThreeOfAKind()
    {
        goalButtons[0].Claim();
    }

    public void TryClaimingFourOfAKind()
    {
        goalButtons[1].Claim();
    }

    public void TryClaimingSmallStraight()
    {
        goalButtons[2].Claim();
    }

    public void TryClaimingLargeStraight()
    {
        goalButtons[3].Claim();
    }

    public void TryClaimingTwoPairs()
    {
        goalButtons[4].Claim();
    }

    public void TryClaimingFullHouse()
    {
        goalButtons[5].Claim();
    }
    #endregion
}
