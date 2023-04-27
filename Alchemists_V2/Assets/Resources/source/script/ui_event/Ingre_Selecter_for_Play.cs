using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alchemists_data;

public class Ingre_Selecter_for_Play : MonoBehaviour
{

    private GameObject pre_ingre_obj;
    private GameObject[] tmp = new GameObject[8];
    // Start is called before the first frame update
    void Start()
    {
        pre_ingre_obj = Resources.Load<GameObject>("source/Prefabs/Custom_Object/ingre_prefab");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    ///  행동 순서에서 재료를 고르기 위해 나타날 재료 오브젝트를 표기할 함수 
    /// </summary>
    /// <param name="board_num">오브젝트가 나타날 행동 번호</param>
    /// <param name="data">내 정보</param>
    public void Construct_ingre_list(int board_num, User_Data_Array data)
    {
        // 내 데이터에 맞는 자료가 박스 아래 y=3으로 생성된 이후
        // 생성된 데이터가 완료되는 대로 y값을 각 위치에 맞게 업데이트 -> 부드럽게

        Vector3 center_pos = new();
        Vector3 rot_y = new();
        Vector3 center_ori_pos = new();
        switch (board_num)
        {
            case 2:
                center_pos.x = 35f;
                center_pos.y = 3f;
                center_pos.z = 45.6f;
                rot_y.x = 0f;
                rot_y.y = 90f;
                rot_y.z = 0f;
                break;
            case 3:
                center_pos.x = 35f;
                center_pos.y = 3f;
                center_pos.z = 102.2f;
                rot_y.x = 0f;
                rot_y.y = 90f;
                rot_y.z = 0f;
                break;
            case 7:
                center_pos.x = -24f;
                center_pos.y = 3f;
                center_pos.z = 73.75f;
                rot_y.x = 0f;
                rot_y.y = -90f;
                rot_y.z = 0f;
                break;
            case 8:
                center_pos.x = -24f;
                center_pos.y = 3f;
                center_pos.z = 102f;
                rot_y.x = 0f;
                rot_y.y = -90f;
                rot_y.z = 0f;
                break;
        }
        center_ori_pos = center_pos;
        // data에 따라 포지션을 지정하고 오브젝트 생성
        int tmp_num = 0;
        if (data.User_ingame_data.Ingredient.Card_1 > 0)
        {
            center_pos.x -= 4;
            tmp[tmp_num] = Instantiate(pre_ingre_obj, center_pos, Quaternion.identity);
            tmp[tmp_num].name = board_num.ToString() + "_ingre_1";
            tmp[tmp_num].GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("source/img/ingre/card_1"));
            tmp[tmp_num].transform.SetParent(GameObject.Find("Room_Part_" + board_num).transform.Find("Play_Sect").transform, false);
            tmp[tmp_num].transform.rotation = Quaternion.Euler(rot_y);
            // 선택 가능하게 object_selecter 붙이기
            tmp[tmp_num].AddComponent<Object_Selecter>();
            StartCoroutine(up_item(6f, tmp[tmp_num].transform));
            tmp_num++;
        }
        // 중점 유지
        center_pos = center_ori_pos;
        if (data.User_ingame_data.Ingredient.Card_2 > 0)
        {
            center_pos.x -= 2 * Mathf.Sqrt(2); center_pos.z -= 2 * Mathf.Sqrt(2);
            tmp[tmp_num] = Instantiate(pre_ingre_obj, center_pos, Quaternion.identity);
            tmp[tmp_num].name = board_num.ToString() + "_ingre_2";
            tmp[tmp_num].GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("source/img/ingre/card_2"));
            tmp[tmp_num].transform.SetParent(GameObject.Find("Room_Part_" + board_num).transform.Find("Play_Sect").transform, false);
            tmp[tmp_num].transform.rotation = Quaternion.Euler(rot_y);
            // 선택 가능하게 object_selecter 붙이기
            tmp[tmp_num].AddComponent<Object_Selecter>();
            StartCoroutine(up_item(7f, tmp[tmp_num].transform));
            tmp_num++;
        }
        // 중점 유지
        center_pos = center_ori_pos;
        if (data.User_ingame_data.Ingredient.Card_3 > 0)
        {
            center_pos.z -= 4;
            tmp[tmp_num] = Instantiate(pre_ingre_obj, center_pos, Quaternion.identity);
            tmp[tmp_num].name = board_num.ToString() + "_ingre_3";
            tmp[tmp_num].GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("source/img/ingre/card_3"));
            tmp[tmp_num].transform.SetParent(GameObject.Find("Room_Part_" + board_num).transform.Find("Play_Sect").transform, false);
            tmp[tmp_num].transform.rotation = Quaternion.Euler(rot_y);
            // 선택 가능하게 object_selecter 붙이기
            tmp[tmp_num].AddComponent<Object_Selecter>();
            StartCoroutine(up_item(8f, tmp[tmp_num].transform));
            tmp_num++;
        }
        // 중점 유지
        center_pos = center_ori_pos;
        if (data.User_ingame_data.Ingredient.Card_4 > 0)
        {
            center_pos.x += 2 * Mathf.Sqrt(2); center_pos.z -= 2 * Mathf.Sqrt(2);
            tmp[tmp_num] = Instantiate(pre_ingre_obj, center_pos, Quaternion.identity);
            tmp[tmp_num].name = board_num.ToString() + "_ingre_4";
            tmp[tmp_num].GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("source/img/ingre/card_4"));
            tmp[tmp_num].transform.SetParent(GameObject.Find("Room_Part_" + board_num).transform.Find("Play_Sect").transform, false);
            tmp[tmp_num].transform.rotation = Quaternion.Euler(rot_y);
            // 선택 가능하게 object_selecter 붙이기
            tmp[tmp_num].AddComponent<Object_Selecter>();
            StartCoroutine(up_item(9f, tmp[tmp_num].transform));
            tmp_num++;
        }
        // 중점 유지
        center_pos = center_ori_pos;
        if (data.User_ingame_data.Ingredient.Card_5 > 0)
        {
            center_pos.x += 4;
            tmp[tmp_num] = Instantiate(pre_ingre_obj, center_pos, Quaternion.identity);
            tmp[tmp_num].name = board_num.ToString() + "_ingre_5";
            tmp[tmp_num].GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("source/img/ingre/card_5"));
            tmp[tmp_num].transform.SetParent(GameObject.Find("Room_Part_" + board_num).transform.Find("Play_Sect").transform, false);
            tmp[tmp_num].transform.rotation = Quaternion.Euler(rot_y);
            // 선택 가능하게 object_selecter 붙이기
            tmp[tmp_num].AddComponent<Object_Selecter>();
            StartCoroutine(up_item(10f, tmp[tmp_num].transform));
            tmp_num++;
        }
        // 중점 유지
        center_pos = center_ori_pos;
        if (data.User_ingame_data.Ingredient.Card_6 > 0)
        {
            center_pos.x += 2 * Mathf.Sqrt(2); center_pos.z += 2 * Mathf.Sqrt(2);
            tmp[tmp_num] = Instantiate(pre_ingre_obj, center_pos, Quaternion.identity);
            tmp[tmp_num].name = board_num.ToString() + "_ingre_6";
            tmp[tmp_num].GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("source/img/ingre/card_6"));
            tmp[tmp_num].transform.SetParent(GameObject.Find("Room_Part_" + board_num).transform.Find("Play_Sect").transform, false);
            tmp[tmp_num].transform.rotation = Quaternion.Euler(rot_y);
            // 선택 가능하게 object_selecter 붙이기
            tmp[tmp_num].AddComponent<Object_Selecter>();
            StartCoroutine(up_item(9f, tmp[tmp_num].transform));
            tmp_num++;
        }
        // 중점 유지
        center_pos = center_ori_pos;
        if (data.User_ingame_data.Ingredient.Card_7 > 0)
        {
            center_pos.z += 4;
            tmp[tmp_num] = Instantiate(pre_ingre_obj, center_pos, Quaternion.identity);
            tmp[tmp_num].name = board_num.ToString() + "_ingre_7";
            tmp[tmp_num].GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("source/img/ingre/card_7"));
            tmp[tmp_num].transform.SetParent(GameObject.Find("Room_Part_" + board_num).transform.Find("Play_Sect").transform, false);
            tmp[tmp_num].transform.rotation = Quaternion.Euler(rot_y);
            // 선택 가능하게 object_selecter 붙이기
            tmp[tmp_num].AddComponent<Object_Selecter>();
            StartCoroutine(up_item(8f, tmp[tmp_num].transform));
            tmp_num++;
        }
        // 중점 유지
        center_pos = center_ori_pos;
        if (data.User_ingame_data.Ingredient.Card_8 > 0)
        {
            center_pos.x -= 2 * Mathf.Sqrt(2); center_pos.z += 2 * Mathf.Sqrt(2);
            tmp[tmp_num] = Instantiate(pre_ingre_obj, center_pos, Quaternion.identity);
            tmp[tmp_num].name = board_num.ToString() + "_ingre_8";
            tmp[tmp_num].GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("source/img/ingre/card_8"));
            tmp[tmp_num].transform.SetParent(GameObject.Find("Room_Part_" + board_num).transform.Find("Play_Sect").transform, false);
            tmp[tmp_num].transform.rotation = Quaternion.Euler(rot_y);
            // 선택 가능하게 object_selecter 붙이기
            tmp[tmp_num].AddComponent<Object_Selecter>();
            StartCoroutine(up_item(7f, tmp[tmp_num].transform));
        }

    }

    // 행동 번호의 행동이 끝난 이후 선택하기 위한 오브젝트들을 지워둠
    public void Destory_ingre_obj()
    {
        for(int i = 0; i < 8; i++)
        {
            if (tmp[i] != null)
            {
                Destroy(tmp[i]);
            }
        }
    }

    IEnumerator up_item(float y, Transform obj)
    {
        float speed = 0.3f;
        Vector3 obj_y = obj.position;
        while(y > obj_y.y)
        {
            yield return new WaitForFixedUpdate();
            obj_y.y += speed;
            obj.position = obj_y;
        }
        obj_y.y = y;
        obj.position = obj_y;
        yield break;
    }
}
