using Zenject;
using UnityEngine;

class GameSceneContext : MonoInstaller
{
    [SerializeField] private AttackDeck _attackDeck;

    [SerializeField] private CardsCollection _cardCollection;
    
    public override void InstallBindings()
    {
        Container.Bind<AttackDeck>().FromComponentOn(_attackDeck.gameObject).AsSingle();
        Container.Bind<CardsCollection>().FromInstance(_cardCollection);
    }
}