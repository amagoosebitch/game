using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyBehavior : MonoBehaviour
{
    public float speed = 3f;
    public int health = 10;
    public int damage = 10;
    
    private GameObject shrine;
    private Vector3 pos;
    private Vector3 direction;
    Vector2 blockDirection,escapeDirection;
    RaycastHit2D hit;
    private LayerMask _layerMask = 1 << 3;
    private float collisionDistance = 1f;
    private bool blocked = false;
    
    
    public GameObject self;
    
    public GameObject miniSnakes;
    
    public AudioClip deadSound;
    private bool isDead;
    
    void Start()
    {
        self = gameObject;
        shrine = GameObject.Find("Shrine");
        isDead = false;
        
    }

    void Update()
    {
        Move();
        if (isDead)
        {
            GetComponent<AudioSource>().PlayOneShot(deadSound);
            isDead = false;
        }
    }

    void Move()
    {
        // pos = (Vector2)self.transform.position;
        // var direction = (Vector2)(shrine.transform.position - pos).normalized;
        // RaycastHit2D way = Physics2D.Raycast(pos,direction, colllisioDistance, _layerMask);
        // if (!(way.collider is null))
        //     Debug.Log(way.collider.name);
        // Vector3 vec = direction.normalized;
        transform.position =  transform.position + speed *Time.deltaTime * RayPath(shrine.transform);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
        
    }

    void Die()
    {
        isDead = true;
        if (self.CompareTag("BigSnake"))
        {
            for(int i = 0; i < 20; ++i)
                Instantiate(miniSnakes, self.transform.position, Quaternion.identity);
        }
        Destroy(self);
    }

    private void OnTriggerEnter2D (Collider2D hitInfo)
    {
        //Debug.Log(hitInfo.name);
        playerMoveScript enemy = hitInfo.GetComponent<playerMoveScript>();
        if (enemy != null)
        { 
            enemy.TakeDamage(damage);
        }
        Shrine shrine = hitInfo.GetComponent<Shrine>();
        if (shrine != null)
        {
            shrine.TakeDamage(damage);
            Destroy(self);
        }
    }
    
    Vector3 RayPath(Transform target)
    {
        Vector2 position = transform.position;
        Vector2 direction = ((Vector2)target.position- position).normalized;

        if (!blocked)
        {
            hit = Physics2D.Raycast(position, direction, collisionDistance, _layerMask);
            if (!(hit.collider is null))
            {
                if (hit.transform == target) return direction;
                
                if(hit.distance < collisionDistance)
                {
                    blocked = true;
                    blockDirection = -hit.normal;
                    var normal = new Vector2(-direction.y, direction.x); 
                    escapeDirection = normal;
                    return escapeDirection;
                }
            }

            return direction;
        }

        hit = Physics2D.Linecast(position, target.position);
        if (!(hit.collider is null))
        {
            if(hit.transform == target) {blocked = false; return direction;}
        }
        
        if(Physics2D.Raycast(position, blockDirection, collisionDistance,_layerMask ))
        {
            return escapeDirection;
        }
        
        if(Physics2D.Raycast(position, direction, collisionDistance,_layerMask)) return escapeDirection;
        
        blocked = false;
        return direction;
    }
}