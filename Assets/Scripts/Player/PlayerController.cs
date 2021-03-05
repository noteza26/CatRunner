using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] PlayerMovement playerMovement;

    [SerializeField] Vector2 m_StartingTouch;
    [SerializeField] bool m_IsSwiping = false;

    private void Awake()
    {
        playerMovement = this.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (PlayerManager.instance.IsStop) return;

        Movement();
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
                        if (diff.y < 0)
                        {
                            Debug.Log("Slide");
                            Slide();
                        }
                        else
                        {
                            Debug.Log("Jump");

                            Jump();
                        }
                    }
                    else
                    {
                        if (diff.x < 0)
                        {
                            Debug.Log("To left");
                            ChangeLane(-1);
                        }
                        else
                        {
                            Debug.Log("To right");

                            ChangeLane(1);
                        }
                    }

                    m_IsSwiping = false;
                }
            }

            // Input check is AFTER the swip test, that way if TouchPhase.Ended happen a single frame after the Began Phase
            // a swipe can still be registered (otherwise, m_IsSwiping will be set to false and the test wouldn't happen for that began-Ended pair)
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
