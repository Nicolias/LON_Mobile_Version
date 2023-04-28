using Zenject;
using UnityEngine;

class GameSceneContext : MonoInstaller
{
    [SerializeField] private AttackDeck _attackDeck;
    
    public override void InstallBindings()
    {
        Container.Bind<AttackDeck>().FromComponentOn(_attackDeck.gameObject).AsSingle();
    }
}