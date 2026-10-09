
using System;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    [SerializeField, Min(1f)]
    private float missionDuration = 120f;

    [SerializeField]
    private bool startAutomatically = false;

    private float timeRemaining;
    private bool isRunning;
    private bool hasFinished;

    public float TimeRemaining => timeRemaining;
    public bool IsRunning => isRunning;
    public bool HasFinished => hasFinished;

    public event Action<float> OnTimeChanged;
    public event Action OnTimerExpired;

    private void Awake()
    {
        timeRemaining = missionDuration;
    }

    private void Start()
    {
        if (startAutomatically)
            StartTimer();
    }

    private void Update()
    {
        if (!isRunning || hasFinished)
            return;

        timeRemaining -= Time.deltaTime;
        timeRemaining = Mathf.Max(0f, timeRemaining);

        OnTimeChanged?.Invoke(timeRemaining);

        if (timeRemaining <= 0f)
        {
            isRunning = false;
            hasFinished = true;

            Debug.Log("Mission time expired!");
            OnTimerExpired?.Invoke();
        }
    }

    public void StartTimer()
    {
        if (hasFinished)
            return;

        if (timeRemaining <= 0f)
            timeRemaining = missionDuration;

        isRunning = true;
        OnTimeChanged?.Invoke(timeRemaining);
    }

    public void PauseTimer()
    {
        isRunning = false;
    }

    public void ResumeTimer()
    {
        if (!hasFinished && timeRemaining > 0f)
            isRunning = true;
    }

    public void ResetTimer()
    {
        timeRemaining = missionDuration;
        isRunning = false;
        hasFinished = false;

        OnTimeChanged?.Invoke(timeRemaining);
    }

    public string GetFormattedTime()
    {
        int totalSeconds = Mathf.CeilToInt(timeRemaining);
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;

        return $"{minutes:00}:{seconds:00}";
    }
}
