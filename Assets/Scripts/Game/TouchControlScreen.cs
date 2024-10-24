using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UI.Game
{
    public class TouchControlScreen : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        public static UnityAction<Quaternion, float> OnDragAction;
        public static UnityAction<Vector2> OnDragEndAction;

        Vector2 m_startPoint;

        public void OnBeginDrag(PointerEventData eventData)
        {
            m_startPoint = eventData.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            var delta = Vector3.Normalize(m_startPoint - eventData.position);
            
            OnDragAction?.Invoke(Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.up, delta)), Vector2.Distance(m_startPoint, eventData.position));
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnDragEndAction?.Invoke(m_startPoint - eventData.position);
        }
    }
}
