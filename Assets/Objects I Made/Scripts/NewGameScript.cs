using UnityEngine;
using UnityEngine.InputSystem;

public class NewGameScript : MonoBehaviour
{
    PlayerActions controls;

    void Awake()
    {
        controls = new PlayerActions();

        controls.gameplay.NewGame.performed += ctx => NewGame();
    }

    void NewGame()
    {
        Debug.Log("Starting new game, resetting all data.");
        DataPersistanceManager.instance.NewGame();
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