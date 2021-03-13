using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{
    public static ItemSpawnManager instance;
    [SerializeField] float minTimer;
    [SerializeField] float maxTimer;
    float maxTimeNotSpawn = Random.Range(2.0f, 3f);
    [SerializeField] List<GameObject> PatternObstacle;
    LoadPatternSpawn loadPatternSpawn;

    float countTimeNotSpawn;
    private void Awake()
    {


    }
    void Start()
    {
        Init();
    }
    void Init()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);

        loadPatternSpawn = this.GetComponent<LoadPatternSpawn>();
        if (loadPatternSpawn)
        {

            PatternObstacle.Clear();
            PatternObstacle = loadPatternSpawn.PatternObstacle;
            Debug.Log(loadPatternSpawn.PatternObstacle.Count);
        }
        else
            Debug.LogError("Cant Load Data " + loadPatternSpawn.name);

        SpawnItem();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.instance.IsStop) return;
        CountNoneSpawn();
    }
    void CountNoneSpawn()
    {
        if (countTimeNotSpawn != maxTimeNotSpawn)
            countTimeNotSpawn += Time.deltaTime;
        else
            countTimeNotSpawn = maxTimeNotSpawn;

    }
    void SpawnPattern(int ran)
    {
        var newObj = Instantiate(PatternObstacle[ran].gameObject, this.transform);
        var addpath = newObj.AddComponent<PathMovement>();
        addpath.SetDeletePoint = newObj;

        var spawnCoin = newObj.GetComponent<ObstacleCoinSpawner>();
        if (spawnCoin == null) Destroy(newObj.gameObject);
        else spawnCoin.InitCoin();

        countTimeNotSpawn = 0;



    }
    public void SpawnItem()
    {
        // StartCoroutine("SpawnItem", 5f);

        /*     if (Random.value > 0.5) //%50 percent chance
             {
             }

             if (Random.value > 0.2) //%80 percent chance (1 - 0.2 is 0.8)
             {
             }*/
        if (countTimeNotSpawn >= maxTimeNotSpawn)
        {
            var ranPattern = Random.Range(0, PatternObstacle.Count);
            SpawnPattern(ranPattern);
            Debug.Log("Spawn With count");

        }
        else
        {
            if (Random.value > 0.7) //%30 percent chance (1 - 0.7 is 0.3)
            {

                var ranPattern = Random.Range(0, PatternObstacle.Count);
                SpawnPattern(ranPattern);
            }
        }


    }
    IEnumerator SpawnItem(float delayStart)
    {
        yield return new WaitForSecondsRealtime(delayStart);

        var ranPattern = Random.Range(0, PatternObstacle.Count);
        SpawnPattern(ranPattern);

        var ranTimer = Random.Range(minTimer, maxTimer - (float)PlayerManager.instance.GetSpeedScore());


        yield return new WaitForSecondsRealtime(ranTimer);

        StartCoroutine("SpawnItem", 0);
    }


    /*
    NO.2
        Create a Obstacle and create pref coin Position in Obstacle;


    */
}
