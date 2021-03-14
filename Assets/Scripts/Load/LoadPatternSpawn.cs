using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LoadPatternSpawn : MonoBehaviour
{
    List<GameObject> _patternObstacle;
    List<GameObject> _patternCoinOnly;
    public List<GameObject> PatternObstacle
    {
        get
        {
            return _patternObstacle;
        }
    }

    public List<GameObject> PatternCoinOnly
    {
        get
        {
            return _patternCoinOnly;
        }
    }
    private void Awake()
    {
        _patternObstacle = LoadPatternObstacle();
        _patternCoinOnly = LoadPatternCoinOnly();
        
    }
    public List<GameObject> LoadPatternAll()
    {
        var patternAll = new List<GameObject>();
        for (int i = 0; i < _patternObstacle.Count; i++)
        {
            patternAll.Add(_patternObstacle[i]);
        }

        for (int i = 0; i < _patternCoinOnly.Count; i++)
        {
            patternAll.Add(_patternCoinOnly[i]);
        }

        return patternAll;

    }
    List<GameObject> LoadPatternCoinOnly()
    {
        var loadCoin = Resources.LoadAll("Item/CoinOnly");
        var gameObj = new List<GameObject>();
        foreach (var i in loadCoin)
        {
            gameObj.Add((GameObject)i);
        }
        return gameObj;
    }
    List<GameObject> LoadPatternObstacle()
    {
        var loadCoin = Resources.LoadAll("Item/Obstacle");
        var gameObj = new List<GameObject>();
        foreach (var i in loadCoin)
        {
            gameObj.Add((GameObject)i);
        }
        return gameObj;
    }
}
