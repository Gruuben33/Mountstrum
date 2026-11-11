using UnityEngine;

[CreateAssetMenu(fileName = "Hook", menuName = "Tools/Hook")]
public class Hook : Tool
{
    public override string Name => "Hook";
    public override int ID => 0;
    public override string Description => "A throwable hook and rope.";
    private Vector3 spawnPosition;
    private float spawnOffset = 0.5f;
    public override void OnUseStart(Bird user)

    {
        // user.canMove = false;
        Debug.Log("Aiming...");
    }
    public override void OnUseEnd(Bird user, Vector2 aimDirection, float elapsedTime)
    {
        // spawnPosition = user.transform.position + (Vector3)aimDirection.normalized * spawnOffset;
        Debug.Log("Hook Thrown!");
    }
}
