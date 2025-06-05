using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    public float moveX = 3.0f; // 横移動距離
    public float moveY = 3.0f; // 縦移動距離
    public float times = 3.0f; // 何秒かけて移動するか
    public float wait = 1.0f; // 折り返しのインターバル（停止時間）
    float distance; // 開始地点と移動予定地点の差
    float secondsDistance; // 1秒あたりの移動予定距離
    float framesDistance; // ある1フレームあたりの移動距離
    float movePercentage = 0; // 目的地までの移動進捗

    bool isMovable = true; // 動いてOKかどうか
    Vector3 startPos; // ブロックの初期位置
    Vector3 endPos; // ブロックの移動後の予定位置
    bool isReverse; // 方向反転フラグ

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        endPos = new Vector3(startPos.x + moveX, startPos.y + moveY, startPos.z);

        // 移動する距離を取得
        distance = Vector2.Distance(startPos, endPos);

        // 1秒あたりの移動予定距離を計算
        secondsDistance = distance / times;
    }

    // Update is called once per frame
    void Update()
    {
        // 停止時間中なら終了
        if (!isMovable)
        {
            return;
        }

        // このフレームで移動する距離を計算
        framesDistance = secondsDistance * Time.deltaTime;

        // 移動進捗を更新
        movePercentage += framesDistance / distance;

        // 移動方向が順方向
        if (!isReverse)
        {
            // 終点方向へ移動
            transform.position = Vector2.Lerp(startPos, endPos, movePercentage);
        }
        else
        {
            // 始点方向へ移動
            transform.position = Vector2.Lerp(endPos, startPos, movePercentage);
        }

        // 移動しきったら
        if (movePercentage >= 1)
        {
            // 停止
            isMovable = false;
            // 移動方向切り替え
            isReverse = !isReverse;
            // 移動進捗を初期化
            movePercentage = 0.0f;
            // 待ってから移動再開
            Invoke("Move", wait);
        }
    }

    // 移動開始させる
    void Move()
    {
        isMovable = true;
    }

    // 何かとぶつかったら発動するメソッド
    void OnCollisionEnter2D(Collision2D coll)
    {
        // プレイヤーと接触したらその親を自分（ブロック）にする
        if (coll.gameObject.tag == "Player")
        {
            coll.transform.SetParent(transform);
        }
    }

    // 何かと離れたときに発動するメソッド
    void OnCollisionExit2D(Collision2D coll)
    {
        // ブロックとプレイヤーの親子関係を解消する
        if (coll.gameObject.tag == "Player")
        {
            coll.transform.SetParent(null);
        }
    }

    //移動範囲表示
    void OnDrawGizmosSelected()
    {
        Vector2 fromPos;
        if (startPos == Vector3.zero)
        {
            fromPos = transform.position;
        }
        else
        {
            fromPos = startPos;
        }
        //移動線
        Gizmos.DrawLine(fromPos, new Vector2(fromPos.x + moveX, fromPos.y + moveY));
        //スプライトのサイズ
        Vector2 size = GetComponent<SpriteRenderer>().size;
        //初期位置
        Gizmos.DrawWireCube(fromPos, new Vector2(size.x, size.y));
        //移動位置
        Vector2 toPos = new Vector3(fromPos.x + moveX, fromPos.y + moveY);
        Gizmos.DrawWireCube(toPos, new Vector2(size.x, size.y));
    }
}
