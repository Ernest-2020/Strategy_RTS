using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CommandButtonsPresenter : MonoBehaviour
{
    [SerializeField] private SelectableValue _selectable;
    [SerializeField] private CommandButtonsView _commandButtonsView;

    [Inject] private CommandButtonsModel _commandButtonsModel;

    private ISelectable _currentSelectable;

    private void Start()
    {
        _commandButtonsView.OnClick += _commandButtonsModel.OnCommandButtonClicked;
        _commandButtonsModel.OnCommandSent += _commandButtonsView.UnblockAllInteractions;
        _commandButtonsModel.OnCommandCancel += _commandButtonsView.UnblockAllInteractions;
        _commandButtonsModel.OnCommandAccepted += _commandButtonsView.BlockInteractions;

        _selectable.OnNewValue += onSelected;
        onSelected(_selectable.CurrentValue);
    }

    private void onSelected(ISelectable selectable)
    {
        if (_currentSelectable == selectable)
        {
            return;
        }
        if (_currentSelectable != null)
        {
            _commandButtonsModel.OnSelectionChange();
        }
        _currentSelectable = selectable;

        _commandButtonsView.Clear();
        if (selectable != null)
        {
            var commandExecutors = new List<ICommandExecutor>();
            commandExecutors.AddRange((selectable as Component).GetComponentsInParent<ICommandExecutor>());
            _commandButtonsView.MakeLayout(commandExecutors);
        }
    }
}
