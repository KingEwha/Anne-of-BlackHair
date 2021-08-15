using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    List<GameObject> TreePool = new List<GameObject>();
    List<GameObject> HousePool = new List<GameObject>();
    List<GameObject> BackGround = new List<GameObject>();
    List<GameObject> BerryPool = new List<GameObject>();
    List<int> StreetLightNum = new List<int>();
    public Sun sun;
    public GameObject[] SideMobs;
    public GameObject[] Item;
    public GameObject[] Tree;
    public GameObject[] House;
    GameObject[][] Road = new GameObject[4][];
    public GameObject[] RoadOne, RoadTwo, RoadThree, RoadFour;
    public GameObject[] BerryBox;
    public GameObject[] BackgroundScrollImage;
    public static int MobStartNum = 0; // SpawnManager ���� ���� Mob�� �����ϴ� �� ����
    public static bool isforest = false;
    public int objCnt = 4;
    int x_Back, x_forest, x_Berry;

    public float startNum_Create, finalNum_Create; // mob ���尣�� �ð� ����. startNum: �ּ� �ð� ����, finalNum: �ִ� �ð� ����
    private void Awake()
    {
        Road[0] = RoadOne;
        Road[1] = RoadTwo;
        Road[2] = RoadThree;
        Road[3] = RoadFour;

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
        for (int i = 0; i < 5; i++)
            BerryPool.Add(CreateObj(BerryBox[3], transform));
    }
    private void Start() 
    {
        x_Back = x_forest = x_Berry = 0;
        StartCoroutine(CreateBack());
        StartCoroutine(CreateMob());
        StartCoroutine(CreateRoad());
        StartCoroutine(CreateItem());
        StartCoroutine(CreateColor());
    }
    private void Update()
    {
        if (isforest && x_forest==0)
        {
            StartCoroutine(BackgroundScroll());
        }
    }
    IEnumerator BackgroundScroll()
    {
        x_forest++;
        while (true)
        {
            if (GameManager.isPlay)
            {   
                BackgroundScrollImage[0].SetActive(true);
                while (isforest)
                {
                    yield return null;
                }
                BackgroundScrollImage[1].SetActive(true);
                break;   
            }
            yield return null;
        }
        StopCoroutine(BackgroundScroll());

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
                    isforest = true;
                    BackGround[x_Back].SetActive(true);
                    yield return new WaitForSeconds(0.2f);
                    x_Back++;
                    if (x_Back == TreePool.Count)
                    {
                        isforest = false;
                        yield return new WaitForSeconds(9.8f);
                    }
                }
                else
                {
                    isforest = false;
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
                        x_forest = 0;
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
        yield return new WaitForSeconds(2f); // �����ϰ� 3�� �ĺ��� Mob ����
        for (int i = 0; i < BerryBox.Length; i++)
        {
            BerryBox[i].SetActive(true);
        }
        yield return new WaitForSeconds(1f);
        while (true)
        {
            if (GameManager.isPlay && !isforest) 
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
                if (!isforest)
                {
                    x_Berry = 0;
                    Item[0].SetActive(true); // �������ָӴ� Ȱ��ȭ
                    yield return new WaitForSeconds(4); // �������� ������ ��, ���� ������ ���� �������� ����
                }
                else
                {
                    if (x_Berry < 5)
                    {
                        BerryPool[x_Berry].SetActive(true);
                        x_Berry++;
                        yield return new WaitForSeconds(0.3f);
                    }
                    else
                        yield return null;
                }
                
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
                yield return new WaitForSeconds(10);
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
                if (Sun.sunRise) // ��
                {
                    if (isforest) // ��
                        Road[0][DeactiveRoad_afternoon()].SetActive(true);
                    else // ����
                        Road[1][DeactiveRoad_afternoon()].SetActive(true);
                }
                else // ��
                { 
                    if(isforest) // ��
                        Road[2][DeactiveRoad_night()].SetActive(true); 
                    else // ����
                        Road[3][DeactiveRoad_night()].SetActive(true);
                }
                yield return new WaitForSeconds(1);
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
    
    int DeactiveRoad_afternoon() // ��Ȱ��ȭ�� ��ֹ��߿��� �����ϴ� �Լ�
    {
        List<int> num = new List<int>();
        if (isforest)
        {
            for (int i = 0; i < Road[0].Length; i++)
            {
                if (!Road[0][i].activeSelf)
                {
                    num.Add(i);
                }
            }
        }
        else
        {
            for (int i = 0; i < Road[1].Length; i++)
            {
                if (!Road[1][i].activeSelf)
                {
                    num.Add(i);
                }
            }
        }
        int x = 0;
        if(num.Count > 0)
        {
            x = num[Random.Range(0, num.Count)];
        }
        return x;
    }
    int DeactiveRoad_night() // ��Ȱ��ȭ�� ��ֹ��߿��� �����ϴ� �Լ�
    {
        List<int> num = new List<int>();
        if (isforest)
        {
            for (int i = 0; i < Road[2].Length; i++)
            {
                if (!Road[2][i].activeSelf)
                {
                    num.Add(i);
                }
            }
        }
        else
        {
            for (int i = 0; i < Road[3].Length; i++)
            {
                if (!Road[3][i].activeSelf)
                {
                    num.Add(i);
                }
            }
        }
        int x = 0;
        if (num.Count > 0)
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
