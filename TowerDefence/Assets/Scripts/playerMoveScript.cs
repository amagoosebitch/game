using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerMoveScript : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rid;
    private Vector2 vec;
    private bool opportunityAcceleration = true;
    public int health = 100;

    [SerializeField] public Text hp;
    [SerializeField] public Text acceleration;

    private bool isInvincible = false;
    private Animator anim;
    
    void Start()
    {
        rid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        acceleration.text = "available";
    }

    void FixedUpdate()
    {
        if (Time.timeScale != 0)
        {
            Move();
            if (Input.GetKey(KeyCode.Space) && opportunityAcceleration)
            {
                acceleration.text = "recharge";
                acceleration.color = Color.green;
                MakeAcceleration();
            }
        }

        hp.text = health + " / " + "100";
    }

    private void MakeAcceleration()
    {
        StartCoroutine(Acceleration(speed));
    }

    IEnumerator Acceleration(float currentSpeed)
    {
        speed = currentSpeed * 1.5f;
        opportunityAcceleration = false;
        yield return new WaitForSeconds(5);
        speed = currentSpeed;
        acceleration.color = Color.blue;
        yield return new WaitForSeconds(15);
        opportunityAcceleration = true;
        acceleration.text = "available";
        acceleration.color = Color.black;

    }

    void Move()
    {
        rid.angularVelocity = 0;
        vec.x = Input.GetAxisRaw("Horizontal");
        vec.y = Input.GetAxisRaw("Vertical");
        rid.velocity = new Vector2(vec.x * speed, vec.y * speed);
    }
    
    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            health -= damage;
            StartCoroutine(Invincible());
            if (health <= 0)
                Die();
        }
    }

    IEnumerator Invincible()
    {
        anim.SetBool("isInvincible", true);
        isInvincible = true;
        yield return new WaitForSeconds(1);
        anim.SetBool("isInvincible", false);
        isInvincible = false;
    }

    void Die()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("Fail");
    }
}