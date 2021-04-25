using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyBehavior : MonoBehaviour
{
    public int health = 10;
    public GameObject shrine;
    public GameObject self;
    public float speed;
    public int damage = 10;
    private Vector3 direction;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Move();
    }

     void Move()
     {
         direction = shrine.transform.position - self.transform.position;
         Vector3 vec = new Vector3(direction.x, direction.y, 0);
         vec.Normalize();
         transform.position =  transform.position + speed *Time.deltaTime * vec;
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
    }

    private void OnTriggerEnter2D (Collider2D hitInfo)
    {
        Debug.Log(hitInfo.name);
        Shrine shrine = hitInfo.GetComponent<Shrine>();
        if (shrine != null)
        {
            shrine.TakeDamage(damage);
            Destroy(gameObject);
            
        }
    }
}
