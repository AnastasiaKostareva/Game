using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Controls input;
    public float speed = 15;
    private float movementX;
    private float movementY;
    private new Rigidbody2D rigidbody;
    private bool canPickUp;


    // Start is called before the first frame update
    void Awake()
    {
        input = new Controls();
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        input.Player.ChangeVelocityX.performed += context => ChangeVelocityX(context.ReadValue<float>());
        input.Player.ChangeVelocityX.canceled += _ => ChangeVelocityX(0);
        input.Player.ChangeVelocityY.performed += context => ChangeVelocityY(context.ReadValue<float>());
        input.Player.ChangeVelocityY.canceled += _ => ChangeVelocityY(0);
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.velocity = new Vector2(movementX, movementY);
    }
    
    private void ChangeVelocityX(float hor)
    {
        movementX = hor * speed;
    }

    private void ChangeVelocityY(float ver)
    {
        movementY = ver * speed;
    }

    private void PickUp()
    {
        
    }


    private void OnEnable() => input.Enable();
    private void OnDisable() => input.Disable();
}