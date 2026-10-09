
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public enum ItemType
    {
        Food,
        Water,
        Wood,
        Stone,
        Medicine,
        Other
    }

    [Header("Item Settings")]
    [SerializeField] private string itemName = "Food";
    [SerializeField] private ItemType itemType = ItemType.Food;
    [SerializeField, Min(1)] private int quantity = 1;

    [Header("Survival Benefits")]
    [SerializeField, Min(0f)] private float healthRestore = 0f;
    [SerializeField, Min(0f)] private float hungerRestore = 20f;
    [SerializeField, Min(0f)] private float thirstRestore = 0f;

    [Header("Collection Settings")]
    [SerializeField] private bool destroyAfterCollection = true;

    private bool collected;

    public string ItemName => itemName;
    public ItemType Type => itemType;
    public int Quantity => quantity;

    private void OnTriggerEnter(Collider other)
    {
        if (collected)
            return;

        if (!other.CompareTag("Player"))
            return;

        SurvivalStats stats = other.GetComponent<SurvivalStats>();

        // Food and water can restore survival stats.
        if (itemType == ItemType.Food ||
            itemType == ItemType.Water ||
            itemType == ItemType.Medicine)
        {
            if (stats == null)
                return;

            if (itemType == ItemType.Food)
            {
                stats.RestoreHunger(hungerRestore);
                stats.RestoreHealth(healthRestore);
            }
            else if (itemType == ItemType.Water)
            {
                stats.RestoreThirst(thirstRestore);
                stats.RestoreHealth(healthRestore);
            }
            else if (itemType == ItemType.Medicine)
            {
                stats.RestoreHealth(healthRestore);
            }
        }

        collected = true;

        Debug.Log("Collected: " + itemName +
                  " x" + quantity);

        if (destroyAfterCollection)
            Destroy(gameObject);
    }
}
