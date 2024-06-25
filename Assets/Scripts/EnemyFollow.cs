using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float attackRange = 2f;
    public int baseHealth = 100;
    public int baseAttackDamage = 10;
    public float timeBtwAttacks = 1f;

    private int currentHealth;
    private int attackDamage;
    private float timeOfLastAttack;
    private Transform player;
    private PlayerController playerController;
    private bool isKilled = false;

    public Animator animator;
    public Rigidbody2D rb;
    public AudioSource playSound;
    public AudioSource DeathSound;
    public SpriteRenderer spriteRenderer;
    public CapsuleCollider2D capsuleCollider;

    private EnemySpawner spawner;

    void Start()
    {
        currentHealth = baseHealth + 15 * GameManager.roundCount;
        attackDamage = baseAttackDamage + 15 * GameManager.roundCount;
        player = GameObject.FindGameObjectWithTag("PirateDungeon").transform;
        if (player != null)
        {
            playerController = player.GetComponent<PlayerController>();
        }
    }

    void Update()
    {
        if (!isKilled)
        {
            if (player != null && !PlayerController.isDead)
            {
                Vector3 direction = player.position - transform.position;
                direction.Normalize();
                transform.position += direction * moveSpeed * Time.deltaTime;

                Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, transform.position.z);
                transform.right = transform.position - targetPosition;

                if (Vector3.Distance(transform.position, player.position) <= attackRange && Time.time - timeOfLastAttack >= timeBtwAttacks)
                {
                    Attack();
                    timeOfLastAttack = Time.time;
                }
            }
        }
    }

    // Functie om damage te krijgen
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"Enemy took {damage} damage.");

        StartCoroutine(ChangeColor());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void SetSpawnerReference(EnemySpawner spawner)
    {
        this.spawner = spawner;
    }

    // Coroutine om rood te worden als de banaan damage krijgt
    IEnumerator ChangeColor()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }

    // Functie om aan te vallen
    void Attack()
    {
        if (playerController != null)
        {
            playerController.TakeDamage(attackDamage);
            playSound.Play();
            Debug.Log($"Enemy attacked the player for {attackDamage}");
        }
    }

    // Functie om dood te gaan
    void Die()
    {
        isKilled = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        animator.SetBool("IsKilled", true);
        Debug.Log("Enemy defeated!");
        StartCoroutine(DieHandler());
        int randomNumber = UnityEngine.Random.Range(30, 100);
        totalBananas.bananas += randomNumber; // Toevoegen van bananen aan het totale aantal bananen
        DeathSound.Play();

        capsuleCollider.enabled = false;
    }

    // Coroutine om dood gaan te behandelen
    IEnumerator DieHandler()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);

        if (spawner != null)
        {
            spawner.EnemyDied();
        }
    }
}

