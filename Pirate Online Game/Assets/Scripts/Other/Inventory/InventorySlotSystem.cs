using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InventorySlotSystem : MonoBehaviour
{
    public bool isSelected {
        set
        {
            isSelected = value;
            UpdateSlotColor();
        }
        get
        {
            return isSelected;
        }
    }
    public Color selectedColor;

    private Image image;
    private Color standartColor;

    [SerializeField] private UnityEvent OnSelect;

    private void Start()
    {
        image = GetComponent<Image>();
        standartColor = image.color;
    }


    public void OnClickSlot()
    {
        transform.parent.GetComponent<InventorySystem>().SelectSlot(this);
        OnSelect.Invoke();
    }

    private void UpdateSlotColor()
    {
        if (isSelected)
        {
            image.color = selectedColor;
        }
        else
        {
            image.color = standartColor;
        }
    }
}
