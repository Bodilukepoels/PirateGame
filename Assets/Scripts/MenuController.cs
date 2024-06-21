using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public AudioSource playSound;
    public Animator animator;

    Vector2 moveDirection;

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal") * moveSpeed;
        float moveY = Input.GetAxisRaw("Vertical") * moveSpeed;

       animator.SetFloat("Horizontal", moveDirection.x);
        animator.SetFloat("Vertical", moveDirection.y);
        animator.SetFloat("Speed", moveDirection.sqrMagnitude);

        moveDirection = new Vector2(moveX, moveY).normalized;
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
}
