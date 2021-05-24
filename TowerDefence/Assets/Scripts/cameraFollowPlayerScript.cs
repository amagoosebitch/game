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
        if (Input.GetKey(KeyCode.LeftShift))
        {
            follow = false;
        }
        else
        {
            follow = true;
        }

        if (follow == true)
        {
            cameraFollow();
        }
        else
        {
            ShiftLook();
        }
    }

    private void ShiftLook()
    {
        var direction = Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)) - transform.position;
        if(player.GetComponent<SpriteRenderer>().isVisible)
            transform.Translate(direction*2*Time.deltaTime);

    }

    void cameraFollow()
    {
        transform.position = new Vector3(
            player.transform.position.x, player.transform.position.y, transform.position.z);
    }
}
