using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerVisual : MonoBehaviour
{
    private Animator animator;
    private const string IS_RUN = "isRun";
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        if (positionPlayer.x <= positionMause.x)
            spriteRenderer.flipX = false;
        else
            spriteRenderer.flipX = true;
    }
}
