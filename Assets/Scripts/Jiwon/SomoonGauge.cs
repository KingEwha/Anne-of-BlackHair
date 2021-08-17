using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomoonGauge : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject Emergency;


    public Animator animator;
    public bool somoonContinue;
    public bool isEmergency;

    public float adultFirstTouchTime;
    public float childFirstTouchTime;
    float startTime;
    public float realTime;

    public float somoonGauge;
    public float adultTouch_Num;
    public float childTouch_Num;

    private void Awake()
    {
        animator = GameObject.Find("Player").GetComponent<Animator>();

        somoonContinue = true;
        isEmergency = false;
        Emergency.SetActive(false);

        startTime = Time.time;
        somoonGauge = 0;

    }

    void Update()
    {
        //��������ð� ������Ʈ
        realTime = Time.time - startTime;
        SomoonCtrl();
    }


    public void SomoonCtrl()
    {
        //Ʈ��Ŭ ������ �� somoonContinue false ����
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.RED") || animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.DEEP_RED"))
        {
            somoonContinue = false;
        }

        else
        {
            somoonContinue = true;

            if (somoonGauge >= 100.0f)
            {
                gameManager.GameOver();
            }

            else if (somoonGauge < 100.0f && somoonContinue == true)
            {
                somoonGauge = 10f * (adultTouch_Num + childTouch_Num * 2);

                if (adultTouch_Num != 0)
                {
                    somoonGauge += 0.1f * (realTime - adultFirstTouchTime);
                }

                if (childTouch_Num != 0)
                {
                    somoonGauge += 0.1f * (realTime - childFirstTouchTime);
                }

                if ((somoonGauge > 70 && somoonGauge < 100) && !isEmergency)
                {
                    OnEmergency();
                    Invoke("OffEmergency", 3.2f);
                }
                else if (somoonGauge < 70)
                    isEmergency = false;
            }

        }





    }

    private void OnEmergency()
    {

        Emergency.SetActive(true);
        isEmergency = true;
    }

    private void OffEmergency()
    {
        Emergency.SetActive(false);
    }

    public void LowerSomoon()
    {
        realTime *= 0.8f;
        adultFirstTouchTime *= 0.8f;
        childFirstTouchTime *= 0.8f;
        adultTouch_Num *= 0.8f;
        childTouch_Num *= 0.8f;

    }

}

