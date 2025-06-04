using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static string gameState; // ゲームの状態管理役 静的変数
    public GameObject stageTitle; // ステージタイトルのUIオブジェクト
    public Sprite gameClearSprite;
    public Sprite gameOverSprite;
    public GameObject buttonPanel; // ボタンパネルのUIオブジェクト
    public GameObject restartButton;
    public GameObject nextButton;
    TimeController timeCnt;
    public TextMeshProUGUI timeText;

    // Start is called before the first frame update
    void Start()
    {
        // ゲーム開始と同時にゲームステータスを"playing"にする
        gameState = "playing";

        // 引数のメソッドを遅延実行させる
        Invoke("InactiveImage", 1.0f);

        // オブジェクトを非表示にする
        buttonPanel.SetActive(false);

        // TimeControllerコンポーネントの情報を取得
        timeCnt = GetComponent<TimeController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == "playing")
        {
            // 残り時間を取得
            int remaining = (int)timeCnt.remaining;

            // 残り時間の表示を更新
            timeText.text = remaining.ToString();

            // 時間経過でゲームオーバー
            if (timeCnt.remaining <= 0)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.GetComponentInParent<PlayerController>().GameOver();
            }
        }
        else if (gameState == "gameclear" || gameState == "gameover")
        {
            // ステージタイトル復活
            if (gameState == "gameclear")
            {
                stageTitle.GetComponent<Image>().sprite = gameClearSprite;
                restartButton.GetComponent<Button>().interactable = false;
            }
            else
            {
                stageTitle.GetComponent<Image>().sprite = gameOverSprite;
                nextButton.GetComponent<Button>().interactable = false;
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
