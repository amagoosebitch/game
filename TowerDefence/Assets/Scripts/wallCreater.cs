using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallCreater : MonoBehaviour
{
    public Transform firePoint;
    private bool added;
    public GameObject playerWall;
    private int waitTime;
    
    void Start()
    {
        waitTime = 30;
        added = false;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.E) && !added)
        {
            added = true;
            Instantiate(playerWall, firePoint.position, firePoint.rotation);
            StartCoroutine(Recharge());
        }
    }

    IEnumerator Recharge()
    {
        yield return new WaitForSeconds(waitTime);
        added = false;
    }
}
