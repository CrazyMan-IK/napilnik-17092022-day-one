using System;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace DayOne.UI
{
    public class Button : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public event Action Clicked = null;

        private bool _isPressed = false;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.pointerCurrentRaycast.gameObject != gameObject)
            {
                return;
            }

            _isPressed = true;

            transform.DOScale(new Vector3(1.2f, 0.8f, 1), 0.25f).SetEase(Ease.OutElastic);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!_isPressed)
            {
                return;
            }

            _isPressed = false;

            transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.OutElastic);

            if (eventData.pointerCurrentRaycast.gameObject != gameObject)
            {
                return;
            }

            Clicked?.Invoke();
        }
    }
}
