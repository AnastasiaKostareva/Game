using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    private BoxCollider2D collider;

    private Controls input;
    public float speed = 15;
    private float movementX;
    private float movementY;
    private new Rigidbody2D rigidbody;
    private bool canPickUp;

    public bool isDashing;
    public float DashPower = 0;
    public float DashDuration = 2;
    [FormerlySerializedAs("curDashTime")] public float curDashPower;
    private float dashKoef;
    private float dashCoolDwon;

    private float minMovementSpeed = 0.1f;
    private bool isRun = false;
    
    public int keyCount;

    void Awake()
    {
        dashKoef = DashPower / 40;
        Instance = this;
        input = new Controls();
        collider = gameObject.GetComponent<BoxCollider2D>();
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        input.Player.ChangeVelocityX.performed += context => ChangeVelocityX(context.ReadValue<float>());
        input.Player.ChangeVelocityX.canceled += _ => ChangeVelocityX(0);
        input.Player.ChangeVelocityY.performed += context => ChangeVelocityY(context.ReadValue<float>());
        input.Player.ChangeVelocityY.canceled += _ => ChangeVelocityY(0);
    }

    void Update()
    {
        Move();
        curDashPower -= dashKoef;
        dashCoolDwon -= Time.deltaTime;
        if (dashCoolDwon <= 0) isDashing = false;

        if (Mathf.Abs(movementX) < minMovementSpeed && Mathf.Abs(movementY) < minMovementSpeed)
            isRun = false;
        else
            isRun = true;
    }

    public bool IsRunning()
    {
        return isRun;
    }

    private void Move()
    {
        if (Input.GetMouseButtonDown(1) && !isDashing)
        {
            dashCoolDwon = DashDuration;
            curDashPower = DashPower;
            isDashing = true;
        }

        if (movementY != 0 && movementX != 0)
            rigidbody.velocity = new Vector2(movementX, movementY) * 1 / 2 + FindDashVector();
        else rigidbody.velocity = (new Vector2(movementX, movementY) + FindDashVector()) * 3 / 4;
    }

    private Vector2 FindDashVector()
    {
        var x = 0f;
        var y = 0f;
        if (curDashPower <= 0) curDashPower = 0;
        if (movementX < 0) x = -curDashPower;
        if (movementX > 0) x = curDashPower;
        if (movementY < 0) y = -curDashPower;
        if (movementY > 0) y = curDashPower;
        return new Vector2(x, y);
    }

    private void ChangeVelocityX(float hor)
    {
        movementX = hor * speed;
    }

    private void ChangeVelocityY(float ver)
    {
        movementY = ver * speed;
    }

    public Vector3 GetPositionPlayer()
    {
        var playerPosition = Camera.main.WorldToScreenPoint(transform.position);
        return playerPosition;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Spike"))
            gameObject.GetComponent<Entity>().TakeDamage(3,0.3f);
    }

    private void OnEnable() => input.Enable();
    private void OnDisable() => input.Disable();
}