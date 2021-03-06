using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{

    [SerializeField] float minTimer;
    [SerializeField] float maxTimer;
    [SerializeField] List<GameObject> coinPref;
    [SerializeField] List<GameObject> hasObstaclePref;
    LoadPatternSpawn loadPatternSpawn;
    private void Awake()
    {
        loadPatternSpawn = this.GetComponent<LoadPatternSpawn>();
        if (loadPatternSpawn)
        {
            coinPref.Clear();
            coinPref = loadPatternSpawn.LoadPatternCoin();

            hasObstaclePref.Clear();
            hasObstaclePref = loadPatternSpawn.LoadPatternObstacle();
        }
    }
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

        var ranPattern = Random.Range(0, coinPref.Count);
        SpawnPattern(ranPattern);

        var ranTimer = Random.Range(minTimer, maxTimer);


        yield return new WaitForSecondsRealtime(ranTimer);

        StartCoroutine("SpawnItem", 0);
    }
}
