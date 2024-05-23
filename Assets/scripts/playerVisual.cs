using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerVisual : MonoBehaviour
{
    private Animator animator;
    private const string IS_RUN = "isRun";
    private SpriteRenderer spriteRenderer;
    private GameObject gun;
    private PauseMenu pause;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gun = HelpTool.FindNearestGameObject("HeroGun", gameObject);
        pause = FindObjectOfType<PauseMenu>();
    }

    private void Update()
    {
        animator.SetBool(IS_RUN, Player.Instance.IsRunning());
        CompareMouseAndPlayer();
    }

    public Vector3 GetMousePosition()
    {
        var positionMouse = Mouse.current.position.ReadValue();
        return positionMouse;
    }

    public void CompareMouseAndPlayer()
    {
        var positionMause = GetMousePosition();
        var positionPlayer = Player.Instance.GetPositionPlayer();
        var gunRender = gun.GetComponent<SpriteRenderer>();
        if (positionPlayer.x <= positionMause.x && !pause.PauseGame)
        {
            spriteRenderer.flipX = false;
            gunRender.flipY = false;
        }
        else if (positionPlayer.x > positionMause.x && !pause.PauseGame)
        {
            spriteRenderer.flipX = true;
            gunRender.flipY = true;
        }   
    }
}
