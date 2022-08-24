using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private Controller _controller;
    private const int MinutesLimit = 10;
    private readonly WaitForSeconds _minuteToWait = new WaitForSeconds(10);

    private void Awake()
    {
        _controller = FindObjectOfType<Controller>();
    }

    private void OnEnable()
    {
        var totalPlayedTime = PlayerPrefs.GetInt(_controller.TotalPlayedTime, 0);
        var minutesIngame = totalPlayedTime / 60;
        if (minutesIngame <= MinutesLimit)
            StartCoroutine(RookieStatusCoroutine(minutesIngame));
    }

    private IEnumerator RookieStatusCoroutine(int minutes)
    {
        while (minutes <= MinutesLimit)
        {
            yield return _minuteToWait;

            minutes++;
            _controller.Log($"\nMinutes: {minutes}");

            if(minutes == MinutesLimit)
                _controller.Log($"\nTimer reached limit: {MinutesLimit}");
        }
    }
}
