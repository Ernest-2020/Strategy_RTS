using UnityEngine;

public class MainBuilding : CommandExecutorBase<IProduceUnitCommand>, ISelectable
{
    public float Health => _health;


    public float MaxHealth => _maxHealth;

    public Sprite icon => _icon;
    [SerializeField] private Transform _unitParent;
    [SerializeField] private float _maxHealth = 1000;
    [SerializeField] Sprite _icon;
    private float _health = 1000;

    public override void ExecuteSpecificCommand(IProduceUnitCommand command)
    {
        Vector3 positionUnits = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
        Instantiate(command.UnitPrefab, positionUnits, Quaternion.identity, _unitParent);

    }
}
