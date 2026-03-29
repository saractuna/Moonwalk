using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    #region
    public List<Gun> allGuns; // Array
    public int currentGunIndex = 0;
    private Gun currentGun;
    #endregion

    void Start()
    {
        EquipGun(currentGunIndex); // to initialize the pistol at the start
    }

    void Update()
    {
        if (currentGun == null) return;

        // Handles the shooting for automatic and semi-automatic guns
        if (currentGun.isAutomatic)
        {
            if (Input.GetMouseButton(0))
            {
                currentGun.Shoot();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                currentGun.Shoot();
            }
        }

        // Handles the weapon swapping with number keys
        if (Input.GetKeyDown(KeyCode.Alpha1)) SwitchGun(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SwitchGun(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SwitchGun(2);

        // Aims the gun towards the cursor (wrong?)
        if (currentGun != null)
        {
            AimAtCursor(); // wrong?
        }

        // Manual reload
        if (Input.GetKeyDown(KeyCode.R))
        {
            currentGun?.Reload();
        }
    }

    // Equips a gun from the list by index
    void EquipGun(int index)
    {
        // Remove currently equipped gun
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Checks for a valid index (not sure why)
        if (index < 0 || index >= allGuns.Count)
        {
            Debug.LogWarning("Invalid gun index!");
            return;
        }

        // Instantiates and equip the new gun
        Gun gunPrefab = allGuns[index];
        Gun newGun = Instantiate(gunPrefab, transform.position, transform.rotation, transform);
        currentGun = newGun;
    }

    // Switches to a different gun
    void SwitchGun(int index)
    {
        if (index == currentGunIndex) return;

        currentGunIndex = index;
        EquipGun(currentGunIndex);
    }

    // Rotates the gun towards the mouse cursor and handles flipping
    void AimAtCursor()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        direction.z = 0f;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        // Flips the gun vertically if aiming to the left
        if (angle > 90f || angle < -90f)
        {
            currentGun.transform.localScale = new Vector3(1f, -1f, 1f);
        }
        else
        {
            currentGun.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}