using System;
using UnityEngine;

public class BattlService: MonoBehaviour
{
   [SerializeField] private UpgradeDataBase dataBase;
   private Hero[] heroData;
   private GameManager _gameManager;

   public static BattlService Instance;
   private void Start()
   {
      if (Instance == null)
      {
         Instance = this; 
      }
      else if(Instance == this)
      { 
         Destroy(gameObject); 
      }
      DontDestroyOnLoad(gameObject);
      
      _gameManager = GetComponent<GameManager>();
      heroData = dataBase.GetUpgrade();
   }

   public  void Attake(CardInfoPlayer player, CardInfoEnamy enamy)
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
               }
            }
         }
      }
   }

   private void Battle(CardInfoPlayer player, CardInfoEnamy enamy, int count)
   {
      var type = enamy.TypeСreature;
      //TODO пернос на кладбище героя
      // foreach ( var infoPlayer in _gameManager.playerCardsInPlay)
      // {
      //    if (infoPlayer.GetComponent<CardInfoPlayer>() == player)
      //    {
      //       
      //    }
      // }
      _gameManager.playerCardsInPlay.Remove(player.gameObject);
      Destroy(player);

      _gameManager.enamyCardsInPlay.Remove(enamy.gameObject);
      Destroy(enamy);
      if (count > 1)
      {
         for (int i = 1; i < count; i++)
         {
            foreach (var enamyCards in _gameManager.enamyCardsInPlay)
            {
               if (enamyCards.GetComponent<CardInfoEnamy>().TypeСreature == type)
               {
                  _gameManager.enamyCardsInPlay.Remove(enamyCards);
                  Destroy(enamyCards);
               }
            }
         }
      }

   }
}
