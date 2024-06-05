using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualHelper : MonoBehaviour
{
    [SerializeField] private GameObject playerCharacter;
    [SerializeField] private GameObject playerColliderVisualHelper;

    private Collider playerCollider;
    private float buffer = 0.3f;

    private void Start()
    {
        playerCollider = playerCharacter.GetComponent<Collider>();
        Vector3 colliderSize = playerCollider.bounds.size;
        playerColliderVisualHelper.transform.localScale = 
            new Vector3(
                colliderSize.x + buffer, 
                playerColliderVisualHelper.transform.localScale.y,
                playerColliderVisualHelper.transform.localScale.z);
    }

    public void TurnOffVisualCollisionHelper()
    {
        playerColliderVisualHelper.gameObject.SetActive(false);
    }

    public void TurnOnVisualCollisionHelper()
    {
        playerColliderVisualHelper.gameObject.SetActive(true);
    }

    private void OnValidate()
    {
        if (playerCollider != null)
        {
            Vector3 colliderSize = playerCollider.bounds.size;
            playerColliderVisualHelper.transform.localScale =
                new Vector3(
                    playerColliderVisualHelper.transform.localScale.x,
                    playerColliderVisualHelper.transform.localScale.y,
                    playerColliderVisualHelper.transform.localScale.z);
        }
    }
}