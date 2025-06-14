using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class ShellController : MonoBehaviour
{
    public float deleteTime = 3.0f; // 削除されるまでの時間
    public bool isDelete; // ぶつかったら消えるか

    // Start is called before the first frame update
    void Start()
    {
        // 時間差で消滅
        Destroy(gameObject, deleteTime);

        isDelete = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDelete)
        {
            Destroy(gameObject);
        }
    }

    // Edit > Project Settings > Physics2D > layer Collision Matrix
}
