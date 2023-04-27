using System.Collections;
using UnityEngine;
using System;

public class Theory_Refute_Obj_for_Play : MonoBehaviour
{
    private GameObject parent;

    private GameObject pre_ele;
    private GameObject pre_ingre;


    private GameObject[] ele_list = new GameObject[3];
    private GameObject[] ingre_list = new GameObject[9];

    private Data_Hub data_hub;
    // 위치
    private Vector3 pos = new();


    // Use this for initialization
    void Start()
    {
        data_hub = GameObject.Find("Data_Hub").GetComponent<Data_Hub>();
        parent = GameObject.Find("Room_Part_5").transform.Find("Play_Sect").gameObject;
        pre_ele = Resources.Load<GameObject>("source/Prefabs/Custom_Object/coin_prefab");
        pre_ingre = Resources.Load<GameObject>("source/Prefabs/Custom_Object/ingre_prefab");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Ele_open()
    {
        pos.y = 0f;
        pos.z = 55f;
        // 3개의 원소 가 올라옴

        // red
        pos.x = -28f;
        ele_list[0] = Instantiate(pre_ele, pos, Quaternion.identity);
        ele_list[0].transform.SetParent(parent.transform, false);
        ele_list[0].gameObject.name = "1";
        ele_list[0].GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("source/img/potion/red_origin"));

        // object_selecter 붙이기
        ele_list[0].AddComponent<Theory_Origin_Ele_Selecter>();
        // 올리기
        StartCoroutine(Appear_item(7.5f, ele_list[0].transform));

        // green
        pos.x = -24f; 
        ele_list[1] = Instantiate(pre_ele, pos, Quaternion.identity);
        ele_list[1].transform.SetParent(parent.transform, false);
        ele_list[1].gameObject.name = "2";
        ele_list[1].GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("source/img/potion/green_origin"));

        // object_selecter 붙이기
        ele_list[1].AddComponent<Theory_Origin_Ele_Selecter>();
        // 올리기
        StartCoroutine(Appear_item(7.5f, ele_list[1].transform));

        // blue
        pos.x = -20f;
        ele_list[2] = Instantiate(pre_ele, pos, Quaternion.identity);
        ele_list[2].transform.SetParent(parent.transform, false);
        ele_list[2].gameObject.name = "3";
        ele_list[2].GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("source/img/potion/blue_origin"));

        // object_selecter 붙이기
        ele_list[2].AddComponent<Theory_Origin_Ele_Selecter>();
        // 올리기
        StartCoroutine(Appear_item(7.5f, ele_list[2].transform));
    }

    public void Ingre_open()
    {
        // 8개의 재료를 모두 올리면 됨
        for(int i = 1; i < 9; i++)
        {
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
            pos.z = 55f;
            string path = i switch
            {
                1 => "source/img/ingre/card_1",
                2 => "source/img/ingre/card_2",
                3 => "source/img/ingre/card_3",
                4 => "source/img/ingre/card_4",
                5 => "source/img/ingre/card_5",
                6 => "source/img/ingre/card_6",
                7 => "source/img/ingre/card_7",
                8 => "source/img/ingre/card_8",
                _ => "source/img/ingre/card_back",
            };

            ingre_list[i] = Instantiate(pre_ingre, pos, Quaternion.identity);
            ingre_list[i].transform.SetParent(parent.transform, false);
            ingre_list[i].gameObject.name = i.ToString();
            ingre_list[i].gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>(path));

            // object selecter 붙이기
            ingre_list[i].AddComponent<Theory_Ingre_Selecter>();

            if (i >= 5)
                StartCoroutine(Appear_item(6.2f, ingre_list[i].transform));
            else
                StartCoroutine(Appear_item(9.2f, ingre_list[i].transform));
        }
    }

    // ele 제거
    public void Destroy_ele()
    {
        for(int i = 0; i < ele_list.Length; i++)
        {
            StartCoroutine(Disappear_item(ele_list[i]));
        }
    }

    // ingre 제거
    public void Destroy_ingre()
    {
        for(int i = 1; i < ingre_list.Length; i++)
        {
            StartCoroutine(Disappear_item(ingre_list[i]));
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
}
