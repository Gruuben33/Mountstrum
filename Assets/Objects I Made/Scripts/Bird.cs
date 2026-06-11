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
        controls.gameplay.ChangeScene1.performed += ctx => ChangeScene1();
        controls.gameplay.ChangeScene2.performed += ctx => ChangeScene2();
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
    }

    void OnDisable()
    {
        controls.gameplay.Disable();
    }
}
