
using System;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    [Header("Mission Settings")]
    [SerializeField, Min(1)] private int currentLevel = 1;
    [SerializeField] private CountdownTimer countdownTimer;

    private const string UnlockedLevelKey = "EarthSurvival_UnlockedLevel";

    public bool IsMissionCompleted { get; private set; }
    public bool IsMissionFailed { get; private set; }

    public event Action OnMissionCompletedEvent;
    public event Action OnMissionFailedEvent;
    public event Action<int> OnNextLevelUnlocked;

    private void OnEnable()
    {
        if (countdownTimer != null)
            countdownTimer.OnTimerExpired += HandleTimerExpired;
    }

    private void OnDisable()
    {
        if (countdownTimer != null)
            countdownTimer.OnTimerExpired -= HandleTimerExpired;
    }

    public void CompleteMission()
    {
        if (IsMissionCompleted || IsMissionFailed)
            return;

        IsMissionCompleted = true;

        int nextLevel = currentLevel + 1;
        int highestUnlockedLevel =
            PlayerPrefs.GetInt(UnlockedLevelKey, 1);

        if (nextLevel > highestUnlockedLevel)
        {
            PlayerPrefs.SetInt(UnlockedLevelKey, nextLevel);
            PlayerPrefs.Save();
            OnNextLevelUnlocked?.Invoke(nextLevel);
        }

        Debug.Log("Mission completed! Next level unlocked.");
        OnMissionCompletedEvent?.Invoke();
    }

    public void FailMission()
    {
        if (IsMissionCompleted || IsMissionFailed)
            return;

        IsMissionFailed = true;

        Debug.Log("Mission failed. You can retry the level.");
        OnMissionFailedEvent?.Invoke();
    }

    private void HandleTimerExpired()
    {
        FailMission();
    }

    public void RetryMission()
    {
        IsMissionCompleted = false;
        IsMissionFailed = false;

        if (countdownTimer != null)
            countdownTimer.ResetTimer();

        Debug.Log("Mission reset. Ready to retry.");
    }

    public int GetUnlockedLevel()
    {
        return PlayerPrefs.GetInt(UnlockedLevelKey, 1);
    }

    public bool IsLevelUnlocked(int level)
    {
        return level >= 1 && level <= GetUnlockedLevel();
    }

    public void SetCurrentLevel(int level)
    {
        if (level < 1)
        {
            Debug.LogWarning("Level number must be at least 1.");
            return;
        }

        currentLevel = level;
        IsMissionCompleted = false;
        IsMissionFailed = false;
    }
}
