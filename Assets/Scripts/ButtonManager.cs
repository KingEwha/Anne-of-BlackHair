using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public GameObject gamePanel; // �г� �ν��Ͻ�
    public GameObject exitPanel;
    public GameObject fadeSprite; // �г� ������ ȭ��κ� ��ο����� �ϴ� ����

    public GameObject BackgroundMusic;
    AudioSource backmusic;
    public GameObject stepSound;
    AudioSource stepAudio;

    public Button audioButton;
    Image buttonImage;
    public Sprite SoundOnImage;
    public Sprite SoundOffImage;

    public void Stop() // �Ͻ����� ��ư ������ ��
    {
        gamePanel.SetActive(true);
        fadeSprite.SetActive(true);
        Time.timeScale = 0;

        stepAudio = stepSound.GetComponent<AudioSource>();
        if (MainMenu.AudioPlay)
        {
            stepAudio.Pause();
        }
    }
    public void Continue() // �ٽ� ���� ���� ��ư ������ ��
    {
        Time.timeScale = 1;
        gamePanel.SetActive(false);
        fadeSprite.SetActive(false);

        stepAudio = stepSound.GetComponent<AudioSource>();
        if (MainMenu.AudioPlay)
        {
            stepAudio.Play();
        }
    }
    public void Setting()
    {
        gamePanel.SetActive(true);
        fadeSprite.SetActive(true);
    }
    public void Home()
    {
        gamePanel.SetActive(false);
        exitPanel.SetActive(false);
        fadeSprite.SetActive(false);
    }

    public void BGM()
    {
        backmusic = BackgroundMusic.GetComponent<AudioSource>();
        buttonImage = audioButton.GetComponent<Image>();
        if (backmusic.isPlaying)
        {
            MainMenu.AudioPlay = false;
            buttonImage.sprite = SoundOffImage;
            backmusic.Pause();

        }
        else
        {
            MainMenu.AudioPlay = true;
            buttonImage.sprite = SoundOnImage;
            backmusic.Play();
        }
    }
}
