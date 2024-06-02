using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimation : MonoBehaviour
{
    private Boss _boss;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private static readonly int Charging = Animator.StringToHash(IsCharging);
    private static readonly int Shooting = Animator.StringToHash(IsAttacking);
    private static readonly int Teleporting = Animator.StringToHash(IsTeleporting);
    private static readonly int Raging = Animator.StringToHash(OnRage);
    private static readonly int Dying = Animator.StringToHash(IsDead);
    private const string IsCharging = "isCharging";
    private const string IsAttacking = "isAttacking";
    private const string IsTeleporting = "isTeleporting";
    private const string OnRage = "onRage";
    private const string IsDead = "isDead";
    private void Awake()
    {
        _boss = gameObject.GetComponent<Boss>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetBool(Charging,_boss.isTriggered);
        _animator.SetBool(Shooting,_boss.isShooting);
        _animator.SetBool(Teleporting,_boss.isTeleporting);
        _animator.SetBool(Raging,_boss.onRage);
        _animator.SetBool(Dying,_boss.isDead);
    }
}
