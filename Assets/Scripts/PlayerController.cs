using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float axisH; // ���E�̃L�[�̒l���i�[
    Rigidbody2D rbody; // Rigidbody2D�̏����������߂̔}��
    Animator animator; // Animator�̏����������߂̔}��
    public float speed = 3.0f; // �����X�s�[�h
    private bool isJump; // �W�����v�����ǂ���
    private bool onGround; // �n�ʔ���
    public LayerMask groundLayer; // �n�ʔ���̑Ώۂ̃��C���[�����������߂Ă���
    public float jump = 9.0f; // �W�����v��

    // Start is called before the first frame update
    void Start()
    {
        // Player�ɕt���Ă���Rigidbody2D�R���|�[�l���g��
        // �ϐ�rbody�Ɋi�[
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

        // ����Horizontal�̏ꍇ�A���������̃L�[�����������ꂽ�ꍇ
        // ���Ȃ�-1�A�E�Ȃ�1�A���̑��Ȃ�0���i�[
        axisH = Input.GetAxisRaw("Horizontal");

        // ����axisH�����̐��Ȃ�E����
        // ���̐��Ȃ獶����
        if (axisH > 0)
        {
            // this.gameObject.GetCommponent<Transform>().localState;
            transform.localScale = new Vector3(1, 1, 1);
            // �S�����Ă���R���g���[���[�̃p�����[�^�[��ς���
            Run();
        }
        else if (axisH < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            // �S�����Ă���R���g���[���[�̃p�����[�^�[��ς���
            Run();
        }
        else
        {
            // �S�����Ă���R���g���[���[�̃p�����[�^�[��ς���
            animator.SetBool("run", false);
        }

        // �������W�����v�{�^���������ꂽ��
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        // �n�ʂɂ��邩�ǂ����𔻕�
        onGround = Physics2D.CircleCast(
            transform.position, // Player�̊�_
            0.1f, // �~�̔��a
            Vector2.down, // �w�肵���_����ǂ̕����Ƀ`�F�b�N��L�΂��� new Vector2(0, -1)
            0.0f, // �w�肵���_����ǂ̂��炢�`�F�b�N�̋�����L�΂���
            groundLayer); // �w�肵�����C���[

        // velocity��2���̕����f�[�^����
        rbody.velocity = new Vector2(axisH * speed, rbody.velocity.y);

        // �W�����v���t���O����������
        if (isJump)
        {
            // ��ɉ����o��
            rbody.AddForce(new Vector2(0, jump), ForceMode2D.Impulse);
            // �S�����Ă���R���g���[���[�̃p�����[�^�[��ς���
            animator.SetTrigger("jump");
            isJump = false;
        }
    }

    // �����ƂԂ������甭�����郁�\�b�h
    // �Ԃ����������Collider��������Collision�ɓ����
    // �����Collider���t���ĂȂ��ƈӖ�������
    // �����Collider��isTrigger�ɂ���i�����ʂ蔲������悤�ɂȂ�j
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

    // ���蔲���Ȃ��ꍇ�͂�����
    // public void OnCollisionEnter2D(Collision2D collision)

    public void GameOver()
    {
        GameController.gameState = "gameover";
        animator.SetBool("gameOver", true);
        Stop();

        // �v���C���[����ɒ��ˏグ��
        rbody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);

        // �����蔻����J�b�g
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
        // ���x0�ɂ��Ď~�߂�
        rbody.velocity = new Vector2(0, 0);
    }

    void Goal()
    {
        GameController.gameState = "gameclear";
        animator.SetBool("gameClear", true);
        Stop();
    }
}
