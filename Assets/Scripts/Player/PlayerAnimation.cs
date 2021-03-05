using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public static PlayerAnimation instance;
    public float SpeedRun;
    [SerializeField] Animator animator;
    void Start()
    {
        instance = this;
        animator = this.GetComponent<Animator>();

        animator.Play("Start");
    }
    public void StartRun()
    {
        Debug.Log("Set");
        animator.SetBool("IsRun", true);
        Invoke("ChangeIsStop", 0.1f);
    }
    public void Jumping()
    {
        animator.SetTrigger("IsJump");
    }
    public void JumpSpeed(float jumpSpeed)
    {
        animator.SetFloat("JumpSpeed", jumpSpeed);
    }
    public void JumpBool(bool isJump)
    {
        animator.SetBool("IsJump", isJump);
    }
    public void SlideSpeed(float slideSpeed)
    {
        animator.SetFloat("SlideSpeed", slideSpeed);
    }
    public void SlideBool(bool isSlide)
    {
        animator.SetBool("IsSlide", isSlide);
    }
    public void PlayerSlide()
    {
        var isSlide = animator.GetBool("IsSlide");

        animator.SetBool("IsSlide", !isSlide);

    }
    void ChangeIsStop()
    {
        PlayerManager.instance.IsStop = false;
    }
    public void SetGround(bool newGround)
    {
        animator.SetBool("IsGround", newGround);

    }
    public void SpeedRunAnim(float newSpeed)
    {
        if (newSpeed.ToString("F1") == SpeedRun.ToString("F1")) return;

        SpeedRun = newSpeed;

        animator.SetFloat("SpeedAnim", SpeedRun);


    }
}
