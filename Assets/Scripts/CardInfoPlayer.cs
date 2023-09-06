using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardInfoPlayer : CardInfoBase, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler,
    IDragHandler,
    IBeginDragHandler, IEndDragHandler
{
    private Vector2 lastMousePosition;

    private Vector2 _initialPos;
    private Transform oldTransform;
    private int _siblingIndex;
    private GameObject oldEnamy;

    protected  GameManager _gameManager;
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }


    void Update()
    {
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

    private void DetectEnamy()
    {
        var k = RaycastMouse();
        bool isEnamy = false;
        foreach (var VARIABLE in k)
        {
            if (VARIABLE.gameObject.GetComponent<CardInfoEnamy>() != null)
            {
                if (oldEnamy != VARIABLE.gameObject)
                {
                    if (oldEnamy != null)
                    {
                        oldEnamy.transform.DOScale(new Vector3(1f, 1f, 0f), 0.5f);
                    }

                    isEnamy = true;
                    oldEnamy = VARIABLE.gameObject;
                    oldEnamy.transform.DOScale(new Vector3(1.2f, 1.2f, 0f), 0.5f);
                }
                else
                {
                    isEnamy = true;
                }
            }
        }
        if (oldEnamy != null && !isEnamy)
        {
            oldEnamy.transform.DOScale(new Vector3(1f, 1f, 0f), 0.5f);
            oldEnamy = null;
        }
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        // Debug.Log($"OnPointerDown");
        if (GameReferance.isReroll)
        {
            _gameManager.rerolPlayer++;
            _gameManager.playerCardsInPlay.Remove(gameObject);
            Destroy(gameObject);
        }
        _siblingIndex = transform.GetSiblingIndex();
        transform.SetParent(GameReferance.CanvasGame);
        transform.DOShakeScale(0.1f, new Vector3(0.4f, 0.3f, 0f), 0, 0f, false);
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        // Debug.Log($"OnPointerEnter");
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (GameReferance.isReroll) return;
        var j = typeCard == TypeCard.Player ? GameReferance.PlayerCardContainer : GameReferance.EnamyCardContainer;
        transform.SetParent(j);
        transform.SetSiblingIndex(_siblingIndex);
        // Debug.Log($"OnPointerExit");
    }

   public virtual void OnDrag(PointerEventData eventData)
    {
        if (GameReferance.isReroll) return;
        // transform.position = Input.mousePosition;
        // var t = eventData.position;
        // transform.localPosition= t;
        transform.localPosition = InputPos();
        DetectEnamy();
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        if (GameReferance.isReroll) return;
        lastMousePosition = eventData.position;
        _initialPos = transform.position;
    }

    public  virtual void OnEndDrag(PointerEventData eventData)
    {
        if (GameReferance.isReroll) return;
        // Debug.Log($"OnEndDrag");
        if (oldEnamy != null && BattlService.Instance.CheckButtleEnamy())
        {
            BattlService.Instance.Attake(this, oldEnamy.GetComponent<CardInfoEnamy>());
        }
        else if (oldEnamy != null)
        {
            Debug.Log($"Добыча");
        }
    }
}