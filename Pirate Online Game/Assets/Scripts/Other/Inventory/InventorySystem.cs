using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    private List<InventorySlotSystem> slots = new List<InventorySlotSystem>();

    [SerializeField] private Color selectedColor;

    private void Start()
    {
        foreach(Transform child in transform)
        {
            slots.Add(child.GetComponent<InventorySlotSystem>());
            child.GetComponent<InventorySlotSystem>().selectedColor = selectedColor;
        }
    }

    public void SelectSlot(InventorySlotSystem selectSlot)
    {
        foreach(InventorySlotSystem slot in slots)
        {
            slot.isSelected = false;
        }
        selectSlot.isSelected = true;
    }
}
