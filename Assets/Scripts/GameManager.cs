using System.Collections;
using System.Collections.Generic;
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
    
    void Start()
    {
        _cardDistribution = GetComponent<CardDistribution>();
        _countRound = 1;
        _cardDistribution.DistributionCard(1, false);
        _cardDistribution.DistributionCard(7, true);
    }

    // Update is called once per frame
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
