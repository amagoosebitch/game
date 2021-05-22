using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class NewTestScript
{
    // A Test behaves as an ordinary method
    [Test]
    public void ShrineTestSimplePasses()
    {
        var go = new GameObject();
        var gameObject = MonoBehaviour.Instantiate(go);
        var shrine = gameObject.AddComponent<Shrine>();
        shrine.TakeDamage(100);
        Type type = shrine.GetType();
        FieldInfo fieldInfo = type.GetField("health", BindingFlags.Instance | BindingFlags.NonPublic);
        var health = fieldInfo.GetValue(shrine);
        Assert.That((int)health == 900);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator ShrineTestWithEnumeratorPasses()
    {
        var go = new GameObject();
        var gameObject = MonoBehaviour.Instantiate(go);
        var shrine = gameObject.AddComponent<Shrine>();
        shrine.TakeDamage(1000);
        yield return null;
        Assert.That(gameObject == null);
    }
}
