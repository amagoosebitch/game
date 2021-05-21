using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shrine : MonoBehaviour
{
    // Start is called before the first frame update
    private int health = 1000;

    [SerializeField] private Text hp;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hp.text = health + " / " + "1000";
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Debug.Log("game over");
            Destroy(gameObject);
        }
       // Debug.Log(health);
    }
}