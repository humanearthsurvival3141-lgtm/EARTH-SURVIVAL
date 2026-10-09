
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventorySlot
{
    public string itemId;
    public int quantity;

    public InventorySlot(string id, int amount)
    {
        itemId = id;
        quantity = amount;
    }
}

public class InventorySystem : MonoBehaviour
{
    [SerializeField, Min(1)]
    private int capacity = 20;

    [SerializeField, Min(1)]
    private int maxStack = 99;

    [SerializeField]
    private List<InventorySlot> slots = new List<InventorySlot>();

    public IReadOnlyList<InventorySlot> Slots => slots;
    public int Capacity => capacity;

    public event Action OnInventoryChanged;

    public bool AddItem(string itemId, int amount = 1)
    {
        if (string.IsNullOrWhiteSpace(itemId) || amount <= 0)
            return false;

        int existingIndex = slots.FindIndex(
            slot => slot.itemId == itemId &&
                    slot.quantity < maxStack
        );

        if (existingIndex >= 0)
        {
            InventorySlot slot = slots[existingIndex];
            int available = maxStack - slot.quantity;
            int added = Mathf.Min(amount, available);

            slot.quantity += added;
            amount -= added;

            if (amount == 0)
            {
                OnInventoryChanged?.Invoke();
                return true;
            }
        }

        while (amount > 0 && slots.Count < capacity)
        {
            int stackAmount = Mathf.Min(amount, maxStack);
            slots.Add(new InventorySlot(itemId, stackAmount));
            amount -= stackAmount;
        }

        OnInventoryChanged?.Invoke();
        return amount == 0;
    }

    public bool HasItem(string itemId, int amount = 1)
    {
        if (amount <= 0)
            return true;

        int total = 0;

        foreach (InventorySlot slot in slots)
        {
            if (slot.itemId == itemId)
                total += slot.quantity;
        }

        return total >= amount;
    }

    public bool RemoveItem(string itemId, int amount = 1)
    {
        if (string.IsNullOrWhiteSpace(itemId) ||
            amount <= 0 || !HasItem(itemId, amount))
            return false;

        int remaining = amount;

        for (int i = slots.Count - 1; i >= 0; i--)
        {
            InventorySlot slot = slots[i];

            if (slot.itemId != itemId)
                continue;

            int removed = Mathf.Min(remaining, slot.quantity);
            slot.quantity -= removed;
            remaining -= removed;

            if (slot.quantity <= 0)
                slots.RemoveAt(i);

            if (remaining == 0)
                break;
        }

        OnInventoryChanged?.Invoke();
        return true;
    }

    public void ClearInventory()
    {
        slots.Clear();
        OnInventoryChanged?.Invoke();
    }
}
