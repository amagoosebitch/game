using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class wallCreater : MonoBehaviour
{
    public Transform firePoint;
    private bool added;
    public GameObject playerWall;
    private int waitTime;
    [SerializeField] public Text wallCount;
    
    void Start()
    {
        waitTime = 30;
        added = false;
        wallCount.text = "available";
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.E) && !added)
        {
            added = true;
            wallCount.text = "recharge";
            wallCount.color = Color.blue;
            Instantiate(playerWall, firePoint.position, firePoint.rotation);
            StartCoroutine(Recharge());
        }
    }

    IEnumerator Recharge()
    {
        yield return new WaitForSeconds(waitTime);
        added = false;
        wallCount.text = "available";
        wallCount.color = Color.black;
    }
}
