using UnityEngine;

[CreateAssetMenu(fileName = "New Road ", menuName = "Road/Create new road", order = 1)]

public class RoadSpawnManagerScript : ScriptableObject
{
    [System.Serializable]
    public struct PrefSpawn
    {
        public string RoadName;
        public GameObject[] RoadPref;
    }

    public PrefSpawn[] RoadTheme;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
