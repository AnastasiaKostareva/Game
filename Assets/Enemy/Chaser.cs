using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class Chaser : MonoBehaviour
{
    public bool isTriggered;
    public bool damageSuccess;
    private readonly float attackDamageDelay = 0.5f;
    private readonly float attackDistance = 1.2f;
    private Player _player;
    private Entity _playerEntity;
    private Rigidbody2D _body;
    public float speed;
    private float walkCooldown;
    private float _attackTime;
    private bool forwalk;
    public bool isDead;
    public bool isMoving;
    public bool isAttacking;
    public AudioSource audioPlayer;
    [FormerlySerializedAs("attack")] public AudioClip attackClip;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _playerEntity = GameObject.FindGameObjectWithTag("Player").GetComponent<Entity>();
        _body = gameObject.GetComponent<Rigidbody2D>();
        _attackTime = 45 * Time.deltaTime;
        audioPlayer = gameObject.GetComponentInChildren<AudioSource>();
    }

    void Update()
    {
        if (isDead)
        {
            _body.velocity = new Vector2();
            return;
        }
        walkCooldown -= Time.deltaTime;
        TurnToPlayer();
        Behave();
    }
    


    private void Behave()
    {
        var distanceToPlayer = Vector2.Distance(transform.position, _player.transform.position);
        var playerPos = _player.transform.position - transform.position;

        if (Mathf.Abs(_body.velocity.x) < 1e-6 && Mathf.Abs(_body.velocity.y) < 1e-6) isMoving = false;
        else isMoving = true;

        ChooseAction(distanceToPlayer, playerPos);
    }

    private void ChooseAction(float distanceToPlayer, Vector3 playerPos)
    {
        if (isTriggered || distanceToPlayer <= 4.66f)
        {
            isTriggered = true;
            _body.velocity = playerPos.normalized * speed;
            isMoving = true;
            TryAttack(distanceToPlayer);
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
                _body.velocity = new Vector2(random.Next(2) - random.Next(2),
                    random.Next(2) - random.Next(2));
                isMoving = true;
            }
        }
    }

    private void TryAttack(float distanceToPlayer)
    {
        if (distanceToPlayer < attackDistance && isAttacking == false)
        {
            isMoving = false;
            audioPlayer.clip = attackClip;
            audioPlayer.Play();
            StartCoroutine(AttackCoroutine());
        }
    }

    private IEnumerator AttackCoroutine()
    {
        damageSuccess = true;
        isAttacking = true;
        var timer = 0f;

        while (timer < attackDamageDelay)
        {
            _body.velocity = new Vector2();
            isMoving = false;
            var distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);
            if (distanceToPlayer > attackDistance + 1f) // Если игрок вышел из радиуса поражения
            {
                damageSuccess = false;
            }

            timer += Time.deltaTime;
            yield return null;
        }

        isAttacking = false;
        // Если цикл завершился и игрок не вышел из радиуса, то наносим урон
        if (damageSuccess) _playerEntity.TakeDamage(1,attackDamageDelay);
        isAttacking = false;
    }
    private void TurnToPlayer()
    {
        GetComponent<SpriteRenderer>().flipX = _body.velocity.x < 0;
        if (_body.velocity.x == 0)
            GetComponent<SpriteRenderer>().flipX =
                _body.transform.position.x - _player.transform.position.x > 0;
    }
}