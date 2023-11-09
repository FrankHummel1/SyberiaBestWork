using System;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class BonusController : MonoBehaviour
{
    [SerializeField] private BonusContainer bonusContainer;
    [SerializeField] private DailyRewardWidget widget;
    [SerializeField] private BonusWidget bonusWidget;
    [SerializeField] private int testBuildTimeMultiplier;

    private SaveManager _saveManager;
    private Timer _timer;
    private ConfigurationsParser _configurationsParser;

    private void Start() => Initialization();

    private void OnEnable()
    {
        widget.ClaimButton.onClick.AddListener(GetBonus);
        bonusWidget.CloseButton.onClick.AddListener(DeactivateBonusWidget);
    }

    private void OnDisable()
    {
        widget.ClaimButton.onClick.RemoveListener(GetBonus);
        bonusWidget.CloseButton.onClick.RemoveListener(DeactivateBonusWidget);
    }

    private void OnApplicationQuit() => _saveManager.SaveData();

    private void Initialization()
    {
        _saveManager = new SaveManager().Initialize();
        _timer = new Timer();
        _configurationsParser = new ConfigurationsParser();

        DateTime bonusActivationDate = DateTime.Parse(_saveManager.GetData().startDate);
        if (bonusActivationDate.AddDays(1) <= DateTime.Now)
        {
            ChangeWidgetText(Constants.TextKeys.Claim);
        }
        else
        {
            string bonusId = _saveManager.GetData().bonusId;

            TimeSpan dailyRewardDelay = bonusActivationDate.AddDays(1) - DateTime.Now;
            StartRewardCountdown((float)dailyRewardDelay.TotalSeconds);

            DailyBonusRefs bonusRefs = bonusContainer.GetBonusRefsById(bonusId);
            if (bonusActivationDate.AddSeconds(bonusRefs.BonusTime) > DateTime.Now)
            {
                bonusRefs.Activate();
                TimeSpan duration = bonusActivationDate.AddSeconds(bonusRefs.BonusTime) - DateTime.Now;
                StartCountDown((float)duration.TotalSeconds, () => { DeactivateBonus(bonusId); }); 
            }
        }
    }

    private void GetBonus()
    {
        if (bonusContainer.GetRandomDailyBonus(out DailyBonusRefs bonusRefs))
        {
            bonusRefs.Activate();

            _saveManager.ChangeBonusData(new BonusData(bonusRefs.Id, DateTime.Now.ToString()));

            StartRewardCountdown(Constants.Values.SecondsInDays);
            StartCountDown(bonusRefs.BonusTime, () => { DeactivateBonus(bonusRefs.Id); });

            bonusWidget.Activate(bonusRefs.AvatarBonus);
        }  
    }

    private void ChangeWidgetText(string value) => 
        widget.ChangeButtonText(value);

    private void OnTimerUpdated(float value)
    {
        TimeSpan time = TimeSpan.FromSeconds(value);
        widget.ChangeButtonText(string.Format("{0:D2}:{1:D2}:{2:D2}", time.Hours, time.Minutes, time.Seconds));
    } 

    private void OnRewardCountdownEnd()
    {
        widget.SetActiveClaimButton(true);
        widget.ChangeButtonText(Constants.TextKeys.Claim);
    }

    private void StartRewardCountdown(float duration) 
    {
        widget.SetActiveClaimButton(false);

        StartCountDown(duration, OnRewardCountdownEnd, OnTimerUpdated);
    }

    private void StartCountDown(float time, Action callBack, Action<float> persecondsCallback = null)
    {
        int timeMultiplier = _configurationsParser.IsReleaseBuild() ? 1 : testBuildTimeMultiplier;
        _timer.StartCountdown(time, timeMultiplier, callBack, persecondsCallback);
    }

    private void DeactivateBonusWidget() => bonusWidget.Deactivate();

    private void DeactivateBonus(string bonusId) => 
        bonusContainer.GetBonusRefsById(_saveManager.GetData().bonusId).Deactivate();
}