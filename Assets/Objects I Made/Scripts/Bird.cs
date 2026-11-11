using UnityEngine;
using UnityEngine.InputSystem;
// using UnityEngine.Physics2D;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class Bird : MonoBehaviour
{
    [SerializeField] private Tool Ability4Tool;
    private bool Ability4IsActive;
    private float Ability4StartTime;
    [SerializeField] public PlayerStats PlayerStats;
    [SerializeField] public GameManager GameManager;
    private int MaxMovementSpeed = 20;
    private float Acceleration = 80f;
    private float JumpForce = 14f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    public PlayerActions controls;
    public bool canMove = true;
    public Dictionary<string, int> abilityInventory = new Dictionary<string, int>();
    Collider2D col;

    [SerializeField]
    LayerMask groundLayer;
    [SerializeField]
    float groundCheckDistance = 0.05f;
    [SerializeField]
    float groundCheckHeight = 0.05f;
    [SerializeField]
    float groundCheckWidthPadding = 0.9f;

    void Awake()
    {
        abilityInventory["Hook"] = PlayerStats.HookCount;
        groundLayer = LayerMask.GetMask("Ground");
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        if (col == null) Debug.LogWarning("Bird: no Collider2D found — grounded checks will fail.");
        if (controls == null) controls = new PlayerActions();

        controls.TestInputs.BackToMenu.performed += ctx => BackToMenu();

        controls.PlayerMovement.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.PlayerMovement.Move.canceled += ctx => moveInput = Vector2.zero;
        controls.PlayerMovement.Jump.performed += ctx => Jump();
        controls.PlayerMovement.Ability4.performed += ctx => OnAbility4Start();
        controls.PlayerMovement.Ability4.canceled += ctx => OnAbility4End();
    }

    void FixedUpdate()
    {
        if (canMove) Move(moveInput);
    }

    void Jump()
    {
        if (IsGrounded())
        {
            if (rb != null)
            {
                rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
                Debug.Log("Jumping");
            }
            else
            {
                Debug.LogWarning("Rigidbody2D missing on Bird; cannot Apply jump force.");
            }
        }
        else
        {
            Debug.Log("Not grounded");
        }
    }

    void Move(Vector2 input)
    {
        float HorizontalInput = input.x;
        if (rb != null)
        {
            if (rb.linearVelocity.x < MaxMovementSpeed && HorizontalInput > 0)
            {
                rb.AddForce(new Vector2(HorizontalInput * Acceleration, 0));
            }
            else if (rb.linearVelocity.x > -MaxMovementSpeed && HorizontalInput < 0)
            {
                rb.AddForce(new Vector2(HorizontalInput * Acceleration, 0));
            }
        }
        else
        {
            Debug.LogWarning("Rigidbody2D missing on Bird; cannot move.");
        }
    }

    bool IsGrounded()
    {
        if (col == null) return false;

        Vector2 origin = (Vector2)transform.position + Vector2.down * (col.bounds.extents.y + groundCheckDistance * 0.5f);
        Vector2 size = new Vector2(col.bounds.size.x * groundCheckWidthPadding, groundCheckHeight);

        Collider2D[] hits = Physics2D.OverlapBoxAll(origin, size, 0f, groundLayer);
        foreach (var h in hits)
        {
            if (h != null && h.gameObject != gameObject)
                return true;
        }
        return false;
    }

    void OnDrawGizmosSelected()
    {
        if (col == null) col = GetComponent<Collider2D>();
        if (col == null) return;
        Gizmos.color = Color.cyan;
        Vector2 origin = (Vector2)transform.position + Vector2.down * (col.bounds.extents.y + groundCheckDistance * 0.5f);
        Vector2 size = new Vector2(col.bounds.size.x * groundCheckWidthPadding, groundCheckHeight);
        Gizmos.DrawWireCube(origin, size);
    }

    void BackToMenu()
    {
        Debug.Log("Returning to Menu");
        GameManager.Instance.LoadSceneFromIndex(0);
    }

    void OnAbility4Start()
    {
        if (Ability4Tool == null)
        {
            Debug.LogError("No tool assigned to Ability 4.", this);
            return;
        }
        Ability4IsActive = true;
        Ability4StartTime = Time.time;
        Ability4Tool.OnUseStart(this);
    }

    void OnAbility4End()
    {
        if (!Ability4IsActive || Ability4Tool == null) return;

        float ElapsedTime = Time.time - Ability4StartTime;
        Ability4Tool.OnUseEnd(this, moveInput, ElapsedTime);
        Ability4IsActive = false;
    }

    void OnEnable()
    {
        if (controls == null) controls = new PlayerActions();
        controls.TestInputs.Enable();
        controls.PlayerMovement.Enable();
    }

    void OnDisable()
    {
        if (controls != null)
        {
            controls.TestInputs.Disable();
            controls.PlayerMovement.Disable();
        }
    }
}
