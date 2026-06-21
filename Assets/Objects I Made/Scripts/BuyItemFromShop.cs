using UnityEngine;

public class BuyItemFromShop : MonoBehaviour
{
    public static BuyItemFromShop Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void BuyNothing()
    {
        Debug.Log("You bought nothing.");
    }

    public void BuyItem()
    {
        Debug.Log("You bought an item.");
    }
}
