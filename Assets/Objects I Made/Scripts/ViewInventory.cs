using UnityEngine;

public class ViewInventory : MonoBehaviour
{
    [SerializeField] private Bird player;
    [SerializeField] private GameObject inventoryUI;
    void Start()
    {
        player.controls.PlayerMovement.OpenInventory.performed += ctx => EnableInventory();
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EnableInventory()
    {
        Debug.Log("Enabling Inventory");
        inventoryUI.SetActive(!inventoryUI.activeSelf);
        Time.timeScale = inventoryUI.activeSelf ? 0f : 1f;
    }
}
