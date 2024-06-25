using System.Collections;
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
            if (player != null)
            {
                Vector3 direction;

                if (!PlayerController.isDead)
                {
                    direction = player.position - transform.position;
                }
                else
                {
                    direction = transform.position - player.position;
                }

                direction.Normalize();
                transform.position += direction * moveSpeed * Time.deltaTime;

                Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, transform.position.z);
                transform.right = transform.position - targetPosition;

                if (!PlayerController.isDead && Vector3.Distance(transform.position, player.position) <= attackRange && Time.time - timeOfLastAttack >= timeBtwAttacks)
                {
                    Attack();
                    timeOfLastAttack = Time.time;
                }
            }
        }
    }

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

    IEnumerator ChangeColor()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }

    void Attack()
    {
        if (playerController != null)
        {
            playerController.TakeDamage(attackDamage);
            playSound.Play();
            Debug.Log($"Enemy attacked the player for {attackDamage}");
        }
    }

    void Die()
    {
        isKilled = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        animator.SetBool("IsKilled", true);
        Debug.Log("Enemy defeated!");
        StartCoroutine(DieHandler());
        int randomNumber = UnityEngine.Random.Range(30, 100);
        totalBananas.bananas += randomNumber;
        DeathSound.Play();

        capsuleCollider.enabled = false;
    }

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
