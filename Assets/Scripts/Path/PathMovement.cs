using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMovement : MonoBehaviour
{
    internal GameObject SetDeletePoint { set { deletePoint = value; } }
    [SerializeField] GameObject deletePoint;
    [SerializeField] bool dontDestroy;

    void Update()
    {
        if (PlayerManager.instance.IsStop || PlayerManager.instance.GodStop) return;
        this.transform.position -= Vector3.forward * PathManager.instance.nowSpeed * Time.deltaTime;
        if (deletePoint == null && !dontDestroy)
            Destroy(this.gameObject);
    }
}
