using System.Collections;
using UnityEngine;
using System;

public class Theory_Present_Obj_for_Play : MonoBehaviour
{
    private GameObject parent;

    private GameObject pre_ele;
    private GameObject pre_stamp;


    private GameObject[] ele_list = new GameObject[9];
    private GameObject[] stamp_list = new GameObject[11];

    private Data_Hub data_hub;
    // 위치
    private Vector3 pos = new();

    // Use this for initialization
    void Start()
    {
        data_hub = GameObject.Find("Data_Hub").GetComponent<Data_Hub>();
        parent = GameObject.Find("Room_Part_6").transform.Find("Play_Sect").gameObject;
        pre_ele = Resources.Load<GameObject>("source/Prefabs/Custom_Object/coin_prefab");
        pre_stamp = Resources.Load<GameObject>("source/Prefabs/Custom_Object/coin_prefab");
    }

    // Update is called once per frame
    void Update()
    {

    }

    // ele가 바닥에서 올라와야함 : 화면 앞으로
    public void Ele_open()
    {
        int[] used_num = new int[9];
        // theory_data를 체크해서 있는 번호를 골라내야함
        for (int i = 1; i <= data_hub.Theory_data.Count; i++)
        {
            if (data_hub.Theory_data[i].element != 0) {
                used_num[i] = data_hub.Theory_data[i].element;
                //Debug.Log(used_num[i]);
            }
                
        }
        // 선택이 되어있는 원소번호는 사용되어서는 안됨
        for (int i = 1; i < 9; i++)
        {
            // i 가 used_num에 들어있으면 발표된 재료이므로 발표 불가
            if (Array.IndexOf(used_num, i) > 0)
            {
                continue;
            }
            // pos x
            int num = i % 4;
            pos.x = num switch
            {
                1 => -30f,
                2 => -26f,
                3 => -22f,
                0 => -18f,
                _ => 0f,
            };
            pos.y = 0f;
            pos.z = 55.5f;
            string path = i switch
            {
                /*
                * 1 : rgbl010
                * 2 : rgbl101
                * 3 : rglb011
                * 4 : rglb100
                * 5 : rlgb001
                * 6 : rlgb110
                * 7 : rlglbl000
                * 8 : rlglbl111
                */
                1 => "source/img/ingre/rgbl010",
                2 => "source/img/ingre/rgbl101",
                3 => "source/img/ingre/rglb011",
                4 => "source/img/ingre/rglb100",
                5 => "source/img/ingre/rlgb001",
                6 => "source/img/ingre/rlgb110",
                7 => "source/img/ingre/rlglbl000",
                8 => "source/img/ingre/rlglbl111",
                _ => "",
            };

            ele_list[i] = Instantiate(pre_ele, pos, Quaternion.identity);
            ele_list[i].transform.SetParent(parent.transform, false);
            ele_list[i].gameObject.name = i.ToString();
            ele_list[i].gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>(path));

            // object selecter 붙이기
            ele_list[i].AddComponent<Theory_Ele_Selecter>();

            if (i >= 5)
                StartCoroutine(Appear_item(6.2f, ele_list[i].transform));
            else
                StartCoroutine(Appear_item(9.23f, ele_list[i].transform));
        }
    }

    IEnumerator Appear_item(float y, Transform obj)
    {
        float speed = 0.3f;
        Vector3 obj_y = obj.position;
        while (y > obj_y.y)
        {
            yield return new WaitForFixedUpdate();
            obj_y.y += speed;
            obj.position = obj_y;
        }
        obj_y.y = y;
        obj.position = obj_y;
        yield break;
    }

    // ele를 바닥으로 내려보낸 뒤 제거
    public void Destroy_ele()
    {
        for (int i = 1; i < 9; i++)
        {
            if (ele_list[i] != null)
                StartCoroutine(Disappear_item(ele_list[i]));
        }
    }

    IEnumerator Disappear_item(GameObject obj)
    {
        float speed = 0.3f;
        Vector3 obj_y = obj.transform.position;
        while (0 < obj_y.y)
        {
            yield return new WaitForFixedUpdate();
            obj_y.y -= speed;
            obj.transform.position = obj_y;
        }
        Destroy(obj);
        yield break;
    }


    // stamp가 올라옴
    public void Stamp_open()
    {
        pos.z = 55.5f;
        // 11개의 obj가 있는지 검사해서 각각 
        //point 5 - 1
        if (data_hub.My_data.User_ingame_data.Have_stamp.Point_5_1)
        {
            //pos.y = 9.23f;
            pos.x = -32.3f; 
            string path = data_hub.My_data.User_color switch
            {
                "red" => "source/img/stamp/stamp_red_5",
                "blue" => "source/img/stamp/stamp_blue_5",
                "black" => "source/img/stamp/stamp_black_5",
                "white" => "source/img/stamp/stamp_white_5",
                _ => "source/img/stamp/stamp_red_5",
            };

            stamp_list[0] = Instantiate(pre_stamp, pos, Quaternion.identity);
            stamp_list[0].transform.SetParent(parent.transform, false);
            stamp_list[0].gameObject.name = "1";
            stamp_list[0].GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>(path));

            stamp_list[0].AddComponent<Theory_Stamp_Selecter>();

            StartCoroutine(Appear_item(9.23f, stamp_list[0].transform));
        }

        // point 5 - 2
        if (data_hub.My_data.User_ingame_data.Have_stamp.Point_5_2)
        {
            //pos.y = 9.23f;
            pos.x = -29.3f;
            string path = data_hub.My_data.User_color switch
            {
                "red" => "source/img/stamp/stamp_red_5",
                "blue" => "source/img/stamp/stamp_blue_5",
                "black" => "source/img/stamp/stamp_black_5",
                "white" => "source/img/stamp/stamp_white_5",
                _ => "source/img/stamp/stamp_red_5",
            };

            stamp_list[1] = Instantiate(pre_stamp, pos, Quaternion.identity);
            stamp_list[1].transform.SetParent(parent.transform, false);
            stamp_list[1].gameObject.name = "2";
            stamp_list[1].GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>(path));
            stamp_list[1].AddComponent<Theory_Stamp_Selecter>();
            StartCoroutine(Appear_item(9.23f, stamp_list[1].transform));
        }

        // point 3 - 1
        if (data_hub.My_data.User_ingame_data.Have_stamp.Point_3_1)
        {
            //pos.y = 6.2f;
            pos.x = -32.3f;
            string path = data_hub.My_data.User_color switch
            {
                "red" => "source/img/stamp/stamp_red_3",
                "blue" => "source/img/stamp/stamp_blue_3",
                "black" => "source/img/stamp/stamp_black_3",
                "white" => "source/img/stamp/stamp_white_3",
                _ => "source/img/stamp/stamp_red_3",
            };

            stamp_list[2] = Instantiate(pre_stamp, pos, Quaternion.identity);
            stamp_list[2].transform.SetParent(parent.transform, false);
            stamp_list[2].gameObject.name = "3";
            stamp_list[2].GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>(path));
            stamp_list[2].AddComponent<Theory_Stamp_Selecter>();
            StartCoroutine(Appear_item(6.2f, stamp_list[2].transform));
        }

        // point 3 - 2
        if (data_hub.My_data.User_ingame_data.Have_stamp.Point_3_2)
        {
            //pos.y = 6.2f;
            pos.x = -29.3f;
            string path = data_hub.My_data.User_color switch
            {
                "red" => "source/img/stamp/stamp_red_3",
                "blue" => "source/img/stamp/stamp_blue_3",
                "black" => "source/img/stamp/stamp_black_3",
                "white" => "source/img/stamp/stamp_white_3",
                _ => "source/img/stamp/stamp_red_3",
            };

            stamp_list[3] = Instantiate(pre_stamp, pos, Quaternion.identity);
            stamp_list[3].transform.SetParent(parent.transform, false);
            stamp_list[3].gameObject.name = "4";
            stamp_list[3].GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>(path));
            stamp_list[3].AddComponent<Theory_Stamp_Selecter>();
            StartCoroutine(Appear_item(6.2f, stamp_list[3].transform));
        }

        // point 3 - 3
        if (data_hub.My_data.User_ingame_data.Have_stamp.Point_3_3)
        {
            //pos.y = 6.2f;
            pos.x = -26.3f;
            string path = data_hub.My_data.User_color switch
            {
                "red" => "source/img/stamp/stamp_red_3",
                "blue" => "source/img/stamp/stamp_blue_3",
                "black" => "source/img/stamp/stamp_black_3",
                "white" => "source/img/stamp/stamp_white_3",
                _ => "source/img/stamp/stamp_red_3",
            };

            stamp_list[4] = Instantiate(pre_stamp, pos, Quaternion.identity);
            stamp_list[4].transform.SetParent(parent.transform, false);
            stamp_list[4].gameObject.name = "5";
            stamp_list[4].GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>(path));
            stamp_list[4].AddComponent<Theory_Stamp_Selecter>();
            StartCoroutine(Appear_item(6.2f, stamp_list[4].transform));
        }

        // question_red_1
        if (data_hub.My_data.User_ingame_data.Have_stamp.Question_red_1)
        {
            //pos.y = 10.1f;
            pos.x = -21.5f;
            string path = data_hub.My_data.User_color switch
            {
                "red" => "source/img/stamp/stamp_red_red",
                "blue" => "source/img/stamp/stamp_blue_red",
                "black" => "source/img/stamp/stamp_black_red",
                "white" => "source/img/stamp/stamp_white_red",
                _ => "source/img/stamp/stamp_red_red",
            };

            stamp_list[5] = Instantiate(pre_stamp, pos, Quaternion.identity);
            stamp_list[5].transform.SetParent(parent.transform, false);
            stamp_list[5].gameObject.name = "6";
            stamp_list[5].GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>(path));
            stamp_list[5].AddComponent<Theory_Stamp_Selecter>();
            StartCoroutine(Appear_item(10.1f, stamp_list[5].transform));
        }

        // question_red_2
        if (data_hub.My_data.User_ingame_data.Have_stamp.Question_red_2)
        {
            //pos.y = 10.1f;
            pos.x = -18.5f;
            string path = data_hub.My_data.User_color switch
            {
                "red" => "source/img/stamp/stamp_red_red",
                "blue" => "source/img/stamp/stamp_blue_red",
                "black" => "source/img/stamp/stamp_black_red",
                "white" => "source/img/stamp/stamp_white_red",
                _ => "source/img/stamp/stamp_red_red",
            };

            stamp_list[6] = Instantiate(pre_stamp, pos, Quaternion.identity);
            stamp_list[6].transform.SetParent(parent.transform, false);
            stamp_list[6].gameObject.name = "7";
            stamp_list[6].GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>(path));
            stamp_list[6].AddComponent<Theory_Stamp_Selecter>();
            StartCoroutine(Appear_item(10.1f, stamp_list[6].transform));
        }

        // question_green_1
        if (data_hub.My_data.User_ingame_data.Have_stamp.Question_green_1)
        {
            //pos.y = 7.4f;
            pos.x = -21.5f;
            string path = data_hub.My_data.User_color switch
            {
                "red" => "source/img/stamp/stamp_red_green",
                "blue" => "source/img/stamp/stamp_blue_green",
                "black" => "source/img/stamp/stamp_black_green",
                "white" => "source/img/stamp/stamp_white_green",
                _ => "source/img/stamp/stamp_red_green",
            };

            stamp_list[7] = Instantiate(pre_stamp, pos, Quaternion.identity);
            stamp_list[7].transform.SetParent(parent.transform, false);
            stamp_list[7].gameObject.name = "8";
            stamp_list[7].GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>(path));
            stamp_list[7].AddComponent<Theory_Stamp_Selecter>();
            StartCoroutine(Appear_item(7.4f, stamp_list[7].transform));
        }

        // question_green_2
        if (data_hub.My_data.User_ingame_data.Have_stamp.Question_green_2)
        {
            //pos.y = 7.4f;
            pos.x = -18.5f;
            string path = data_hub.My_data.User_color switch
            {
                "red" => "source/img/stamp/stamp_red_green",
                "blue" => "source/img/stamp/stamp_blue_green",
                "black" => "source/img/stamp/stamp_black_green",
                "white" => "source/img/stamp/stamp_white_green",
                _ => "source/img/stamp/stamp_red_green",
            };

            stamp_list[8] = Instantiate(pre_stamp, pos, Quaternion.identity);
            stamp_list[8].transform.SetParent(parent.transform, false);
            stamp_list[8].gameObject.name = "9";
            stamp_list[8].GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>(path));
            stamp_list[8].AddComponent<Theory_Stamp_Selecter>();
            StartCoroutine(Appear_item(7.4f, stamp_list[8].transform));
        }

        // question_blue_1
        if (data_hub.My_data.User_ingame_data.Have_stamp.Question_blue_1)
        {
            //pos.y = 4.7f;
            pos.x = -21.5f;
            string path = data_hub.My_data.User_color switch
            {
                "red" => "source/img/stamp/stamp_red_blue",
                "blue" => "source/img/stamp/stamp_blue_blue",
                "black" => "source/img/stamp/stamp_black_blue",
                "white" => "source/img/stamp/stamp_white_blue",
                _ => "source/img/stamp/stamp_red_blue",
            };

            stamp_list[9] = Instantiate(pre_stamp, pos, Quaternion.identity);
            stamp_list[9].transform.SetParent(parent.transform, false);
            stamp_list[9].gameObject.name = "10";
            stamp_list[9].GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>(path));
            stamp_list[9].AddComponent<Theory_Stamp_Selecter>();
            StartCoroutine(Appear_item(4.7f, stamp_list[9].transform));
        }

        // question_blue_2
        if (data_hub.My_data.User_ingame_data.Have_stamp.Question_blue_2)
        {
            //pos.y = 4.7f;
            pos.x = -18.5f;
            string path = data_hub.My_data.User_color switch
            {
                "red" => "source/img/stamp/stamp_red_blue",
                "blue" => "source/img/stamp/stamp_blue_blue",
                "black" => "source/img/stamp/stamp_black_blue",
                "white" => "source/img/stamp/stamp_white_blue",
                _ => "source/img/stamp/stamp_red_blue",
            };

            stamp_list[10] = Instantiate(pre_stamp, pos, Quaternion.identity);
            stamp_list[10].transform.SetParent(parent.transform, false);
            stamp_list[10].gameObject.name = "11";
            stamp_list[10].GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>(path));
            stamp_list[10].AddComponent<Theory_Stamp_Selecter>();
            StartCoroutine(Appear_item(4.7f, stamp_list[10].transform));
        }


    }

    public void Destroy_stamp()
    {
        for (int i = 0; i < 11; i++)
        {
            if (stamp_list[i] != null)
                StartCoroutine(Disappear_item(stamp_list[i]));
        }
    }
    // stamp 선택이후 내려보내며 발표 종료 : 내려가는게 끝나면 destroy
}