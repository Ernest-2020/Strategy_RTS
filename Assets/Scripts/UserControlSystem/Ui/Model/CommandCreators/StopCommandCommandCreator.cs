using System;
using Zenject;

class StopCommandCommandCreator : CommandCreatorBase<IStopCommand>
{
    [Inject] AssetsContext _context;
    protected override void ClassSpecificCommandCreation(Action<IStopCommand> creationCallback)
    {
        creationCallback?.Invoke(_context.Inject(new StopCommand()));
    }
}

