using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{
    public static ItemSpawnManager instance;
    [SerializeField] float minTimer;
    [SerializeField] float maxTimer;
    [SerializeField] float maxTimeNotSpawn = 2.5f;
    [SerializeField] float percentSpawn = 70f;
    List<GameObject> PatternObstacle;
    List<GameObject> PatternCoinOnly;
    List<GameObject> PatternAll;
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
            PatternObstacle = loadPatternSpawn.PatternObstacle;

            PatternCoinOnly = loadPatternSpawn.PatternCoinOnly;

            PatternAll = loadPatternSpawn.LoadPatternAll();

        }
        else
            Debug.LogError("Cant Load Data " + loadPatternSpawn.name);

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
    void SpawnPattern(int ran, Transform position)
    {
        var newObj = Instantiate(PatternAll[ran].gameObject, position.transform);

        var newPosition = new Vector3(newObj.transform.position.x, newObj.transform.position.y + 1, newObj.transform.position.z);
        newObj.transform.position = newPosition;

        var spawnCoin = newObj.GetComponent<ObstacleCoinSpawner>();
        if (spawnCoin == null)
        {
            var newSpawner = newObj.AddComponent<ObstacleCoinSpawner>();
            newSpawner.InitCoin();
        }
        else spawnCoin.InitCoin();

        countTimeNotSpawn = 0;



    }
    public void SpawnItem(Transform position)
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
            var ranPattern = Random.Range(0, PatternAll.Count);
            SpawnPattern(ranPattern, position);

        }
        else
        {
            var newPercent = (100 - percentSpawn) / 100;
            if (Random.value > newPercent) //%30 percent chance (1 - 0.7 is 0.3)
            {

                var ranPattern = Random.Range(0, PatternAll.Count);
                SpawnPattern(ranPattern, position);
            }
        }


    }
    IEnumerator SpawnItem(float delayStart)
    {
        yield return new WaitForSecondsRealtime(delayStart);

        var ranPattern = Random.Range(0, PatternObstacle.Count);
        //SpawnPattern(ranPattern);

        var ranTimer = Random.Range(minTimer, maxTimer - (float)PlayerManager.instance.GetSpeedScore());


        yield return new WaitForSecondsRealtime(ranTimer);

        StartCoroutine("SpawnItem", 0);
    }


    /*
    NO.2
        Create a Obstacle and create pref coin Position in Obstacle;


    */
}
