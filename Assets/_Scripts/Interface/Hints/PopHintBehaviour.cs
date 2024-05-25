using popuphints;
using System;
using UnityEngine;
using UnityEngine.UIElements;

public class PopHintBehaviour : MonoBehaviour
{
    UIDocument popUpHintDoc;
    Button popUpHintExitBtn;

    private void Awake()
    {
        popUpHintDoc = GetComponent<UIDocument>();
        popUpHintExitBtn = popUpHintDoc.rootVisualElement.Q("ExitHintBtn") as Button;

        popUpHintExitBtn.RegisterCallback<ClickEvent>(onPopUpExitBtnClick);
    }

    private void onPopUpExitBtnClick(ClickEvent evt)
    {
        PopUpHintManager.instance.OnPopUpExitBtnClick();
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        popUpHintExitBtn.UnregisterCallback<ClickEvent>(onPopUpExitBtnClick);
    }
}
