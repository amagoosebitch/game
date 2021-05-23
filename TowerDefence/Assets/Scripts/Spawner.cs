using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public GameObject SmallSnake;
    public GameObject BigSnake;
    public GameObject ShootMob;
    private int smallMobCount;
    private int bigMobCount;
    private int shootMobCount;
    private bool created = false;
    private Coroutine crt;
    private float expectation;
    private bool flagForStart;
    private int waveCount;
    
    void Start()
    {
        expectation = 15;
        smallMobCount = 10;
        bigMobCount = 1;
        shootMobCount = 1;
        StartCoroutine(WaitForStart());
    }

    IEnumerator WaitForStart()
    {
        yield return new WaitForSeconds(expectation);
        flagForStart = true;
        expectation += 10;
        waveCount = 0;
    }

    void Update()
    {
        if (!created && flagForStart)
        {
            crt = StartCoroutine(Spawn());
        }
        if (waveCount == 5)
        {
            StopCoroutine(crt);
            SceneManager.LoadScene("Finish");
        }
        
    }
    
    
    IEnumerator Spawn()
    {
        created = true;
        for (int i = 0; i < smallMobCount; i++)
        {
            if(i % 2 == 0)
                CreateEnemy(SmallSnake, new Vector3(-30 + Random.Range(0, 5), 
                    32 - Random.Range(1, 5), 0));
            else
                CreateEnemy(SmallSnake, new Vector3(47 - Random.Range(0, 5), 
                    32 - Random.Range(1, 5), 0));
        }

        for (int i = 0; i < bigMobCount; ++i)
        {
            if(i % 2 == 0)
                CreateEnemy(BigSnake, new Vector3(-30 + Random.Range(0, 5), 
                    -29 + Random.Range(1, 5), 0));
            else
                CreateEnemy(BigSnake, new Vector3(45 - Random.Range(0, 5), 
                    -29 + Random.Range(1, 5), 0));
        }

        for (int i = 0; i < shootMobCount; ++i)
        {
            if(i % 2 == 0)
                CreateEnemy(ShootMob, new Vector3(10 + Random.Range(0, 5), 
                    -27 + Random.Range(1, 5), 0));
            else
                CreateEnemy(ShootMob, new Vector3(10 - Random.Range(0, 5), 
                    29 - Random.Range(1, 5), 0));
        }
        yield return new WaitForSeconds(expectation);
        created = false;
        expectation += 15;
        smallMobCount += Random.Range(10, 20);
        bigMobCount += Random.Range(1, 3);
        shootMobCount += shootMobCount <= 4 ? Random.Range(1, 2) : 0;
        waveCount += 1;
    }

    void CreateEnemy(GameObject mob, Vector3 vector)
    {
        Instantiate(mob, vector, Quaternion.identity);
    }
}
