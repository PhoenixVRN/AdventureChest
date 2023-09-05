using System;
using UnityEngine;

public class BattlService : MonoBehaviour
{
    [SerializeField] private UpgradeDataBase dataBase;
    private Hero[] heroData;
    private GameManager _gameManager;
    private int batlleEnamy;

    public static BattlService Instance;

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

        _gameManager = GetComponent<GameManager>();
        heroData = dataBase.GetUpgrade();
    }

    public void Attake(CardInfoPlayer player, CardInfoEnamy enamy)
    {
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

    private void Battle(CardInfoPlayer player, CardInfoEnamy enamy, int count)
    {
        var type = enamy.TypeСreature;
        //TODO пернос на кладбище героя
      
        _gameManager.playerCardsInPlay.Remove(player.gameObject);
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
            _gameManager.LevelHandler();
        }

        if (_gameManager.playerCardsInPlay.Count == 0)
        {
            _gameManager.SetTextPanel("Тебя сожрали!\nХа-Ха-Ха\nЛузер!");
        }
    }

    public bool CheckButtleEnamy()
    {
        foreach (var cardEnamy in _gameManager.enamyCardsInPlay)
        {
            if (cardEnamy.GetComponent<CardInfoEnamy>().isBattle)
            {
                return true;
            }
        }
        return false;
    }
}