using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorBehaviour : MonoBehaviour
{
    public float speed;
    public int damage = 20;

    public Rigidbody2D rb;
    private GameObject player;
    void Start()
    {
        if (rb != null && rb.name != "AlgemEnemy")
            rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        playerMoveScript enemy = hitInfo.GetComponent<playerMoveScript>();
        if (enemy != null)
        { 
            enemy.TakeDamage(damage);
        }
        Shrine shrine = hitInfo.GetComponent<Shrine>();
        if (shrine != null)
        { 
            shrine.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
    
}
