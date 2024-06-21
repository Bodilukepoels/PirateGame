using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
		Destroy(gameObject);
	}
}