using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class Chaser : MonoBehaviour
{
    public bool isTriggered;
    private Player player;
    private Rigidbody2D body;
    public float speed;
    private float walkCooldown;
    private bool forwalk;
    public bool isMoving;
    public bool isAttacking;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        body = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        walkCooldown -= Time.deltaTime;
        GetComponent<SpriteRenderer>().flipX = body.velocity.x < 0;
        Chase();
    }

    void Chase()
    {
        var playerPos = player.transform.position - transform.position;
        if (Mathf.Abs(body.velocity.x) < 1e-6 && Mathf.Abs(body.velocity.y) < 1e-6) isMoving = false;

        if ( isTriggered || playerPos.x * playerPos.x + playerPos.y * playerPos.y <= 25)
        {
            isTriggered = true;
            body.velocity = playerPos.normalized * speed;
            isMoving = true;
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
                body.velocity = new Vector2(random.Next(2) - random.Next(2),
                    random.Next(2) - random.Next(2));
                isMoving = true;
            }
        }
    }
}