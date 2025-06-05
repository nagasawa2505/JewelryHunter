using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    public float moveX = 3.0f; // ���ړ�����
    public float moveY = 3.0f; // �c�ړ�����
    public float times = 3.0f; // ���b�����Ĉړ����邩
    public float wait = 1.0f; // �܂�Ԃ��̃C���^�[�o���i��~���ԁj
    float distance; // �J�n�n�_�ƈړ��\��n�_�̍�
    float secondsDistance; // 1�b������̈ړ��\�苗��
    float framesDistance; // ����1�t���[��������̈ړ�����
    float movePercentage = 0; // �ړI�n�܂ł̈ړ��i��

    bool isMovable = true; // ������OK���ǂ���
    Vector3 startPos; // �u���b�N�̏����ʒu
    Vector3 endPos; // �u���b�N�̈ړ���̗\��ʒu
    bool isReverse; // �������]�t���O

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        endPos = new Vector3(startPos.x + moveX, startPos.y + moveY, startPos.z);

        // �ړ����鋗�����擾
        distance = Vector2.Distance(startPos, endPos);

        // 1�b������̈ړ��\�苗�����v�Z
        secondsDistance = distance / times;
    }

    // Update is called once per frame
    void Update()
    {
        // ��~���Ԓ��Ȃ�I��
        if (!isMovable)
        {
            return;
        }

        // ���̃t���[���ňړ����鋗�����v�Z
        framesDistance = secondsDistance * Time.deltaTime;

        // �ړ��i�����X�V
        movePercentage += framesDistance / distance;

        // �ړ�������������
        if (!isReverse)
        {
            // �I�_�����ֈړ�
            transform.position = Vector2.Lerp(startPos, endPos, movePercentage);
        }
        else
        {
            // �n�_�����ֈړ�
            transform.position = Vector2.Lerp(endPos, startPos, movePercentage);
        }

        // �ړ�����������
        if (movePercentage >= 1)
        {
            // ��~
            isMovable = false;
            // �ړ������؂�ւ�
            isReverse = !isReverse;
            // �ړ��i����������
            movePercentage = 0.0f;
            // �҂��Ă���ړ��ĊJ
            Invoke("Move", wait);
        }
    }

    // �ړ��J�n������
    void Move()
    {
        isMovable = true;
    }

    // �����ƂԂ������甭�����郁�\�b�h
    void OnCollisionEnter2D(Collision2D coll)
    {
        // �v���C���[�ƐڐG�����炻�̐e�������i�u���b�N�j�ɂ���
        if (coll.gameObject.tag == "Player")
        {
            coll.transform.SetParent(transform);
        }
    }

    // �����Ɨ��ꂽ�Ƃ��ɔ������郁�\�b�h
    void OnCollisionExit2D(Collision2D coll)
    {
        // �u���b�N�ƃv���C���[�̐e�q�֌W����������
        if (coll.gameObject.tag == "Player")
        {
            coll.transform.SetParent(null);
        }
    }

    //�ړ��͈͕\��
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
        //�ړ���
        Gizmos.DrawLine(fromPos, new Vector2(fromPos.x + moveX, fromPos.y + moveY));
        //�X�v���C�g�̃T�C�Y
        Vector2 size = GetComponent<SpriteRenderer>().size;
        //�����ʒu
        Gizmos.DrawWireCube(fromPos, new Vector2(size.x, size.y));
        //�ړ��ʒu
        Vector2 toPos = new Vector3(fromPos.x + moveX, fromPos.y + moveY);
        Gizmos.DrawWireCube(toPos, new Vector2(size.x, size.y));
    }
}
