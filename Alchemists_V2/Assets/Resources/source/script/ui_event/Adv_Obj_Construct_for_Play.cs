using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alchemists_data;

public class Adv_Obj_Construct_for_Play : MonoBehaviour
{
    private GameObject pre_coin;
    private GameObject pre_potion;

    private GameObject[] coin_list = new GameObject[4];
    private GameObject[] coin_order_list = new GameObject[4];
    private GameObject[] selling_list = new GameObject[4];
    private GameObject[] potion_list = new GameObject[3];

    private GameObject parent;

    // 위치
    private Vector3 pos = new();
    // y 90도로 돌림
    private Vector3 rot = new(0, 90, 0);
    // Start is called before the first frame update
    void Start()
    {
        parent = GameObject.Find("Room_Part_3").transform.Find("Play_Sect").gameObject;
        pre_coin = Resources.Load<GameObject>("source/Prefabs/Custom_Object/coin_prefab");
        pre_potion = Resources.Load<GameObject>("source/Prefabs/Custom_Object/potion_prefab");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 코인 생성
    public void Discount_obj_show(User_Data_Array data)
    {
        pos.x = 35.23f;
        pos.y = 6.3f;

        // 0
        if (data.User_ingame_data.Discount_adventurer.Ad_0)
        {
            pos.z = 105.5f;

            //위치 각도 수정
            coin_list[0] = Instantiate(pre_coin, pos, Quaternion.identity);
            coin_list[0].transform.rotation = Quaternion.Euler(rot);
            // 부모 수정
            coin_list[0].transform.SetParent(parent.transform, false);
            // 이름 수정
            coin_list[0].gameObject.name = "dis_coin_0";
            // material 수정
            coin_list[0].GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("source/img/adventurer/ad_0"));

            // 오브젝트 하이라이트 및 선택 함수 추가
            coin_list[0].AddComponent<Coin_Selecter>();
        }
        // -1
        if (data.User_ingame_data.Discount_adventurer.Ad_1)
        {
            pos.z = 103.5f;

            //위치 각도 수정
            coin_list[1] = Instantiate(pre_coin, pos, Quaternion.identity);
            coin_list[1].transform.rotation = Quaternion.Euler(rot);
            // 부모 수정
            coin_list[1].transform.SetParent(parent.transform, false);
            // 이름 수정
            coin_list[1].gameObject.name = "dis_coin_1";
            // material 수정
            coin_list[1].GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("source/img/adventurer/ad_1"));
            coin_list[1].AddComponent<Coin_Selecter>();
        }
        //-2
        if (data.User_ingame_data.Discount_adventurer.Ad_2)
        {
            pos.z = 101.5f;

            //위치 각도 수정
            coin_list[2] = Instantiate(pre_coin, pos, Quaternion.identity);
            coin_list[2].transform.rotation = Quaternion.Euler(rot);
            // 부모 수정
            coin_list[2].transform.SetParent(parent.transform, false);
            // 이름 수정
            coin_list[2].gameObject.name = "dis_coin_2";
            // material 수정
            coin_list[2].GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("source/img/adventurer/ad_2"));
            coin_list[2].AddComponent<Coin_Selecter>();
        }
        //-3
        if (data.User_ingame_data.Discount_adventurer.Ad_3)
        {
            pos.z = 99.5f;

            //위치 각도 수정
            coin_list[3] = Instantiate(pre_coin, pos, Quaternion.identity);
            coin_list[3].transform.rotation = Quaternion.Euler(rot);
            // 부모 수정
            coin_list[3].transform.SetParent(parent.transform, false);
            // 이름 수정
            coin_list[3].gameObject.name = "dis_coin_3";
            // material 수정
            coin_list[3].GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("source/img/adventurer/ad_3"));
            coin_list[3].AddComponent<Coin_Selecter>();
        }
    }

    // 순서 코인 생성
    public void Dis_order_show(Dictionary<int, Adv_Dis_Confirm_Data> data)
    {
        pos.x = 37f;
        pos.y = 10.8f;
        pos.z = 93.6f;
        GameObject _show_sect = GameObject.Find("Room_Part_3").transform.Find("Coin_Order_Sect").gameObject;

        for(int i = 0; i < data.Count; i++)
        {
            Color my_color = data[i].color switch {
                "red"  => Color.red,
                "blue" => Color.blue,
                "black"=> Color.blue,
                "white"=> Color.white,
                _      => Color.gray
            };

            string path = data[i].dis_coin_num switch {
                0 => "source/img/adventurer/ad_0",
                1 => "source/img/adventurer/ad_1",
                2 => "source/img/adventurer/ad_2",
                3 => "source/img/adventurer/ad_3",
                _ => "source/img/adventurer/adventurer_back"
            };

            coin_order_list[i] = Instantiate(pre_coin, pos, Quaternion.identity);
            coin_order_list[i].transform.SetParent(_show_sect.transform, false);
            coin_order_list[i].transform.rotation = Quaternion.Euler(rot);
            coin_order_list[i].GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>(path));
            coin_order_list[i].GetComponent<Renderer>().material.SetFloat("_Outline", 0.2f);
            coin_order_list[i].GetComponent<Renderer>().material.SetColor("_OutlineColor",my_color);

            pos.y -= 2f;
        }
    }

    // 순서 코인 제거
    public void Dis_order_destroy()
    {
        for (int i = 0; i < coin_order_list.Length; i++)
        {
            if (coin_order_list[i] != null)
                Destroy(coin_order_list[i]);
        }
    }

    // 코인 제거
    public void Discount_obj_destroy()
    {
        for (int i = 0; i < coin_list.Length; i++)
        {
            if(coin_list[i] != null)
            {
                Destroy(coin_list[i]);
            }
        }
    }

    // 판매가 생성
    // 판매가는 모두가 동일하게 선택할 수 있으므로 언제나 4개가 생성됨
    public void Price_obj_show()
    {
        pos.x = 35.23f;
        pos.y = 6.3f;
        pos.z = 106f;

        // 4원
        string path = "source/img/adventurer/sell_4";
        selling_list[0] = Instantiate(pre_potion, pos, Quaternion.identity);
        selling_list[0].transform.rotation = Quaternion.Euler(rot);
        selling_list[0].transform.SetParent(parent.transform, false);
        selling_list[0].gameObject.name = "sell_potion_4";
        selling_list[0].GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>(path));
        selling_list[0].AddComponent<Price_Selecter>();

        // 3
        path = "source/img/adventurer/sell_3";
        pos.z = 103f;
        selling_list[1] = Instantiate(pre_potion, pos, Quaternion.identity);
        selling_list[1].transform.rotation = Quaternion.Euler(rot);
        selling_list[1].transform.SetParent(parent.transform, false);
        selling_list[1].gameObject.name = "sell_potion_3";
        selling_list[1].GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>(path));
        selling_list[1].AddComponent<Price_Selecter>();

        // 2
        path = "source/img/adventurer/sell_2";
        pos.z = 100f;
        selling_list[2] = Instantiate(pre_potion, pos, Quaternion.identity);
        selling_list[2].transform.rotation = Quaternion.Euler(rot);
        selling_list[2].transform.SetParent(parent.transform, false);
        selling_list[2].gameObject.name = "sell_potion_2";
        selling_list[2].GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>(path));
        selling_list[2].AddComponent<Price_Selecter>();

        // 1
        path = "source/img/adventurer/sell_1";
        pos.z = 97f;
        selling_list[3] = Instantiate(pre_potion, pos, Quaternion.identity);
        selling_list[3].transform.rotation = Quaternion.Euler(rot);
        selling_list[3].transform.SetParent(parent.transform, false);
        selling_list[3].gameObject.name = "sell_potion_1";
        selling_list[3].GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>(path));
        selling_list[3].AddComponent<Price_Selecter>();
    }

    public void Price_obj_Destroy()
    {
        for(int i = 0; i<4; i++)
        {
            if(selling_list[i] != null)
                Destroy(selling_list[i]);
        }
    }

    // 포션 생성
    public void Potion_obj_show(List<Dictionary<string, bool>> data, int adv_num)
    {
        pos.x = 36f;
        pos.y = 6.8f;

        Texture tex = Resources.Load<Texture>("source/img/potion/blank"); // err :: blank 

        // red 공통
        // 위치 & 각도
        pos.z = 105f;
        potion_list[0] = Instantiate(pre_potion, pos, Quaternion.identity);
        potion_list[0].transform.rotation = Quaternion.Euler(rot);
        potion_list[0].transform.SetParent(parent.transform, false);

        // 이름 , texture 세부
        if (data[adv_num]["red_1"])
        {
            potion_list[0].gameObject.name = "red_1";
            tex = Resources.Load<Texture>("source/img/potion/red_+");
        }

        else if (data[adv_num]["red_0"])
        {
            potion_list[0].gameObject.name = "red_0";
            tex = Resources.Load<Texture>("source/img/potion/red_-");
        }
        
        // texture 설정
        potion_list[0].GetComponent<Renderer>().material.SetTexture("_MainTex", tex);
        // 함수 달기
        potion_list[0].AddComponent<Potion_Selecter>();

        // green
        pos.z = 102f;
        potion_list[1] = Instantiate(pre_potion, pos, Quaternion.identity);
        potion_list[1].transform.rotation = Quaternion.Euler(rot);
        potion_list[1].transform.SetParent(parent.transform, false);

        // 이름 , texture 세부
        if (data[adv_num]["green_1"])
        {
            potion_list[1].gameObject.name = "green_1";
            tex = Resources.Load<Texture>("source/img/potion/green_+");
        }
        else if (data[adv_num]["green_0"])
        {
            potion_list[1].gameObject.name = "green_0";
            tex = Resources.Load<Texture>("source/img/potion/green_-");
        }
        // texture 설정
        potion_list[1].GetComponent<Renderer>().material.SetTexture("_MainTex", tex);
        // 함수 달기
        potion_list[1].AddComponent<Potion_Selecter>();

        // blue
        pos.z = 99f;
        potion_list[2] = Instantiate(pre_potion, pos, Quaternion.identity);
        potion_list[2].transform.rotation = Quaternion.Euler(rot);
        potion_list[2].transform.SetParent(parent.transform, false);

        if (data[adv_num]["blue_1"])
        {
            potion_list[2].gameObject.name = "blue_1";
            tex = Resources.Load<Texture>("source/img/potion/blue_+");
        }
        else if (data[adv_num]["blue_0"])
        {
            potion_list[2].gameObject.name = "blue_0";
            tex = Resources.Load<Texture>("source/img/potion/blue_-");
        }

        // texture 설정
        potion_list[2].GetComponent<Renderer>().material.SetTexture("_MainTex", tex);
        // 함수 달기
        potion_list[2].AddComponent<Potion_Selecter>();
    }

    // 포션 제거
    public void Potion_obj_destroy()
    {
        for (int i = 0; i < potion_list.Length; i++)
        {
            if (potion_list[i] != null)
            {
                Destroy(potion_list[i]);
            }
        }
    }
}
