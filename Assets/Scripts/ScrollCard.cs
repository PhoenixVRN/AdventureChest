using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollCard : CardInfoPlayer, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (GameReferance.isReroll)
        {
            // transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 1f);
            _gameManager.playerCardsInPlay.Remove(gameObject);
            _gameManager.rerolPlayer++;
            Destroy(gameObject);
        }

        transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 1f);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if (GameReferance.isReroll) return;
        transform.DOScale(new Vector3(1f, 1f, 1f), 1f);
        //TODO рерол
        _gameManager.cemetery++;
        GameReferance.isReroll = true;
        _gameManager.buttonReRoll.gameObject.SetActive(true);
        _gameManager.playerCardsInPlay.Remove(gameObject);
        Destroy(gameObject);
        Debug.Log($"РеРолл");
    }

    public override void OnDrag(PointerEventData eventData)
    {
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
    }
}