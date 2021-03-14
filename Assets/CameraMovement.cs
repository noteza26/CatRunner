using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public static CameraMovement instance;
    public Transform mainCamera;
    CameraFollow cameraFollow;

    Vector3 mainPosition;
    void Start()
    {
        instance = this;
        mainPosition = this.transform.position;

        cameraFollow = this.GetComponent<CameraFollow>();
        cameraFollow.enabled = false;
    }
    public void StartGame()
    {
        StartCoroutine("FadeCamera");
    }
    IEnumerator FadeCamera()
    {
        var dis = Vector3.Distance(this.transform.position, mainCamera.position);
        while (dis > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, mainCamera.position, 3f * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, mainCamera.rotation, 3f * Time.deltaTime);
            yield return null;
            cameraFollow.enabled = true;
            this.enabled = false;
        }


    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector3.Distance(this.transform.position, mainCamera.position) <= 0.01f)
        {

        }
        else
        {


        }

    }
}
