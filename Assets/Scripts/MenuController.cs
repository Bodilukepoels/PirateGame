using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public AudioSource playSound;
    public Animator animator;
    public float currentHealth;

    Vector2 moveDirection;
    bool isDead = false;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (isDead)
        {
            return;
        }

        float moveX = Input.GetAxisRaw("Horizontal") * moveSpeed;
        float moveY = Input.GetAxisRaw("Vertical") * moveSpeed;

        moveDirection = new Vector2(moveX, moveY).normalized;

        animator.SetFloat("Horizontal", moveDirection.x);
        animator.SetFloat("Vertical", moveDirection.y);
        animator.SetFloat("Speed", moveDirection.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        if (isDead)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            Die();
        }
    }

    void Die()
    {
        animator.SetBool("IsDead", true);
        StartCoroutine(DieHandler());

        playSound.Play();
    }

    IEnumerator DieHandler()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("youDied");
        Destroy(gameObject);
    }
}
