using UnityEngine;

public interface ISelectable : IHealthHolder
{
    Transform PivotPoint { get; }
    Sprite icon { get; }
}
