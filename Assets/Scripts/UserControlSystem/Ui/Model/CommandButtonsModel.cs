﻿using System;
using Zenject;


public class CommandButtonsModel
{
    public event Action<ICommandExecutor> OnCommandAccepted;
    public event Action OnCommandSent;
    public event Action OnCommandCancel;

    [Inject] CommandCreatorBase<IProduceUnitCommand> _unitProducer;
    [Inject] CommandCreatorBase<IAttackCommand> _attacer;
    [Inject] CommandCreatorBase<IMoveCommand> _mover;
    [Inject] CommandCreatorBase<IStopCommand> _stopper;
    [Inject] CommandCreatorBase<IPatrolCommand> _patroller;

    private bool _commanIsPending;


    public void OnCommandButtonClicked(ICommandExecutor commandExecutor)
    {
        if (_commanIsPending)
        {
            ProcessOnCancel();
        }
        _commanIsPending = true;
        OnCommandAccepted?.Invoke(commandExecutor);
        _unitProducer.ProcessCommandExecutor(commandExecutor, command => ExecuteCommandWrapper(commandExecutor, command));
        _attacer.ProcessCommandExecutor(commandExecutor, command => ExecuteCommandWrapper(commandExecutor, command));
        _mover.ProcessCommandExecutor(commandExecutor, command => ExecuteCommandWrapper(commandExecutor, command));
        _stopper.ProcessCommandExecutor(commandExecutor, command => ExecuteCommandWrapper(commandExecutor, command));
        _patroller.ProcessCommandExecutor(commandExecutor, command => ExecuteCommandWrapper(commandExecutor, command));
    }

    public void ExecuteCommandWrapper(ICommandExecutor commandExecutor, object command)
    {
        commandExecutor.ExecuteComand(command);
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
        _attacer.ProcessCancel();
        _mover.ProcessCancel();
        _stopper.ProcessCancel();
        _patroller.ProcessCancel();

        OnCommandCancel?.Invoke();
    }

}
