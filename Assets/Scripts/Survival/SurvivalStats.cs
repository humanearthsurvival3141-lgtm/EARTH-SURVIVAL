
using System;
using UnityEngine;

public class SurvivalStats : MonoBehaviour
{
    [Header("Maximum Stats")]
    [SerializeField, Min(1f)] private float maxHealth = 100f;
    [SerializeField, Min(1f)] private float maxHunger = 100f;
    [SerializeField, Min(1f)] private float maxThirst = 100f;
    [SerializeField, Min(1f)] private float maxStamina = 100f;

    [Header("Starting Stats")]
    [SerializeField] private float startingHealth = 100f;
    [SerializeField] private float startingHunger = 100f;
    [SerializeField] private float startingThirst = 100f;
    [SerializeField] private float startingStamina = 100f;

    [Header("Survival Drain Per Second")]
    [SerializeField, Min(0f)] private float hungerDrain = 0.15f;
    [SerializeField, Min(0f)] private float thirstDrain = 0.25f;

    [Header("Health Damage")]
    [SerializeField, Min(0f)] private float starvationDamage = 2f;
    [SerializeField, Min(0f)] private float dehydrationDamage = 4f;

    public float Health { get; private set; }
    public float Hunger { get; private set; }
    public float Thirst { get; private set; }
    public float Stamina { get; private set; }

    public bool IsAlive => Health > 0f;

    public event Action OnStatsChanged;
    public event Action OnPlayerDied;

    private bool deathEventSent;

    private void Awake()
    {
        ResetStats();
    }

    private void Update()
    {
        if (!IsAlive)
            return;

        float delta = Time.deltaTime;

        Hunger = Mathf.Max(0f, Hunger - hungerDrain * delta);
        Thirst = Mathf.Max(0f, Thirst - thirstDrain * delta);

        if (Hunger <= 0f)
            Health -= starvationDamage * delta;

        if (Thirst <= 0f)
            Health -= dehydrationDamage * delta;

        ClampStats();
        CheckDeath();
        OnStatsChanged?.Invoke();
    }

    public void RestoreHealth(float amount)
    {
        if (amount <= 0f || !IsAlive)
            return;

        Health = Mathf.Min(maxHealth, Health + amount);
        OnStatsChanged?.Invoke();
    }

    public void RestoreHunger(float amount)
    {
        if (amount <= 0f || !IsAlive)
            return;

        Hunger = Mathf.Min(maxHunger, Hunger + amount);
        OnStatsChanged?.Invoke();
    }

    public void RestoreThirst(float amount)
    {
        if (amount <= 0f || !IsAlive)
            return;

        Thirst = Mathf.Min(maxThirst, Thirst + amount);
        OnStatsChanged?.Invoke();
    }

    public void RestoreStamina(float amount)
    {
        if (amount <= 0f || !IsAlive)
            return;

        Stamina = Mathf.Min(maxStamina, Stamina + amount);
        OnStatsChanged?.Invoke();
    }

    public bool UseStamina(float amount)
    {
        if (amount <= 0f)
            return true;

        if (!IsAlive || Stamina < amount)
            return false;

        Stamina -= amount;
        OnStatsChanged?.Invoke();
        return true;
    }

    public void SetStats(
        float health,
        float hunger,
        float thirst,
        float stamina)
    {
        Health = health;
        Hunger = hunger;
        Thirst = thirst;
        Stamina = stamina;

        ClampStats();
        CheckDeath();
        OnStatsChanged?.Invoke();
    }

    public void ResetStats()
    {
        Health = Mathf.Clamp(startingHealth, 0f, maxHealth);
        Hunger = Mathf.Clamp(startingHunger, 0f, maxHunger);
        Thirst = Mathf.Clamp(startingThirst, 0f, maxThirst);
        Stamina = Mathf.Clamp(startingStamina, 0f, maxStamina);

        deathEventSent = false;
        OnStatsChanged?.Invoke();
    }

    private void ClampStats()
    {
        Health = Mathf.Clamp(Health, 0f, maxHealth);
        Hunger = Mathf.Clamp(Hunger, 0f, maxHunger);
        Thirst = Mathf.Clamp(Thirst, 0f, maxThirst);
        Stamina = Mathf.Clamp(Stamina, 0f, maxStamina);
    }

    private void CheckDeath()
    {
        if (Health > 0f || deathEventSent)
            return;

        deathEventSent = true;
        OnPlayerDied?.Invoke();
        Debug.Log("Player has run out of health.");
    }
}
