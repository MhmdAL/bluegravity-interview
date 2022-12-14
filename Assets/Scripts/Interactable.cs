using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public string InteractionDisplayName;
    public abstract void Interact(Player p);
}
