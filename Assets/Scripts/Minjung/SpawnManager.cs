using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    List<GameObject> TreePool = new List<GameObject>();
    List<GameObject> HousePool = new List<GameObject>();
    List<GameObject> BackGround = new List<GameObject>();
    List<int> StreetLightNum = new List<int>();
    public GameObject[] SideMobs;
    public GameObject[] Item;
    public GameObject[] Tree;
    public GameObject[] House;
    public GameObject[] Road;
    public static int MobStartNum = 0; // SpawnManager ���� ���� Mob�� �����ϴ� �� ����
    public int objCnt = 4;
    int x_Back;

    public float startNum_Create, finalNum_Create; // mob ���尣�� �ð� ����. startNum: �ּ� �ð� ����, finalNum: �ִ� �ð� ����
    private void Awake()
    {
        for(int q = 0; q < objCnt; q++)
        {
            for(int i = 0; i < Tree.Length; i++)
            {
                TreePool.Add(CreateObj(Tree[i], transform));
            }
            for(int i=0; i < House.Length; i++)
            {
                HousePool.Add(CreateObj(House[i], transform));
            }
        }
        for(int i = 0; i < TreePool.Count; i++)
        {
            BackGround.Add(TreePool[i]);
        }
        for (int i = 0; i < HousePool.Count; i++)
        {
            BackGround.Add(HousePool[i]);
            if (HousePool[i].name.Contains("streetlight"))
            {
                StreetLightNum.Add(TreePool.Count + i);
            }
        }
    }
    private void Start()
    {
        x_Back = 0;
        StartCoroutine(CreateMob());
        StartCoroutine(CreateRoad());
        StartCoroutine(CreateBack());
        StartCoroutine(CreateItem());
        StartCoroutine(CreateColor());
    }
    IEnumerator CreateBack() // Ǯ,����,�� ����
    {
        yield return new WaitForSeconds(5f);
        while (true)
        {
            if (GameManager.isPlay)
            {
                if (x_Back <= TreePool.Count)
                {
                    BackGround[x_Back].SetActive(true);
                    yield return new WaitForSeconds(0.2f);
                    x_Back++;
                    if (x_Back == TreePool.Count)
                    {
                        yield return new WaitForSeconds(9.8f);
                    }
                }
                else
                {
                    if (StreetLightNum.Contains(x_Back))
                    {
                        BackGround[x_Back].SetActive(true);
                        BackGround[x_Back + 1].SetActive(true);
                        x_Back += 2;
                    }
                    else
                    {
                        BackGround[x_Back].SetActive(true);
                        x_Back++;
                    }
                    yield return new WaitForSeconds(0.7f);
                    
                    if (x_Back == BackGround.Count)
                    {
                        yield return new WaitForSeconds(9.3f);
                        x_Back = 0;
                    }
                }

            }
            else
            {
                yield return new WaitForSeconds(GameManager.instance.Count.Length);
            }
            yield return null;
        }
    }
    IEnumerator CreateMob() // ��������� ����
    {
        yield return new WaitForSeconds(3f); // �����ϰ� 3�� �ĺ��� Mob ����
        while (true)
        {
            if (GameManager.isPlay)
            {
                float time = Random.Range(startNum_Create, finalNum_Create); // ����������� �����ϴ� �ð� ����
                SideMobs[DeactiveMob()].SetActive(true); // ��Ȱ��ȭ�� Mob�� �߿��� 1���� Ȱ��ȭ
                yield return new WaitForSeconds(time);
                
                if (MobStartNum ==0)
                    MobStartNum++;
            }
            else
            {
                yield return new WaitForSeconds(GameManager.instance.Count.Length);
            }
            yield return null;
        }
    }
    IEnumerator CreateItem() // ������ �ָӴ�:  2�ʸ��� ����
    {
        while (true)
        {
            if (GameManager.isPlay)
            {
                Item[0].SetActive(true); // �������ָӴ� Ȱ��ȭ
                yield return new WaitForSeconds(2); // �������� ������ ��, ���� ������ ���� �������� ����
            }
            else
            {
                yield return new WaitForSeconds(GameManager.instance.Count.Length);
            }
            yield return null;
        }
    }
    IEnumerator CreateColor() // Ż���� -> ������ ������ ����
    {
        while (true)
        {
            if (GameManager.isPlay)
            {
                Item[1].SetActive(true); // Ż���� Ȱ��ȭ �Ŀ� ������ Ȱ��ȭ
                yield return new WaitForSeconds(1);
                Item[2].SetActive(true);
                yield return new WaitForSeconds(3);
            }
            else
            {
                yield return new WaitForSeconds(GameManager.instance.Count.Length);
            }
            yield return null;
        }
    }
    IEnumerator CreateRoad() // ��ֹ� ����
    {
        while (true)
        {
            if (GameManager.isPlay)
            {
                Road[DeactiveRoad()].SetActive(true);
                yield return new WaitForSeconds(Random.Range(1, 3));
            }
            else
            {
                yield return new WaitForSeconds(GameManager.instance.Count.Length);
            }
            yield return null;
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
    
    int DeactiveRoad() // ��Ȱ��ȭ�� ��ֹ��߿��� �����ϴ� �Լ�
    {
        List<int> num = new List<int>();
        for(int i=0; i< Road.Length; i++)
        {
            if (!Road[i].activeSelf)
            {
                num.Add(i);
            }
        }
        int x = 0;
        if(num.Count > 0)
        {
            x = num[Random.Range(0, num.Count)];
        }
        return x;
    }
    GameObject CreateObj(GameObject obj, Transform parent)
    {
        GameObject copy = Instantiate(obj);
        copy.transform.SetParent(parent);
        copy.SetActive(false);
        return copy;
    }
}
