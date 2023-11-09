using UnityEngine;

[CreateAssetMenu(fileName = "Bonus", menuName = "Bonuses/Bonus")]
public class StandartBonus : DailyBonusRefs
{
    public override void Activate() => Debug.Log($"{bonusName} bonus activated");
    public override void Deactivate() => Debug.Log($"{bonusName} bonus deactivated");
}
