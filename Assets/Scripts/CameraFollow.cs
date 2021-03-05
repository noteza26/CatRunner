using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject player;
    [SerializeField] float speedMove;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        // transform.position = player.transform.position + offset;
        var playerVec = new Vector3(player.transform.position.x, player.transform.position.y + offset.y, offset.z);

        transform.position = Vector3.Lerp(transform.position, playerVec, speedMove * Time.deltaTime);

    }
}
