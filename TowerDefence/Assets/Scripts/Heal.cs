using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnTriggerEnter2D(Collider2D other)
    {
        playerMoveScript player = other.GetComponent<playerMoveScript>();
        if (player != null)
        {
            if (player.health + 2 > 100)
                player.health = 100;
            else
                player.health += 2;
            Destroy(gameObject);
        }
    }
}
