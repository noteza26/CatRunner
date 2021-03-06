using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LoadPatternSpawn : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
    public List<GameObject> LoadPatternCoin()
    {
        var loadCoin = Resources.LoadAll("Item/CoinOnly");
        var gameObj = new List<GameObject>();
        foreach (var i in loadCoin)
        {
            gameObj.Add((GameObject)i);
        }
        return gameObj;
    }
    public List<GameObject> LoadPatternObstacle()
    {
        var loadCoin = Resources.LoadAll("Item/HasObstacle");
        var gameObj = new List<GameObject>();
        foreach (var i in loadCoin)
        {
            gameObj.Add((GameObject)i);
        }
        return gameObj;
    }
}
