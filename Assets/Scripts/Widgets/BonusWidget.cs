using UnityEngine;
using UnityEngine.UI;

public class BonusWidget : Widget
{
    [SerializeField] private Button closeButton;
    [SerializeField] private Image icon;

    public Button CloseButton => closeButton;

    public void Activate(Sprite value)
    {
        icon.sprite = value;

        Activate();
    }
}
