using UnityEngine;

public class SetRallyPointCommandCreator : CancellableCommandCreatorBase<ISetRallyPointCommand, Vector3>
{
	protected override ISetRallyPointCommand createCommand(Vector3 argument) => new SetRallyPointCommand(argument);
}