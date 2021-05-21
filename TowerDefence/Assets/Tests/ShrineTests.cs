using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class NewTestScript
{
    // A Test behaves as an ordinary method
    [Test]
    public void ShrineTestSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator ShrineTestWithEnumeratorPasses()
    {
        var go = new GameObject();
        var gameObject = MonoBehaviour.Instantiate<GameObject>(go);
        var shrine = gameObject.AddComponent<Shrine>();
        shrine.TakeDamage(1000);
        yield return null;
        Assert.That(gameObject == null);
    }
}
