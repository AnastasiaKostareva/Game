using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Entity : MonoBehaviour
{
    public int hp;
    public int damage;
    public float resist;
    public int maxHp;
    public GameObject chest;

    private void Update()
    {
        if (hp <= 0)
        {
            var flag = false;
            if (gameObject.CompareTag("Player"))
                flag = true;
            Destroy(gameObject);
            if (!flag && chest != null)
                Instantiate(chest, transform.position, transform.rotation);
            if (flag)
                SceneManager.LoadScene("PlayersDeath");
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
