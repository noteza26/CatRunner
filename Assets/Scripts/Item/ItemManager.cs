using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public Item Item;
    protected virtual void OnTriggerEnter(Collider other)
    {
        //        Debug.Log(other.name);
        var getItem = other.GetComponent<PlayerMovement>();

        if (getItem)
        {
            PlayerManager.instance.AddItem(Item);
            Destroy(this.gameObject);
        }
    }
}
