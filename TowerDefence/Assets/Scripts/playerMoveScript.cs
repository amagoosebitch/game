using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

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

    void Update()
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
        speed = currentSpeed * 2;
        opportunityAcceleration = false;
        yield return new WaitForSeconds(5);
        speed = currentSpeed;
        yield return new WaitForSeconds(15);
        opportunityAcceleration = true;

    }

    void Move()
    {
        rid.angularVelocity = 0;
        vec.x = Input.GetAxis("Horizontal");
        vec.y = Input.GetAxis("Vertical");
        rid.velocity = new Vector2(vec.x * speed, vec.y * speed);
    }
    
    
}
