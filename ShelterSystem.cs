
using UnityEngine;

public class ShelterSystem : MonoBehaviour
{
    [Header("Shelter Settings")]
    [SerializeField] private string shelterName = "Safe Shelter";
    [SerializeField, Min(1)] private int restDurationSeconds = 5;

    [Header("Recovery")]
    [SerializeField, Min(0f)] private float healthRecovery = 10f;
    [SerializeField, Min(0f)] private float staminaRecovery = 30f;

    [Header("Shelter Status")]
    [SerializeField] private bool shelterAvailable = true;

    public string ShelterName => shelterName;
    public bool IsAvailable => shelterAvailable;

    public bool Rest(GameObject player)
    {
        if (!shelterAvailable || player == null)
            return false;

        SurvivalStats stats =
            player.GetComponent<SurvivalStats>();

        if (stats == null)
        {
            Debug.LogWarning(
                "Player needs a SurvivalStats component."
            );
            return false;
        }

        stats.RestoreHealth(healthRecovery);
        stats.RestoreStamina(staminaRecovery);

        Debug.Log(
            player.name + " rested at " + shelterName +
            " for " + restDurationSeconds + " seconds."
        );

        return true;
    }

    public void SetAvailability(bool available)
    {
        shelterAvailable = available;
    }
}
