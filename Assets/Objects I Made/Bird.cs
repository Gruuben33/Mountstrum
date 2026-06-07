using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class Bird : MonoBehaviour
{

    PlayerControls controls;

    void Awake()
    {
        controls = new PlayerControls();

        controls.gameplay.Grow.performed += ctx => Grow();
    }

    void Grow()
    {
        transform.localScale *= 1.1f;
    }

    void OnEnable()
    {
        controls.gameplay.Enable();
    }

    void OnDisable()
    {
        controls.gameplay.Disable();
    }
}
