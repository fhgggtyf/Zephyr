using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller<PlayerInstaller>
{
    [SerializeField] PlayerData _playerData;
    [SerializeField] PlayerStateMachineFactory _playerStateMachineFactory;
    [SerializeField] PlayerCapabilityFactory _playerCapabilityFactory;

    public override void InstallBindings()
    {
        Container.Bind<Core>().FromComponentInChildren().WhenInjectedInto<Player>();
        Container.Bind<Animator>().FromComponentInChildren().WhenInjectedInto<Player>();
        Container.Bind<PlayerInputHandler>().FromComponentInHierarchy().WhenInjectedInto<Player>();
        Container.Bind<Rigidbody2D>().FromComponentInHierarchy().WhenInjectedInto<Player>();
        Container.Bind<BoxCollider2D>().FromComponentInHierarchy().WhenInjectedInto<Player>();
        Container.Bind<Ground>().FromComponentInHierarchy().WhenInjectedInto<Player>();
        Container.Bind<Stats>().FromResolveGetter<Core>(core => core.GetCoreComponent<Stats>()).WhenInjectedInto<Player>();
        Container.Bind<InteractableDetector>().FromResolveGetter<Core>(core => core.GetCoreComponent<InteractableDetector>()).WhenInjectedInto<Player>();

        Container.Bind<List<PlayerCapabilities>>().AsSingle();

        Container.Bind<Weapon>().WithId("Primary").FromComponentInHierarchy().WhenInjectedInto<Player>();
        Container.Bind<Weapon>().WithId("Secondary").FromComponentInHierarchy().WhenInjectedInto<Player>();


        Container.BindInstances(_playerData, _playerStateMachineFactory, _playerCapabilityFactory);
    }
}
