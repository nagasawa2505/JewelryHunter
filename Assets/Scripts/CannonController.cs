using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public float searchLength = 8.0f; // 索敵範囲
    GameObject player;
    public float delayTime = 3.0f; // 発射インターバル
    float pastTime = 0; // 経過時間

    public GameObject objPrefab; // 生成するべきPrefabデータ
    public Transform gateTransform; // ゲートの位置情報
    public float fireSpeed = 4.0f; // 発射速度

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // 経過時間を蓄積
        pastTime += Time.deltaTime;

        // プレイヤーとの距離チェック
        if (IsTargetClose(player.transform.position))
        {
            // 発射インターバル時間経過してれば
            if (pastTime > delayTime)
            {
                pastTime = 0;

                // 砲弾の生成（対象物、位置、回転）
                GameObject obj = Instantiate(objPrefab, gateTransform.position, Quaternion.identity);
                Rigidbody2D rbody = obj.GetComponent<Rigidbody2D>();

                // 現在のキャノンの傾きを取得
                float angleZ = transform.localEulerAngles.z;
                float x = Mathf.Cos(angleZ * Mathf.Deg2Rad);
                float y = Mathf.Sin(angleZ * Mathf.Deg2Rad);
                Vector2 v = new Vector2(x, y) * fireSpeed;

                // Euler角（オイラー角） は、X軸・Y軸・Z軸それぞれの回転を「度数（°）」で表現する方法
                // 特定の回転状態で軸が重なり、自由に回転できなくなる現象。
                // これを避けるために、内部的には Quaternion（四元数）で回転を処理しています。

                // 砲弾自身のRigidbodyの力で砲弾を飛ばす
                rbody.AddForce(v, ForceMode2D.Impulse);
            }
        }
    }

    // 対象ポジションが自分と近ければ真を返す
    bool IsTargetClose(Vector2 targetPos)
    {
        float distance = Vector2.Distance(transform.position, targetPos);
        return distance <= searchLength;
    }
}
