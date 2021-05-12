using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMoveScript : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rid;
    private Vector2 vec;
    private bool opportunityAcceleration = true;
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
}
