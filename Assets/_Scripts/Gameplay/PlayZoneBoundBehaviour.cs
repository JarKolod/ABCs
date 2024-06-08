using UnityEngine;

public class PlayZoneBoundBehaviour : MonoBehaviour
{
    [SerializeField] LayerMask playerCharacterLayer;
    [SerializeField] float detectionThreshold = 5.0f;
    [Range(0f,1f)]
    [SerializeField] float maxColorAlpha = 0.3f;

    Collider boundColl;
    MeshRenderer boundRenderer;
    Color boundCurrentColor;

    private void Awake()
    {
        boundColl = GetComponentInChildren<Collider>();
        boundRenderer = GetComponentInChildren<MeshRenderer>();
        boundCurrentColor = boundRenderer.material.color;
    }

    private void FixedUpdate()
    {
        Vector3 boxCenter = boundColl.bounds.center;
        Vector3 boxHalfExtents = boundColl.bounds.extents;

        Vector3 forwardDirection = transform.forward;


        if (Physics.BoxCast(boxCenter, boxHalfExtents, forwardDirection, out RaycastHit hit, Quaternion.identity, detectionThreshold, playerCharacterLayer))
        {
            boundCurrentColor.a = maxColorAlpha * (1 - (hit.distance / detectionThreshold));
            boundRenderer.material.color = boundCurrentColor;
        }
        else
        {
            boundCurrentColor.a = 0f;
            boundRenderer.material.color = boundCurrentColor;
        }
    }
}
