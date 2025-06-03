using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static string gameState; // �Q�[���̏�ԊǗ��� �ÓI�ϐ�
    public GameObject stageTitle; // �X�e�[�W�^�C�g����UI�I�u�W�F�N�g
    public Sprite gameClearSprite;
    public Sprite gameOverSprite;
    public GameObject buttonPanel; // �{�^���p�l����UI�I�u�W�F�N�g

    // Start is called before the first frame update
    void Start()
    {
        // �Q�[���J�n�Ɠ����ɃQ�[���X�e�[�^�X��"playing"�ɂ���
        gameState = "playing";

        // �����̃��\�b�h��x�����s������
        Invoke("InactiveImage", 1.0f);

        // �I�u�W�F�N�g���\���ɂ���
        buttonPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == "gameclear" || gameState == "gameover")
        {
            // �X�e�[�W�^�C�g������
            if (gameState == "gameclear")
            {
                stageTitle.GetComponent<Image>().sprite = gameClearSprite;
            }
            else
            {
                stageTitle.GetComponent<Image>().sprite = gameOverSprite;
            }
            // �X�e�[�W�^�C�g���\��
            stageTitle.SetActive(true);

            // �{�^������
            buttonPanel.SetActive(true);
        }
    }

    // �X�e�[�W�^�C�g�����\���ɂ���
    void InactiveImage()
    {
        stageTitle.SetActive(false);
    }
}
