using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BerryController : MonoBehaviour
{
    public GameObject[] Berry_play;
    public GameObject player;
    public TextMeshProUGUI BerryText;
    int BerryNum;
    public static bool getBerryBox, getBerry, BumpOntheRoad, BumpWithCat= false;

    private void Start()
    {
        BerryNum = 3;

        for (int i = 0; i < Berry_play.Length; i++)
            Berry_play[i].SetActive(false);
        BerryText.gameObject.SetActive(false);
        getBerryBox = false;
        getBerry = false;
        BumpOntheRoad = false;
        RoadBase.jump = false;
        StartCoroutine(BerryStart());
        StartCoroutine(BerryChange());
    }
    private void Update()
    {
        if (getBerryBox)
        {
            if (BerryNum == 0)
            {
                for (int i = 0; i < Berry_play.Length; i++)
                    Berry_play[i].SetActive(false);

                BerryText.gameObject.SetActive(false);
                GameManager.instance.GameOver();
            }
            else if (BerryNum == 2)
            {
                Berry_play[1].SetActive(false);
                BerryText.gameObject.SetActive(false);
                Berry_play[2].SetActive(true);
            }
            else if (BerryNum == 1)
            {
                Berry_play[2].SetActive(false);
            }
            else
            {
                Berry_play[1].SetActive(true);
                BerryText.gameObject.SetActive(true);
                Berry_play[2].SetActive(false);
                BerryText.text = "<color=#000000>" + BerryNum.ToString() + "</color>";
            }
        }
    }
    // Update is called once per frame
    public IEnumerator BerryStart()
    {
        while (true)
        {
            if (getBerryBox)
            {
                for (int i = 0; i < 2; i++)
                    Berry_play[i].SetActive(true);
                BerryText.gameObject.SetActive(true);
                
                break;                
            }
            yield return null;
        }
        StopCoroutine(BerryStart());
    }
    public IEnumerator BerryChange()
    {
        while (true)
        {
            if (getBerry)
            {
                BerryNum++;
                yield return new WaitForSeconds(0.1f);
                getBerry = false;
            }
            if (BumpOntheRoad)
            {
                BerryNum--;
                Berry_play[3].transform.position = new Vector2(player.transform.position.x + 0.2f, -4.5f);
                if (!BumpWithCat)
                    StartCoroutine(BerryDrop());
                
                yield return new WaitForSeconds(0.1f);
                BumpOntheRoad = BumpWithCat = false;
            }
            yield return null;
        }
    }
    public IEnumerator BerryDrop()
    {
        Berry_play[3].SetActive(true);
        while (true)
        {
            if (Berry_play[3].transform.position.y > -5.5f)
            {
                Berry_play[3].transform.Translate(Vector2.down * Time.deltaTime * GameManager.instance.gameSpeed * 7);
            }
            else
            {
                Berry_play[3].SetActive(false);
                break;
            }
             yield return null;  
        }
        StopCoroutine(BerryDrop());
    }
}
