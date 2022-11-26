using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items")]
public class ItemData : ScriptableObject
{
    public string DisplayName;
    public ItemType Type;
    public int Price;
    public Sprite Sprite;
}

public enum ItemType
{
    Hat,
    Armor
}