using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu: MonoBehaviour
{
    public GameObject exitPanel;
    public GameObject settingPanel;
    public GameObject fadeSprite;

    public GameObject BackgroundMusic;
    AudioSource backmusic;
    public Button audioButton;
    Image buttonImage;
    public Sprite SoundOnImage;
    public Sprite SoundOffImage;

    public static bool AudioPlay = true; //  �����(ȿ���� ����)�� ���������� true

    private void Awake()
    {
        SpawnManager.MobStartNum = 0;
        buttonImage = audioButton.GetComponent<Image>();
        if (AudioPlay == true) //  ������� ����������
        {
            buttonImage.sprite = SoundOnImage;
            backmusic = BackgroundMusic.GetComponent<AudioSource>();
            if (backmusic.isPlaying) return;
            else
            {
                backmusic.Play();
                //DontDestroyOnLoad(BackgroundMusic); // ������� ��� ���
            }
        }
        else // ������� ����������
        {
            buttonImage.sprite = SoundOffImage;
        }
    }
    private void Start()
    {
        exitPanel.SetActive(false); // �г� �����
        fadeSprite.SetActive(false);
        settingPanel.SetActive(false);
    }
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Escape)) // ����Ʈ�� �ڷΰ��� ��ư ������ ��
        {
            exitPanel.SetActive(true); // �г� ���̱�
            fadeSprite.SetActive(true);
        }
    }
}
