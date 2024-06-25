using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Basis stats van de player
    public float baseMoveSpeed = 3f;
    public float baseMaxHealth = 100f;
    public float baseAttackSpeed = 1f;
    public float baseAttackDamage = 10f;

    // Variabelen voor huidige stats van de player en componenten
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
    public AudioSource playSound;

    // Variabele waarmee we de doodsanimatie laten werken
    public static bool isDead = false;

    private float timeOfLastShot;

    public SpriteRenderer spriteRenderer;

    // Referentie naar de health bar
    public HealthBar healthBar;

    // Richting van beweging en positie van de muis
    Vector2 moveDirection;
    Vector2 mousePosition;

    void Start()
    {
        // Initialiseer de stats van de player
        UpdateStats();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!isDead)
        {
            // Movement + schieten logica
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");

            if (Input.GetMouseButton(0) && Time.time - timeOfLastShot >= attackSpeed)
            {
                weapon.Fire(attackDamage);
                timeOfLastShot = Time.time;
                playSound.Play(); //SchietGeluid
            }

            moveDirection = new Vector2(moveX, moveY).normalized;
            mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            rb.velocity = moveDirection * moveSpeed;

            Vector2 aimDirection = mousePosition - rb.position;
            float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = aimAngle;
        }
        else
        {
            // Stopt alle beweging als de player dood is
            rb.velocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        StartCoroutine(ChangeColor()); // Start de coroutine voor kleurverandering

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    IEnumerator ChangeColor()
    {
        // Verandert de sprite naar rood voor eventjes, zodat je wel weet wanneer je bent gehit
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }

    void Die()
    {
        // IsDead wordt gezet op true, doodsanimatie en een schreeuw geluid wanneer je doodgaat
        isDead = true;
        animator.SetBool("IsDead", true);
        Scream.Play();
        StartCoroutine(DieHandler()); // Start het verwerkingsproces na de dood
    }

    IEnumerator DieHandler()
    {
        // Wacht een paar seconden en laad vervolgens de "youDied" scene en vernietig dit object
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("youDied");
        Destroy(gameObject);
    }

    public void UpdateStats()
    {
        // Update de stats van de player op basis van upgrades uit de shop
        attackSpeed = baseAttackSpeed / Mathf.Pow(1.08f, ShopManagerScript.attackSpeed);
        moveSpeed = baseMoveSpeed * Mathf.Pow(1.01f, ShopManagerScript.speed);
        maxHealth = baseMaxHealth * Mathf.Pow(1.15f, ShopManagerScript.hp);
        attackDamage = baseAttackDamage * Mathf.Pow(1.15f, ShopManagerScript.attackDamage);

        // Zorgt ervoor dat je currentHealth gelijk staat aan je maxHealth
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        // Update de health bar
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);
    }
}
