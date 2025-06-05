using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickBlock : MonoBehaviour
{
    // publicにするとUnityエディタ上の初期値が優先されるぽい
    public float length = 1f; // 落下検知距離
    public bool isDelete; // 落下後に削除するかどうか
    public GameObject deadObj;
    bool isFell; // 消滅開始フラグ
    float fadeTime = 0.5f;
    Rigidbody2D rbody;
    GameObject player;
    float distance;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();

        // 動かないようにする
        rbody.bodyType = RigidbodyType2D.Static;

        deadObj.SetActive(false);

        // isDelete = true;
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            // distance = Vector2.Distance(transform.position, player.transform.position);
            distance = Vector2.Distance(new Vector2(transform.position.x, 0), new Vector2(player.transform.position.x, 0));
Debug.Log("D:" + distance);
Debug.Log("L:" + length);
            if (distance <= length)
            {
                // 落とす
                if (rbody.bodyType == RigidbodyType2D.Static)
                {
                    rbody.bodyType = RigidbodyType2D.Dynamic;
                    deadObj.SetActive(true);
                }
            }

        }

        // 消滅開始
        if (isFell)
        {
            // 現状の色を保存
            Color col = GetComponent<SpriteRenderer>().color;

            // 透明にする
            fadeTime -= Time.deltaTime;
            col.a -= fadeTime;
            GetComponent<SpriteRenderer>().color = col;

            // 透明になったらこのオブジェクトをヒエラルキーから削除
            if (fadeTime <= 0.0f)
            {
                Destroy(gameObject);
            }
        }
    }

    // 何かとぶつかったら発動するメソッド
    // isTriggerじゃないやつ
    // OnTriggerEnter2Dと引数が違うことに注意
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (isDelete)
        {
            // 落下開始させる
            isFell = true;
        }
    }

    // 定義するとエディタ画面になんか描く
    // 罠とか作るときに便利
    void OnDrawGizmosSelected()
    {
        // ブロックを中心にlengthの半径の円を描く
        Gizmos.DrawWireSphere(transform.position, length);
    }
}
