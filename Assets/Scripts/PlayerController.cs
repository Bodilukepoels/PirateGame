using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float baseMoveSpeed = 3f;
    public float baseMaxHealth = 100f;
    public float baseAttackSpeed = 1f;
    public float baseAttackDamage = 10f;

    private float moveSpeed;
    private float maxHealth;
    private float attackSpeed;
    private float attackDamage;

    public float currentHealth;
    public Rigidbody2D rb;
    public Weapon weapon;
    public Camera cam;

    public Animator animator;
    public AudioSource Scream;

    public static bool isDead = false;

    private float timeOfLastShot;
    public AudioSource playSound;

    public SpriteRenderer spriteRenderer;

    public HealthBar healthBar;

    Vector2 moveDirection;
    Vector2 mousePosition;

    void Start()
    {
        UpdateStats();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!isDead)
        {
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");

            if (Input.GetMouseButton(0))
            {
                if (Time.time - timeOfLastShot >= attackSpeed)
                {
                    weapon.Fire(attackDamage);
                    timeOfLastShot = Time.time;
                    playSound.Play();
                }
            }

            moveDirection = new Vector2(moveX, moveY).normalized;
            mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);

            Vector2 aimDirection = mousePosition - rb.position;
            float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = aimAngle;
        }
        else
        {
            rb.velocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        StartCoroutine(ChangeColor());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    IEnumerator ChangeColor()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }

    void Die()
    {
        isDead = true;
        animator.SetBool("IsDead", true);
        Scream.Play();
        StartCoroutine(DieHandler());
    }

    IEnumerator DieHandler()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("youDied");
        Destroy(gameObject);
    }

    public void UpdateStats()
    {
        Debug.Log("Updating player stats...");

        attackSpeed = baseAttackSpeed / Mathf.Pow(1.08f, ShopManagerScript.attackSpeed);
        moveSpeed = baseMoveSpeed * Mathf.Pow(1.01f, ShopManagerScript.speed);
        maxHealth = baseMaxHealth * Mathf.Pow(1.15f, ShopManagerScript.hp);
        attackDamage = baseAttackDamage * Mathf.Pow(1.15f, ShopManagerScript.attackDamage);

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);
    }
}
