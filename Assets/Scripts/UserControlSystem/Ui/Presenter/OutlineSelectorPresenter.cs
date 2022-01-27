using UnityEngine;
using System;
using UniRx;
using Zenject;

public class OutlineSelectorPresenter : MonoBehaviour
{
    [Inject] private IObservable<ISelectable> _selectedValues;

    private OutlineSelector[] _outlineSelectors;
    private ISelectable _currentSelectable;

    private void Start()
    {
        _selectedValues.Subscribe(onSelected);
    }

    private void onSelected(ISelectable selectable)
    {
        if (_currentSelectable == selectable)
        {
            return;
        }
        _currentSelectable = selectable;

        setSelected(_outlineSelectors, false);
        _outlineSelectors = null;

        if (selectable != null)
        {
            _outlineSelectors = (selectable as Component).GetComponentsInParent<OutlineSelector>();
            setSelected(_outlineSelectors, true);
        }

        static void setSelected(OutlineSelector[] selectors, bool value)
        {
            if (selectors != null)
            {
                for (int i = 0; i < selectors.Length; i++)
                {
                    selectors[i].SetSelected(value);
                }
            }
        }
    }
}

