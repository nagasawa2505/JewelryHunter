using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float axisH; // ���E�̃L�[�̒l���i�[
    Rigidbody2D rbody; // Rigidbody2D�̏����������߂̔}��
    public float speed = 3.0f; // �����X�s�[�h

    // Start is called before the first frame update
    void Start()
    {
        // Player�ɕt���Ă���Rigidbody2D�R���|�[�l���g��
        // �ϐ�rbody�Ɋi�[
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // ����Horizontal�̏ꍇ�A���������̃L�[�����������ꂽ�ꍇ
        // ���Ȃ�-1�A�E�Ȃ�1�A���̑��Ȃ�0���i�[
        axisH = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        // velocity��2���̕����f�[�^����
        rbody.velocity = new Vector2(axisH * speed, rbody.velocity.y);
    }
}
