using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/New Item")]
public class ItemData : ScriptableObject
{
    public ItemType Type;
    public int Price;
    public Sprite Sprite;
    public string DisplayName;
}

public enum ItemType
{
    Hat,
    Weapon
}