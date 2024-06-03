using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseCallsBehaviour : MonoBehaviour
{
    [SerializeField] float rayMaxDistance = 1f;
    [SerializeField] Animator closeCallAnimator;
    [SerializeField] float closeCallCoolDown = 1f;

    RaycastHit hitLeft;
    RaycastHit hitRight;

    bool isAbleToCloseCall = true;

    void FixedUpdate()
    {
        if (!isAbleToCloseCall)
        {
            return;
        }

        if (Physics.Raycast(transform.position, -transform.right, out hitLeft, rayMaxDistance))
        {
            if (hitLeft.transform.tag.Equals("Obstacle"))
            {
                Debug.Log("Hit Left: " + hitLeft.collider.name + ", distance: " + hitLeft.distance);
                closeCallAnimator.SetTrigger("CloseCall");
                StartCoroutine(CloseCallCoolDown());
            }
        }

        if (Physics.Raycast(transform.position, transform.right, out hitRight, rayMaxDistance))
        {
            if (hitRight.transform.tag.Equals("Obstacle"))
            {
                Debug.Log("Hit Right: " + hitRight.collider.name + ", distance: " + hitRight.distance);
                closeCallAnimator.SetTrigger("CloseCall");
                StartCoroutine(CloseCallCoolDown());
            }
        }
    }

    IEnumerator CloseCallCoolDown()
    {
        isAbleToCloseCall = false;
        yield return new WaitForSeconds(closeCallCoolDown);
        isAbleToCloseCall = true;
    }

    void OnDrawGizmos()
    {
        // Draw the left raycast
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position - transform.right * rayMaxDistance);
        if (hitLeft.collider != null)
        {
            Gizmos.DrawSphere(hitLeft.point, 0.1f);
        }

        // Draw the right raycast
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * rayMaxDistance);
        if (hitRight.collider != null)
        {
            Gizmos.DrawSphere(hitRight.point, 0.1f);
        }
    }
}
