using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private bool round_order_select_end = false;
    private float speed = 0.03f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (round_order_select_end)
        {
            
            GameObject _target = GameObject.Find("Select_order_obj").transform.Find("Under_3").gameObject;
            float y = _target.transform.localPosition.y;
            // 15이상이 되면 update 종료
            if (y < -17f) round_order_select_end = false;

            // 아니라면 15가 될때까지 y값 증가
            Vector3 tmp_pos = _target.transform.localPosition;

            y -= speed;
            tmp_pos.y = y;
            _target.transform.localPosition = tmp_pos;
            
            /*
            bool wall_1 = false;
            bool wall_2 = false;
            bool wall_3 = false;
            
            GameObject _target = GameObject.Find("Paint_wall_1");
            Vector3 tmp_pos = _target.transform.localPosition;
            float x = _target.transform.localPosition.x;

            if (x < -25f) wall_1 = true;

            tmp_pos.x = x -= speed;
            _target.transform.localPosition = tmp_pos;

            _target = GameObject.Find("Paint_wall_2");
            tmp_pos = _target.transform.localPosition;
            float y = _target.transform.localPosition.y;

            if (y < -17f) wall_2 = true;

            tmp_pos.y = y -= speed;
            _target.transform.localPosition = tmp_pos;

            _target = GameObject.Find("Paint_wall_3");
            tmp_pos = _target.transform.localPosition;
            x = _target.transform.localPosition.x;

            if (x > 36f) wall_3 = true;

            tmp_pos.x = x += speed;
            _target.transform.localPosition = tmp_pos;


            if (wall_1 && wall_2 && wall_3) round_order_select_end = true;
            */
        }
    }

    public void test_btn()
    {
/*
        int num = GameObject.Find("Paint_wall_1").transform.childCount;
        for(int i = 0; i < num; i++)
        {
            GameObject.Find("Paint_wall_1").transform.GetChild(i).gameObject.SetActive(false);
        }
        num = GameObject.Find("Paint_wall_2").transform.childCount;
        for (int i = 0; i < num; i++)
        {
            GameObject.Find("Paint_wall_2").transform.GetChild(i).gameObject.SetActive(false);
        }
        num = GameObject.Find("Paint_wall_3").transform.childCount;
        for (int i = 0; i < num; i++)
        {
            GameObject.Find("Paint_wall_3").transform.GetChild(i).gameObject.SetActive(false);
        }

        */
        round_order_select_end = true;
    }
}
