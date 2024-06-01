using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ZombieAnime : MonoBehaviour
{
    private Chaser _chaserParent;
    private Animator _zombieAnimator;
    private const string IsRunning = "isRunning";
    private const string IsTriggered = "isTriggered";
    private const string IsAttacking = "isAttacking";
    private const string IsDead = "isDead";
    private SpriteRenderer _spriteRenderer;
    private GameObject _player;
    private static readonly int Running = Animator.StringToHash(IsRunning);
    private static readonly int Triggered = Animator.StringToHash(IsTriggered);
    private static readonly int Attacking = Animator.StringToHash(IsAttacking);
    private static readonly int Dying = Animator.StringToHash(IsDead);

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _chaserParent = gameObject.GetComponent<Chaser>();
        _zombieAnimator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
          _zombieAnimator.SetBool(Running,_chaserParent.isMoving);
          _zombieAnimator.SetBool(Triggered,_chaserParent.isTriggered);
          _zombieAnimator.SetBool(Attacking, _chaserParent.isAttacking);
          _zombieAnimator.SetBool(Dying,_chaserParent.isDead);
          if (_chaserParent.isTriggered)
          {
              // GetComponent<SpriteRenderer>().flipX = !(_player.transform.position.x > transform.position.x - 0.1);
              
          }
    }
}
