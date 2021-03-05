using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public static PlayerAnimation instance;
    public float SpeedRun; Animator animator;
    void Start()
    {
        Init();
    }

    private void Init()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        if (animator == null)
        {
            animator = this.GetComponent<Animator>();
            if (animator == null)
                Debug.LogError("Cant find Animator");
        }
        else
            Destroy(this.gameObject);

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
