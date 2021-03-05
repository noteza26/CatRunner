using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    [Header("Lane")]
    [SerializeField] int laneNow;
    [SerializeField] float laneOffset;
    [Header("Jump")]
    [SerializeField] float jumpHeight = 1.2f;
    [SerializeField] float jumpLength = 2.0f;
    [Header("Slide")]
    [SerializeField] float slideLength = 2.0f;

    [Header("Normal Collider")]
    [SerializeField] Vector3 normalCollider = new Vector3(1, 1, 1);
    [SerializeField] Vector3 normalPositonCollider = new Vector3(0, 0, 0);


    [Header("Slide Collider")]
    [SerializeField] Vector3 slideCollider;
    [SerializeField] Vector3 slidePositonCollider;

    [Header("Jump Collider")]
    [SerializeField] Vector3 jumpCollider;
    [SerializeField] Vector3 jumpPositionCollider;

    Vector3 startPosition;

    Rigidbody rigid;
    bool isGrounded;
    bool onLerpMove;
    // bool onJump;
    bool onSlide;
    bool onJump;
    Vector3 m_TargetPosition = Vector3.zero;

    float m_JumpStart;
    float m_SlideStart;

    float k_GroundingSpeed = 80f;

    private void Start()
    {
        instance = this;
        startPosition = this.transform.position;
        rigid = this.GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (PlayerManager.instance.IsStop) return;
        CheckGround();
        PlayerInputMovement();
        CheckMove();
    }
    void CheckGround()
    {
        var playerVec = new Vector2(transform.position.x, transform.position.y + .5f);
        Debug.DrawRay(playerVec, Vector3.down, Color.blue, 1.25f);
        bool grounded = (Physics.Raycast(playerVec, Vector3.down, 1.25f, 1 << LayerMask.NameToLayer("Ground"))); // raycast down to look for ground is not detecting ground? only works if allowing jump when grounded = false; // return "Ground" layer as layer


        if (grounded != isGrounded)
        {
            isGrounded = grounded;
            PlayerAnimation.instance.SetGround(isGrounded);

            var boxcollider = this.GetComponent<BoxCollider>();

            /*   if (grounded)
               {

                   boxcollider.center = normalPositonCollider;
                   boxcollider.size = normalCollider;
               }
               else
               {

                   boxcollider.center = jumpPositionCollider;
                   boxcollider.size = jumpCollider;
               }*/

        }
    }
    void PlayerInputMovement()
    {

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangeLane(-1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeLane(1);

        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Slide();
        }
    }
    public void ChangeLane(int moveTo)
    {
        if (laneNow >= -1 && laneNow <= 1)
        {
            if (laneNow == -1 && moveTo == -1 || laneNow == 1 && moveTo == 1)
                return;

            laneNow += moveTo;
            var newCal = laneNow * laneOffset;
            m_TargetPosition = new Vector3(newCal, 0, 0);
            Debug.Log(newCal);
            Debug.Log(m_TargetPosition);

            // StartCoroutine(LerpMovement(laneOffset * moveTo));
        }

    }
    public void Slide()
    {
        // StartCoroutine(SlideMovement());
        if (onSlide) return;

        m_SlideStart = PlayerManager.instance.GetPlayerScore();

        float correctSlideLength = slideLength * (1.0f + PathManager.instance.speedRatio);
        float animSpeed = 0.6f * (PathManager.instance.nowSpeed / correctSlideLength);

        PlayerAnimation.instance.SlideSpeed(animSpeed);
        PlayerAnimation.instance.SlideBool(true);


        SlideCollider(true);
        onSlide = true;


    }
    public void Jump()
    {
        if (isGrounded)
        {
            if (onJump) return;

            if (onSlide)
                StopSliding();
            //  PlayerAnimation.instance.Jumping();

            m_JumpStart = PlayerManager.instance.GetPlayerScore();

            float correctJumpLength = jumpLength * (1.0f + PathManager.instance.speedRatio);
            float animSpeed = 0.6f * (PathManager.instance.nowSpeed / correctJumpLength);

            PlayerAnimation.instance.JumpSpeed(animSpeed);
            PlayerAnimation.instance.JumpBool(true);

            JumpCollider(true);
            onJump = true;
        }
    }
    public void CheckMove()
    {
        Vector3 verticalTargetPosition = m_TargetPosition;

        if (onSlide)
        {
            // Slide time isn't constant but the slide length is (even if slightly modified by speed, to slide slightly further when faster).
            // This is for gameplay reason, we don't want the character to drasticly slide farther when at max speed.
            float correctSlideLength = slideLength * (1.0f + PathManager.instance.speedRatio);
            float ratio = (PlayerManager.instance.GetPlayerScore() - m_SlideStart) / correctSlideLength;
            if (ratio >= 1.0f)
            {
                // We slid to (or past) the required length, go back to running
                StopSliding();
            }
        }

        if (onJump)
        {
            if (!PlayerManager.instance.IsStop)
            {
                float correctJumpLength = jumpLength * (1.0f + PathManager.instance.speedRatio);
                float ratio = (PlayerManager.instance.GetPlayerScore() - m_JumpStart) / correctJumpLength;
                if (ratio >= 1)
                {
                    onJump = false;
                    PlayerAnimation.instance.JumpBool(false);
                    JumpCollider(false);
                }
                else
                {
                    verticalTargetPosition.y = Mathf.Sin(ratio * Mathf.PI) * jumpHeight;
                }
            }
            else if (!AudioListener.pause)//use AudioListener.pause as it is an easily accessible singleton & it is set when the app is in pause too
            {
                verticalTargetPosition.y = Mathf.MoveTowards(verticalTargetPosition.y, 0, k_GroundingSpeed * Time.deltaTime);
                if (Mathf.Approximately(verticalTargetPosition.y, 0f))
                {
                    PlayerAnimation.instance.JumpBool(false);
                    onJump = false;
                }
            }
        }

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, verticalTargetPosition, 14f * Time.deltaTime);

    }
    public void StopSliding()
    {
        if (onSlide)
        {

            SlideCollider(false);
            PlayerAnimation.instance.SlideBool(false);
            onSlide = false;
        }
    }
    void JumpCollider(bool isJump)
    {
        var boxcollider = this.GetComponent<BoxCollider>();

        if (isJump)
        {
            boxcollider.center = jumpPositionCollider;
            boxcollider.size = jumpCollider;
        }
        else
        {
            boxcollider.center = normalPositonCollider;
            boxcollider.size = normalCollider;
        }
    }
    void SlideCollider(bool isSlide)
    {
        var boxcollider = this.GetComponent<BoxCollider>();

        if (isSlide)
        {
            boxcollider.center = slidePositonCollider;
            boxcollider.size = slideCollider;
        }
        else
        {
            boxcollider.center = normalPositonCollider;
            boxcollider.size = normalCollider;
        }
    }

}
