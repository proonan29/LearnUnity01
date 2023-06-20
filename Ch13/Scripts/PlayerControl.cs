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
        // ���ݰ��� ó���� ���� �Ѵ�.
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
            // �����̴� ����
            if (!isMoving)
            {
                // �����̱� ����
                isMoving = true;
                anim.Play("walk");
                Debug.Log("Walk");
            }
        }
        else
        {
            // �������� �ʴ� ����
            if (isMoving)
            {
                // �����̰� �ִٸ� ����
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
