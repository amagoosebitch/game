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
    public int currentBullets;
    public bool shootingFlag;
    public AudioClip fireSound;
    public AudioClip reloadSound;

    [SerializeField] public Text ammoCount;

    public GameObject bullet;
    void Start()
    {
        currentBullets = countBullets;
        shootingFlag = true;
    }

    void Update()
    {
        if (Time.timeScale != 0)
        {
            if (ammoCount != null)
            {
                if (shootingFlag && Input.GetMouseButtonDown(0))
                {
                    fireSequence = StartCoroutine(Shoot());
                }

                if (Input.GetMouseButtonUp(0) && fireSequence != null)
                {
                    StopCoroutine(fireSequence);
                }

                if (Input.GetKey(KeyCode.R) && shootingFlag)
                {
                    ammoCount.text = "Reload";
                    StartCoroutine(Recharge());
                }

                if (shootingFlag)
                    ammoCount.text = currentBullets + " / " + countBullets;
            }
        }
    }

    public IEnumerator Recharge()
    {
        shootingFlag = false;
        if(reloadSound != null)
            GetComponent<AudioSource>().PlayOneShot(reloadSound);
        yield return new WaitForSeconds(recharge);
        currentBullets = countBullets;
        shootingFlag = true;
    }

    public IEnumerator Shoot()
    {
        while (true)
        {
            if (currentBullets <= 0)
                yield break;
            if(bullet != null)
                Instantiate(bullet, firePoint.position, firePoint.rotation);
            if(firePoint != null)
                GetComponent<AudioSource>().PlayOneShot(fireSound);
            currentBullets--;
            yield return new WaitForSeconds(fireDelay);
        }
    }
    
}