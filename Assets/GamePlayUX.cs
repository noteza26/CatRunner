using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayUX : MonoBehaviour
{
    public static GamePlayUX instance;

    public Vector3 position;
    public Vector3 newPosition;
    [SerializeField] Transform rightTop;
    [SerializeField] float speedLerp;
    void Start()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);

    }

    public void Tween()
    {
        var isNear = false;
        while (!isNear)
        {
            rightTop.transform.position = Vector3.Lerp(rightTop.position, position, speedLerp * Time.deltaTime);
            var dis = Vector3.Distance(rightTop.position, position);
            if (dis < 1f)
            {
                isNear = true;
                break;
            }
            Debug.Log(dis);
        }
    }
}
