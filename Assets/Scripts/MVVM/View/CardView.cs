using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardView : View, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler,
    IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform rt;

    private void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log($"OnPointerDown");
        _viewModel.DownHandler();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log($"OnPointerUp");
        _viewModel.UpHandler();
    }

    protected override void DisplaySpinResult(List<int> state)
    {
    }

    protected override void DisplayYouWin(bool isWin)
    {
    }

    protected override void DisplayGold(int gold)
    {
    }

    protected override void MoveCard(Vector2 pos)
    {
        rt.localPosition = pos;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        _viewModel.DownHandler();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}