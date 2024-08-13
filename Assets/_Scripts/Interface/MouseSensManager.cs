using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MouseSensManager : MonoBehaviour
{
    public static float MouseSensValue { get => mouseSensValue; set => mouseSensValue = value; }

    [SerializeField] private PlayerCameraLook playerLookManager;
    [SerializeField] private TMPro.TextMeshProUGUI mouseSensText;
    [SerializeField] private TMPro.TMP_InputField mouseSensInputField;

    private static float mouseSensValue = 0.1f;

    public void SetMouseSens(float value)
    {
        mouseSensText.text = value.ToString();
        mouseSensInputField.text = value.ToString();

        mouseSensValue = value;
        playerLookManager.MouseSense = value;
    }

    public void SetMouseSens(string value)
    {
        if (float.TryParse(value, out float result) && result > 0f)
        {
            SetMouseSens(result);
        }
    }
}
