using Player;
using Spawners;
using Unity.AI.Navigation;
using UnityEngine;
using Utility;
using Zenject;

namespace ZenjectMonoInstaller
{
    public class DragonInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IPlayerController>().To<PlayerController>().AsSingle().NonLazy();
            Container.Bind<IIDManager>().To<IDManager>().AsSingle().NonLazy();
            
            Container.Bind<IPlayerSpawner>().To<PlayerSpawner>().FromInstance(FindAnyObjectByType<PlayerSpawner>()).AsSingle();
        }
    }
}