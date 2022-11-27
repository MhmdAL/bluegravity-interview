using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private Button itemButton;

    private ItemData _item;
    private GameUIController _uiController;

    private void Awake()
    {
        _uiController = FindObjectOfType<GameUIController>();
    }

    public void LoadItem(ItemData item)
    {
        _item = item;

        itemIcon.color = Color.white;
        itemIcon.sprite = item?.Sprite;

        itemButton.interactable = true;
    }

    public void Reset()
    {
        _item = null;

        itemIcon.color = new Color(0, 0, 0, 0);
        itemButton.interactable = false;
    }

    public void SellItem()
    {
        _uiController.OnItemSold(_item);
    }
}
