using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : Interactable
{
    [SerializeField] private DialogData shopKeeperDialog;

    private DialogBox _dialogBox;

    private void Awake()
    {
        _dialogBox = FindObjectOfType<DialogBox>(true);
    }

    public override void Interact(Player p)
    {
        Time.timeScale = 0;
        
        _dialogBox.Reset(shopKeeperDialog);
    }
}
