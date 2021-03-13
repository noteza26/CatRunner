using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{

    [SerializeField] float minTimer;
    [SerializeField] float maxTimer;
    [SerializeField] List<GameObject> PatternObstacle;
    LoadPatternSpawn loadPatternSpawn;
    private void Awake()
    {


    }
    void Start()
    {
        Init();
    }
    void Init()
    {
        loadPatternSpawn = this.GetComponent<LoadPatternSpawn>();
        if (loadPatternSpawn)
        {

            PatternObstacle.Clear();
            PatternObstacle = loadPatternSpawn.PatternObstacle;
            Debug.Log(loadPatternSpawn.PatternObstacle.Count);
        }
        else
            Debug.LogError("Cant Load Data " + loadPatternSpawn.name);

        StartSpawnItem();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void SpawnPattern(int ran)
    {
        var newObj = Instantiate(PatternObstacle[ran].gameObject, this.transform);
        var addpath = newObj.AddComponent<PathMovement>();
        addpath.SetDeletePoint = newObj;

        var spawnCoin = newObj.GetComponent<ObstacleCoinSpawner>();
        if (spawnCoin == null) Destroy(newObj.gameObject);
        else spawnCoin.InitCoin();
    }
    public void StartSpawnItem()
    {
        StartCoroutine("SpawnItem", 5f);

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
