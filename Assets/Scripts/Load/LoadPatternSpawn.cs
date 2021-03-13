using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LoadPatternSpawn : MonoBehaviour
{
    List<GameObject> _patternObstacle;

    public List<GameObject> PatternObstacle
    {
        get
        {
            return _patternObstacle;
        }
    }
    private void Awake()
    {
        _patternObstacle = LoadPatternObstacle();
    }
    List<GameObject> LoadPatternObstacle()
    {
        var loadCoin = Resources.LoadAll("Obstacle");
        var gameObj = new List<GameObject>();
        foreach (var i in loadCoin)
        {
            gameObj.Add((GameObject)i);
        }
        return gameObj;
    }
}
