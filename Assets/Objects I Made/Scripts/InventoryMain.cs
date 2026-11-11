using UnityEngine;

[CreateAssetMenu(fileName = "InventoryMain", menuName = "Game/InventoryMain")]
public class InventoryMain : ScriptableObject
{
    // has checkpoint/checkpoint states, do later
    // Rebind function, do today
    // No UI functionality here, do elsewhere
    // Change Bird.cs so the buttons don't do specific functions but actually call the button itself so that rebinding is possible.
    // The catalog inventory class, do FIRST
    /* the catalog inventory class:
    Have a list of IDs to go find external classes by an ID number that all of said classes will have.
    Each external class is a tool. They each have a name, ID, description, and function.
    The catalog inventory class is the place for all changing data about the tools, how many the player has of each, and what button they are bound to.
    each tool will have their external class, and the catalog inventory will track their activity, using the two data points stated on line 13.
    */
}

/*
My idea on how to write the catalog inventory nested class and the external tool classes based on what I learned just now:
a) I don't want anything to inherit from Inventory Main, catalog inventory, or the external tool classes.
b) I want the ability to call InventoryMain.CatalogInventory.ButtonEast-- to decrease the number of the tools bound to the east gamepad button, the "B" button on xbox.

public abstract class Tool : ScriptableObject
{
    public abstract string Tool.Name
    public abstract int Tool.ID
    public abstract string Tool.Description
    public abstract void Tool.Function()
    public sealed list ChildrenOfTool = [empty list that gets filled with the "external tool classes"]
}

public sealed class Hook : Tool
{
    Tool.Name = "Hook"
    Tool.ID = 0
    Tool.Description = "A throwable hook and rope."
    public override void Tool.Function()
    {
        // do hook stuff
    }
}

public sealed class SecondExample : Tool
{
    Tool.Name = "Second Example"
    Tool.ID = 1
    Tool.Description = "A second example tool."
    public override void Tool.Function()
    {
        // do second example stuff
    }
}
*/

/*
Now that I have the tool class and the first external class (Hook), I need to make something call the function in Hook.
I am thinking, for the sake of not making a massive change in one day I should:
a) tell bird.cs that the ability4 button is the hook button manually, no remapping yet.
b) I need a way for the aiming to be done for the hook ability, I don't think I can make a player inputs object in the Hook class like I did with the HookAbility page, so I think I must input it somehow.
c) the aiming vector must be constantly changing for the hook to aim properly, so I need it to constantly be calling the on start hook function I think.
d) I need to give Hook.cs the prefab and functionality of HookAbility.cs, but I want to keep out all of the item count data for now, because that will be in the InventoryMain class, and I don't want to make a massive change in one day. I will do that later.
*/