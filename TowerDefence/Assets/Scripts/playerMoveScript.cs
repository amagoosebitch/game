using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerMoveScript : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rid;
    private Vector2 vec;
    private bool opportunityAcceleration = true;
    public int health = 100;

    [SerializeField] public Text hp;
    void Start()
    {
        rid = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Move();
        if (Input.GetKey(KeyCode.Space) && opportunityAcceleration)
        {
            MakeAcceleration();
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
        yield return new WaitForSeconds(15);
        opportunityAcceleration = true;

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
        health -= damage;
        if (health <= 0)
            Die();
    }
    
    void Die()
    {
        Destroy(gameObject);
        Debug.Log("game over");
    }
}
