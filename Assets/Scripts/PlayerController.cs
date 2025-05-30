using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float axisH; // 左右のキーの値を格納
    Rigidbody2D rbody; // Rigidbody2Dの情報を扱うための媒体
    public float speed = 3.0f; // 歩くスピード
    private bool isJump; // ジャンプ中がどうか
    private bool onGround; // 地面判定
    public LayerMask groundLayer; // 地面判定の対象のレイヤーが何かを決めておく
    public float jump = 9.0f; // ジャンプ力

    // Start is called before the first frame update
    void Start()
    {
        // Playerに付いているRigidbody2Dコンポーネントを
        // 変数rbodyに格納
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // 引数Horizontalの場合、水平方向のキーが何か押された場合
        // 左なら-1、右なら1、その他なら0を格納
        axisH = Input.GetAxisRaw("Horizontal");

        // もしaxisHが正の数なら右向き
        // 負の数なら左向き
        if (axisH > 0)
        {
            // this.gameObject.GetCommponent<Transform>().localState;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (axisH < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // もしもジャンプボタンが押されたら
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        // 地面にいるかどうかを判別
        onGround = Physics2D.CircleCast(
            transform.position, // Playerの基準点
            0.2f, // 円の半径
            Vector2.down, // 指定した点からどの方向にチェックを伸ばすか new Vector2(0, -1)
            0.0f, // 指定した点からどのくらいチェックの距離を伸ばすか
            groundLayer); // 指定したレイヤー

        // velocityに2軸の方向データを代入
        rbody.velocity = new Vector2(axisH * speed, rbody.velocity.y);

        // ジャンプ中フラグが立ったら
        if (isJump)
        {
            // 上に押し出す
            rbody.AddForce(new Vector2(0, jump), ForceMode2D.Impulse);
            isJump = false;
        }
    }

    public void Jump()
    {
        if (onGround)
        {
            isJump = true;
        }
    }
}
