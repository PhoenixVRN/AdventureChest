using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardDistribution : MonoBehaviour
{
    public List<GameObject> AllHeroCard;
    public List<GameObject> AllEmamyCard;

    public GameObject containerHero;
    public GameObject containerEnamy;

    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GetComponent<GameManager>();
    }

    public List<GameObject> DistributionCard(int count, bool player)
    {
        List<GameObject> DistributionCard = new List<GameObject>();
        var allCard = player ? AllHeroCard : AllEmamyCard;
        var container = player ? containerHero : containerEnamy;
        for (int i = 0; i < count; i++)
        {
            var card = Instantiate(allCard[Random.Range(0, allCard.Count)], container.transform);
            DistributionCard.Add(card);
        }

        return DistributionCard;
    }

    public void DistributionCardDragons()
    {
        if (_gameManager.enamyCardsInPlay.Count > 0)
        {
            for (int i = _gameManager.enamyCardsInPlay.Count - 1; i > -1; i--)
            {
                var obj = _gameManager.enamyCardsInPlay[i];
                _gameManager.enamyCardsInPlay.RemoveAt(i);
                Destroy(obj);
            }
        }

        foreach (var dragon in _gameManager.dragonsCardsInPlay)
        {
            dragon.SetActive(true);
            _gameManager.enamyCardsInPlay.Add(dragon);
            dragon.transform.SetParent(containerEnamy.transform);
        }
    }
}