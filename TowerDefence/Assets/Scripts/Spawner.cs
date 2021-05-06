using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public GameObject mob;
    public int mobsCount;
    private bool created = false;
    private Coroutine crt;
    private float expectation;
    private int aliveMobs;
    
    // Start is called before the first frame update
    void Start()
    {
        expectation = 15;
        aliveMobs = mobsCount;
    }

    // Update is called once per frame
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
            Instantiate(mob, new Vector3(-30 + Random.Range(0, 5), 
                32 - Random.Range(0, 5), 0), Quaternion.identity);
        }

        aliveMobs -= mobsCount / 3;
        yield return new WaitForSeconds(expectation);
        created = false;
    }
}
