using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action<Interactable> NearestInteractableChanged;

    public int Money;

    public TextMeshProUGUI InteractionText;

    public List<ItemData> Items;

    public SpriteRenderer HatRenderer;
    public SpriteRenderer ArmorRenderer;
    private ItemData _currentHat;

    private Interactable _nearestInteractable;
    public Interactable NearestInteractable
    {
        get
        {
            return _nearestInteractable;
        }
        set
        {
            var shouldNotify = _nearestInteractable != value;

            _nearestInteractable = value;

            if(shouldNotify)
                NearestInteractableChanged?.Invoke(value);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            var hatIndex = Items.IndexOf(_currentHat);
            if (hatIndex != -1)
            {
                hatIndex = (hatIndex + 1) % Items.Count;
                var hat = Items[hatIndex];

                Equip(hat);
            }
            else
            {
                Equip(Items.First());
            }
        }

        FindNearestInteractable();

        if (NearestInteractable != null && Input.GetKeyDown(KeyCode.F))
        {
            NearestInteractable.Interact(this);
        }
    }

    private void FindNearestInteractable()
    {
        Interactable nearest = null;

        var cols = Physics2D.OverlapCircleAll(transform.position, 1f);
        if (cols != null)
        {
            var interactables = new List<(Collider2D, Interactable)>();
            foreach (var col in cols)
            {
                var itemDisplay = col.GetComponent<Interactable>();

                if (itemDisplay != null)
                {
                    interactables.Add((col, itemDisplay));
                }
            }

            float nearestDist = float.PositiveInfinity;
            foreach (var interactable in interactables)
            {
                var closestPoint = interactable.Item1.ClosestPoint(transform.position);
                var dir = (Vector2)transform.position - closestPoint;

                if (dir.sqrMagnitude < nearestDist)
                {
                    nearest = interactable.Item2;
                    nearestDist = dir.sqrMagnitude;
                }
            }
        }

        NearestInteractable = nearest;
    }

    private void Equip(ItemData item)
    {
        _currentHat = item;

        UpdateVisuals();
    }

    public void PurchaseItem(ItemDisplay item)
    {
        if (Money >= item.ItemData.Price && !Items.Contains(item.ItemData))
        {
            Items.Add(item.ItemData);
            Money -= item.ItemData.Price;

            Destroy(item.gameObject);
        }
    }

    public bool PurchaseItem(ItemData item)
    {
        if (Money >= item.Price && !Items.Contains(item))
        {
            Items.Add(item);
            Money -= item.Price;

            return true;
        }

        return false;
    }

    private void UpdateVisuals()
    {
        if (_currentHat == null)
            return;

        if (_currentHat.Type == ItemType.Hat)
        {
            HatRenderer.sprite = _currentHat?.Sprite ?? null;
            ArmorRenderer.sprite = null;
        }
        else if (_currentHat.Type == ItemType.Armor)
        {
            HatRenderer.sprite = null;
            ArmorRenderer.sprite = _currentHat?.Sprite ?? null;
        }
    }
}