using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDisplay : Interactable
{
    public ItemData ItemData;

    private GameUIController _uiController;

    private void Awake()
    {
        _uiController = FindObjectOfType<GameUIController>();

        InteractionDisplayName = ItemData.DisplayName;

        GetComponent<SpriteRenderer>().sprite = ItemData.Sprite;
    }

    public override void Interact(Player p)
    {
        _uiController.LoadItem(ItemData);
    }
}
