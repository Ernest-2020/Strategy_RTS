using System;
using UnityEngine;
using Zenject;


public class CommandButtonsModel
{
    public event Action<ICommandExecutor> OnCommandAccepted;
    public event Action OnCommandSent;
    public event Action OnCommandCancel;

    [Inject] CommandCreatorBase<IProduceUnitCommand> _unitProducer;
    [Inject] CommandCreatorBase<IAttackCommand> _attacker;
    [Inject] CommandCreatorBase<IMoveCommand> _mover;
    [Inject] CommandCreatorBase<IStopCommand> _stopper;
    [Inject] CommandCreatorBase<IPatrolCommand> _patroller;
    [Inject] CommandCreatorBase<ISetRallyPointCommand> _setRally;

    private bool _commanIsPending;


    public void OnCommandButtonClicked(ICommandExecutor commandExecutor, ICommandsQueue commandsQueue)
    {
        if (_commanIsPending)
        {
            OnCommandCancel();
        }
        _commanIsPending = true;
        OnCommandAccepted?.Invoke(commandExecutor);

        _unitProducer.ProcessCommandExecutor(commandExecutor, command => executeCommandWrapper(command, commandsQueue));
        _attacker.ProcessCommandExecutor(commandExecutor, command => executeCommandWrapper(command, commandsQueue));
        _stopper.ProcessCommandExecutor(commandExecutor, command => executeCommandWrapper(command, commandsQueue));
        _mover.ProcessCommandExecutor(commandExecutor, command => executeCommandWrapper(command, commandsQueue));
        _patroller.ProcessCommandExecutor(commandExecutor, command => executeCommandWrapper(command, commandsQueue));
    }

    public void executeCommandWrapper(object command, ICommandsQueue commandsQueue)
    {
        if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
        {
            commandsQueue.Clear();
        }
        commandsQueue.EnqueueCommand(command);
        _commanIsPending = false;
        OnCommandSent?.Invoke();
    }

    public void OnSelectionChange()
    {
        _commanIsPending = false;
        ProcessOnCancel();
    }

    public void ProcessOnCancel()
    {
        _unitProducer.ProcessCancel();
        _attacker.ProcessCancel();
        _mover.ProcessCancel();
        _stopper.ProcessCancel();
        _patroller.ProcessCancel();
        _setRally.ProcessCancel();

        OnCommandCancel?.Invoke();
    }

}

