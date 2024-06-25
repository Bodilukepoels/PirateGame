using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public static MenuController Instance;

    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public AudioSource playSound;
    public Animator animator;
    public float currentHealth;

    private Vector2 moveDirection;
    private bool isDead = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // Update de bewegingsrichting en animaties van de speler
        if (!isDead)
        {
            float moveX = Input.GetAxisRaw("Horizontal") * moveSpeed;
            float moveY = Input.GetAxisRaw("Vertical") * moveSpeed;
            moveDirection = new Vector2(moveX, moveY).normalized;

            animator.SetFloat("Horizontal", moveDirection.x);
            animator.SetFloat("Vertical", moveDirection.y);
            animator.SetFloat("Speed", moveDirection.sqrMagnitude);
        }
    }

    private void FixedUpdate()
    {
        // Pas de snelheid van de speler toe op de Rigidbody
        if (!isDead)
        {
            rb.velocity = moveDirection * moveSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    public void TakeDamage(float damage)
    {
        // Verminder de gezondheid van de speler en handel de dood af als de gezondheid nul is
        currentHealth -= damage;

        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            Die();
        }
    }

    void Die()
    {
        // Start de dood-animatie en laad vervolgens de "youDied" scene
        animator.SetBool("IsDead", true);
        StartCoroutine(DieHandler());
        playSound.Play();
    }

    IEnumerator DieHandler()
    {
        // Wacht enkele seconden voordat de scene geladen wordt
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("youDied");
        Destroy(gameObject);
    }
}
