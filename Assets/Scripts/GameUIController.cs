using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    [SerializeField] private string purchaseButtonUnownedText;
    [SerializeField] private string purchaseButtonOwnedText;
    [SerializeField] private string interactionStringFormat;

    [SerializeField] private TextMeshProUGUI interactionText;
    [SerializeField] private TextMeshProUGUI goldText;

    [SerializeField] private Image itemIconImage;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemTypeText;
    [SerializeField] private TextMeshProUGUI itemPriceText;

    [SerializeField] private Button purchaseButton;
    [SerializeField] private TextMeshProUGUI purchaseButtonText;

    [SerializeField] private GameObject purchaseMenu;
    [SerializeField] private GameObject sellMenu;

    private List<InventoryItem> _inventoryItems;

    private Player _player;

    private ItemData _currentItem;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();

        _player.NearestInteractableChanged += OnNearestInteractableChanged;

        _inventoryItems = sellMenu.GetComponentsInChildren<InventoryItem>().ToList();
    }

    private void Update()
    {
        goldText.text = _player.Money.ToString();
    }

    private void OnNearestInteractableChanged(Interactable other)
    {
        UpdateInteractionText(other);
    }

    private void UpdateInteractionText(Interactable interactable)
    {
        if (interactable != null && !purchaseMenu.activeSelf && !sellMenu.activeSelf)
        {
            interactionText.gameObject.SetActive(true);
            interactionText.text = string.Format(interactionStringFormat, interactable.InteractionDisplayName);
        }
        else
        {
            interactionText.gameObject.SetActive(false);
        }
    }

    public void UpdateInventoryItems()
    {
        _inventoryItems.ForEach(x => x.Reset());

        for (int i = 0; i < _player.Items.Count && i < _inventoryItems.Count; i++)
        {
            _inventoryItems[i].LoadItem(_player.Items[i]);
        }
    }

    public void LoadSellMenu()
    {
        UpdateInventoryItems();

        sellMenu.SetActive(true);

        Time.timeScale = 0;

        UpdateInteractionText(_player.NearestInteractable);
    }

    public void CloseSellMenu()
    {
        sellMenu.SetActive(false);

        UpdateInteractionText(_player.NearestInteractable);

        Time.timeScale = 1;
    }

    public void OnItemSold(ItemData item)
    {
        _player.SellItem(item);

        UpdateInventoryItems();
    }

    public void OnCloseSellMenuClicked()
    {
        CloseSellMenu();
    }

    public void LoadPurchaseMenu(ItemData item)
    {
        Time.timeScale = 0;

        _currentItem = item;

        itemIconImage.sprite = item.Sprite;
        itemNameText.text = item.DisplayName;
        itemTypeText.text = item.Type.ToString();
        itemPriceText.text = item.Price.ToString();

        if (_player.Items.Contains(item))
        {
            purchaseButton.interactable = false;
            purchaseButtonText.text = purchaseButtonOwnedText;
        }
        else
        {
            purchaseButton.interactable = true;
            purchaseButtonText.text = purchaseButtonUnownedText;
        }

        purchaseMenu.SetActive(true);

        UpdateInteractionText(_player.NearestInteractable);
    }

    public void OnClosePurchaseMenuClicked()
    {
        ClosePurchaseMenu();
    }

    private void ClosePurchaseMenu()
    {
        purchaseMenu.SetActive(false);

        UpdateInteractionText(_player.NearestInteractable);

        Time.timeScale = 1;
    }

    public void OnPurchaseItemClicked()
    {
        if (_player.PurchaseItem(_currentItem))
        {
            ClosePurchaseMenu();
        }
    }

    private void OnDestroy()
    {
        _player.NearestInteractableChanged -= OnNearestInteractableChanged;
    }
}
