using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class WinTrigger : MonoBehaviour
    {
        public static UnityAction OnTrigger;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            OnTrigger?.Invoke();
        }
    }
}
