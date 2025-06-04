using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public float gameTime = 60.0f;
    public float remaining = 0; // 残り時間
    public bool isTimeOver; // カウントダウン停止フラグ
    float elapsed = 0; // 経過時間

    // Start is called before the first frame update
    void Start()
    {
        remaining = gameTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimeOver)
        {
            return;
        }

        // 経過時間を更新
        elapsed += Time.deltaTime; // 直前のフレームとの時間差

        // 残り時間を計算
        remaining = gameTime - elapsed;


        if (remaining <= 0)
        {
            remaining = 0;
            isTimeOver = true;
        }

        // Debug.Log("残り時間：" + remaining);
    }

}
