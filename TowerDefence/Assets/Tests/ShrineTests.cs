using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class NewTestScript
{
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
