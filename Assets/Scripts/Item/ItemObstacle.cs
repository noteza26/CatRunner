using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObstacle : ItemManager
{
    protected override void OnTriggerEnter(Collider other)
    {
        var getPlayer = other.GetComponent<PlayerMovement>();
        if (getPlayer)
        {
            PlayerManager.instance.HitObstacle();
        }
    }
}
