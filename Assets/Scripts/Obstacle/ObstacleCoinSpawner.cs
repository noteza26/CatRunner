using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCoinSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] coinPattern;

    public void InitCoin()
    {
        var ran = Random.Range(0, coinPattern.Length);
        for (int i = 0; i < coinPattern.Length; i++)
        {
            coinPattern[i].SetActive(false);
            if (i == ran)
                coinPattern[i].SetActive(true);


        }
    }
}
