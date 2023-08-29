using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardInfo : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IDragHandler,
    IBeginDragHandler, IEndDragHandler
{
    public Sprite image;
    public string name;
    public TypeCard typeCard;

    private Vector2 lastMousePosition;

    private Vector2 _initialPos;
    private Transform oldTransform;
    private int _siblingIndex;
    private GameObject oldEnamy;

    void Start()
    {
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

        Debug.Log(results.Count);

        return results;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log($"OnPointerDown");
        _siblingIndex = transform.GetSiblingIndex();
        transform.SetParent(GameReferance.CanvasGame);
        transform.DOShakeScale(0.1f, new Vector3(0.4f, 0.3f, 0f), 0, 0f, false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log($"OnPointerEnter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        var j = typeCard == TypeCard.Player ? GameReferance.PlayerCardContainer : GameReferance.EnamyCardContainer;
        transform.SetParent(j);
        transform.SetSiblingIndex(_siblingIndex);
        Debug.Log($"OnPointerExit");
    }

    public void OnDrag(PointerEventData eventData)
    {
        // transform.position = Input.mousePosition;
        // var t = eventData.position;
        // transform.localPosition= t;
        transform.localPosition = InputPos();
        var k = RaycastMouse();
        foreach (var VARIABLE in k)
        {
            if (VARIABLE.gameObject.GetComponent<CardInfo>() != null)
            {
                if (VARIABLE.gameObject.GetComponent<CardInfo>().typeCard == TypeCard.Enamy)
                {
                    if (oldEnamy != VARIABLE.gameObject)
                    {
                        if (oldEnamy != null)
                        {
                            oldEnamy.transform.DOScale(new Vector3(1f, 1f, 0f), 0.5f);
                        }

                        oldEnamy = VARIABLE.gameObject;
                        oldEnamy.transform.DOScale(new Vector3(1.2f, 1.2f, 0f), 0.5f);
                    }
                }
                else
                {
                    if (oldEnamy != null)
                    {
                        oldEnamy.transform.DOScale(new Vector3(1f, 1f, 0f), 0.5f);
                        oldEnamy = null;
                    }
                }
            }
        }
        // Vector2 currentMousePosition = eventData.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        lastMousePosition = eventData.position;
        _initialPos = transform.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log($"OnEndDrag");
        transform.position = _initialPos;
    }
}