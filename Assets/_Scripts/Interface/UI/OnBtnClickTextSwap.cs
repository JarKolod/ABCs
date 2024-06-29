using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBtnClickTextSwap : MonoBehaviour
{
    [SerializeField] private string textToSwapTo;
    private string originalText;

    private TMPro.TextMeshProUGUI uiText;

    private void OnEnable()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnBtnClickSwapText);

        uiText = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        originalText = uiText.text;
    }

    public void OnBtnClickSwapText()
    {
        if (uiText.text == originalText)
        {
            uiText.SetText(textToSwapTo);
        }
        else
        {
            uiText.SetText(originalText);
        }
    }
}
