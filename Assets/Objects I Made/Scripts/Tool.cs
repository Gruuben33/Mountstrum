using UnityEngine;

public abstract class Tool : ScriptableObject
{
    public abstract string Name { get; }
    public abstract int ID { get; }
    public abstract string Description { get; }
    public virtual void OnUseStart() { }
    public virtual void OnUseEnd() { }
}