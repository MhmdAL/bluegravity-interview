using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    public Player Player;

    public TextMeshProUGUI InteractionText;
    public TextMeshProUGUI GoldText;

    public Image ItemIconImage;
    public TextMeshProUGUI ItemNameText;
    public TextMeshProUGUI ItemTypeText;
    public TextMeshProUGUI ItemPriceText;

    public GameObject PurchaseMenu;
    private ItemData _currentItem;

    private void Awake()
    {
        Player.NearestInteractableChanged += OnNearestInteractableChanged;
    }

    private void OnNearestInteractableChanged(Interactable other)
    {
        UpdateInteractionText(other);
    }

    private void UpdateInteractionText(Interactable interactable)
    {
        if (interactable != null && PurchaseMenu.activeSelf == false)
        {
            InteractionText.gameObject.SetActive(true);
            InteractionText.text = $"Press F to interact ({interactable.InteractionDisplayName})";
        }
        else
        {
            InteractionText.gameObject.SetActive(false);
        }
    }

    public void LoadItem(ItemData item)
    {
        Time.timeScale = 0;

        _currentItem = item;

        ItemIconImage.sprite = item.Sprite;
        ItemNameText.text = item.DisplayName;
        ItemTypeText.text = item.Type.ToString();
        ItemPriceText.text = item.Price.ToString();

        PurchaseMenu.SetActive(true);

        UpdateInteractionText(Player.NearestInteractable);
    }

    private void Update()
    {
        GoldText.text = Player.Money.ToString();
    }

    public void OnClosePurchaseMenuClicked()
    {
        ClosePurchaseMenu();
    }

    private void ClosePurchaseMenu()
    {
        PurchaseMenu.SetActive(false);

        UpdateInteractionText(Player.NearestInteractable);

        Time.timeScale = 1;
    }

    public void OnPurchaseItemClicked()
    {
        if (Player.PurchaseItem(_currentItem))
        {
            ClosePurchaseMenu();
        }
    }
}
