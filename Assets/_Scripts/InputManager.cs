using UnityEngine;

public class InputManager : MonoBehaviour
{
    public InputMaster inputMaster;

    void Awake()
    {
        inputMaster = new InputMaster();
    }

    private void OnEnable()
    {
        inputMaster.Enable();
    }

    private void OnDisable()
    {
        inputMaster.Disable();
    }

}
