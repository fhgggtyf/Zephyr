using UnityEngine;
using Zenject;

public class TutorialSceneInstaller : MonoInstaller
{
    [SerializeField] Player _player;

    public override void InstallBindings()
    {
        Container.BindInstances(_player);
    }
}