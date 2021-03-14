using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform mainCamera;
    CameraFollow cameraFollow;

    Vector3 mainPosition;
    void Start()
    {
        mainPosition = this.transform.position;

        cameraFollow = this.GetComponent<CameraFollow>();
        cameraFollow.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector3.Distance(this.transform.position, mainCamera.position) <= 0.01f)
        {
            cameraFollow.enabled = true;
            this.enabled = false;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, mainCamera.position, 2 * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, mainCamera.rotation, 1.5f * Time.deltaTime);

        }

    }
}
