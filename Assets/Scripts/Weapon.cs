using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireForce = 20f;

    public void Fire(float attackDamage)
    {
        GameObject bulletObject = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletObject.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.damage = Mathf.RoundToInt(attackDamage);
            bullet.rb.velocity = firePoint.up * bullet.speed;
        }
        else
        {
            Debug.LogError("Bullet script not found.");
        }
    }
}
