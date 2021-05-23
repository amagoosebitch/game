using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{

    public float speed;
    public int damage = 10;

    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        if (rb.name != "player")
            rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        EnemyBehavior enemy = hitInfo.GetComponent<EnemyBehavior>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        if (hitInfo.GetComponent<Heal>() == null)
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
