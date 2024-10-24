using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Problem : MonoBehaviour
{
    [SerializeField] private GameObject[] variants;
    
    public void SetRandom()
    {
        int rnd = Random.Range(0, variants.Length);
        for(int i = 0; i < variants.Length; i++)
            variants[i].SetActive(i == rnd);
    }
}
