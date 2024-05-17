using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuCameraMovement : MonoBehaviour
{
    [SerializeField] List<GameObject> cameraMovePoints;
    [SerializeField] float cameraSpeed = 2f;
    [SerializeField] float minDistanceToPoint = 0.1f;

    int nextMovePointIdx = -1;

    private void Awake()
    {
        transform.position = cameraMovePoints[0].transform.position;
        nextMovePointIdx = 0;
    }

    void Update()
    {
        Vector3 targetPosition = cameraMovePoints[nextMovePointIdx].transform.position;
        float distance = Vector3.Distance(transform.position, targetPosition);

        if (distance > minDistanceToPoint)
        {
            //transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * cameraSpeed);
            transform.Translate((targetPosition - transform.position).normalized * Time.deltaTime * cameraSpeed , Space.World);
        }
        else
        {
            nextMovePointIdx = (1 + nextMovePointIdx) % cameraMovePoints.Count;
        }
    }
}
