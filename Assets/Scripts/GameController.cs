using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static string gameState; // ゲームの状態管理役 静的変数
    public GameObject stageTitle; // ステージタイトルのUIオブジェクト
    public Sprite gameClearSprite;
    public Sprite gameOverSprite;
    public GameObject buttonPanel; // ボタンパネルのUIオブジェクト

    // Start is called before the first frame update
    void Start()
    {
        // ゲーム開始と同時にゲームステータスを"playing"にする
        gameState = "playing";

        // 引数のメソッドを遅延実行させる
        Invoke("InactiveImage", 1.0f);

        // オブジェクトを非表示にする
        buttonPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == "gameclear" || gameState == "gameover")
        {
            // ステージタイトル復活
            if (gameState == "gameclear")
            {
                stageTitle.GetComponent<Image>().sprite = gameClearSprite;
            }
            else
            {
                stageTitle.GetComponent<Image>().sprite = gameOverSprite;
            }
            // ステージタイトル表示
            stageTitle.SetActive(true);

            // ボタン復活
            buttonPanel.SetActive(true);
        }
    }

    // ステージタイトルを非表示にする
    void InactiveImage()
    {
        stageTitle.SetActive(false);
    }
}
