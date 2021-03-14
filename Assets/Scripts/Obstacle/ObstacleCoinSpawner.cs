using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCoinSpawner : MonoBehaviour
{
    [SerializeField] Transform coinTransform;

    [SerializeField] float percentSpawn;

    [SerializeField] List<GameObject> coinPattern;

    private void Awake()
    {
        if (coinPattern == null) Debug.LogError("None Transfrom");
        for (int i = 0; i < coinTransform.childCount; i++)
        {
            coinPattern.Add(coinTransform.GetChild(i).gameObject);
        }
    }

    public void InitCoin()
    {
        var newPercent = (100 - percentSpawn) / 100;
        if (Random.value > newPercent)
        {
            SpawnItem(true);
        }
        else
        {
            SpawnItem(false);

        }
    }

    void SpawnItem(bool spawnItem)
    {

        var ran = Random.Range(0, coinPattern.Count);
        for (int i = 0; i < coinPattern.Count; i++)
        {
            coinPattern[i].SetActive(false);
            if (i == ran && spawnItem)
                coinPattern[i].SetActive(true);


        }
    }
}

