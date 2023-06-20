using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawFlag : MonoBehaviour
{
    const int DIAMETER = 20;
    const float DISTANCE = 1.5f;

    public List<GameObject> prefabObj;

    // Start is called before the first frame update
    void Start()
    {
        if (prefabObj.Count > 1)
        {
            float r = (DIAMETER * DISTANCE / 2.0f);
            float r2 = r * r;
            float small_r = r / 2.0f;
            float small_r2 = small_r * small_r;

            float y = -DIAMETER*DISTANCE / 2.0f + DISTANCE / 2.0f;
            for (int i=0; i<DIAMETER; i++)
            {
                float y2 = y * y;
                float x = -DIAMETER * DISTANCE / 2.0f + DISTANCE / 2.0f;
                for (int j=0; j<DIAMETER; j++)
                {
                    float x2 = x * x;

                    if (x2+y2 < r2)
                    {
                        // 바깥 원의 안쪽만 오브젝트 배치
                        float newX;
                        int newIdx;
                        if (x>0)
                        {
                            // 우측반원
                            newX = (x - small_r);
                            newIdx = 0;
                        }
                        else
                        {
                            // 좌측반원
                            newX = (x + small_r);
                            newIdx = 1;
                        }
                        float newX2 = newX * newX;

                        // 작은 원내에 들어갔는지 여부
                        bool isSmall = (newX2 + y2) < small_r2;

                        int idx = y > 0 ? 1 : 0;
                        if (isSmall)
                        {
                            idx = newIdx;
                        }
                        GameObject box = Instantiate(prefabObj[idx]);
                        box.transform.position = new Vector3(x, 5, y);
                    }
                    x += DISTANCE;
                }
                y += DISTANCE;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
