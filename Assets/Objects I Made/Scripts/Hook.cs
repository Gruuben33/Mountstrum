using UnityEngine;

[CreateAssetMenu(fileName = "Hook", menuName = "Tools/Hook")]
public class Hook : ScriptableObject
{
    public override string Name => "Hook";
    public override int ID => 0;
    public override string Description => "A throwable hook and rope.";
    public override void OnUseStart()
    {
        // Implement hook start functionality here
    }
    public override void OnUseEnd()
    {
        // Implement hook end functionality here
    }
}
