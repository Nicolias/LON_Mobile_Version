using Zenject;
using UnityEngine;

class GameSceneContext : MonoInstaller
{
    [SerializeField] private AttackDeck _attackDeck;

    private CardCollection _cardCollection = new();
    
    public override void InstallBindings()
    {
        Container.Bind<AttackDeck>().FromComponentOn(_attackDeck.gameObject).AsSingle();
        Container.Bind<CardCollection>().FromInstance(_cardCollection);
    }
}