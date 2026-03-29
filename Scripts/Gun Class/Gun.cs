using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    #region Variables
    public string gunName;
    public int maxAmmo;
    public int currentAmmo;
    public float fireRate;
    public float damage;
    public Transform gunPoint;
    public GameObject bulletPrefab;
    public bool isAutomatic = false;
    public float reloadTime = 1.5f;
    public GameObject muzzleFlashPrefab;
    private float timeSinceLastShot;
    #endregion

    public bool isReloading { get; private set; } // Property to check if gun is currently reloading

    public virtual void Shoot()
    {
        if (isReloading) return; // Don't shoot while reloading

        if (Time.time - timeSinceLastShot >= 1 / fireRate)
        {
            if (currentAmmo > 0)
            {
                FireBullet();
                currentAmmo--;
                timeSinceLastShot = Time.time; // To prevent macros, by adding a cooldown
            }
            else
            {
                Debug.Log("Out of ammo!");
            }
        }
    }

    public virtual void Reload()
    {
        if (!isReloading && currentAmmo < maxAmmo) // There is no point reloading when ammo is full... duh
        {
            MonoBehaviour mono = GetComponent<MonoBehaviour>(); // Required to start coroutine in subclass
            if (mono != null) mono.StartCoroutine(ReloadCoroutine());
        }
    }

    private IEnumerator ReloadCoroutine()
    {
        isReloading = true;
        Debug.Log($"Reloading {gunName}...");

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;
        Debug.Log($"{gunName} reloaded.");
    }

    protected virtual void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, gunPoint.position, gunPoint.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.damage = damage;
        }
        StartCoroutine(ShowMuzzleFlash());
    }

    IEnumerator ShowMuzzleFlash()
    {
        if (muzzleFlashPrefab != null)
        {
            GameObject flash = Instantiate(muzzleFlashPrefab, gunPoint.position, gunPoint.rotation, gunPoint);
            flash.transform.localPosition += new Vector3(0.17f, 0, 0); // Offset for positioning adjustment

            SpriteRenderer sr = flash.GetComponent<SpriteRenderer>();
            sr.enabled = true;

            yield return new WaitForSeconds(0.05f);

            Destroy(flash);
        }
    }
}