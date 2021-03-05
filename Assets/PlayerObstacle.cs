using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObstacle : MonoBehaviour
{
    [SerializeField] GameObject[] obstaclePref;
    [SerializeField] int minLane;
    [SerializeField] int maxLane;

    [SerializeField] int maxSpawn;
    [SerializeField] int laneOffset;

    [SerializeField] int minZoffset;
    [SerializeField] int maxZoffset;
    [SerializeField] List<GameObject> obsList;


    [SerializeField] bool left;
    [SerializeField] bool middle;
    [SerializeField] bool right;
    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }
    void Setup()
    {
        var ranSpawn = Random.Range(0, maxSpawn);

        for (var i = 0; i < 3; i++)
        {
            var ranitem = Random.Range(0, obstaclePref.Length);
            var ranZ = Random.Range(minZoffset, maxZoffset);
            var ranLan = Random.Range(minLane, maxLane + 1);
            var lane = ranLan * laneOffset;

            Debug.Log(ranLan);
            if (lane == -1 && left)
            {
                var ran = Random.Range(0, 1);
                if (ran == 0)
                    lane = 0 * laneOffset;
                else
                    lane = 1 * laneOffset;
            }
            else if (lane == 0 && middle)
            {
                var ran = Random.Range(0, 1);
                if (ran == 0)
                    lane = -1 * laneOffset;
                else
                    lane = 1 * laneOffset;
            }
            else if (lane == 1 && right)
            {
                var ran = Random.Range(0, 1);
                if (ran == 0)
                    lane = -1 * laneOffset;
                else
                    lane = 0 * laneOffset;
            }

            if (lane == -1)
                left = true;
            else if (lane == 0)
                middle = true;
            else if (lane == 1)
                right = true;

            var Obs = Instantiate(obstaclePref[ranitem], new Vector3(0, 0, 0), Quaternion.identity);
            Obs.transform.SetParent(this.transform);

            Obs.transform.localScale = obstaclePref[ranitem].transform.localScale;
            Obs.transform.localPosition = new Vector3(lane, 0, ranZ);

            obsList.Add(Obs);

        }





    }
    // Update is called once per frame
    void Update()
    {



    }
}
