using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellItemsInteractable : Interactable
{
    private GameUIController _uiController;

    private void Awake()
    {
        _uiController = FindObjectOfType<GameUIController>();
    }

    public override void Interact(Player p)
    {
        _uiController.LoadSellMenu();
    }
}
