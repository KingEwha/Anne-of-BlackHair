using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideMob_Controller : MonoBehaviour
{
    public Animator[] mob_ani;
    public SpriteRenderer[] spriteRenderers;
    public GameObject[] mobs;
    public bool isSurprise;

    private void Awake()
    {
        isSurprise = false;
    }

    private void Update()
    {
        if (isSurprise==true)
        {
            StartCoroutine(Control_isSurprise());
        }
    }

    IEnumerator Control_isSurprise()
    {
        yield return new WaitForSeconds(2f);
        isSurprise = false;
    }

    //������ ���� ���� ���ۺ��� �ִϸ��̼� ON
    public void Set()
    {
        //for���� �ִϸ��̼� �ִ� ����ĳ���Ϳ��� ����
        for(int i = 0; i < mob_ani.Length; i++)
        {
            mob_ani[i].SetBool("isErase", true);
        }

        //�ִϸ��̼� ���� �ֵ��� �̹����� ��ü
        for(int i = 0; i < mobs.Length; i++)
        {
            spriteRenderers[i].sprite = mobs[i].GetComponent<Mob_Image>().sprites[2];
        }

        Invoke("UnSet", 2f);    //������ ���� ȿ�� 2�ʵ��� ����
    }

    //������ ���� ȿ�� ����
    public void UnSet()
    {
        for (int i = 0; i < mob_ani.Length; i++)
        {
            mob_ani[i].SetBool("isErase", false);
        }

        for (int i = 0; i < mobs.Length; i++)
        {
            spriteRenderers[i].sprite = mobs[i].GetComponent<Mob_Image>().sprites[0];
        }
    }
}
