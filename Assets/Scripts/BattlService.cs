using UnityEngine;

public class BattlService:MonoBehaviour
{

   public ScriptableObject DataBatle;
   public  void Battle(CardInfoPlayer player, CardInfoEnamy enamy)
   {
      switch (player.TypeСreature)
      {
         case TypeСreature.Paladin:
            AttakePaladin();
            break;
      }
   }

   private void AttakePaladin()
   {
      
   }
}
