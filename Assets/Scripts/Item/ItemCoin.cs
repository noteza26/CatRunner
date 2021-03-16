using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCoin : ItemManager
{
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        var tag = other.tag;
        if (tag == "Obstacle")
        {
            this.transform.position += new Vector3(0, 2.355f, 0);
            Debug.Log("in obstacle");
        }


    }
}
