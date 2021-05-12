using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShootingEnemy : MonoBehaviour
{
    private GameObject player;
    public GameObject Vector;
    void Start()
    {
        player = GameObject.Find("player");
    }

    // Update is called once per frame
    void Update()
    {
        var destination = player.transform.position - gameObject.transform.position;
        var distance = Math.Sqrt(Math.Pow(destination.x, 2) + Math.Pow(destination.y, 2));
        if (distance <= 70 + double.Epsilon)
        {
            StartCoroutine(Shoot(destination));
        }
    }
    
    
    IEnumerator Shoot(Vector3 destination)
    {
        while (true)
        {
            destination.Normalize();
            var angle = Mathf.Atan2(destination.y, destination.x);
            var rotation = Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg);
            Instantiate(Vector, gameObject.transform.position, gameObject.transform.rotation);
            yield return new WaitForSeconds(0.7f);
        }
    }
}
