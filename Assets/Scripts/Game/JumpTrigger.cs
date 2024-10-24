using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class JumpTrgger : MonoBehaviour
    {
        public static UnityAction<Transform> OnTrigger;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            OnTrigger?.Invoke(transform);
        }
    }
}
