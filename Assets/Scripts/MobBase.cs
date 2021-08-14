using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBase : MonoBehaviour
{
    public Vector2 StartPosition;

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
        transform.position = StartPosition;
    }
    private void Update()
    {

        transform.Translate(Vector2.down * Time.deltaTime * GameManager.instance.gameSpeed*12);
        if (transform.position.y < -8) // ȭ�� ������ Mob�� �̵��ϸ� �ش� Mob ��Ȱ��ȭ
        {
            gameObject.SetActive(false);
        }
    }
}
