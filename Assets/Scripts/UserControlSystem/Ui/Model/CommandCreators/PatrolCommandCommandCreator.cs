using System;
using UnityEngine;
using Zenject;

public class PatrolCommandCommandCreator : CommandCreatorBase<IPatrolCommand>
{
	[Inject] private AssetsContext _context;
	[Inject] private SelectableValue _selectable;

	private Action<IPatrolCommand> _creationCallback;

	[Inject]
	private void Init(Vector3Value groundClicks)
	{
		groundClicks.OnNewValue += onNewValue;
	}

	private void onNewValue(Vector3 groundClick)
	{
		_creationCallback?.Invoke(_context.Inject(new PatrolCommand(_selectable.CurrentValue.PivotPoint.position, groundClick)));
		_creationCallback = null;
	}

	protected override void ClassSpecificCommandCreation(Action<IPatrolCommand> creationCallback)
	{
		_creationCallback = creationCallback;
	}

	public override void ProcessCancel()
	{
		base.ProcessCancel();

		_creationCallback = null;
	}
}


