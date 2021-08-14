using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{   
    public string nextScene; // �ε��Ϸ��� ���� �̸�
    
    public Image progressBar;
    

    private void Start()
    {
        StartCoroutine(LoadAsynScene());
    }
    IEnumerator LoadAsynScene()
    {
        yield return null;
        AsyncOperation operation = SceneManager.LoadSceneAsync(nextScene);
        operation.allowSceneActivation = false;
        
        float timer = 0.0f;
        while (!operation.isDone) // �ε��� �Ϸ�ɶ����� �ݺ�
        {
            yield return null;
            timer += Time.deltaTime;
            if (operation.progress >= 0.9f)
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1.0f, timer);

                if (progressBar.fillAmount == 1.0f)
                {
                    operation.allowSceneActivation = true;
                }
            }
            else
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, operation.progress, timer);
                if (progressBar.fillAmount >= operation.progress)
                {
                    timer = 0.0f;
                }
            }
        }
    }
}
