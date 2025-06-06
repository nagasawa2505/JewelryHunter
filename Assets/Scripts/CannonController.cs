using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public float searchLength = 8.0f; // ���G�͈�
    GameObject player;
    public float delayTime = 3.0f; // ���˃C���^�[�o��
    float pastTime = 0; // �o�ߎ���

    public GameObject objPrefab; // ��������ׂ�Prefab�f�[�^
    public Transform gateTransform; // �Q�[�g�̈ʒu���
    public float fireSpeed = 4.0f; // ���ˑ��x

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // �o�ߎ��Ԃ�~��
        pastTime += Time.deltaTime;

        // �v���C���[�Ƃ̋����`�F�b�N
        if (IsTargetClose(player.transform.position))
        {
            // ���˃C���^�[�o�����Ԍo�߂��Ă��
            if (pastTime > delayTime)
            {
                pastTime = 0;

                // �C�e�̐����i�Ώە��A�ʒu�A��]�j
                GameObject obj = Instantiate(objPrefab, gateTransform.position, Quaternion.identity);
                Rigidbody2D rbody = obj.GetComponent<Rigidbody2D>();

                // ���݂̃L���m���̌X�����擾
                float angleZ = transform.localEulerAngles.z;
                float x = Mathf.Cos(angleZ * Mathf.Deg2Rad);
                float y = Mathf.Sin(angleZ * Mathf.Deg2Rad);
                Vector2 v = new Vector2(x, y) * fireSpeed;

                // Euler�p�i�I�C���[�p�j �́AX���EY���EZ�����ꂼ��̉�]���u�x���i���j�v�ŕ\��������@
                // ����̉�]��ԂŎ����d�Ȃ�A���R�ɉ�]�ł��Ȃ��Ȃ錻�ہB
                // ���������邽�߂ɁA�����I�ɂ� Quaternion�i�l�����j�ŉ�]���������Ă��܂��B

                // �C�e���g��Rigidbody�̗͂ŖC�e���΂�
                rbody.AddForce(v, ForceMode2D.Impulse);
            }
        }
    }

    // �Ώۃ|�W�V�����������Ƌ߂���ΐ^��Ԃ�
    bool IsTargetClose(Vector2 targetPos)
    {
        float distance = Vector2.Distance(transform.position, targetPos);
        return distance <= searchLength;
    }
}
