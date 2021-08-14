using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameManager gameManager;
    public float AdultTime;     //����� ó�� ��Ų �ð�
    public float ChildTime;     //���̿��� ó�� ��Ų �ð�
    float startTime;        //���� ���� �ð��� ��Ÿ���� ���� �ʿ��� ���۽ð�
    public float RealTime;      //���� ���� �ð�

    public float SomoonGauge;    //�ҹ��������� �󸶳� á����
    public int AdultTouch;      //��� �ε��� Ƚ��
    public int ChildTouch;      //���̿� �ε��� Ƚ��

    public float maxSpeed;    //�÷��̾��� �ִ� ���ǵ�

    
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D capsuleCollider;
    Rigidbody2D rigid;

    void Awake()
    {
        startTime = Time.time;

        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {
        //��������ð� ������Ʈ
        RealTime = Time.time - startTime;

        //Stop speed
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }

        SomoonCtl();
    }

    void FixedUpdate()
    {
        //�̵�
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h * 2, ForceMode2D.Impulse);

        //Max Speed
        if (rigid.velocity.x > maxSpeed)
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < maxSpeed * (-1))
        {
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //f=0.55*(a+2b) + 0.1(t-(ó�� ����� �߰��� �ð�)) + 0.2(t-(ó�� ���̿��� �߰��� �ð�))
        if (collision.gameObject.tag == "Enemy")
        {
            //�ε��� �� ����� �������� ����
            bool isAdult = collision.gameObject.name.Contains("Adult");
            bool isChildren = collision.gameObject.name.Contains("Children");

            //��� ó�� �ε����� ���
            if (isAdult && AdultTouch == 0)
            {
                AdultTouch++;
                AdultTime = RealTime;  //ó�� �ε��� �ð� ����
                
            }

            //��� n ��° �ε��� ���
            else if (isAdult && AdultTouch != 0)
            {
                AdultTouch++;
                
            }

            //���̿� ó�� �ε����� ���
            else if (isChildren && ChildTouch == 0)
            {
                ChildTouch++;
                ChildTime = RealTime;
                
            }

            //���̿� n ��° �ε��� ���
            else if (isChildren && ChildTouch != 0)
            {
                ChildTouch++;
                
            }
        }
    }

    //�÷��̾� �״� �Լ�
    public void OnDie()
    {
        Debug.Log("�׾����ϴ�");
        //Sprite Flip Y
        spriteRenderer.flipY = true;
        //Collider Disable
        capsuleCollider.enabled = false;
        //Die Effect Jump
        rigid.AddForce(Vector2.down * 5, ForceMode2D.Impulse);
    }

    public void SomoonCtl()     //�ҹ������� ��Ʈ�� �Լ�
    {

        if (SomoonGauge >= 100.0f)  //�ҹ��������� 100�� �����ϸ� �÷��̾� ����
        {
            OnDie();
        }
        else
        {
            //��� �ٲ�
            SomoonGauge = 0.5f * (AdultTouch + ChildTouch * 2);
            
            if (AdultTouch != 0)
            {
                SomoonGauge += 0.5f * (RealTime - AdultTime);
                Debug.Log("��� �ҹ� �۶߷���");
            }
                

            if (ChildTouch != 0)
            {
                SomoonGauge += 0.8f * (RealTime - ChildTime);
                Debug.Log("���̰� �ҹ� �۶߷���");
            }
                

        }

        //�ҹ������� ���� �溸
        if (SomoonGauge > 70 && SomoonGauge < 72)
        {
            Debug.Log("�ҹ��������� 70�Դϴ�!");
        }
            
        else if (SomoonGauge > 80 && SomoonGauge < 82)
            Debug.Log("�ҹ��������� 80�Դϴ�!");
        else if (SomoonGauge > 90 && SomoonGauge < 92)
            Debug.Log("�ҹ��������� 90�Դϴ�!");
        /*
        else
        {
            gameManager.ReturnDisplay();
        }
        */

    }

    
}
