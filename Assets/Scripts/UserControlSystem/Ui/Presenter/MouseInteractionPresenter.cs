using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using UniRx;

public sealed class MouseInteractionPresenter : MonoBehaviour
{
    [SerializeField] private EventSystem _eventSystem;
    [SerializeField] private Camera _camera;
    [SerializeField] private SelectableValue _selectedObject;

    [SerializeField] private Vector3Value _groundClicksRMB;
    [SerializeField] private AttackableValue _attackablesRMB;
    [SerializeField] private Transform _groundTransform;

    private Plane _groundPlane;

    [Inject]
    private void Init()
    {
        _groundPlane = new Plane(_groundTransform.up, 0);

        //Сначала берем поток всех кадров, в которых клики нe блочaт ui
        var nonBlockedByUiFramesStream = Observable.EveryUpdate()
            .Where(_ => !_eventSystem.IsPointerOverGameObject());

        //Затем формируем из него два потока кликов — правой и левой кнопкой мыши
        var leftClicksStream = nonBlockedByUiFramesStream
            .Where(_ => Input.GetMouseButtonDown(0));
        var rightClicksStream = nonBlockedByUiFramesStream
            .Where(_ => Input.GetMouseButtonDown(1));

        //Выбираем лучи, стреляющие из точки экрана
        var lmbRays = leftClicksStream
            .Select(_ => _camera.ScreenPointToRay(Input.mousePosition));
        var rmbRays = rightClicksStream
            .Select(_ => _camera.ScreenPointToRay(Input.mousePosition));

        //Выбираем из них все пересечения с лучом
        var lmbHitsStream = lmbRays
            .Select(ray => Physics.RaycastAll(ray));
        //Для правой кнопки мыши нам еще понадобится сам луч, поэтому передаем его в кортеже
        var rmbHitsStream = rmbRays
            .Select(ray => (ray, Physics.RaycastAll(ray)));

        //наконец подписываемся на результат и анализируем подробно что нам нужно из этих потоков
        lmbHitsStream.Subscribe(hits =>
        {
            if (weHit<ISelectable>(hits, out var selectable))
            {
                _selectedObject.SetValue(selectable);
            }
        });
        rmbHitsStream.Subscribe(data =>
        {
            var (ray, hits) = data;

            if (weHit<IAttackable>(hits, out var attackable))
            {
                _attackablesRMB.SetValue(attackable);
            }
            else if (_groundPlane.Raycast(ray, out var enter))
            {
                _groundClicksRMB.SetValue(ray.origin + ray.direction * enter);
            }
        });
    }

    private bool weHit<T>(RaycastHit[] hits, out T result) where T : class
    {
        result = default;
        if (hits.Length == 0)
        {
            return false;
        }
        result = hits
            .Select(hit => hit.collider.GetComponentInParent<T>())
            .Where(c => c != null)
            .FirstOrDefault();
        return result != default;
    }
}