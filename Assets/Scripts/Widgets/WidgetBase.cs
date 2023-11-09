using UnityEngine;

public abstract class WidgetBase : MonoBehaviour, IActivatable, IDeactivatable
{
    public abstract void Activate();

    public abstract void Deactivate();
}
