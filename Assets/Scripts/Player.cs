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

    public List<ItemData> Items;

    public SpriteRenderer HatRenderer;
    public SpriteRenderer WeaponRenderer;
    private ItemData _currentHat;
    private ItemData _currentWeapon;

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

            if (shouldNotify)
                NearestInteractableChanged?.Invoke(value);
        }
    }

    private void Update()
    {
        HandleHatCycleInput();
        HandleWeaponCycleInput();

        FindNearestInteractable();

        if (NearestInteractable != null && Input.GetKeyDown(KeyCode.F))
        {
            NearestInteractable.Interact(this);
        }
    }

    private void HandleHatCycleInput()
    {
        var isRightArrowDown = Input.GetKeyDown(KeyCode.RightArrow);
        var isLeftArrowDown = Input.GetKeyDown(KeyCode.LeftArrow);

        if (isRightArrowDown || isLeftArrowDown)
        {
            var hats = Items.Where(x => x.Type == ItemType.Hat).ToList();
            if (hats.Count == 0)
                return;

            var hatIndex = hats.IndexOf(_currentHat);
            if (hatIndex == -1)
            {
                hatIndex = isRightArrowDown ? 0 : hats.Count - 1;
            }
            else if (isRightArrowDown)
            {
                hatIndex = (hatIndex + 1) % hats.Count;
            }
            else
            {
                hatIndex = hatIndex - 1 >= 0 ? hatIndex - 1 : hats.Count - 1;
            }

            var hat = hats[hatIndex];
            Equip(hat);
        }
    }

    private void HandleWeaponCycleInput()
    {
        var isUpArrowDown = Input.GetKeyDown(KeyCode.UpArrow);
        var isDownArrowDown = Input.GetKeyDown(KeyCode.DownArrow);

        if (isUpArrowDown || isDownArrowDown)
        {
            var weapons = Items.Where(x => x.Type == ItemType.Weapon).ToList();
            if (weapons.Count == 0)
                return;

            var weaponIndex = weapons.IndexOf(_currentWeapon);
            if (weaponIndex == -1)
            {
                weaponIndex = isUpArrowDown ? 0 : weapons.Count - 1;
            }
            else if (isUpArrowDown)
            {
                weaponIndex = (weaponIndex + 1) % weapons.Count;
            }
            else
            {
                weaponIndex = weaponIndex - 1 >= 0 ? weaponIndex - 1 : weapons.Count - 1;
            }

            var weapon = weapons[weaponIndex];
            Equip(weapon);
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
        if (item.Type == ItemType.Hat)
        {
            _currentHat = item;
        }
        else if (item.Type == ItemType.Weapon)
        {
            _currentWeapon = item;
        }

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
        HatRenderer.sprite = _currentHat?.Sprite ?? null;

        WeaponRenderer.sprite = _currentWeapon?.Sprite ?? null;
    }
}