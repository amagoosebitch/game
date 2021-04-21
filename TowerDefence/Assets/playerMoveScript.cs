using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMoveScript : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rid;
    private Vector2 vec;
    void Start()
    {
        rid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        vec.x = Input.GetAxis("Horizontal");
        vec.y = Input.GetAxis("Vertical");
        rid.velocity = new Vector2(vec.x * speed, vec.y * speed);
    }

    /*void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime, Space.World);
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
        }
    }*/
}
