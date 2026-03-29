using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    #region
    public int pelletCount = 5;
    public float spreadAngle = 15f;
    #endregion

    void Start()
    {
        gunName = "Shotgun";
        maxAmmo = 3;
        currentAmmo = maxAmmo;
        fireRate = 1.5f;
        damage = 25f;
        isAutomatic = false;
        reloadTime = 2.0f;
    }

    protected override void FireBullet() // I'll be perfectly honest this "pellet spread" logic isn't mine...
    {
        float halfSpread = spreadAngle / 2f;

        for (int i = 0; i < pelletCount; i++)
        {
            float angle = Random.Range(-halfSpread, halfSpread);
            Quaternion rotation = Quaternion.Euler(0, 0, gunPoint.eulerAngles.z + angle);
            Instantiate(bulletPrefab, gunPoint.position, rotation);
        }
    }
}