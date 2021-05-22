using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class ShrineUI : MonoBehaviour
{
    [SerializeField] private Text hp;

    // Update is called once per frame
    void Update()
    {
        Type type = gameObject.GetType();
        FieldInfo fieldInfo = type.GetField("health", BindingFlags.Instance | BindingFlags.NonPublic);
        var health = fieldInfo.GetValue(gameObject);
        hp.text = health + " / " + "1000";
    }
}
