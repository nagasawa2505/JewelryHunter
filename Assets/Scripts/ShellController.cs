using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class ShellController : MonoBehaviour
{
    public float deleteTime = 3.0f; // �폜�����܂ł̎���
    public bool isDelete; // �Ԃ�����������邩

    // Start is called before the first frame update
    void Start()
    {
        // ���ԍ��ŏ���
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
