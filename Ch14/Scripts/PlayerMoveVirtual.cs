using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveVirtual : MonoBehaviour
{
    const float SPEED_JUMP = 5.0f;
    const float SPEED_MOVE = 3.0f;

    public FixedJoystick joystick;
    private Rigidbody2D rb;

    bool leftPressed = false;
    bool rightPressed = false;
    bool isMoving = false;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb != null)
        {
            float dist = SPEED_MOVE * Time.deltaTime;
            Vector2 pos = transform.position;
            if (joystick.Horizontal < -0.5f)
            {
                leftPressed = true;
                rightPressed = false;
            }
            else if (joystick.Horizontal > 0.5f)
            {
                rightPressed = true;
                leftPressed = false;
            }
            else
            {
                leftPressed = false;
                rightPressed = false;
            }

            bool bMoving = false;
            // 좌측이동
            if (leftPressed)
            {
                pos.x -= dist;
                transform.localScale = new Vector3(-2, 2, 2);
                bMoving = true;
            }
            // 우측이동
            if (rightPressed)
            {
                pos.x += dist;
                transform.localScale = new Vector3(2, 2, 2);
                bMoving = true;
            }
            transform.position = pos;

            // 점프
            if (joystick.Vertical > 0.5f)
            {
                Vector2 moveVelocity = rb.velocity;
                moveVelocity.y = SPEED_JUMP;
                rb.velocity = moveVelocity;
            }

            if (anim != null)
            {
                // 움직이기 시작할때 한번만 적용
                if (bMoving && !isMoving)
                {
                    anim.Play("Player");
                    isMoving = true;
                }
                // 멈추기 시작할때 한번만 적용
                if (!bMoving && isMoving)
                {
                    anim.Play("Idle");
                    isMoving = false;
                }
            }
        }
    }
}
