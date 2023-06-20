using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    const float SPEED_JUMP = 5.0f;
    const float SPEED_MOVE = 3.0f;

    Rigidbody2D rb;
    bool leftPressed = false;
    bool rightPressed = false;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb != null)
        {
            float dist = SPEED_MOVE * Time.deltaTime;
            Vector2 pos = transform.position;
            // �����̵�
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                leftPressed = true;
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                leftPressed = false;
            }
            if (leftPressed)
            {
                pos.x -= dist;
            }
            // �����̵�
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                rightPressed = true;
            }
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                rightPressed = false;
            }
            if (rightPressed)
            {
                pos.x += dist;
            }
            transform.position = pos;

            // ����
            if ( //Input.GetKeyDown(KeyCode.UpArrow) ||
                 Input.GetMouseButtonDown(0))
            {
                Vector2 moveVelocity = rb.velocity;
                moveVelocity.y = SPEED_JUMP;
                rb.velocity = moveVelocity;
            }
            /*
            if (Input.GetAxis("Vertical") > 0.5f)
            {
                Vector2 moveVelocity = rb.velocity;
                moveVelocity.y = SPEED_JUMP;
                rb.velocity = moveVelocity;
            }
            */

            // �����̵� - ������ ��ư
            if (Input.GetMouseButtonDown(1))
            {
                Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = newPos;
                rb.velocity = Vector2.zero;
            }
        }
    }
}
