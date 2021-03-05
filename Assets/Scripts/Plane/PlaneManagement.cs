using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneManagement : MonoBehaviour
{
    [SerializeField] Transform spawwnPoint;

    public Transform SpawnPoint()
    {
        return spawwnPoint;
    }
}
