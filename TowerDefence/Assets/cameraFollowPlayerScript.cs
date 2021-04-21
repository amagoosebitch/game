using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollowPlayerScript : MonoBehaviour
{
    public GameObject player;
    private bool follow = true;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if(follow == true)
            cameraFollow();
    }

    void setPlayer(bool setting)
    {
        follow = setting;
    }

    void cameraFollow()
    {
        transform.position = new Vector3(
            player.transform.position.x, player.transform.position.y, transform.position.z);
    }
}
