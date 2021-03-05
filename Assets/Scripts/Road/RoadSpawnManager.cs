using UnityEngine;

public class RoadSpawnManager : MonoBehaviour
{
    public static RoadSpawnManager instance;

    public int NowTheme;
    public int NowRoad;
    public bool IsLast;
    public RoadSpawnManagerScript.PrefSpawn DataTheme;

    public RoadSpawnManagerScript DataRoad;

    void Start()
    {
        ResetTheme();
        GenTheme();
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }
    void ResetTheme()
    {
        NowTheme = 0;
        NowRoad = 0;
        IsLast = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Spawn();
        }
    }
    public GameObject Spawn()
    {
        if (IsLast)
        {
            if (DataRoad.RoadTheme.Length == NowTheme + 1)// Reset a Road to last Theme when max theme edit here
                ResetTheme();
            else
                AddNewTheme();

            GenTheme();
        }

        var spawnRoad = SpawnRoad();
        if (spawnRoad) return spawnRoad;
        else return null;
    }

    public GameObject SpawnRoad()
    {
        var countTheme = DataTheme.RoadPref;


        for (int i = 0; i < countTheme.Length; i++)
        {
            if (i == NowRoad)
            {
                var genNewRoad = GenerateRoad();
                if (genNewRoad)
                {
                    AddNewRoad();
                    if (DataTheme.RoadPref.Length == NowRoad)
                        IsLast = true;
                    return genNewRoad;
                }
            }
        }
        return null;
    }
    private void AddNewRoad()
    {
        NowRoad++;
    }
    private void AddNewTheme()
    {
        // Change data to new Theme
        NowTheme++;
        NowRoad = 0;
        IsLast = false;
    }
    private void GenTheme()
    {
        DataTheme = DataRoad.RoadTheme[NowTheme];
    }
    private GameObject GenerateRoad()
    {
        var genNewRoad = DataTheme.RoadPref[NowRoad];
        if (genNewRoad == null)
        {
            AddNewTheme();
            GenTheme();
            return null;
        }
        else
            return genNewRoad;
    }
}
