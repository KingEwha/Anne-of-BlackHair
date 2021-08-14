using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeManager : MonoBehaviour
{
    public static bool tap, swipeLeft, swipeRight;
    private bool isDraging = false; // �巡���ϰ� ������ true
    private Vector2 startTouch; // tap�� ���۵� ��ǥ��
    private Vector2 swipeDelta; // ���������� �Ÿ�

    private void Update()
    {
        tap = swipeLeft = swipeRight = false;

        // Standalone Input ���: ��Ʈ�ѷ�/���콺 �Է¿� ���� �����ϵ��� �����.
        #region Standalone Inputs 
        if (Input.GetMouseButtonDown(0)) // ���� ���콺 ��ư�� �����ٸ� (0: ���콺 ���ʹ�ư)
        {
            tap = true;
            isDraging = true;
            startTouch = Input.mousePosition; // startTouch = Ŭ���� ���콺 ��ǥ��
        }
        else if (Input.GetMouseButtonUp(0)) // ���� ���콺 ��ư�� ������ �ٷ� �ôٸ� (0: ���콺 ���ʹ�ư)
        {
            isDraging = false;
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
                startTouch = Input.touches[0].position; // startTouch = ��ġ�� ��ǥ��
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)// ���� ��ġ�� �հ����� ��ũ������ ������ ��|| ��������� �Ϳ� ���� ��ų� touch tracking�� �������� �ʾƾ� �� ����
            {
                isDraging = false;
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
            float x = swipeDelta.x;

            if (x < 0)
                swipeLeft = true; // swipeDelta.x<0�̸� swipeLeft = true
            else
                swipeRight = true; // swipeDelta.x>0�̸� swipeRight = true
            Reset();
        }
     
    } 
    private void Reset()
        {
            startTouch = swipeDelta = Vector2.zero; // (0,0)���� �ʱ�ȭ
            isDraging = false;
        }
}
