using System.Collections;
using System.Collections.Generic;
using Infrastructure.Services;
using QuestPage.Evolve;
using UnityEngine;
using UnityEngine.UI;
using QuestPage.Enhance;

public class EvolutionCardCell : CardCellView
{
    private  CardCell _cardCollection;
    private EvolveCardCollection _evolveCardCollection;

    public void Init(EvolveCardCollection evolveCardCollection, CardCell cardInCollection)
    {
        if (evolveCardCollection == null || cardInCollection == null)
            throw new System.NullReferenceException();            

        _evolveCardCollection = evolveCardCollection;
        _cardCollection = cardInCollection;
    }

    //protected override void OnSelectedCard()
    //{
    //    _evolveCardCollection.SelectCard(_cardCollection);
    //}
}
