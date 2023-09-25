using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootStrap : MonoBehaviour
{
    public CardView cardView;
  
    void Start()
    {
        ModelCard model = new ModelCard();
        CardViewModel cardViewModel = new CardViewModel(model);
        cardView.Init(cardViewModel);
    }

    
}
