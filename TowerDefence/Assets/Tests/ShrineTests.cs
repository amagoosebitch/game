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
    
    [UnityTest]
    public IEnumerator WallCreaterTest()
    {
        var go = new GameObject();
        var gameObject = MonoBehaviour.Instantiate(go);
        var wall = gameObject.AddComponent<wallCreater>();
        wall.CreateWall();
        Assert.That(wall.added);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator ReloadBulletsTest()
    {
        var go = new GameObject();
        var gameObject = MonoBehaviour.Instantiate(go);
        var bullets = gameObject.AddComponent<Shooter>();
        bullets.Recharge();
        yield return new WaitForSeconds(2);
        Assert.That(bullets.shootingFlag);
    }
    
    [UnityTest]
    public IEnumerator ShootBulletsTest()
    {
        var go = new GameObject();
        var gameObject = MonoBehaviour.Instantiate(go);
        var bullets = gameObject.AddComponent<Shooter>();
        bullets.Shoot();
        yield return new WaitForSeconds(1);
        Assert.Greater(200, bullets.currentBullets);
    }
}
