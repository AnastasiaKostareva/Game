using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public int hp;
    public int damage;
    public float resist;
    public int maxHp;
    public bool isDead;
    public float deathTime = 2f;

    private void Update()
    {
        if (hp <= 0)
        {
            StartCoroutine(DeathRoutine());
        }
        resist -= Time.deltaTime;
    }

    public void TakeDamage(int damage, float delay)
    {
        if (resist <= 0)
        {
            hp -= damage;
            resist = delay;
        }
    }

    private IEnumerator DeathRoutine()
    {
        isDead = true;
        var timer = 0f;

        while (timer < deathTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}
