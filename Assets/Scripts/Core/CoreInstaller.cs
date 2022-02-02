using Zenject;
public class CoreInstaller : MonoInstaller
	
{
	private GameStatus _gameStatus;
	public override void InstallBindings()
	{
		Container.BindInterfacesAndSelfTo<TimeModel>().AsSingle();
		Container.Bind<IGameStatus>().FromInstance(_gameStatus);
	}
}
