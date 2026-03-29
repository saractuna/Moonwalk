using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPickup : MonoBehaviour
{
    [Header("Floating")]
    public float floatSpeed = 1f;       // Speed of up-down motion
    public float floatHeight = 0.1f;    // Distance of up-down sway

    [Header("Pulsing")]
    public float pulseSpeed = 2f;       // Speed of scaling
    public float pulseAmount = 0.05f;   // How much it scales

    private Vector3 startPos;
    private Vector3 baseScale;

    void Start()
    {
        startPos = transform.position;
        baseScale = transform.localScale;
    }

    void Update()
    {
        // Floats up and down
        float yOffset = Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(startPos.x, startPos.y + yOffset, startPos.z);

        // Pulse scale
        float scaleOffset = Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
        transform.localScale = baseScale + new Vector3(scaleOffset, scaleOffset, 0f);
    }
}
