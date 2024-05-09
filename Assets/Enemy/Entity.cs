using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public int hp;
    public int damage;
    public float resist;
    public float delay;

    private void Update()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }

        if (resist > 0) resist -= Time.deltaTime;
    }

    public void TakeDamage(int damage)
    {
        if (resist <= 0)
        {
            hp -= damage;
        }
    }
}
