using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public GameObject SmallSnake;
    public GameObject BigSnake;
    public int mobsCount;
    private bool created = false;
    private Coroutine crt;
    private float expectation;
    private int aliveMobs;
    
    void Start()
    {
        expectation = 15;
        aliveMobs = mobsCount;
    }

    void Update()
    {
        if(!created)
            crt = StartCoroutine(Spawn());
        if(aliveMobs <= 0)
            StopCoroutine(crt);
    }

    IEnumerator Spawn()
    {
        created = true;
        for (int i = 0; i < mobsCount / 3; i++)
        {
            Instantiate(SmallSnake, new Vector3(-30 + Random.Range(0, 5), 
                32 - Random.Range(0, 5), 0), Quaternion.identity);
            Instantiate(SmallSnake, new Vector3(47 - Random.Range(0, 5), 
                32 - Random.Range(0, 5), 0), Quaternion.identity);
            aliveMobs -= 2;
        }

        for (int i = 0; i < 2; ++i)
        {
            Instantiate(BigSnake, new Vector3(-30 + Random.Range(0, 5), 
                -29 + Random.Range(0, 5), 0), Quaternion.identity);
            Instantiate(BigSnake, new Vector3(45 - Random.Range(0, 5), 
                -29 + Random.Range(0, 5), 0), Quaternion.identity);
            aliveMobs -= 2;
        }
        
        yield return new WaitForSeconds(expectation);
        created = false;
    }
}
