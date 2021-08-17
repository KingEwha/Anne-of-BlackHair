using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    public int type; // 0 = item_box , 1 = bleach , 2 = dye
    public int LineNum;
    float posX;

    GameObject _player;
    GameObject _twinkle;

    ItemController _item_controller;
    //AnimationController _animation_controller;

    void Start()
    {
        _player = GameObject.Find("Player");
        _twinkle = GameObject.Find("Twinkle");

        _item_controller = GameObject.Find("Item_Controller").GetComponent<ItemController>();
        //_animation_controller = GameObject.Find("Animation_Controller").GetComponent<AnimationController>();
    }

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
        #region position X
            LineNum = Random.Range(0, 3);
            if (LineNum == 0)
            {
                posX = -1.4f;
            }
            if (LineNum == 1)
            {
                posX = 0;
            }
            if (LineNum == 2)
            {
                posX = 1.4f;
            }
        #endregion
        transform.position = new Vector2(posX, 8);

    }

    private void Update()
    {
        if (GameManager.isPlay)
        {
            transform.Translate(Vector2.down * Time.deltaTime * GameManager.instance.gameSpeed * 12);
            
            if (transform.position.y < -8) // ȭ�� ������ Mob�� �̵��ϸ� �ش� Mob ��Ȱ��ȭ
            {
                gameObject.SetActive(false);
            }
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        Animator _player_animator = _player.GetComponent<Animator>();

        if (collision.tag == "Player")
        {
            gameObject.SetActive(false);
            if (this.type == 0)
            {
                // ann_get_item_box
                _item_controller._get_new_item_on_the_road();
            }
            else if (this.type == 1)
            {
                // ann_get_bleach
                if (_player_animator.GetInteger("State") <= 5 ) _player_animator.SetInteger("State",5);
            }
            else if (this.type == 2)
            {
                // ann_get_dye
                _player_animator.SetInteger("State",9);
                if (_player_animator.GetInteger("State") >= 5 ) _player_animator.SetBool("RED",true);
                // twinkle on
                _twinkle.GetComponent<Animator>().SetBool("T",true);
            }
        }
        else if (collision.tag == "Radar")
        {
            collision.gameObject.SetActive(false);
        }
        else if(collision.tag == "Item")
        {
            if (gameObject.tag != "Item")
                collision.gameObject.SetActive(false);
        }
    }
}
