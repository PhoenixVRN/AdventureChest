using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardInfoEnamy : CardInfoBase ,  IPointerDownHandler
{
  public bool isBattle;
  
  public void OnPointerDown(PointerEventData eventData)
  {
    if (GameReferance.isReroll)
    {
      transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 1f);
      var gm = GameReferance.GameManager.gameObject.GetComponent<GameManager>();
      gm.enamyCardsInPlay.Remove(gameObject);
      gm.rerollEnamy++;
      Destroy(gameObject);
    }
  }
}
