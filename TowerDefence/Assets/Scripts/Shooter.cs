using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooter : MonoBehaviour
{

    public Transform firePoint;
    Coroutine fireSequence;
    private float fireDelay = 0.1f;
    public int countBullets;
    private float recharge = 2.0f;
    private int currentBullets;
    private bool shootingFlag;
    public AudioClip fireSound;

    [SerializeField] public Text ammoCount;

    public GameObject bullet;
    void Start()
    {
        currentBullets = countBullets;
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
            ammoCount.text = "Перезарядка";
            StartCoroutine(Recharge());
        }
        if(shootingFlag)
            ammoCount.text = currentBullets + " / " + countBullets;
    }

    IEnumerator Recharge()
    {
        shootingFlag = false;
        yield return new WaitForSeconds(recharge);
        currentBullets = countBullets;
        shootingFlag = true;
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            if (currentBullets <= 0)
                yield break;
            Instantiate(bullet, firePoint.position, firePoint.rotation);
            GetComponent<AudioSource>().PlayOneShot(fireSound);
            currentBullets--;
            yield return new WaitForSeconds(fireDelay);
        }
    }
    
}