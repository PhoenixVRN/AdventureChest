using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CardDistribution : MonoBehaviour
{
    public List<GameObject> AllHeroCard;
    public List<GameObject> AllEmamyCard;

    public GameObject dragon;

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

    public void DistributionReRoll(int player, int enamy)
    {
        for (int i = 0; i < player; i++)
        {
            var card = Instantiate(AllHeroCard[Random.Range(0, AllHeroCard.Count)], containerHero.transform);
            _gameManager.playerCardsInPlay.Add(card);
        }
        for (int i = 0; i < enamy; i++)
        {
            var card = Instantiate(AllEmamyCard[Random.Range(0, AllEmamyCard.Count)], containerEnamy.transform);
            _gameManager.enamyCardsInPlay.Add(card);
        }

        _gameManager.ChekDragon();
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
            //
            // for (int i = 0; i < _gameManager.countDragon; i++)
            // {
            //     var cardDragon = Instantiate(dragon, containerEnamy.transform);
            //     _gameManager.enamyCardsInPlay.Add(cardDragon);
            // }
        }

        for (int i = _gameManager.dragonsCardsInPlay.Count - 1; i > -1; i--)
        {
            var dragon = _gameManager.dragonsCardsInPlay[i];
            dragon.SetActive(true);
            dragon.GetComponent<LayoutElement>().ignoreLayout = false;
            _gameManager.dragonsCardsInPlay.Remove(dragon);
            _gameManager.enamyCardsInPlay.Add(dragon);
            dragon.transform.SetParent(containerEnamy.transform);
        }
    }
}