using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickBlock : MonoBehaviour
{
    // public�ɂ����Unity�G�f�B�^��̏����l���D�悳���ۂ�
    public float length = 1f; // �������m����
    public bool isDelete; // ������ɍ폜���邩�ǂ���
    public GameObject deadObj;
    bool isFell; // ���ŊJ�n�t���O
    float fadeTime = 0.5f;
    Rigidbody2D rbody;
    GameObject player;
    float distance;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();

        // �����Ȃ��悤�ɂ���
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
                // ���Ƃ�
                if (rbody.bodyType == RigidbodyType2D.Static)
                {
                    rbody.bodyType = RigidbodyType2D.Dynamic;
                    deadObj.SetActive(true);
                }
            }

        }

        // ���ŊJ�n
        if (isFell)
        {
            // ����̐F��ۑ�
            Color col = GetComponent<SpriteRenderer>().color;

            // �����ɂ���
            fadeTime -= Time.deltaTime;
            col.a -= fadeTime;
            GetComponent<SpriteRenderer>().color = col;

            // �����ɂȂ����炱�̃I�u�W�F�N�g���q�G�����L�[����폜
            if (fadeTime <= 0.0f)
            {
                Destroy(gameObject);
            }
        }
    }

    // �����ƂԂ������甭�����郁�\�b�h
    // isTrigger����Ȃ����
    // OnTriggerEnter2D�ƈ������Ⴄ���Ƃɒ���
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (isDelete)
        {
            // �����J�n������
            isFell = true;
        }
    }

    // ��`����ƃG�f�B�^��ʂɂȂ񂩕`��
    // 㩂Ƃ����Ƃ��ɕ֗�
    void OnDrawGizmosSelected()
    {
        // �u���b�N�𒆐S��length�̔��a�̉~��`��
        Gizmos.DrawWireSphere(transform.position, length);
    }
}
