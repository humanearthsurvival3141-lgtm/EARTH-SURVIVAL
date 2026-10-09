
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryManager : MonoBehaviour
{
    [Header("Retry Settings")]
    [SerializeField] private CountdownTimer countdownTimer;
    [SerializeField] private SurvivalStats survivalStats;
    [SerializeField] private InventorySystem inventorySystem;

    [SerializeField] private Transform player;
    [SerializeField] private Transform checkpoint;

    [SerializeField] private bool clearInventoryOnRetry = false;

    private bool retrying;

    public void RetryMission()
    {
        if (retrying)
            return;

        retrying = true;

        if (countdownTimer != null)
            countdownTimer.ResetTimer();

        if (survivalStats != null)
            survivalStats.ResetStats();

        if (clearInventoryOnRetry && inventorySystem != null)
            inventorySystem.ClearInventory();

        if (player != null && checkpoint != null)
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

        retrying = false;

        Debug.Log("Mission reset. Ready to retry!");
    }

    public void ReloadCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
