using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursorScript : MonoBehaviour
{
    private Vector3 mousePosition;

    void Start()
    {
    }

    void Update()
    {
        if (Time.timeScale != 0)
            RotateToMousePosition();
    }

    void RotateToMousePosition()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var difference = mousePosition - transform.position; 
        difference.Normalize();
        transform.rotation = Quaternion.Euler(
            0f, 0f, Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg);  
    }
}
