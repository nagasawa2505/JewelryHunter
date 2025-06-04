using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static string gameState; // �Q�[���̏�ԊǗ��� �ÓI�ϐ�
    public GameObject stageTitle; // �X�e�[�W�^�C�g����UI�I�u�W�F�N�g
    public Sprite gameClearSprite;
    public Sprite gameOverSprite;
    public GameObject buttonPanel; // �{�^���p�l����UI�I�u�W�F�N�g
    public GameObject restartButton;
    public GameObject nextButton;
    TimeController timeCnt;
    public TextMeshProUGUI timeText;

    // Start is called before the first frame update
    void Start()
    {
        // �Q�[���J�n�Ɠ����ɃQ�[���X�e�[�^�X��"playing"�ɂ���
        gameState = "playing";

        // �����̃��\�b�h��x�����s������
        Invoke("InactiveImage", 1.0f);

        // �I�u�W�F�N�g���\���ɂ���
        buttonPanel.SetActive(false);

        // TimeController�R���|�[�l���g�̏����擾
        timeCnt = GetComponent<TimeController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == "playing")
        {
            // �c�莞�Ԃ��擾
            int remaining = (int)timeCnt.remaining;

            // �c�莞�Ԃ̕\�����X�V
            timeText.text = remaining.ToString();

            // ���Ԍo�߂ŃQ�[���I�[�o�[
            if (timeCnt.remaining <= 0)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.GetComponentInParent<PlayerController>().GameOver();
            }
        }
        else if (gameState == "gameclear" || gameState == "gameover")
        {
            // �X�e�[�W�^�C�g������
            if (gameState == "gameclear")
            {
                stageTitle.GetComponent<Image>().sprite = gameClearSprite;
                restartButton.GetComponent<Button>().interactable = false;
            }
            else
            {
                stageTitle.GetComponent<Image>().sprite = gameOverSprite;
                nextButton.GetComponent<Button>().interactable = false;
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
