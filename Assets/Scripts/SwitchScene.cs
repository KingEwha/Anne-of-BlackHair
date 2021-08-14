using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwitchScene : MonoBehaviour // �� ��ȯ�Լ��� ���ִ� class
{
    public void PlayGame()
    {
        SceneManager.LoadScene("mainGame");
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Title");
        Time.timeScale = 1;
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void LoadScene()
    {
        SceneManager.LoadScene("Loading");
    }
}
