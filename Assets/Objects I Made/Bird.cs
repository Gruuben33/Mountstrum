using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class Bird : MonoBehaviour
{

    PlayerActions controls;

    void Awake()
    {
        controls = new PlayerActions();

        controls.gameplay.Grow.performed += ctx => Grow();
    }

    void Grow()
    {
        transform.localScale *= 1.1f;
    }

    void ChangeScene1()
    {
        GameManager.Instance.LoadSceneFromIndex(0);
    }

    void ChangeScene2()
    {
        GameManager.Instance.LoadSceneFromIndex(1);
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
