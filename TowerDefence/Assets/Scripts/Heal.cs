using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(LivingTime());
    }

    IEnumerator LivingTime()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        playerMoveScript player = other.GetComponent<playerMoveScript>();
        if (player != null)
        {
            if (player.health != 100)
                player.health++;
            Destroy(gameObject);
        }
    }
}
