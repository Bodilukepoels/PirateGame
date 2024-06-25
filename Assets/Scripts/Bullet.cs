using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 40;
    public Rigidbody2D rb;
    public GameObject impactEffect;

    void Start()
    {
        rb.velocity = transform.up * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyFollow enemy = collision.gameObject.GetComponent<EnemyFollow>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }

        TilemapCollider2D tilemapCollider = collision.collider.GetComponent<TilemapCollider2D>();
        if (tilemapCollider != null)
        {
            InstantiateImpactEffect(collision.contacts[0].point);
        }

        Destroy(gameObject);
    }

    void InstantiateImpactEffect(Vector2 position)
    {
        if (impactEffect != null)
        {
            Vector3 impactPosition = new Vector3(position.x, position.y, -15f);

            GameObject effectInstance = Instantiate(impactEffect, impactPosition, Quaternion.identity);

            Destroy(effectInstance, 1f);
        }
    }
}
