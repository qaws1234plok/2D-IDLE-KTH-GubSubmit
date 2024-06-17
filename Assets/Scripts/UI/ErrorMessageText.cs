using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ErrorMessageText : SingletonMonoBehaviour<ErrorMessageText>
{
    public TextMeshProUGUI errorMessageText;

    protected override void Awake()
    {
        base.Awake();
    }

    public void ShowErrorMessage(string message)
    {
        StartCoroutine(ShowMessageCoroutine(message));
    }

    private IEnumerator ShowMessageCoroutine(string message)
    {
        errorMessageText.text = message;
        yield return new WaitForSeconds(2f); 
        errorMessageText.text = "";
    }
}
