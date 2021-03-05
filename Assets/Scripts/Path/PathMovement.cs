using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMovement : MonoBehaviour
{
    [SerializeField] GameObject deletePoint;

    void Update()
    {
        if (PlayerManager.instance.IsStop || PlayerManager.instance.GodStop) return;
        this.transform.position -= Vector3.forward * PathManager.instance.nowSpeed * Time.deltaTime;
        if (deletePoint == null)
            Destroy(this.gameObject);
    }
}
