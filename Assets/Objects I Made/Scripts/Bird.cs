using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class Bird : MonoBehaviour
{
    private int MaxMovementSpeed = 20;
    private float Acceleration = 20f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    PlayerActions controls;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controls = new PlayerActions();

        controls.gameplay.Grow.performed += ctx => Grow();
        controls.gameplay.ChangeScene1.performed += ctx => ChangeScene1();
        controls.gameplay.ChangeScene2.performed += ctx => ChangeScene2();

        controls.PlayerMovement.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.PlayerMovement.Move.canceled += ctx => moveInput = Vector2.zero;
    }

    void FixedUpdate()
    {
        Move(moveInput);
    }

    void Move(Vector2 input)
    {
        float HorizontalInput = input.x;
        if (Mathf.Abs(rb.linearVelocity.x) < MaxMovementSpeed)
        {
            rb.AddForce(new Vector2(HorizontalInput * Acceleration, 0));
        }
    }

    void Grow()
    {
        Debug.Log("Growing!");
        transform.localScale *= 1.1f;
    }

    void ChangeScene1()
    {
        Debug.Log("Changing to SampleScene");
        GameManager.Instance.LoadSceneFromIndex(0);
    }

    void ChangeScene2()
    {
        Debug.Log("Changing to SecondScene");
        GameManager.Instance.LoadSceneFromIndex(1);
    }


    void OnEnable()
    {
        controls.gameplay.Enable();
        controls.PlayerMovement.Enable();
    }

    void OnDisable()
    {
        controls.gameplay.Disable();
        controls.PlayerMovement.Disable();
    }
}
