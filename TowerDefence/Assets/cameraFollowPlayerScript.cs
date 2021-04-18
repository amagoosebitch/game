using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollowPlayerScript : MonoBehaviour
{
    public GameObject player;
    
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("player");
    }

    void Update()
    {
        cameraFollow();
    }

    void cameraFollow()
    {
        var newPosition = new Vector3(
            player.transform.position.x, player.transform.position.y, this.transform.position.z);
        this.transform.position = newPosition;
    }
}
