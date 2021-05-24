using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
using UnityEngine.UI;

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
    private bool waitForWave;
    [SerializeField] public Text WaveText;

    void Start()
    {
        waitForWave = true;
        expectation = 15;
        smallMobCount = 10;
        bigMobCount = 0;
        shootMobCount = 0;
        StartCoroutine(WaitForStart());
        WaveText.text = "wave soon!";
    }

    IEnumerator WaitForStart()
    {
        yield return new WaitForSeconds(expectation);
        flagForStart = true;
        waveCount = 0;
    }

    void Update()
    {
        if (!created && flagForStart && !GameObject.FindGameObjectWithTag("snake") && 
            !GameObject.FindGameObjectWithTag("BigSnake") && !GameObject.FindGameObjectWithTag("shootmob"))
        {
            if (!waitForWave)
                StartCoroutine(WaitForWave());
            else
                crt = StartCoroutine(Spawn());
        }
    }

    IEnumerator WaitForWave()
    {
        if (waveCount == 5)
        {
            StopCoroutine(crt);
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene("Finish");
        }
        WaveText.text = "wave soon!";
        yield return new WaitForSeconds(expectation);
        waitForWave = true;
    }

    IEnumerator Spawn()
    {
        WaveText.text = "Wave: " + (waveCount + 1);
        created = true;
        CreateWave(SmallSnake, 
            new Vector3(-28 + Random.Range(1, 5), 30 - Random.Range(1, 5), 0), 
            new Vector3(43 - Random.Range(1, 5), 30 - Random.Range(1, 5), 0), 
            smallMobCount);

        if (waveCount >= 1)
        {
            CreateWave(BigSnake, 
                new Vector3(-30 + Random.Range(1, 5), -29 + Random.Range(1, 5), 0), 
                new Vector3(45 - Random.Range(1, 5), -29 + Random.Range(1, 5), 0), 
                bigMobCount);
        }

        if (waveCount >= 2)
        {
            CreateWave(ShootMob,  
                new Vector3(10 + Random.Range(1, 5), -27 + Random.Range(1, 5), 0), 
                new Vector3(10 - Random.Range(1, 5), 29 - Random.Range(1, 5), 0), 
                shootMobCount);
        }
        
        
        yield return new WaitForSeconds(expectation);
        waveCount += 1;
        created = false;
        smallMobCount += Random.Range(10, 20);
        bigMobCount += Random.Range(1, 3);
        shootMobCount += shootMobCount <= 4 ? Random.Range(1, 2) : 0;
        waitForWave = false;
    }

    void CreateEnemy(GameObject mob, Vector3 vector)
    {
        Instantiate(mob, vector, Quaternion.identity);
    }

    void CreateWave(GameObject mob, Vector3 firstVector, Vector3 secondVector, int mobsCount)
    {
        for (int i = 0; i < mobsCount; i++)
        {
            CreateEnemy(mob, i % 2 == 0 ? firstVector : secondVector);
        }
    }
}