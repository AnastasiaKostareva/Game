using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Chaser : MonoBehaviour
{
    private bool isTriggered;
    private Player player;
    private Rigidbody2D body;
    public float speed;
    private float walkCooldown;
    private bool forwalk;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        body = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Chase();
        walkCooldown -= Time.deltaTime;
    }

    void Chase()
    {
        var playerPos = player.transform.position - transform.position;
        if (isTriggered || playerPos.x * playerPos.x + playerPos.y * playerPos.y <= 25)
        {
            isTriggered = true;
            body.velocity = playerPos.normalized * speed;
        }
        else if (walkCooldown <= 0)
        {
            if (forwalk)
            {
                walkCooldown = Time.deltaTime * 60;
                forwalk = false;
            }
            else
            {
                forwalk = true;
                walkCooldown = 120 * Time.deltaTime;
                var random = new Random();
                body.velocity = new Vector2(random.Next(2) - random.Next(2), random.Next(2) - random.Next(2));
            }
        }
    }
}