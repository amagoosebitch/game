using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Heal))]
public class EnemyTest
{
    [UnityTest]
    public IEnumerator EnemyTestWithEnumeratorDies()
    {
        var go = new GameObject();
        var gameObject = MonoBehaviour.Instantiate(go);
        var enemy = gameObject.AddComponent<EnemyBehavior>();
        gameObject.tag = "snake";
        enemy.health = 10;
        enemy.TakeDamage(5);
        Assert.That(enemy.health == 5);
        enemy.TakeDamage(5);
        yield return null;
        Assert.That(gameObject == null);
    }
    
    [UnityTest]
    public IEnumerator EnemyTestBigSnakeSpawnsSmallSnakes()
    {
        var go = new GameObject();
        var gameObject = MonoBehaviour.Instantiate(go);
        var enemy = gameObject.AddComponent<EnemyBehavior>();
        var snakeGO = new GameObject();
        snakeGO.tag = "snake";
        snakeGO.AddComponent(typeof(EnemyBehavior));
        gameObject.tag = "BigSnake";
        enemy.health = 10;
        enemy.TakeDamage(5);
        enemy.miniSnakes = snakeGO;
        Assert.That(enemy.health == 5);
        enemy.TakeDamage(5);
        yield return null;
        Assert.That(gameObject == null);
        Assert.NotNull(GameObject.FindWithTag("snake"));
    }
}
