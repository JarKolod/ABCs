using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class ShootingCameraBehaviour : MonoBehaviour
{
    [Description("Players input manager")]
    [SerializeField] bool isShootingEnabled = false;
    [SerializeField] private InputManager inputManager;
    [SerializeField] LayerMask targetsLayer;
    [SerializeField] float maxShootingDistance = 500f;
    [SerializeField] AudioSource shootingAudio;

    private void Start()
    {
        if(inputManager != null)
        {
            inputManager.inputMaster.CameraLook.Shoot.performed += Shoot;
        }
    }

    public void Shoot(CallbackContext context)
    {
        if (isShootingEnabled && Time.timeScale != 0)
        {
            if (shootingAudio != null)
            {
                shootingAudio.Play();
            }

            Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, maxShootingDistance, targetsLayer);
            if (hit.collider != null)
            {
                IDestructible target = hit.collider.GetComponent<IDestructible>();
                if (target != null)
                {
                    target.OnDestroyObj();
                }
            }
        }
    }
}
