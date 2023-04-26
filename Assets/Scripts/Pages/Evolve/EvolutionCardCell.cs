using System.Collections;
using System.Collections.Generic;
using Infrastructure.Services;
using FarmPage.Evolve;
using UnityEngine;
using UnityEngine.UI;
using FarmPage.Enhance;

public class EvolutionCardCell : EnchanceCardCell
{
    private  CardCollectionCell _cardCollection;
    private EvolveCardCollection _evolveCardCollection;

    public void Init(EvolveCardCollection evolveCardCollection, CardCollectionCell cardInCollection)
    {
        if (evolveCardCollection == null || cardInCollection == null)
            throw new System.NullReferenceException();            

        _evolveCardCollection = evolveCardCollection;
        _cardCollection = cardInCollection;
    }

    protected override void SelectCard()
    {
        _evolveCardCollection.SelectCard(_cardCollection);
    }
}
