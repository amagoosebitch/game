using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyBehavior : MonoBehaviour
{
    public int health = 10;
    public GameObject shrine;
    public float speed = 3f;
    public int damage = 10;
    public GameObject self;
    private Vector3 direction;
    void Start()
    {
        self = gameObject;
        shrine = GameObject.Find("Shrine");
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        direction = shrine.transform.position - self.transform.position;
        RaycastHit2D way = Physics2D.Raycast(self.transform.position, direction);
        Vector3 vec = new Vector3(direction.x, direction.y, 0);
        vec.Normalize();
        transform.position =  transform.position + speed *Time.deltaTime * vec;
        
        // if (way)
        // {
        //     Wall obj = way.transform.GetComponent<Wall>();
        //     Debug.Log(obj.name);
        // }
        
         
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
            Die();
    }

    void Die()
    {
        Destroy(self);
    }

    private void OnTriggerEnter2D (Collider2D hitInfo)
    {
        //Debug.Log(hitInfo.name);
        Shrine shrine = hitInfo.GetComponent<Shrine>();
        if (shrine != null)
        {
            shrine.TakeDamage(damage);
            Destroy(self);
            
        }
    }
}