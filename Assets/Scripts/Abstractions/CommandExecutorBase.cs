using UnityEngine;
public abstract class CommandExecutorBase<T> : MonoBehaviour, ICommandExecutor where T : ICommand
{
    public void ExecuteComand(object command) => ExecuteSpecificCommand((T)command);

	public abstract void ExecuteSpecificCommand(T command);
}
