using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterControllerSetup : MonoBehaviour
{
    private void OnEnable()
    {
        CharacterController controller = GetComponent<CharacterController>();
        controller.detectCollisions = true;
    }
}
