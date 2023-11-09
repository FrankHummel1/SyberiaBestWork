using System;

public struct BonusData
{
    public string bonusId;
    public string startDate;

    public BonusData(string id, string date)
    {
        bonusId = id;
        startDate = date;
    }
}
