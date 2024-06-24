using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float baseMoveSpeed = 3f;
    public float baseMaxHealth = 100f;
    public float baseAttackSpeed = 1f;

    private float moveSpeed;
    private float maxHealth;
    private float attackSpeed;

    public float currentHealth;
    public Rigidbody2D rb;
    public Weapon weapon;
    public Camera cam;

    private float timeOfLastShot;
    public AudioSource playSound;

    public HealthBar healthBar;

    Vector2 moveDirection;
    Vector2 mousePosition;

    void Start()
    {
        UpdateStats();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButton(0))
        {
            if (Time.time - timeOfLastShot >= attackSpeed)
            {
                weapon.Fire();
                timeOfLastShot = Time.time;
                playSound.Play();
            }
        }

        moveDirection = new Vector2(moveX, moveY).normalized;
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);

        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("youDied");
    }

    public void UpdateStats()
    {
        attackSpeed = baseAttackSpeed / Mathf.Pow(1.05f, ShopManagerScript.attackSpeed);
        moveSpeed = baseMoveSpeed * Mathf.Pow(1.01f, ShopManagerScript.speed);
        maxHealth = baseMaxHealth * Mathf.Pow(1.01f, ShopManagerScript.hp);

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);
    }
}