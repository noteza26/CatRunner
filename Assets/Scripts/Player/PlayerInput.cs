using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    PlayerMovement playerMovement;

    [SerializeField] Vector2 m_StartingTouch;
    [SerializeField] bool m_IsSwiping = false;

    private void Awake()
    {
        Init();
    }

    void Update()
    {
        if (PlayerManager.instance.IsStop) return;
        if (Input.GetKeyDown(KeyCode.Escape))

            PauseGame();

        Movement();
    }

    void Init()
    {
        playerMovement = this.GetComponent<PlayerMovement>();
        if (playerMovement == null)
            Debug.LogError("Cant Find Player");
    }
    void PauseGame()
    {
        PlayerManager.instance.PauseGame();
    }
    protected void Movement()
    {
        // Use touch input on mobile
        if (Input.touchCount == 1)
        {
            if (m_IsSwiping)
            {
                Vector2 diff = Input.GetTouch(0).position - m_StartingTouch;

                // Put difference in Screen ratio, but using only width, so the ratio is the same on both
                // axes (otherwise we would have to swipe more vertically...)
                diff = new Vector2(diff.x / Screen.width, diff.y / Screen.width);

                if (diff.magnitude > 0.01f) //we set the swip distance to trigger movement to 1% of the screen width
                {
                    if (Mathf.Abs(diff.y) > Mathf.Abs(diff.x))
                    {
                        if (diff.y < 0) //Slide
                            Slide();
                        else //Jump
                            Jump();
                    }
                    else
                    {
                        if (diff.x < 0) //left
                            ChangeLane(-1);
                        else //right
                            ChangeLane(1);

                    }

                    m_IsSwiping = false;
                }
            }
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                m_StartingTouch = Input.GetTouch(0).position;
                m_IsSwiping = true;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                m_IsSwiping = false;
            }
        }

    }

    void Jump()
    {
        playerMovement.Jump();
    }

    void Slide()
    {
        playerMovement.Slide();
    }

    void ChangeLane(int moveTo)
    {
        playerMovement.ChangeLane(moveTo);
    }

}
