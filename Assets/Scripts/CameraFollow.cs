using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow instance;
    public GameObject Player;
    [SerializeField] float speedMove;
    private Vector3 offset;
    public Transform mainCamera;

    private void Awake()
    {
        if (instance) Destroy(this.gameObject);
        else instance = this;
        StartOffset();
    }
    public void SetCamera(GameObject player)
    {
        Player = player;
        StartOffset();
    }
    void StartOffset()
    {
        if (Player)
            offset = mainCamera.position - Player.transform.position;
    }

    void LateUpdate()
    {
        if (Player == null) return;

        // transform.position = player.transform.position + offset;
        var playerVec = new Vector3(Player.transform.position.x, Player.transform.position.y + offset.y, offset.z);

        transform.position = Vector3.Lerp(transform.position, playerVec, speedMove * Time.deltaTime);

    }
}
