using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Gun
{
    void Start()
    {
        gunName = "Rifle";
        maxAmmo = 30;
        currentAmmo = maxAmmo;
        fireRate = 7.5f;
        damage = 8f;
        isAutomatic = true;
        reloadTime = 1.7f;
    }
}