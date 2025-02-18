using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMain : MonoBehaviour
{
    public List<GameObject> bricks;
    public GameObject ball;
    public GameObject bar;
    public GameObject brickNode;
    public float barLimit;
    public float brickY;
    public int brickAcross;
    public float brickWidth;
    public float brickHeight;
    public Text resultText;
    public Text scoreText;
    public Text ballText;
    public Button restart;

    private Ball ballScript;
    private bool isPressed;
    private int totalBricks;
    private int score;
    private int balls;
    private bool paused;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        balls = 3;
        ResetStage();

        ballScript = ball.GetComponent<Ball>();
        ballScript.ballCallBack += OnBallEvent;
    }

    void ResetStage()
    {
        // 모든 벽돌을 지운다.
        foreach (Transform child in brickNode.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        paused = true;

        // ui 세팅
        restart.gameObject.SetActive(false);
        resultText.gameObject.SetActive(false);

        // 벽돌무리의 구현
        float y = brickY;
        float brickX = -(brickAcross - 1) * brickWidth / 2.0f;

        totalBricks = 0;
        for (int i = 0; i < bricks.Count; i++)
        {
            float x = brickX;
            for (int j = 0; j < brickAcross; j++)
            {
                GameObject block = Instantiate(bricks[i]);
                block.transform.position = new Vector3(x, y, 0);
                x += brickWidth;
                totalBricks++;
                block.transform.parent = brickNode.transform;
            }
            y += brickHeight;
        }

        RefreshBalls();
    }

    void RefreshBalls()
    {
        ballText.text = "Balls: " + balls;
    }

    void RefreshScore()
    {
        scoreText.text = "Score: " + score;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isPressed = true;
            if (paused && balls > 0)
            {
                paused = false;
                ballScript.StartMove();
            }
        }

        if (isPressed)
        {
            Vector3 worldPos = 
                Camera.main.ScreenToWorldPoint(Input.mousePosition);

            worldPos.x = Mathf.Clamp(worldPos.x, -barLimit, barLimit);

            Vector3 pos = bar.transform.position;
            bar.transform.position = 
                new Vector3((float)worldPos.x, (float)pos.y, 0);
            if (paused)
            {
                ballScript.ResetBall(bar.transform.position);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isPressed = false;
        }
    }

    public void OnBallEvent(int eventID)
    {
        if (eventID == Ball.EVENT_BRICKBREAK)
        {
            totalBricks--;

            // 스코어 갱신
            score += 10;
            RefreshScore();

            if (totalBricks == 0)
            {
                Debug.Log("Stage clear");
                ballScript.StopMove();

                ResetStage();
                ballScript.ResetBall(bar.transform.position);
            }
        }
        else if (eventID == Ball.EVENT_DEAD)
        {
            balls--;
            paused = true;
            RefreshBalls();

            if (balls <= 0)
            {
                resultText.gameObject.SetActive(true);
                restart.gameObject.SetActive(true);
                resultText.text = "Game Over";
                Debug.Log("Game Over");
                isPressed = false;
            }
            else
            {
                ballScript.ResetBall(bar.transform.position);
            }
        }
    }

    public void OnRestart()
    {
        score = 0;
        balls = 3;

        ResetStage();
        RefreshScore();
    }
}