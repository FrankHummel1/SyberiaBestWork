using System;
using UnityEngine;

public class SaveManager
{
    private BonusData _data;

    public SaveManager Initialize ()
    {
        LoadData();

        return this;
    }

    public BonusData GetData() => _data;

    public void SaveData() => 
        PlayerPrefs.SetString(Constants.PrefsKeys.LastTimeBonus, JsonUtility.ToJson(_data));


    public void ChangeBonusData(BonusData data) => 
        _data = data;

    public void LoadData()
    {
        string lastBonusTimeStr = PlayerPrefs.GetString(Constants.PrefsKeys.LastTimeBonus, string.Empty);

        if (string.IsNullOrEmpty(lastBonusTimeStr))
            _data = new BonusData(string.Empty, DateTime.MinValue.ToString());
        else
            _data = JsonUtility.FromJson<BonusData>(lastBonusTimeStr);
    }
}
