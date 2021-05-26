using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = System.Random;

public class EnemyBehavior : MonoBehaviour
{
    public float speed = 3f;
    public int health = 10;
    public int damage = 10;
    
    public GameObject shrine;
    private Vector3 pos;
    private Vector3 direction;
    Vector2 blockDirection,escapeDirection;
    RaycastHit2D hit;
    private LayerMask _layerMask = 1 << 3;
    private float collisionDistance = 0.3f;
    private bool blocked = false;

    private Vector2[] dirrections = new[] {Vector2.left, Vector2.down,Vector2.right , Vector2.up };
    
    
    public GameObject self;
    public GameObject miniSnakes;

    public GameObject heal;
    
    void Start()
    {
        self = gameObject;
        shrine = GameObject.Find("Shrine");
    }

    void Update()
    {
        if (shrine != null)
            transform.position =  transform.position + speed * Time.deltaTime * RayPath(shrine.transform);
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
        if (gameObject.tag == "BigSnake")
        {
            for(int i = 0; i < 20; ++i)
                Instantiate(miniSnakes, gameObject.transform.position, Quaternion.identity);
        }
        if (heal != null)
            Instantiate(heal, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D (Collider2D hitInfo)
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
            Die();
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
                    var offset = -1;
                    var upDot = Vector2.Dot(direction, Vector2.up);
                    var rightDot = Vector2.Dot(direction, Vector2.right);
                    if (upDot*rightDot >= 0)
                        if (upDot <= 0)
                            offset = 0;
                        else
                            offset = 2;
                    else
                        if (upDot <= 0)
                            offset = 1;
                        else 
                            offset = 3;
                    for (var i = 0; i < 4; i++)
                    {
                        escapeDirection = Vector2.down;
                        var temp = Physics2D.Raycast(position, 
                            dirrections[(i + offset) % 4], collisionDistance, _layerMask);
                        if (temp.collider is null)
                        {
                            escapeDirection = dirrections[(i + offset) % 4];
                            return escapeDirection;
                        }
                    }

                    return escapeDirection;
                }
            }

            return direction;
        }

        hit = Physics2D.Linecast(position, target.position, _layerMask);
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