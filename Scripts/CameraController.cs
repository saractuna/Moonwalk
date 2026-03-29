using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region
    public Transform player;
    public float smoothSpeed = 1f;
    public Vector3 offset;
    public float lagTime = 0.2f;

    private Vector3 velocity = Vector3.zero; // Keeps track of smooth velocity
    #endregion

    void LateUpdate()
    {
        // Desired position including offset on top of it
        Vector3 desiredPosition = player.position + offset;

        // Applies a delay for smooth effect by using SmoothDamp
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, lagTime);
        transform.position = smoothedPosition;
    }
}
