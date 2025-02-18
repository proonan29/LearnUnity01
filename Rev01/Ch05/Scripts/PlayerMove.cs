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
            // 좌측이동
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
            // 우측이동
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

            // 점프
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetMouseButtonDown(0) || Input.GetAxis("Vertical") > 0.5f)
            {
                Vector2 moveVelocity = rb.linearVelocity;
                moveVelocity.y = SPEED_JUMP;
                rb.linearVelocity = moveVelocity;
            }
        }
    }
}