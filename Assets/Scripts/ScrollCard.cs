using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollCard : CardInfoPlayer, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    private bool isDrag;
    private GameObject potion;
    private int _siblingIndex;
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (GameReferance.isReroll)
        {
            // transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 1f);
            _gameManager.playerCardsInPlay.Remove(gameObject);
            _gameManager.rerolPlayer++;
            Destroy(gameObject);
        }
        _siblingIndex = transform.GetSiblingIndex();
        transform.SetParent(GameReferance.CanvasGame);
        transform.DOShakeScale(0.1f, new Vector3(0.4f, 0.3f, 0f), 0, 0f, false);
        transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 1f);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        var j = typeCard == TypeCard.Player ? GameReferance.PlayerCardContainer : GameReferance.EnamyCardContainer;
        transform.SetParent(j);
        transform.SetSiblingIndex(_siblingIndex);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if (GameReferance.isReroll || potion != null) return;
        if (isDrag)
        {
            var j = typeCard == TypeCard.Player ? GameReferance.PlayerCardContainer : GameReferance.EnamyCardContainer;
            transform.SetParent(j);
            transform.SetSiblingIndex(_siblingIndex);
            return;
        }
        transform.DOScale(new Vector3(1f, 1f, 1f), 1f);
        _gameManager.cemetery++;
        GameReferance.isReroll = true;
        _gameManager.buttonReRoll.gameObject.SetActive(true);
        _gameManager.playerCardsInPlay.Remove(gameObject);
        Destroy(gameObject);
        Debug.Log($"РеРолл");
    }
    
    private Vector2 InputPos()
    {
        Vector2 localPoint = Vector2.zero;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(GameReferance.CanvasGame.GetComponent<RectTransform>(),
            Input.mousePosition,
            Camera.main, out localPoint);
        return localPoint;
    }
    
    public List<RaycastResult> RaycastMouse()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            pointerId = -1,
        };

        pointerData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        // Debug.Log(results.Count);

        return results;
    }
    
    private void DetectPotion()
    {
        var k = RaycastMouse();
        bool isPotion = false;
        foreach (var VARIABLE in k)
        {
            if (VARIABLE.gameObject.GetComponent<CardInfoEnamy>() != null)
            {
                if (potion != VARIABLE.gameObject)
                {
                    if (potion != null)
                    {
                        potion.transform.DOScale(new Vector3(1f, 1f, 0f), 0.5f);
                    }
                    isPotion = true;
                    potion = VARIABLE.gameObject;
                    potion.transform.DOScale(new Vector3(1.2f, 1.2f, 0f), 0.5f);
                }
                else
                {
                    isPotion = true;
                }
            }
        }
        if (potion != null && !isPotion)
        {
            potion.transform.DOScale(new Vector3(1f, 1f, 0f), 0.5f);
            potion = null;
        }
    }
    public override void OnDrag(PointerEventData eventData)
    {
        if (GameReferance.isReroll) return;
        isDrag = true;
        transform.localPosition = InputPos();
        DetectPotion();
    }

    

    public override void OnBeginDrag(PointerEventData eventData)
    {
        isDrag = true;
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;
        if (potion != null)
        {
            BattlService.Instance.Reward(this, potion.GetComponent<CardInfoEnamy>());
        }
    }
}