using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private List<GameObject> _cardsHero;
    private List<GameObject> _cardsEnamy;
    
    private List<GameObject> _treasures;
    private GameObject _hero;
    public int countDragon;
    private int _countRound;
    private CardDistribution _cardDistribution;

    public int cemetery = 0;

    

    public List<GameObject> playerCardsInPlay;
    public List<GameObject> enamyCardsInPlay;
    public List<GameObject> dragonsCardsInPlay;

    public TMP_Text textCountDragon;
    public TMP_Text infoPanelText;

    public Button buttonEndTurn;
    public Button buttonBackTavern;
    public Button buttonReRoll;

    public int rerollEnamy;
    public int rerolPlayer;

    void Start()
    {
        _cardDistribution = GetComponent<CardDistribution>();
        _countRound = 0;
        countDragon = 0;
        textCountDragon.text = countDragon.ToString();
        LevelHandler();
        // ChekDragon();
    }

    private void OnEnable()
    {
        buttonEndTurn.gameObject.SetActive(false);
        buttonBackTavern.gameObject.SetActive(false);
        buttonReRoll.gameObject.SetActive(false);
        buttonEndTurn.onClick.AddListener(EndTurn);
        buttonBackTavern.onClick.AddListener(BackToTavern);
        buttonReRoll.onClick.AddListener(ScrolReRoll);
    }

    private void OnDisable()
    {
        buttonEndTurn.onClick.RemoveListener(EndTurn);
        buttonBackTavern.onClick.RemoveListener(BackToTavern);
        buttonReRoll.onClick.AddListener(ScrolReRoll);
    }

    public void ChekDragon()
    {
        ActiveButtonEndTurn();
        
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

    public void ActiveButtonEndTurn()
    {
        if (!BattlService.Instance.CheckButtleEnamy())
        {
            buttonEndTurn.gameObject.SetActive(true);
            buttonBackTavern.gameObject.SetActive(true);
        }
        else
        {
            buttonEndTurn.gameObject.SetActive(false);
            buttonBackTavern.gameObject.SetActive(false);
        }
    }
    private void EndTurn()
    {
        ClearEnamyHands();
        LevelHandler();
        buttonEndTurn.gameObject.SetActive(false);
        buttonBackTavern.gameObject.SetActive(false);
    }

    public void ActiveButtonBackTavern()
    {
        buttonBackTavern.gameObject.SetActive(true);
    }
    private void BackToTavern()
    {
        //TODO вернутся в наверну и начислить коньячку
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
            buttonBackTavern.gameObject.SetActive(false);
            buttonEndTurn.gameObject.SetActive(false);
            //TODO реалидация боя с драконами
            SetTextPanel("Дракоши голодные");
            _cardDistribution.DistributionCardDragons();
            //TODO бой на драконах
            return;
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
            InitLevel(cardEnamyCount);
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

    public void ClearEnamyHands()
    {
        if (enamyCardsInPlay.Count == 0 ) return;
        for (int i = enamyCardsInPlay.Count - 1; i > -1; i--)
        {
            var card = enamyCardsInPlay[i];
                enamyCardsInPlay.RemoveAt(i);
                Destroy(card);
        }
    }

    public void PotionUse()
    {
        if (cemetery == 0)
        {
            Debug.Log($"на кладбиже нету нихуя");
            SetTextPanel("На каладбище\nветер свищет!");
            return;
        }
    }

    public void ScrolReRoll()
    {
        GameReferance.isReroll = false;
        _cardDistribution.DistributionReRoll(rerolPlayer, rerollEnamy);
        buttonReRoll.gameObject.SetActive(false);
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