using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCoinSpawner : MonoBehaviour
{
    [SerializeField] Transform coinTransform;
    [SerializeField]
    List<GameObject> coinPattern;

    private void Awake()
    {

        for (int i = 0; i < coinTransform.childCount; i++)
        {
            coinPattern.Add(coinTransform.GetChild(i).gameObject);
        }
    }

    public void InitCoin()
    {

        var ran = Random.Range(0, coinPattern.Count);
        for (int i = 0; i < coinPattern.Count; i++)
        {
            coinPattern[i].SetActive(false);
            if (i == ran)
                coinPattern[i].SetActive(true);


        }
    }
}

