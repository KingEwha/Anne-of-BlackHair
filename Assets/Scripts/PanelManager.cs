using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public GameObject gamePanel;
    public GameObject fadeSprite;

    void Start()
    {
        gamePanel.SetActive(false); // �г� �����
        fadeSprite.SetActive(false);
    }

}
