using UnityEngine;

public interface ISelectable
{
    float Health { get; }
    float MaxHealth { get; }
    Sprite icon { get; }
}
