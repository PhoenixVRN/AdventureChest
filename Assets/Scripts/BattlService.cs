using System;
using System.Collections.Generic;
using UnityEngine;

public class BattlService : MonoBehaviour
{
    [SerializeField] private UpgradeDataBase dataBase;

    [SerializeField] private GameObject resurrectionPanel;
    private Hero[] heroData;
    private GameManager _gameManager;
    private int batlleEnamy;

    private List<TypeСreature> threeHeroes;

    public static BattlService Instance;

    private CardInfoPlayer player;
    private CardInfoEnamy enamy;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance == this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        threeHeroes = new List<TypeСreature>();
        _gameManager = GetComponent<GameManager>();
        heroData = dataBase.GetUpgrade();
    }

    public void Attake(CardInfoPlayer player, CardInfoEnamy enamy)
    {
        this.player = player;
        this.enamy = enamy;
            
        if (GameReferance.isBattleDragon)
        {
           FightDragon();
        }
        
        if (!enamy.isBattle && !CheckButtleEnamy())
        {
            //TODO реализовать механику Добычи
            _gameManager.SetTextPanel("Добыча");
            Reward();
            return;
        }

        if (!enamy.isBattle)
        {
            _gameManager.SetTextPanel("Убете всех монстров");
            return;
        }

        foreach (var hero in heroData)
        {
            if (hero.whoBeats == player.TypeСreature)
            {
                foreach (var hitting in hero.whoIsHitting)
                {
                    if (hitting.whoIsHitting == enamy.TypeСreature)
                    {
                        var countHit = hitting.quantity;
                        Battle(player, enamy, countHit);
                        return;
                    }
                }
            }
        }
    }

    private void Reward()
    {
        _gameManager.resurrection = 0;
        if (enamy.TypeСreature == TypeСreature.Potion)
        {
            _gameManager.playerCardsInPlay.Remove(player.gameObject);
            _gameManager.cemetery++;
            Destroy(player.gameObject);

            
            _gameManager.enamyCardsInPlay.Remove(enamy.gameObject);
            Destroy(enamy.gameObject);
            
            for (int i = _gameManager.enamyCardsInPlay.Count - 1; i > -1; i--)
            {
                if (_gameManager.enamyCardsInPlay[i].GetComponent<CardInfoEnamy>().TypeСreature == TypeСreature.Potion )
                {
                    var card = _gameManager.enamyCardsInPlay[i];
                    _gameManager.enamyCardsInPlay.RemoveAt(i);
                    Destroy(card);
                    _gameManager.resurrection++;
                }
            }
            ResurrectionHero();
        }
        else
        {
            //TODO chest
        }
    }

    private void ResurrectionHero()
    {
        GameReferance.stateResurrectionHero = true;
        resurrectionPanel.SetActive(true);
        
    }
    
    private void FightDragon()
    {
        var war = false;
        if (player.TypeСreature != TypeСreature.Scroll)
        {
            foreach (var typeHero in threeHeroes)
            {
                if (player.TypeСreature != typeHero)
                {
                    threeHeroes.Add(player.TypeСreature);
                    _gameManager.enamyCardsInPlay.Remove(enamy.gameObject);
                    Destroy(enamy);
                    _gameManager.playerCardsInPlay.Remove(player.gameObject);
                    Destroy(player);
                    if (!CheckButtleEnamy())
                    {
                        _gameManager.SetTextPanel("Ебать ты крут!");
                        _gameManager.ActiveButtonEndTurn();
                        threeHeroes.Clear();
                    }
                    else
                    {
                        foreach (var hero in _gameManager.playerCardsInPlay)
                        {
                            if (hero.GetComponent<CardInfoPlayer>().IsBattle)
                            {
                                war = true;
                            }
                        }

                        if (!war)
                        {
                            _gameManager.SetTextPanel("Ты Сдох!!!");
                            threeHeroes.Clear();
                        }
                    }
                    return;
                }
                else
                {
                    _gameManager.SetTextPanel("Разные герои\nнужны");
                    return;
                }
            }
        }
        else
        {
            _gameManager.SetTextPanel("Ты дуркак\nсвитком бить!");
            return;
        }
    }
    private void Battle(CardInfoPlayer player, CardInfoEnamy enamy, int count)
    {
        var type = enamy.TypeСreature;

        _gameManager.playerCardsInPlay.Remove(player.gameObject);
        _gameManager.cemetery++;
        Destroy(player.gameObject);

        _gameManager.enamyCardsInPlay.Remove(enamy.gameObject);
        Destroy(enamy.gameObject);
        count--;
        if (count > 0)
        {
            for (int i = _gameManager.enamyCardsInPlay.Count - 1; i > -1; i--)
            {
                if (_gameManager.enamyCardsInPlay[i].GetComponent<CardInfoEnamy>().TypeСreature == type && count != 0)
                {
                    var card = _gameManager.enamyCardsInPlay[i];
                    _gameManager.enamyCardsInPlay.RemoveAt(i);
                    Destroy(card);
                    count--;
                }
            }
        }

        if (_gameManager.enamyCardsInPlay.Count == 0)
        {
            // _gameManager.LevelHandler();
            _gameManager.ActiveButtonBackTavern();
            _gameManager.ActiveButtonEndTurn();
        }
        else
        {
            _gameManager.ActiveButtonEndTurn();
        }

        if (_gameManager.playerCardsInPlay.Count == 0 &&  CheckButtleEnamy())
        {
            //TODO реализовать проигрышь
            _gameManager.SetTextPanel("Тебя сожрали!\nХа-Ха-Ха\nЛузер!");
        }
    }

    public bool CheckButtleEnamy()
    {
        foreach (var cardEnamy in _gameManager.enamyCardsInPlay)
        {
            if (cardEnamy.GetComponent<CardInfoEnamy>().isBattle)
            {
                GameReferance.stateReaward = true;
                return true;
            }
        }
        GameReferance.stateReaward = false;
        return false;
    }
}