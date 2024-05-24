using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class dolbaeb_zombie : MonoBehaviour
{
    private Chaser chaserParent;
    private Animator _zombieAnimator;
    private const string IsRunning = "isRunning";
    private const string IsTriggered = "isTriggered";
    private SpriteRenderer _spriteRenderer;
    private GameObject player;
    
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        chaserParent = gameObject.GetComponent<Chaser>();
        _zombieAnimator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
          _zombieAnimator.SetBool(IsRunning,chaserParent.isMoving);
          _zombieAnimator.SetBool(IsTriggered,chaserParent.isTriggered);
          if (chaserParent.isTriggered)
          {
              GetComponent<SpriteRenderer>().flipX = !(player.transform.position.x > transform.position.x - 0.1);
          }
    }
}
