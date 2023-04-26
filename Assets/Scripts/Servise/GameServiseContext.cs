using UnityEngine;
using Zenject;

public class GameServiseContext : MonoInstaller
{
    [SerializeField] private ExceptionServise _exceptionServise;

    public override void InstallBindings()
    {
        Container.Bind<ExceptionServise>().FromComponentOn(_exceptionServise.gameObject).AsSingle();
    }
}