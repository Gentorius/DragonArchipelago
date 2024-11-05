using Player;
using Unity.AI.Navigation;
using UnityEngine;
using Zenject;

namespace ZenjectMonoInstaller
{
    public class DragonInstaller : MonoInstaller
    {
        [SerializeField]
        Bootstrap.Bootstrap _bootstrap;
        public override void InstallBindings()
        {
            Container.Bind<IPlayerController>().To<PlayerController>().AsSingle().NonLazy();
            
            Container.Bind<IPlayerSpawner>().To<PlayerSpawner>().FromInstance(FindAnyObjectByType<PlayerSpawner>()).AsSingle();
        }
    }
}