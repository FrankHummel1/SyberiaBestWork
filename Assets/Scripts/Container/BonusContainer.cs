using System.Collections.Generic;
using UnityEngine;

public class BonusContainer : MonoBehaviour
{
    [SerializeField] private List<DailyBonusRefs> bonuses;

    private Dictionary<string, DailyBonusRefs> _bonusesDictionary = new();

    public DailyBonusRefs GetBonusRefsById(string id) =>
        FilledBonusesDictionary()[id];

    public bool GetRandomDailyBonus(out DailyBonusRefs bonusRefs)
    {
        if(bonuses == null || bonuses.Count == 0)
        {
            bonusRefs = null;
            return false;
        }

        bonusRefs = bonuses[Random.Range(0, bonuses.Count)];
        return true;
    }

    private Dictionary<string, DailyBonusRefs> FilledBonusesDictionary()
    {
        if (_bonusesDictionary.Count == 0)
            FillBonusesDictionary(bonuses.Count);

        return _bonusesDictionary;
    }

    private void FillBonusesDictionary(int bonusesCount)
    {
        for (int index = 0; index < bonusesCount; index++)
            _bonusesDictionary.Add(bonuses[index].Id, bonuses[index]);
    }
}
