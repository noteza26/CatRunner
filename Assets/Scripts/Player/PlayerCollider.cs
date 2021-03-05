using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    [Header("Normal Collider")]
    [SerializeField] Vector3 normalCenter = new Vector3(0, 0, 0);
    [SerializeField] Vector3 normalSize = new Vector3(1, 1, 1);



    [Header("Slide Collider")]
    [SerializeField] Vector3 slideCenter;
    [SerializeField] Vector3 slideSize;


    [Header("Jump Collider")]

    [SerializeField] Vector3 jumpCenter;
    [SerializeField] Vector3 jumpSize;


    BoxCollider boxCollider;
    private void Start()
    {
        boxCollider = this.GetComponent<BoxCollider>();
        if (boxCollider == null)
            Debug.LogError("Cant find Box Collider");
        else
            ResetCollider();
    }

    internal void JumpCollider(bool isJump)
    {

        if (isJump)
        {
            boxCollider.center = jumpCenter;
            boxCollider.size = jumpSize;
        }
        else
        {
            ResetCollider();

        }
    }
    internal void SlideCollider(bool isSlide)
    {

        if (isSlide)
        {
            boxCollider.center = slideCenter;
            boxCollider.size = slideSize;
        }
        else
        {
            ResetCollider();
        }
    }

    private void ResetCollider()
    {
        boxCollider.center = normalCenter;
        boxCollider.size = normalSize;
    }

}
