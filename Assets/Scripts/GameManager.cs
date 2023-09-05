using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    public List<GameObject> dragonsCardsInPlay;

    public TMP_Text textCountDragon;
    public TMP_Text infoPanelText;

    void Start()
    {
        _cardDistribution = GetComponent<CardDistribution>();
        _countRound = 0;
        countDragon = 0;
        textCountDragon.text = countDragon.ToString();
        LevelHandler();
        // ChekDragon();
    }

    private void ChekDragon()
    {
        for (int i = enamyCardsInPlay.Count - 1; i > -1; i--)
        {
            if (enamyCardsInPlay[i].GetComponent<CardInfoEnamy>().TypeСreature == TypeСreature.Dragon)
            {
                var dragonCard = enamyCardsInPlay[i];
                dragonCard.transform.SetAsLastSibling();
                dragonsCardsInPlay.Add(enamyCardsInPlay[i]);
                enamyCardsInPlay.RemoveAt(i);
                var l = GameReferance.CanvasGame.transform.TransformPoint(dragonCard.transform.localPosition);
                dragonCard.GetComponent<LayoutElement>().ignoreLayout = true;
                // dragonCard.transform.SetParent(GameReferance.CanvasGame);
                dragonCard.transform.localPosition = l;
                dragonCard.transform.DOMove(GameReferance.DragonDang.transform.position, 1f).OnComplete(() => MoveDragon(dragonCard));
                return;
            }
        }
       

        if (enamyCardsInPlay.Count == 0 && countDragon < 3)
        {
            LevelHandler();
        }
    }

    public void SetTextPanel(string text)
    {
        infoPanelText.gameObject.SetActive(true);
        infoPanelText.text = text;
        Invoke("FadeTextPanel", 2f);
    }

    private void FadeTextPanel()
    {
        infoPanelText.gameObject.SetActive(false);
    }
    
    private void MoveDragon(GameObject dragonCard)
    {
        countDragon++;
        textCountDragon.text = countDragon.ToString();
        dragonCard.SetActive(false);
        ChekDragon();
    }

    public void LevelHandler()
    {
        if (countDragon > 2)
        {
            //TODO реалидация боя с драконами
            SetTextPanel("Дракоши голодные");
            _cardDistribution.DistributionCardDragons();
        }
        
        _countRound++;
        SetTextPanel("Level " + _countRound);
        var cardEnamyCount = _countRound > 7 ? 7 : _countRound;

        if (_countRound == 1)
        {
            InitLevel(1, 7);
        }
        else
        {
            InitLevel(_countRound);
        }
    }

    private void InitLevel(int countEnamyCard, int countPlayerCard = 0)
    {
        enamyCardsInPlay = new List<GameObject>(_cardDistribution.DistributionCard(countEnamyCard, false));
        if (countPlayerCard > 0)
        {
            playerCardsInPlay = new List<GameObject>(_cardDistribution.DistributionCard(7, true));
        }
        Invoke("ChekDragon", 1f);
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