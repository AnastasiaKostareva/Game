using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimation : MonoBehaviour
{
    private Boss _boss;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private static readonly int Charging = Animator.StringToHash(IsCharging);
    private static readonly int Shooting = Animator.StringToHash(IsShooting);
    private const string IsCharging = "isCharging";
    private const string IsShooting = "isShooting";
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
    }
}
