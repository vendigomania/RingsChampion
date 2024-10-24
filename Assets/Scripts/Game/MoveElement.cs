using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveElement : MonoBehaviour
{
    public static float YVelocity = 0f;

    // Update is called once per frame
    void Update()
    {

        if(transform.position.y < -2f)
        {
            transform.Translate(Vector2.up * YVelocity * Time.deltaTime);
        }
    }

    public float GetRandomXPosition()
    {
        return Random.Range(-2f, 2f);
    }
}
