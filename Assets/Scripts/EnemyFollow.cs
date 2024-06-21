using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float moveSpeed = 5f;
    public int maxHealth = 100;
    public int currentHealth;
    public float attackRange = 2f;
    public int attackDamage = 10;
    public float timeBtwAttacks = 1f;
    private float timeOfLastAttack;

    private Transform player;
    private PlayerController playerController;

    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("PirateDungeon").transform;
        playerController = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 direction = player.position - transform.position;
            direction.Normalize();
            transform.position += direction * moveSpeed * Time.deltaTime;

            Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, transform.position.z);
            transform.right = targetPosition - transform.position;

            if (Vector3.Distance(transform.position, player.position) <= attackRange && Time.time - timeOfLastAttack >= timeBtwAttacks)
            {
                Attack();
                timeOfLastAttack = Time.time;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(10);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy took " + damage + " damage. Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();

        }
    }

    void Attack()
    {
        playerController.TakeDamage(attackDamage);
        Debug.Log("Enemy attacked the player for " + attackDamage + " damage!");
    }

    void Die()
    {
        Debug.Log("Enemy defeated!");
        int randomNumber = UnityEngine.Random.Range(10, 30);
        totalBananas.bananas += randomNumber;
        Destroy(gameObject);
    }
}