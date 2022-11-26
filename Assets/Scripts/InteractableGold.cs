using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableGold : Interactable
{
    [SerializeField] private int Value;

    public override void Interact(Player p)
    {
        p.Money += Value;
    }
}
