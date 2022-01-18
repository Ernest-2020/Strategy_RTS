using UnityEngine;
using UnityEngine.AI;

public class MoveCommandExecutor : CommandExecutorBase<IMoveCommand>
{
    [SerializeField] private UnitMovementStop _stop;
    [SerializeField] private Animator _animator;

    public async override void ExecuteSpecificCommand(IMoveCommand command)
    {
        GetComponent<NavMeshAgent>().destination = command.Target;
        _animator.SetTrigger("Walk");
        await _stop;
        _animator.SetTrigger("Idle");

    }
}
