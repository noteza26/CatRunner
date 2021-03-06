using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{

    [SerializeField] float minTimer;
    [SerializeField] float maxTimer;
    [SerializeField] GameObject[] coinPref;
    [SerializeField] GameObject[] hasObstaclePref;


    void Start()
    {
        StartCoroutine("SpawnItem", 5f);

    }

    // Update is called once per frame
    void Update()
    {

    }
    void SpawnPattern(int ran)
    {
        var newObj = Instantiate(coinPref[ran].gameObject, this.transform);
        var addpath = newObj.AddComponent<PathMovement>();
        addpath.SetDeletePoint = newObj;
    }
    IEnumerator SpawnItem(float delayStart)
    {
        yield return new WaitForSecondsRealtime(delayStart);

        var ranPattern = Random.Range(0, coinPref.Length);
        SpawnPattern(ranPattern);

        var ranTimer = Random.Range(minTimer, maxTimer);


        yield return new WaitForSecondsRealtime(ranTimer);

        StartCoroutine("SpawnItem", 0);
    }
}
