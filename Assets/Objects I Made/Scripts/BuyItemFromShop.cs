using UnityEngine;

public class BuyItemFromShop : MonoBehaviour
{
    [SerializeField] private PlayerStats PlayerStats;
    public static BuyItemFromShop Instance;

    public void BuyNothing()
    {
        Debug.Log("You bought something.");
        PlayerStats.HookCount += 1;
    }

    public void BuyItem()
    {
        Debug.Log("You bought an item.");
    }
}
