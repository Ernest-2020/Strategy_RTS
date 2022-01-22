using UnityEngine;
public class MoveCommandCommandCreator : CancellableCommandCreatorBase<IMoveCommand, Vector3>
{
    protected override IMoveCommand createCommand(Vector3 argument) => new MoveCommand(argument);
}