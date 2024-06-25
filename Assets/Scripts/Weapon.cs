using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint; // De positie waar de kogels vandaan komen
    public float fireForce = 20f;

    public void Fire(float attackDamage)
    {
        // Maakt een nieuw kogelobject aan op de positie van de firepoint en de rotation
        GameObject bulletObject = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Bullet bullet = bulletObject.GetComponent<Bullet>();

        if (bullet != null)
        {
            //Hoeveel damage er wordt aangebracht
            bullet.damage = Mathf.RoundToInt(attackDamage);

            // Kogelsnelheid
            bullet.rb.velocity = firePoint.up * bullet.speed;
        }
        else
        {
            Debug.LogError("Bullet script not found.");
        }
    }
}
