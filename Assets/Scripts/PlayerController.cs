﻿using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    public bool grounded;
    public float movementForce = 7.5f;
    public float maxspeed;
    public float bulletSpeed = 5f;
    public GameObject Bullet;
    public Text wintext;

    private Rigidbody rb;

    public int maxHealth = 100;
    public int currentHealth = 100;

    public Slider healthBar;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("ground"))
        {
            grounded = true;
        }
        if (col.CompareTag("ebullet"))
        {
            TakeDamage(5);
        }
        if (col.CompareTag("enemy"))
        {
            TakeDamage(10);
        }
        if (col.CompareTag("win"))
        {
            gameObject.SetActive(false);
            wintext.text = "You win!";
        }

    }

    // Update is called once per frame
    void Update()
    {
        float xmovement = Input.GetAxisRaw("Horizontal");
        Vector3 movement = new Vector3(xmovement, 0, 0);
        rb.velocity = new Vector3(xmovement * movementForce, rb.velocity.y, rb.velocity.z);

        if (Input.GetKeyDown("space") && grounded)
        {
            //rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            rb.velocity = new Vector3(rb.velocity.x, 10, rb.velocity.z);
            grounded = false;
        }

        if (rb.velocity.x > 7.5f)
        {
            rb.velocity = new Vector3(7.5f, rb.velocity.y, rb.velocity.z);
        }
        if (rb.velocity.x < -7.5f)
        {
            rb.velocity = new Vector3(-7.5f, rb.velocity.y, rb.velocity.z);
        }


        if (currentHealth <= 0)
        {
            this.gameObject.SetActive(false);
            wintext.text = "Game Over";
        }

    }
    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.value = currentHealth;
    }
}
