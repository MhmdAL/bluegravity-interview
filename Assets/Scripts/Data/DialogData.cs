using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogs/New Dialog")]
public class DialogData : ScriptableObject
{
    [Multiline]
    public List<string> Dialogs;
}
