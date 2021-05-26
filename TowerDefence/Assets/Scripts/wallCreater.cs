using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class wallCreater : MonoBehaviour
{
    public Transform firePoint;
    public bool added;
    public GameObject playerWall;
    private int waitTime;
    [SerializeField] public Text wallCount;
    
    void Start()
    {
        waitTime = 30;
        added = false;
        if(wallCount != null)
            wallCount.text = "available";
    }

    void Update()
    {
        if (Time.timeScale != 0 && Input.GetKey(KeyCode.E) && !added)
        {
            CreateWall();
        }
    }

    public void CreateWall()
    {
        added = true;
        if (wallCount != null)
        {
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
