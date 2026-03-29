using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    #region
    public Texture2D crosshairTexture;
    private Vector2 crosshairOffset;
    private SpriteRenderer spriteRenderer;
    #endregion

    void Start()
    {
        Cursor.visible = false; // Hides the mouse cursor at the start (comes back if paused :/)
        Cursor.lockState = CursorLockMode.Confined; // Keeps the crosshair inside the game view

        crosshairOffset = new Vector2(crosshairTexture.width / 2f, crosshairTexture.height / 2f);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10f; // Makes sure it’s rendered in the front of everything (doesn't work with ui :/)

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = worldPos;
    }
}
