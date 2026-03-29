using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    void Start()
    {
        gunName = "Pistol";
        maxAmmo = 12;
        currentAmmo = maxAmmo;
        fireRate = 5f;
        damage = 10f;
        isAutomatic = false;
        reloadTime = 1.2f;
    }
}