using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Animator anim;
    bool isAttack = false;
    bool isMoving = false;
    bool isLeft;
    bool isRight;
    Vector3 initScale;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        initScale = transform.localScale;
    }

    void ProcessAttack()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isAttack)
        {
            anim.Play("idle");
            anim.SetBool("Attacking", true);
            isAttack = true;
            isLeft = isRight = isMoving = false;
            Debug.Log("Attack");
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            anim.SetBool("Attacking", false);
            isAttack = false;
        }
    }

    void ProcessMove()
    {
        // 공격관련 처리를 먼저 한다.
        ProcessAttack();

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            isLeft = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            isLeft = false;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            isRight = true;
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            isRight = false;
        }

        if (isLeft || isRight)
        {
            // 움직이는 상태
            if (!isMoving)
            {
                // 움직이기 시작
                isMoving = true;
                anim.Play("walk");
                Debug.Log("Walk");
            }
        }
        else
        {
            // 움직이지 않는 상태
            if (isMoving)
            {
                // 움직이고 있다면 정지
                isMoving = false;
                anim.Play("idle");
                Debug.Log("Idle");
            }
        }
        if (isRight)
        {
            initScale.x = Mathf.Abs(initScale.x) * -1f;
            transform.localScale = initScale;
        }
        else if (isLeft)
        {
            initScale.x = Mathf.Abs(initScale.x);
            transform.localScale = initScale;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ProcessMove();
    }
}
