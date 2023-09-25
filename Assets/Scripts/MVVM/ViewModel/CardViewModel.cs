using System;
using UnityEngine;

public class CardViewModel : ViewModel
{
    public CardViewModel(Model model) : base(model)
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
    
    
    protected override bool AnalyzeResult()
    {
        throw new NotImplementedException();
    }
}
