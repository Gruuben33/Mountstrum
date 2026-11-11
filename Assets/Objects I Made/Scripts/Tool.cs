using UnityEngine;

public abstract class Tool : ScriptableObject
{
    public abstract string Name { get; }
    public abstract int ID { get; }
    public abstract string Description { get; }
    public abstract void OnUseStart(Bird user);
    public virtual void OnUseUpdate(Bird user, Vector2 aimDirection, float elapsedTime) { }
    public abstract void OnUseEnd(Bird user, Vector2 aimDirection, float elapsedTime);
}