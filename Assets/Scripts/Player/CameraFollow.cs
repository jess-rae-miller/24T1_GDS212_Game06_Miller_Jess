using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform; // Assign the player's Transform in the Inspector
    public float smoothSpeed = 0.125f; // How smoothly the camera catches up with its target position
    public Vector3 offset; // Optional offset from the player's position

    void LateUpdate()
    {
        Vector3 desiredPosition = playerTransform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Optionally, if you want the camera to always look at the player:
        // transform.LookAt(playerTransform);
    }
}