using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeManager : MonoBehaviour
{
    public static bool tap, doubleTap, swipeLeft, swipeRight, swipeUp;
    private bool isDraging = false; // �巡���ϰ� ������ true
    private bool Move = false;
    private Vector2 startTouch; // tap�� ���۵� ��ǥ��
    private Vector2 swipeDelta; // ���������� �Ÿ�

    private float lastTouchTime;
    private const float doubleTouchDelay = 0.5f;

    public static int touchNum;

    private void Start()
    {
        lastTouchTime = Time.time;
        touchNum = 0;
    }
    private void Update()
    {
        doubleTap = tap = swipeLeft = swipeRight = swipeUp = false;

        // Standalone Input ���: ��Ʈ�ѷ�/���콺 �Է¿� ���� �����ϵ��� �����.
        #region Standalone Inputs 
        if (Input.GetMouseButtonDown(0) && GameManager.isPlay) // ���� ���콺 ��ư�� �����ٸ� (0: ���콺 ���ʹ�ư)
        {
            tap = true;
            isDraging = true;
            Move = false;
            if(doubleTap) 
            {
                doubleTap = false;
            } // ¦���� ���������� ������ �ν�
            startTouch = Input.mousePosition; // startTouch = Ŭ���� ���콺 ��ǥ��
        }
        else if (Input.GetMouseButtonUp(0) && GameManager.isPlay) // ���� ���콺 ��ư�� ������ �ٷ� �ôٸ� (0: ���콺 ���ʹ�ư)
        {
            isDraging = false;
            StartCoroutine("isDoubleTap");
            if (!doubleTap && !Move) 
            { 
                lastTouchTime = Time.time;
            }
            Reset();
        }
        #endregion

        #region Mobile Input
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began) // ���� ��ġ�� ���۵Ǿ�����
            {
                tap = true;
                isDraging = true;
                Move = false;
                startTouch = Input.touches[0].position; // startTouch = ��ġ�� ��ǥ��
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)// ���� ��ġ�� �հ����� ��ũ������ ������ ��|| ��������� �Ϳ� ���� ��ų� touch tracking�� �������� �ʾƾ� �� ����
            {
                if (GameManager.isPlay)
                {
                    touchNum++;
                }
                isDraging = false;
                if (touchNum == 2 && !Move)
                {
                    StartCoroutine("isDoubleTap");
                    lastTouchTime = Time.time;
                    touchNum = 0;
                }
                Reset();
            }
        }
        #endregion
        

        swipeDelta = Vector2.zero;
        if (isDraging) // ���� �巡���ϰ� �ִٸ�
        {
            if (Input.touches.Length > 0) // ������� ���
                swipeDelta = Input.touches[0].position - startTouch;
            else if (Input.GetMouseButton(0)) // ���콺 �Է��� ���
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
        }
        //Did we cross the distance?
        if (swipeDelta.magnitude > 125) // ���� ���������� �Ÿ��� ������ �̻��̸�
        {
            Move = true; // �������� �ߴ��� üũ
            float x = swipeDelta.x;
            float y = swipeDelta.y;
            if (Math.Abs(swipeDelta.y) > Math.Abs(swipeDelta.x)) // ������ ���� �ν�
            {
                swipeUp = true; // swipeDelta.y>0�̸� swipeUp = true
            }
            else // �� ������ �¿� ���������� �ν�
            {
                if (x < 0)
                    swipeLeft = true; // swipeDelta.x<0�̸� swipeLeft = true
                if (x >= 0)
                    swipeRight = true; // swipeDelta.x>0�̸� swipeRight = true
            }
            Reset();
        }
    } 
    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero; // (0,0)���� �ʱ�ȭ
        isDraging = false;
    }
    IEnumerator isDoubleTap()
    {
        if ((Time.time - lastTouchTime < doubleTouchDelay) && !Move)
        {
            doubleTap = true;
        }
        StopCoroutine("isDoubleTap");
        yield return null;

    }

}
