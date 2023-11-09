using System;
using System.Threading.Tasks;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public async void StartCountdown(float value, int multiplier, Action callBack, Action<float> persecondCallBack = null)
    {
        await CountDown(value, multiplier, persecondCallBack);

        callBack?.Invoke();
    }

    private async Task CountDown(float value, int multiplier, Action<float> callBack = null)
    {
        while (value > 0)
        {
            callBack?.Invoke(value);

            await Task.Delay(1000);

            value -= multiplier;
        }
    }
}
