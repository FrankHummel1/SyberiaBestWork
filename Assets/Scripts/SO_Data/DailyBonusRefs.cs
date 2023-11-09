using UnityEngine;

public abstract class DailyBonusRefs : ScriptableObject, IActivatable, IDeactivatable
{
    [SerializeField] private string id;
    [SerializeField] protected string bonusName;
    [SerializeField] private Sprite avatarBonus;
    [SerializeField] private float bonusTime;

    public string Id => id;
    public string BonusName => bonusName;
    public Sprite AvatarBonus => avatarBonus;
    public float BonusTime => bonusTime;

    public abstract void Activate();
    public abstract void Deactivate();
}
