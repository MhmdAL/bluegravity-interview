using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDisplay : Interactable
{
    [SerializeField] private ItemData itemData;

    private GameUIController _uiController;

    private void Awake()
    {
        _uiController = FindObjectOfType<GameUIController>();

        InteractionDisplayName = itemData.DisplayName;

        GetComponent<SpriteRenderer>().sprite = itemData.Sprite;
    }

    public override void Interact(Player p)
    {
        _uiController.LoadPurchaseMenu(itemData);
    }
}
