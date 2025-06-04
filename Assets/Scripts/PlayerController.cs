using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float axisH; // 左右のキーの値を格納
    Rigidbody2D rbody; // Rigidbody2Dの情報を扱うための媒体
    Animator animator; // Animatorの情報を扱うための媒体
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
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.gameState != "playing")
        {
            return;
        }

        // 引数Horizontalの場合、水平方向のキーが何か押された場合
        // 左なら-1、右なら1、その他なら0を格納
        axisH = Input.GetAxisRaw("Horizontal");

        // もしaxisHが正の数なら右向き
        // 負の数なら左向き
        if (axisH > 0)
        {
            // this.gameObject.GetCommponent<Transform>().localState;
            transform.localScale = new Vector3(1, 1, 1);
            // 担当しているコントローラーのパラメーターを変える
            Run();
        }
        else if (axisH < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            // 担当しているコントローラーのパラメーターを変える
            Run();
        }
        else
        {
            // 担当しているコントローラーのパラメーターを変える
            animator.SetBool("run", false);
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
            0.1f, // 円の半径
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
            // 担当しているコントローラーのパラメーターを変える
            animator.SetTrigger("jump");
            isJump = false;
        }
    }

    // 何かとぶつかったら発動するメソッド
    // ぶつかった相手のCollider情報を引数Collisionに入れる
    // 相手にColliderが付いてないと意味が無い
    // 相手のColliderをisTriggerにする（相手を通り抜けられるようになる）
    public void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.tag;

        if (tag == "Goal") 
        {
            Goal();
        }
        else if (tag == "Dead")
        {
            GameOver();
        }
    }

    // すり抜けない場合はこっち
    // public void OnCollisionEnter2D(Collision2D collision)

    public void GameOver()
    {
        GameController.gameState = "gameover";
        animator.SetBool("gameOver", true);
        Stop();

        // プレイヤーを上に跳ね上げる
        rbody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);

        // 当たり判定をカット
        GetComponent<CapsuleCollider2D>().enabled = false;
    }

    void Run()
    {
        if (onGround)
        {
            animator.SetBool("run", true);
        }
    }

    void Jump()
    {
        if (onGround)
        {
            isJump = true;
        }
    }

    void Stop()
    {
        // 速度0にして止める
        rbody.velocity = new Vector2(0, 0);
    }

    void Goal()
    {
        GameController.gameState = "gameclear";
        animator.SetBool("gameClear", true);
        Stop();
    }
}
