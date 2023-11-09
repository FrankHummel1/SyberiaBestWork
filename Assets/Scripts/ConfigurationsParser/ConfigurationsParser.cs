using UnityEngine;
public class ConfigurationsParser
{
    public bool IsReleaseBuild()
    {
        TextAsset configText = Resources.Load<TextAsset>(Constants.PrefsKeys.ConfigPathJson);

        if (configText != null)
        {
            try
            {
                ConfigData configData = JsonUtility.FromJson<ConfigData>(configText.text);

                if (System.Enum.TryParse<BuildType>(configData.DailyBonusType, out BuildType result))
                    return result == BuildType.Release;

                return false;
            }
            catch
            {
                Debug.LogError("Cant parse config data");
            }
        }

        return false;
    }
}
