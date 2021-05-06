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

    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        currentBullets = countBullets;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            if (currentBullets <= 0)
                StartCoroutine(Recharge());
            fireSequence = StartCoroutine(Shoot());
        }

        if (Input.GetMouseButtonUp(0))
        {
            StopCoroutine(fireSequence);
        }
    }

    IEnumerator Recharge()
    {
        currentBullets = countBullets;
        yield return new WaitForSeconds(recharge);
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            if (currentBullets <= 0)
            {
                currentBullets = countBullets;
                yield return new WaitForSeconds(recharge);
            }
            Instantiate(bullet, firePoint.position, firePoint.rotation);
            currentBullets--;
            yield return new WaitForSeconds(fireDelay);
        }
    }
    
}