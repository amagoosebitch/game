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
        player = GameObject.Find("player");
        var destination = player.transform.position - gameObject.transform.position;
        var v2 = new Vector2(destination.x, destination.y);
        v2.Normalize();
        if (rb.name != "AlgemEnemy")
            rb.velocity = destination * speed;
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
