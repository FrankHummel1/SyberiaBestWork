using UnityEngine;

public class Widget : WidgetBase
{
    [SerializeField] private GameObject root;
    
    public override void Activate() => SetActiveWidget(true);

    public override void Deactivate() => SetActiveWidget(false);

    private void SetActiveWidget(bool active) => root.SetActive(active);
}
