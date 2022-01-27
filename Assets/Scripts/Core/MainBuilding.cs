using UnityEngine;

public class MainBuilding : MonoBehaviour, ISelectable, IAttackable
{
    public float Health => _health;


    public float MaxHealth => _maxHealth;

    public Sprite Icon => _icon;

    public Transform PivotPoint => _pivotPoint;


    [SerializeField] private Transform _unitParent, _pivotPoint;
    [SerializeField] private float _maxHealth = 1000;
    [SerializeField] Sprite _icon;
    public Vector3 RallyPoint { get; set; }
    private float _health = 1000;
    private void Start()
    {
        RallyPoint = transform.position;
    }
}
