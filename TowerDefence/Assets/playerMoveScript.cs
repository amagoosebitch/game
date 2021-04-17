using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMoveScript : MonoBehaviour
{
    public GameObject player;

    public float speed;
    
    void Start()
    {
        
    }

    void Update()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        player.transform.Translate(
            new Vector3(h*speed*Time.deltaTime, v*speed*Time.deltaTime, 0));
        
    }
}
