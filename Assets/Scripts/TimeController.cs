using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public float gameTime = 60.0f;
    public float remaining = 0; // �c�莞��
    public bool isTimeOver; // �J�E���g�_�E����~�t���O
    float elapsed = 0; // �o�ߎ���

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

        // �o�ߎ��Ԃ��X�V
        elapsed += Time.deltaTime; // ���O�̃t���[���Ƃ̎��ԍ�

        // �c�莞�Ԃ��v�Z
        remaining = gameTime - elapsed;


        if (remaining <= 0)
        {
            remaining = 0;
            isTimeOver = true;
        }

        // Debug.Log("�c�莞�ԁF" + remaining);
    }

}
