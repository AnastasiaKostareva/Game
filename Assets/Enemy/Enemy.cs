using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Health;
    public int Damage;
    public float resist;
    public float delay;
    public LayerMask isSolid;

    void Update()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
        }

        if (resist > 0) resist -= Time.deltaTime;
    }

    public void TakeDamage(int damage)
    {
        if (resist <= 0)
        {
            Health -= damage;
            resist = delay;
        }
    }
}