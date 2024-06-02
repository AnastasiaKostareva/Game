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
    public bool isDead;
    public float deathTime = 2f;
    public GameObject chest;

    private void Update()
    {
        if (hp <= 0)
        {
            var flag = gameObject.CompareTag("Player");
            StartCoroutine(DeathRoutine());
            switch (flag)
            {
                case false when chest != null:
                    
                    break;
                case true:
                    SceneManager.LoadScene("PlayersDeath");
                    break;
            }
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
        if (gameObject.GetComponent<Chaser>() != null)
        {
            gameObject.GetComponent<Chaser>().isDead = true;
        }
        else if (gameObject.GetComponent<Ctrelok>() != null)
        {
            gameObject.GetComponent<Ctrelok>().isDead = true;
        }

        while (timer < deathTime)
        {
            timer += Time.deltaTime;    
            yield return null;
        }
        if (chest != null)
            Instantiate(chest, transform.position, transform.rotation);
        if (CounterMonsters.instance != null)
            CounterMonsters.instance.RemoveMonster();
        Destroy(gameObject);
    }
}
