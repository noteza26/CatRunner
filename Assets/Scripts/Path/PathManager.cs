using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{

    public static PathManager instance;
    public float MinSpeed = 10f;
    public float MaxSpeed = 20f;
    public float nowSpeed;

    public float SpeedAcceleration = 0.2f;
    public Transform spawnParent;
    [SerializeField] PlaneManagement LastPlane;

    public float speedRatio { get { return (nowSpeed - MinSpeed) / (MaxSpeed - MinSpeed); } }


    //[Header("Plane Pref")]

    // Start is called before the first frame update
    private void Start()
    {
        nowSpeed = MinSpeed;
        //InvokeRepeating("SpeedCheck", 0f, 2.5f);
        // StartCoroutine(CheckSpeed());
        instance = this;
    }
    public void DestroyPlane(GameObject plane)
    {
        Destroy(plane);
        InstantNewPlane();
        InstantNewItem();
    }
    private void Update()
    {
        if (PlayerManager.instance.IsStop) return;

        if (nowSpeed < MaxSpeed)
            nowSpeed += SpeedAcceleration * Time.deltaTime;
        else
            nowSpeed = MaxSpeed;

        var newSpeed = nowSpeed * Time.deltaTime;

        PlayerManager.instance.SetSpeedScore(nowSpeed);
        PlayerAnimation.instance.SpeedRunAnim(newSpeed);
        PlayerManager.instance.AddPlayerScore(newSpeed);
    }
    void InstantNewItem()
    {
        ItemSpawnManager.instance.SpawnItem();
    }
    void InstantNewPlane()
    {
        var newPlane = RoadSpawnManager.instance.Spawn();
        if (newPlane)
        {
            var getSpawnPoint = LastPlane.SpawnPoint();
            var newObj = Instantiate(newPlane, getSpawnPoint.transform.position, getSpawnPoint.transform.rotation);
            newObj.gameObject.transform.SetParent(spawnParent);
            LastPlane = newObj.GetComponent<PlaneManagement>();
        }
        else
        {
            Debug.LogError(" Cant Spawn new Plane");
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        var Tag = other.tag;
        if (Tag == "Path")
            DestroyPlane(other.gameObject);
        else if (Tag == "Obstacle")
            Destroy(other.gameObject);
        else if (Tag == "Item")
            Destroy(other.gameObject);
    }
}
