using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Initially set to the ghost in the Inspector

    // Method to update the camera's target
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    // Method to reset the camera's target back to the ghost
    public void ResetTarget()
    {
        target = GameObject.FindGameObjectWithTag("Ghost").transform;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // Simple follow logic, can be expanded with smoothing, limits, etc.
            transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        }
    }
}
