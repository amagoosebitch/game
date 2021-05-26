using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
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
        if (Time.timeScale != 0 && !created && flagForStart && !GameObject.FindGameObjectWithTag("snake") && 
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
        if (waveCount == 6)
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
        StartCoroutine( CreateWave(SmallSnake, 
            new Vector3(-28, 30, 0), 
            new Vector3(43, 30, 0), 
            smallMobCount));

        if (waveCount >= 1)
        {
            StartCoroutine(CreateWave(BigSnake, 
                new Vector3(-20, -25, 0), 
                new Vector3(30, -23, 0), 
                bigMobCount));
        }

        if (waveCount >= 2)
        {
            StartCoroutine(CreateWave(ShootMob,  
                new Vector3(10, -25, 0), 
                new Vector3(10, 25, 0), 
                shootMobCount));
        }
        
        
        yield return new WaitForSeconds(expectation);
        waveCount += 1;
        created = false;
        smallMobCount += Random.Range(10, 25);
        bigMobCount += Random.Range(1, 3);
        shootMobCount += shootMobCount <= 6 ? Random.Range(1, 2) : 0;
        waitForWave = false;
    }

    void CreateEnemy(GameObject mob, Vector3 vector)
    {
        Instantiate(mob, vector, Quaternion.identity);
    }

    IEnumerator CreateWave(GameObject mob, Vector3 firstVector, Vector3 secondVector, int mobsCount)
    {
        for (int i = 0; i < mobsCount; i++)
        {
            CreateEnemy(mob, i % 2 == 0 ? firstVector + new Vector3(Random.Range(-2,2) , Random.Range(-2,2) , 0)
                : secondVector + new Vector3(Random.Range(-2,2) , Random.Range(-2,2) , 0));
            yield return new WaitForSeconds(Random.Range(0.05f, 0.4f) / (waveCount + 1));
        }
    }
}