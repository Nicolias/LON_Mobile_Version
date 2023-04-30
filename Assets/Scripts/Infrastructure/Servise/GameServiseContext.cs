using UnityEngine;
using Zenject;

public class GameServiseContext : MonoInstaller
{
    [SerializeField] private ExceptionServise _exceptionServise;
    [SerializeField] private CoroutineServise _coroutineSevise;

    public override void InstallBindings()
    {
        Container.Bind<ExceptionServise>().FromComponentOn(_exceptionServise.gameObject).AsSingle();
        Container.Bind<CoroutineServise>().FromComponentOn(_coroutineSevise.gameObject).AsSingle();
    }
}