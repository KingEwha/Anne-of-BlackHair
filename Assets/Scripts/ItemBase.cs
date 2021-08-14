using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    int LineNum;
    float posX;

    private void OnEnable() // ������Ʈ�� Ȱ��ȭ�Ǹ� ����
    {
        if (SpawnManager.MobStartNum == 0)
        {
            gameObject.SetActive(false); // SpawnManager ���� ���� Mob�� �����ϴ� �� ����

        }
        else
        {
            gameObject.SetActive(true);
        }
        #region position X
        LineNum = (int)Random.Range(0, 3);
        if(LineNum == 0)
        {
            posX = -1.4f;
        }
        if (LineNum == 1)
        {
            posX = 0;
        }
        else
        {
            posX = 1.4f;
        }
        #endregion
        transform.position = new Vector2(posX,8);
    }
    
    private void Update()
    {

        transform.Translate(Vector2.down * Time.deltaTime * GameManager.instance.gameSpeed * 12);
        if (transform.position.y < -8) // ȭ�� ������ Mob�� �̵��ϸ� �ش� Mob ��Ȱ��ȭ
        {
            gameObject.SetActive(false);
        }
    }
}
