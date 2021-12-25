using UnityEngine;

public class MainBuilding : MonoBehaviour, IUnitProducer, ISelectable
{
    public float Health => _health;
    

    public float MaxHealth => _maxHealth;

    public Sprite icon => _icon;
    [SerializeField] private GameObject _unitPrefab;
    [SerializeField] private Transform _unitParent;
    [SerializeField] private float _maxHealth = 1000;
    [SerializeField] Sprite _icon;
    private float _health = 1000;

    public void ProduceUnit()
    {
        Instantiate(_unitPrefab,new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)),Quaternion.identity,_unitParent);
    }
}
