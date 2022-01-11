using System;
using Zenject;

public class ProduceUnitCommandCommandCreator : CommandCreatorBase<IProduceUnitCommand>
{
    [Inject] AssetsContext _context;
    protected override void ClassSpecificCommandCreation(Action<IProduceUnitCommand> creationCallback)
    {
        creationCallback?.Invoke(_context.Inject(new ProduceUnitCommandHeir()));
    }
}
