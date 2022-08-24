using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    [SerializeField] private GameObject _androidPanel;
    [SerializeField] private GameObject _iosUnityPanel;
    [SerializeField] private Text _androidLog;
    [SerializeField] private Text _iosUnityLog;

    private DateTime _sessionStartTime;
    private DateTime _lastSessionTimestamp;

    private const int MinSessionLength = 5;

    private void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        _androidPanel.SetActive(true);
        _iosUnityPanel.SetActive(false);
#endif

#if UNITY_EDITOR || UNITY_IOS
        _androidPanel.SetActive(false);
        _iosUnityPanel.SetActive(true);
#endif
        StartSession();
    }

#if UNITY_ANDROID && !UNITY_EDITOR
    void OnApplicationFocus(bool focus)
    {
        Log($"\nOnApplicationFocus {focus}");
        if (!focus && (DateTime.UtcNow - _sessionStartTime).TotalSeconds > MinSessionLength)
        {
            EndSession();
        }
        else
        {
            _sessionStartTime = DateTime.UtcNow;
        }
    }
#endif

#if UNITY_EDITOR || UNITY_IOS
    void OnApplicationPause(bool pause)
    {
        Log($"\nOnApplicationPause {pause}");
        if (pause && (DateTime.UtcNow - _sessionStartTime).TotalSeconds > MinSessionLength)
        {
            EndSession();
        }
        else
        {
            _sessionStartTime = DateTime.UtcNow;
        }
    }

    private void OnApplicationQuit()
    {
        Log($"\nOnApplicationQuit");
    }
#endif

    public readonly string TotalSessions = "totalSessions";
    public readonly string TotalPlayedTime = "totalPlayedTime";
    private void StartSession()
    {
        var totalSessions = PlayerPrefs.GetInt(TotalSessions, 0);
        totalSessions++;
        PlayerPrefs.SetInt(TotalSessions, totalSessions);

        _sessionStartTime = DateTime.UtcNow;
        _lastSessionTimestamp = DateTime.UtcNow;

        Log($"\nStartSession totalSessions {totalSessions}");
    }

    private void EndSession()
    {
        var totalPlayedTime = PlayerPrefs.GetInt(TotalPlayedTime, 0);
        var playedTime = DateTime.UtcNow - _sessionStartTime;
        totalPlayedTime += (int)playedTime.TotalSeconds;
        PlayerPrefs.SetInt(TotalPlayedTime, totalPlayedTime);

        Log($"\nEndSession playedTime {playedTime}");
    }

    public void Log(string message)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        LogAndroid(message);
#endif

#if UNITY_EDITOR || UNITY_IOS
        LogIosUnity(message);
#endif
    }

    private void LogAndroid(string message)
    {
        _androidLog.text += message;
        Debug.Log(message.Replace('\n', ' '));
    }

    private void LogIosUnity(string message)
    {
        _iosUnityLog.text += message;
        Debug.Log(message.Replace('\n', ' '));
    }
}
