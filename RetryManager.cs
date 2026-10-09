
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class RetryManager : MonoBehaviour
{
    [Header("Retry Settings")]
    [SerializeField] private CountdownTimer countdownTimer;
    [SerializeField] private SurvivalStats survivalStats;
    [SerializeField] private InventorySystem inventorySystem;

    [Header("Player Checkpoint")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform checkpoint;

    [Header("Options")]
    [SerializeField] private bool clearInventoryOnRetry = false;
    [SerializeField] private bool resetPlayerPosition = true;

    private bool retrying;

    public bool IsRetrying => retrying;

    public void RetryMission()
    {
        if (retrying)
            return;

        StartCoroutine(RetryRoutine());
    }

    private IEnumerator RetryRoutine()
    {
        retrying = true;

        // Stop the timer before resetting the mission.
        if (countdownTimer != null)
            countdownTimer.PauseTimer();

        // Reset player survival stats.
        if (survivalStats != null)
            survivalStats.ResetStats();

        // Clear inventory only when enabled.
        if (clearInventoryOnRetry && inventorySystem != null)
            inventorySystem.ClearInventory();

        // Return player to the checkpoint.
        if (resetPlayerPosition &&
            player != null &&
            checkpoint != null)
        {
            CharacterController controller =
                player.GetComponent<CharacterController>();

            if (controller != null)
                controller.enabled = false;

            player.SetPositionAndRotation(
                checkpoint.position,
                checkpoint.rotation
            );

            if (controller != null)
                controller.enabled = true;
        }

        // Reset and restart the mission timer.
        if (countdownTimer != null)
        {
            countdownTimer.ResetTimer();
            countdownTimer.StartTimer();
        }

        // Wait one frame before allowing another retry.
        yield return null;

        retrying = false;

        Debug.Log("EARTH SURVIVAL: Mission restarted successfully!");
    }

    public void ReloadCurrentScene()
    {
        if (retrying)
            return;

        retrying = true;

        Scene currentScene = SceneManager.GetActiveScene();

        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void SetCheckpoint(Transform newCheckpoint)
    {
        if (newCheckpoint == null)
        {
            Debug.LogWarning("Checkpoint is missing.");
            return;
        }

        checkpoint = newCheckpoint;

        Debug.Log("New checkpoint saved.");
    }
}
