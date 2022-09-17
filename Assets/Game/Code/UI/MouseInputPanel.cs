using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DayOne.UI
{
    public class MouseInputPanel : MonoBehaviour, IPointerClickHandler
    {
        public event Action<Vector2> Clicked = null;

        public void OnPointerClick(PointerEventData eventData)
        {
            Clicked?.Invoke(eventData.position);
        }
    }
}
