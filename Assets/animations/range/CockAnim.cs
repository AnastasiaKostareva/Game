using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CockAnim : MonoBehaviour
{
    private Animator _animator;
    private Ctrelok _cock;
    private Entity _cockEntity;
    private const string IsAttacking = "isAttacking";
    private const string IsDead = "isDead";
    private static readonly int Attacking = Animator.StringToHash(IsAttacking);
    private static readonly int Dying = Animator.StringToHash(IsDead);
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _cock = gameObject.GetComponentInParent<Ctrelok>();
        _cockEntity = gameObject.GetComponentInParent<Entity>();
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetBool(Attacking,_cock.isShooting);
        _animator.SetBool(Dying,_cockEntity.isDead);
    }
    
}
