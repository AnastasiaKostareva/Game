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

    private void Update()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
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
}
