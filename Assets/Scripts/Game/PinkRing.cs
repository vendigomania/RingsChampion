using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PinkRing : MonoBehaviour
    {
        [SerializeField] private Sprite blueSkin;
        [SerializeField] private Sprite pinkSkin;

        [SerializeField] private SpriteRenderer spriteRenderer;

        bool isMoving;

        Vector3 start;
        Vector3 target;

        public void Init(bool _isMoving)
        {
            isMoving = _isMoving;
            spriteRenderer.sprite = isMoving ? pinkSkin : blueSkin;

            start = transform.position;
            target = start + new Vector3(Random.Range(0, 2) == 0 ? -1 : 1, Random.Range(0, 2) == 0 ? -1 : 1);

            if(target == start) isMoving = false;
        }

        // Update is called once per frame
        void Update()
        {
            if(isMoving)
            {
                if(transform.position != target)
                {
                    transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime);
                }
                else
                {
                    Vector3 temp = target;
                    target = start;
                    start = temp;
                }
            }
        }
    }
}
