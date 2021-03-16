using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public static CameraMovement instance;
    public Transform mainCamera;

    [SerializeField] float lerpPosition;
    [SerializeField]
    float lerpRotation;
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
        var isNear = false;
        while (!isNear)
        {
            var disNow = Vector3.Distance(transform.position, mainCamera.position);
            Debug.Log(disNow);
            if (disNow <= .1f)
            {
                isNear = false;
                break;
            }
            else
            {
                cameraFollow.enabled = true;
                this.enabled = false;

                transform.position = Vector3.Lerp(transform.position, mainCamera.position, lerpPosition * Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, mainCamera.rotation, lerpRotation * Time.deltaTime);
                yield return null;

            }

        }


    }
}
