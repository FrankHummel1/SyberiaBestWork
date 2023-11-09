using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewardWidget : MonoBehaviour
{
    [SerializeField] private Button claimButton; 
    [SerializeField] private TMP_Text claimButtonText;

    public Button ClaimButton => claimButton;

    public void SetActiveClaimButton(bool active) => claimButton.enabled = active;

    public void ChangeButtonText(string value) => claimButtonText.text = value;
}
