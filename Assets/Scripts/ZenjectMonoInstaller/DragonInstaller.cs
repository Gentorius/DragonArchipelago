using Player;
using Zenject;

namespace ZenjectMonoInstaller
{
    public class DragonInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IPlayerController>().To<PlayerController>().AsSingle().NonLazy();
            Container.Bind<IPlayerSpawner>().To<PlayerSpawner>().FromInstance(FindAnyObjectByType<PlayerSpawner>()).AsSingle();
        }
    }
}