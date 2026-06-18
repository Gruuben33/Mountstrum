using UnityEngine;
using UnityEngine.InputSystem;

public class NewGameScript : MonoBehaviour
{
    PlayerActions controls;

    void Awake()
    {
        controls = new PlayerActions();

        controls.TestInputs.NewGame.performed += ctx => NewGame();
    }

    void NewGame()
    {
        Debug.Log("Starting new game, resetting all data.");
        DataPersistanceManager.instance.NewGame();
    }

    void OnEnable()
    {
        if (controls == null) controls = new PlayerActions();
        controls.TestInputs.Enable();
    }

    void OnDisable()
    {
        if (controls != null) controls.TestInputs.Disable();
    }
}