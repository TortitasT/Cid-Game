using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTarget;
    [SerializeField] private Transform target;
    [SerializeField] private float targetZoom = 30f;
    [SerializeField] private float smoothness = 0.125f;
    [SerializeField] private Vector3 offset;
    private Vector3 velocity = Vector3.zero;

    private void FixedUpdate()
    {
        Vector3 targetPosition = target.position + offset;
        targetPosition.z = target.position.z - 100; // ZasY for camera

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothness);

        GetComponent<Camera>().orthographicSize = Mathf.SmoothStep(GetComponent<Camera>().orthographicSize, targetZoom, 0.5f);
    }
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void SetZoom(float newZoom)
    {
        targetZoom = newZoom;
    }

    public void SetOffset(Vector3 newOffset)
    {
        offset = newOffset;
    }

    public void Reset()
    {
        target = playerTarget;
        targetZoom = 30f;
        offset = Vector3.zero;
    }
}
