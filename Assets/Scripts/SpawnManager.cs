using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] SideMobs;
    public GameObject[] Item;
    public static int MobStartNum = 0; // SpawnManager ���� ���� Mob�� �����ϴ� �� ����
    int MobCreateNum = 0;
    int ItemCreateTerm; // Mob�� ItemCreateTerm�� ������ ������ Item 1�� ����

    public float startNum_Create, finalNum_Create; // mob ���尣�� �ð� ����. startNum: �ּ� �ð� ����, finalNum: �ִ� �ð� ����
    private void Start()
    {
        StartCoroutine(CreateMob());
    }
    IEnumerator CreateMob()
    {
        ItemCreateTerm = Random.Range(3, 6);
        yield return new WaitForSeconds(3f); // �����ϰ� 3�� �ĺ��� Mob ����
        while (true)
        {
            if (MobCreateNum != ItemCreateTerm)
            {
                SideMobs[DeactiveMob()].SetActive(true); // ��Ȱ��ȭ�� Mob�� �߿��� 1���� Ȱ��ȭ
                yield return new WaitForSeconds(Random.Range(startNum_Create, finalNum_Create));
                MobCreateNum++;
            }
            else
            {
                Item[DeactiveItem()].SetActive(true); // ��Ȱ��ȭ�� Item�� �߿��� 1���� Ȱ��ȭ
                yield return new WaitForSeconds(Random.Range(0.4f, 0.7f)); // �������� ������ ��, 0.4��-0.7�� ������ �ٷ� Mob�� ����
                SideMobs[DeactiveMob()].SetActive(true);
                yield return new WaitForSeconds(Random.Range(startNum_Create, finalNum_Create));
                ItemCreateTerm = Random.Range(3, 6); // ���ο� ItemCreateTerm ���ϱ�
                MobCreateNum = 0;
            }
            if (MobStartNum == 0)
            {
                MobStartNum++;
            }
        }
    }


    int DeactiveMob() // ��Ȱ��ȭ�� Mob�߿��� �����ϴ� �Լ�
    {
        List<int> num = new List<int>();
        for(int i = 0; i < SideMobs.Length; i++)
        {
            if (!SideMobs[i].activeSelf) // ��Ȱ��ȭ�� Mob�� �ε����� List�� �߰�
            {
                num.Add(i);
            }
        }
        int x = 0;
        if (num.Count > 0)
        {
            x = num[Random.Range(0, num.Count)]; 
        }
        return x; // ��Ȱ��ȭ�� Mob�� �ε����� 1���� ��ȯ
    }
    int DeactiveItem() // ��Ȱ��ȭ�� ������ �߿��� �����ϴ� �Լ�
    {
        List<int> num = new List<int>();
        for (int i = 0; i < Item.Length; i++)
        {
            if (!Item[i].activeSelf) // ��Ȱ��ȭ�� ����ĳ������ �ε����� List�� �߰�
            {
                num.Add(i);
            }
        }
        int x = 0;
        if (num.Count > 0)
        {
            x = num[Random.Range(0, num.Count)];
        }
        return x; // ��Ȱ��ȭ�� ����ĳ������ �ε����� 1���� ��ȯ
    }

    GameObject CreateObj(GameObject obj, Transform parent)
    {
        GameObject copy = Instantiate(obj); // �Ű������� ���� ���� ������Ʈ�� ����
        copy.transform.SetParent(parent);
        copy.SetActive(false);
        return copy;
    }
}
