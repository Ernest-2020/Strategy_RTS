using System.Linq;
using UnityEngine;

public class MouseInteractionPresenter : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] SelectableValue _selectableObject;
    private void Update()
    {

        if (!Input.GetMouseButtonUp(0))
        {
            return;
        }
        var hits =  Physics.RaycastAll(_camera.ScreenPointToRay(Input.mousePosition));
        if (hits.Length ==0)
        {
            return;
        }
        var selectable = hits
            .Select(hit => hit.collider.GetComponentInParent<ISelectable>())
            .Where(c => c != null)
            .FirstOrDefault();
        if (selectable == default)
        {
            return;
        }
        _selectableObject.SetValue(selectable);
    }
}
