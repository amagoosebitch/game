using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    public Transform firePoint;
    Coroutine fireSequence;
    private float fireDelay = 0.1f;
    public int countBullets;
    private float recharge = 2.0f;
    private int currentBullets;
    private bool shootingFlag;

    public GameObject bullet;
    void Start()
    {
        currentBullets = 5;
        shootingFlag = true;
    }

    void Update()
    {
        
        if (shootingFlag && Input.GetMouseButtonDown(0))
        {
            fireSequence = StartCoroutine(Shoot());
        }

        if (Input.GetMouseButtonUp(0) && fireSequence != null)
        {
            StopCoroutine(fireSequence);
        }

        if (Input.GetKey(KeyCode.R))
        {
            StartCoroutine(Recharge());
        }
    }

    IEnumerator Recharge()
    {
        shootingFlag = false;
        currentBullets = countBullets;
        yield return new WaitForSeconds(recharge);
        shootingFlag = true;
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            if (currentBullets <= 0)
                yield break;
            Instantiate(bullet, firePoint.position, firePoint.rotation);
            currentBullets--;
            yield return new WaitForSeconds(fireDelay);
        }
    }
    
}