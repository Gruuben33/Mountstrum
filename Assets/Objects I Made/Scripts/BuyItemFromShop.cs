using UnityEngine;

public class BuyItemFromShop : MonoBehaviour
{
    public static BuyItemFromShop Instance;

    public void BuyNothing()
    {
        Debug.Log("You bought nothing.");
    }

    public void BuyItem()
    {
        Debug.Log("You bought an item.");
    }
}
