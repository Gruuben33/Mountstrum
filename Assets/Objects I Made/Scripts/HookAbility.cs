using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class HookAbility : MonoBehaviour
{
    [SerializeField] private Transform HookPosition;
    [SerializeField] private GameObject HookPrefab;
    [SerializeField] private Bird player;
    [SerializeField] private PlayerStats PlayerStats;
    [SerializeField] private float ThrowStrength = 10f;
    // Coroutine HookCoroutine;
    private Vector3 spawnPosition;
    private float spawnOffset = 0.5f;
    private bool canThrowHook = false;

    private Vector2 throwDirection = Vector2.right; // Default throw direction

    private void Start()
    {
        player.controls.PlayerMovement.ThrowHook.performed += ctx => StartThrowHook();
        player.controls.PlayerMovement.ThrowHook.canceled += ctx => StopThrowHook();
        player.controls.PlayerMovement.Move.performed += ctx => throwDirection = ctx.ReadValue<Vector2>();
    }

    private void OnDisable()
    {
       if (player.controls != null)
        {
           player.controls.PlayerMovement.ThrowHook.performed -= ctx => StartThrowHook();   
           player.controls.PlayerMovement.ThrowHook.canceled -= ctx => StopThrowHook();
        }
    }

    private void StartThrowHook()
    {
        if (player.abilityInventory["Hook"] > 0)
        {
            canThrowHook = true;
            player.canMove = false;
            Debug.Log("Aiming...");
        }
        else canThrowHook = false;

    }

    private void StopThrowHook()
    {
        if (canThrowHook)
        {
            player.abilityInventory["Hook"]--;
            PlayerStats.HookCount--;
            spawnPosition = HookPosition.position + (Vector3)throwDirection.normalized * spawnOffset;
            GameObject hook = Instantiate(HookPrefab, spawnPosition, HookPosition.rotation);
            Rigidbody2D rb = hook.GetComponent<Rigidbody2D>();
            rb.linearVelocity = throwDirection.normalized * ThrowStrength;
            player.canMove = true;
            Debug.Log("Hook Thrown!");
        }
        else Debug.Log("No hooks left!");
    }
}