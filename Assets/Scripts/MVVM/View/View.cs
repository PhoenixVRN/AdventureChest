using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public abstract class View : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        protected ViewModel _viewModel;

        public void Init(ViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.ViewStateChanged += DisplaySpinResult;
            _viewModel.ViewGoldChanged += DisplayGold;
            _viewModel.ViewIsWinChanged += DisplayYouWin;
        }

        protected abstract void DisplaySpinResult(List<int> state);
        protected abstract void DisplayYouWin(bool isWin);
        protected abstract void DisplayGold(int gold);
        protected abstract void DownHandler();

        protected virtual void Dispose()
        {
            _viewModel.ViewStateChanged -= DisplaySpinResult;
            _viewModel.ViewGoldChanged -= DisplayGold;
            _viewModel.ViewIsWinChanged -= DisplayYouWin;
        }

        protected void OnDestroy()
        {
            Dispose();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _viewModel.DownHandler();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }
    }
