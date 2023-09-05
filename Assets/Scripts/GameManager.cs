using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<GameObject> _cardsHero;
    private List<GameObject> _cardsEnamy;
    private List<GameObject> _treasures;
    private GameObject _hero;
    private int countDragon;
    private int _countRound;
    private CardDistribution _cardDistribution;

    private int CountDragon;

    public List<GameObject> playerCardsInPlay;
    public List<GameObject> enamyCardsInPlay;

    void Start()
    {
       _cardDistribution = GetComponent<CardDistribution>();
        _countRound = 1;
        enamyCardsInPlay = new List<GameObject>(_cardDistribution.DistributionCard(7, false));
        playerCardsInPlay = new List<GameObject>(_cardDistribution.DistributionCard(7, true));
        ChekDragon();
    }

    private void ChekDragon()
    {
        for (int i = enamyCardsInPlay.Count - 1; i > -1; i--)
        {
            if (enamyCardsInPlay[i].GetComponent<CardInfoEnamy>().TypeСreature == TypeСreature.Dragon)
            {

                var dragonCard = enamyCardsInPlay[i];
                enamyCardsInPlay.RemoveAt(i);
                dragonCard.transform.DOMove(GameReferance.DragonDang.transform.localPosition, 2f).OnComplete( () => Destroy(dragonCard));
            }
        }
    }
    void Update()
    {
        
    }
}

public enum StateGame
{
    Distribution,
    PlayerTurn,
    EndTurn,
    Pause,
    GameOver
}

public enum TypeCard
{
    Player,
    Enamy
}

public enum TypeСreature
{
    Paladin,
    Thief,
    Priest,
    Mage,
    Warrior,
    Scroll,
    Dragon,
    Skelenon,
    Slug,
    Goblin,
    Potion,
    Chest
}

public enum TypePlayerHero
{
    DragonHunter,
    Sourcerer,
    Warlock,
    Templar,
    Skald,
    Warlord,
    Grog,
    Enchantress
}
