using System.Collections;
using UnityEngine;


public class Exhibit_Ingre_for_Play : MonoBehaviour
{
    private Data_Hub data_hub;

    private Vector3 pre_pos = new();

    private GameObject pre_ingre;
    private Transform parent;
    private GameObject[] ingre_list = new GameObject[8];
    // Use this for initialization
    void Start()
    {
        data_hub = GameObject.Find("Data_Hub").GetComponent<Data_Hub>();
        pre_ingre = Resources.Load<GameObject>("source/Prefabs/Custom_Object/ingre_prefab");
        parent = GameObject.Find("Room_Part_9").transform.Find("Ingre_Sect").transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Draw_ingre_for_exhibition(string potion)
    {
        pre_pos.x = 0f;
        // 중점 설정
        pre_pos.y = 8.3f;               // every ingre object have same y value;
        pre_pos.z = 160f;               // every ingre object have same z value;

        int ingre_cnt = 0;
        // ingre 1번부터 우~좌 로 원을 그리며 생김 y를 일단 동일하게 해봄
        if (data_hub.My_data.User_ingame_data.Ingredient.Card_1 > 0)
        {
            // 0, 8.3, 160
            pre_pos.x = 0f;

            ingre_list[ingre_cnt] = Instantiate(pre_ingre, pre_pos, Quaternion.identity);
            ingre_list[ingre_cnt].transform.SetParent(parent, false);
            ingre_list[ingre_cnt].gameObject.name = "card_1";
            ingre_list[ingre_cnt].GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("source/img/ingre/card_1"));
            ingre_list[ingre_cnt].AddComponent<Exhibit_Ingre_Selecter>();
        }

        ingre_cnt += 1;
        if (data_hub.My_data.User_ingame_data.Ingredient.Card_2 > 0)
        {
            // 1.5, 8.3, 160
            pre_pos.x += 1.5f;

            ingre_list[ingre_cnt] = Instantiate(pre_ingre, pre_pos, Quaternion.identity);
            ingre_list[ingre_cnt].transform.SetParent(parent, false);
            ingre_list[ingre_cnt].gameObject.name = "card_2";
            ingre_list[ingre_cnt].GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("source/img/ingre/card_2"));
            ingre_list[ingre_cnt].AddComponent<Exhibit_Ingre_Selecter>();
        }

        ingre_cnt += 1;
        if (data_hub.My_data.User_ingame_data.Ingredient.Card_3 > 0)
        {
            // 3, 8.3, 160
            pre_pos.x += 1.5f;

            ingre_list[ingre_cnt] = Instantiate(pre_ingre, pre_pos, Quaternion.identity);
            ingre_list[ingre_cnt].transform.SetParent(parent, false);
            ingre_list[ingre_cnt].gameObject.name = "card_3";
            ingre_list[ingre_cnt].GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("source/img/ingre/card_3"));
            ingre_list[ingre_cnt].AddComponent<Exhibit_Ingre_Selecter>();
        }

        ingre_cnt += 1;
        if (data_hub.My_data.User_ingame_data.Ingredient.Card_4 > 0)
        {
            // 4.5, 8.3, 160
            pre_pos.x += 1.5f;

            ingre_list[ingre_cnt] = Instantiate(pre_ingre, pre_pos, Quaternion.identity);
            ingre_list[ingre_cnt].transform.SetParent(parent, false);
            ingre_list[ingre_cnt].gameObject.name = "card_4";
            ingre_list[ingre_cnt].GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("source/img/ingre/card_4"));
            ingre_list[ingre_cnt].AddComponent<Exhibit_Ingre_Selecter>();
        }

        ingre_cnt += 1;
        if (data_hub.My_data.User_ingame_data.Ingredient.Card_5 > 0)
        {
            // 6, 8.3, 160
            pre_pos.x += 1.5f;

            ingre_list[ingre_cnt] = Instantiate(pre_ingre, pre_pos, Quaternion.identity);
            ingre_list[ingre_cnt].transform.SetParent(parent, false);
            ingre_list[ingre_cnt].gameObject.name = "card_5";
            ingre_list[ingre_cnt].GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("source/img/ingre/card_5"));
            ingre_list[ingre_cnt].AddComponent<Exhibit_Ingre_Selecter>();
        }

        ingre_cnt += 1;
        if (data_hub.My_data.User_ingame_data.Ingredient.Card_6 > 0)
        {
            // 7.5, 8.3, 160
            pre_pos.x += 1.5f;

            ingre_list[ingre_cnt] = Instantiate(pre_ingre, pre_pos, Quaternion.identity);
            ingre_list[ingre_cnt].transform.SetParent(parent, false);
            ingre_list[ingre_cnt].gameObject.name = "card_6";
            ingre_list[ingre_cnt].GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("source/img/ingre/card_6"));
            ingre_list[ingre_cnt].AddComponent<Exhibit_Ingre_Selecter>();
        }

        ingre_cnt += 1;
        if (data_hub.My_data.User_ingame_data.Ingredient.Card_7 > 0)
        {
            // 9, 8.3, 160
            pre_pos.x += 1.5f;

            ingre_list[ingre_cnt] = Instantiate(pre_ingre, pre_pos, Quaternion.identity);
            ingre_list[ingre_cnt].transform.SetParent(parent, false);
            ingre_list[ingre_cnt].gameObject.name = "card_7";
            ingre_list[ingre_cnt].GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("source/img/ingre/card_7"));
            ingre_list[ingre_cnt].AddComponent<Exhibit_Ingre_Selecter>();
        }

        ingre_cnt += 1;
        if (data_hub.My_data.User_ingame_data.Ingredient.Card_8 > 0)
        {
            // 10.5, 8.3, 160
            pre_pos.x += 1.5f;

            ingre_list[ingre_cnt] = Instantiate(pre_ingre, pre_pos, Quaternion.identity);
            ingre_list[ingre_cnt].transform.SetParent(parent, false);
            ingre_list[ingre_cnt].gameObject.name = "card_8";
            ingre_list[ingre_cnt].GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("source/img/ingre/card_8"));
            ingre_list[ingre_cnt].AddComponent<Exhibit_Ingre_Selecter>();
        }
    }

    public void Destroy_ingre_for_exhibition()
    {
        for(int i = 0; i < ingre_list.Length; i++)
        {
            if (ingre_list[i] != null)
            {
                Destroy(ingre_list[i]);
            }
        }
    }
}
