using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DialogBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private TextMeshProUGUI continueText;

    private DialogData _dialogData;
    private int _currentDialogText;

    public void Reset(DialogData data)
    {
        _currentDialogText = 0;

        _dialogData = data;

        dialogText.text = _dialogData?.Dialogs?.FirstOrDefault() ?? "";

        continueText.text = "Press space to continue";

        gameObject.SetActive(true);
    }

    public void UpdateDialogText()
    {
        dialogText.text = _dialogData.Dialogs[_currentDialogText];
    }

    public void CloseDialog()
    {
        gameObject.SetActive(false);

        Time.timeScale = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _currentDialogText++;

            if (_currentDialogText >= _dialogData.Dialogs.Count)
            {
                CloseDialog();
            }
            else
            {
                UpdateDialogText();
            }
        }
    }
}
