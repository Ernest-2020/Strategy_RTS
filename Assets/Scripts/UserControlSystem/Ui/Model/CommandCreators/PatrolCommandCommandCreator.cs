using UnityEngine;
using Zenject;
public class PatrolCommandCommandCreator : CancellableCommandCreatorBase<IPatrolCommand, Vector3>
{
	[Inject] private SelectableValue _selectable;

	protected override IPatrolCommand createCommand(Vector3 argument) => new PatrolCommand(_selectable.CurrentValue.PivotPoint.position, argument);
}