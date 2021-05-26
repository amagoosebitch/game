using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShootingEnemy : MonoBehaviour
{
    public GameObject player;
    public GameObject Vector;
    private bool flag = true;
    private Coroutine fire;
    public Transform firePoint;
    
    public void Start()
    {
        player = GameObject.Find("player");
    }

    // Update is called once per frame
    void Update()
    {
        RotateFace();
        var destination = player.transform.position - gameObject.transform.position;
        var distance = Math.Sqrt(Math.Pow(destination.x, 2) + Math.Pow(destination.y, 2));
        if (flag && distance <= 20)
        {
            flag = false;
            fire = StartCoroutine(Shoot());
        }
        
        if(fire != null && distance > 20 + double.Epsilon)
        {
            flag = true;
            StopCoroutine(fire);
        }
    }
    
    
    IEnumerator Shoot()
    {
        while (true)
        {
            var destination = gameObject.transform.right;
            destination.Normalize();
            if (Vector != null && firePoint != null)
                Instantiate(Vector, firePoint.position, firePoint.rotation);
            yield return new WaitForSeconds(1f);
        }
    }
    
    public void RotateFace()
    {
        var playerPosition = player.transform.position;
        var difference = playerPosition - transform.position; 
        difference.Normalize();
        var angle =
            Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(
            0f, 0f, angle);  
    }
    
}
