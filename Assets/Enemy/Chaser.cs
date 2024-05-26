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
    private float _attackTime;
    private bool forwalk;
    public bool isMoving;
    public bool isAttacking;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        body = gameObject.GetComponent<Rigidbody2D>();
        _attackTime = 45 * Time.deltaTime;
    }

    void Update()
    {
        walkCooldown -= Time.deltaTime;
        GetComponent<SpriteRenderer>().flipX = body.velocity.x < 0;
        if (body.velocity.x == 0)
            GetComponent<SpriteRenderer>().flipX =
                body.transform.position.x - player.transform.position.x > 0;
        Chase();
    }

    void Chase()
    {
        var playerPos = player.transform.position - transform.position;
        if (isAttacking)
        {
            if (_attackTime > 0)
            {
                body.velocity = new Vector2();
                _attackTime -= Time.deltaTime;
                return;
            }
            else
            {
                _attackTime = 450 * Time.deltaTime;
                isAttacking = false;
            }
        }
        if (Mathf.Abs(body.velocity.x) < 1e-6 && Mathf.Abs(body.velocity.y) < 1e-6) isMoving = false;

        if ( isTriggered || playerPos.x * playerPos.x + playerPos.y * playerPos.y <= 25)
        {
            isTriggered = true;
            body.velocity = playerPos.normalized * speed;
            if (playerPos.x * playerPos.x < 0.1 &&  playerPos.y * playerPos.y < 0.01)
            {
                isAttacking = true;
            }
            else
            {
                isMoving = true;
            }
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