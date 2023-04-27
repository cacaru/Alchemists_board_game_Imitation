using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Favor_Card_Big_Man_Selecter : MonoBehaviour
{
    private Data_Hub data_hub;
    private Favor_Card_Checker checker;

    private Vector3 ori_pos;
    private Vector3 shadow_pos;

    public bool selected = false;

    // Start is called before the first frame update
    void Start()
    {
        data_hub = GameObject.Find("Data_Hub").GetComponent<Data_Hub>();
        checker = GameObject.Find("Data_Controller").GetComponent<Favor_Card_Checker>();

        ori_pos = gameObject.transform.position;
        shadow_pos = ori_pos;
        shadow_pos.x -= 10f;
        shadow_pos.y += 10f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        // 현재 포지션의 x 좌표 -10, 7좌표 +10
        // shadow 키기
        gameObject.transform.position = shadow_pos;
        gameObject.GetComponent<Shadow>().enabled = true;
    }

    private void OnMouseExit()
    {
        gameObject.transform.position = ori_pos;
        gameObject.GetComponent<Shadow>().enabled = false;
        if (selected)
        {
            gameObject.GetComponent<Outline>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<Outline>().enabled = false;
        }
    }

    private void OnMouseUp()
    {
        data_hub.Select_board_num = int.Parse(gameObject.name[^1].ToString());
        checker.Board_checker(gameObject.name);
    }

    public void Init_pos()
    {
        gameObject.transform.position = ori_pos;
    }
}
