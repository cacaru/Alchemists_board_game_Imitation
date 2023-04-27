using System.Collections;
using UnityEngine;
using Alchemists_data;
using UnityEngine.UI;

public class Result_Table_Checker : MonoBehaviour
{
    public static void Result_table_chekcing(Ingredient_Result data)
    {
        // 결과 테이블 작성
        // 0_0 ~ 7_7 까지 전부 돌아서 각 데이터가 있으면 작성하기... 
        // n_n 은 변경할 일이 없으므로 검사 필요 없음
        // n_m 에 데이터가 있다면 m_n에도 같은 데이터가 있음
        string path;

        // part 마다 반복
        // part_1
        for(int i = 1; i < 8; i++)
        {
            //Debug.Log("data_part_1 > " + i + " > " + data.part_1[i]);
            path = data.part_1[i] switch
            {
                "red_1" => "source/img/potion/red_icon_+",
                "red_0" => "source/img/potion/red_icon_-",
                "green_1" => "source/img/potion/green_icon_+",
                "green_0" => "source/img/potion/green_icon_-",
                "blue_1" => "source/img/potion/blue_icon_+",
                "blue_0" => "source/img/potion/blue_icon_-",
                "blank" => "source/img/potion/blank_icon",
                _ => "source/img/background/parchment_6",
            };
            //Debug.Log(path);
            GameObject.Find("GUI").transform.Find("Canvas").Find("Show_area").Find("Reasoning_table").Find("Result_Sect").Find("Table_area").Find("0_" + i.ToString()).GetComponent<Image>().sprite =
                Resources.Load<Sprite>(path);
            path = "";
        }

        //part_2
        for (int i = 0; i < 8; i++)
        {
            
            if (i == 1) continue;
            //Debug.Log("data_part_2 > " + i + " > " + data.part_2[i]);
            path = data.part_2[i] switch
            {
                "red_1" => "source/img/potion/red_icon_+",
                "red_0" => "source/img/potion/red_icon_-",
                "green_1" => "source/img/potion/green_icon_+",
                "green_0" => "source/img/potion/green_icon_-",
                "blue_1" => "source/img/potion/blue_icon_+",
                "blue_0" => "source/img/potion/blue_icon_-",
                "blank" => "source/img/potion/blank_icon",
                _ => "source/img/background/parchment_6",
            };
            //Debug.Log(path);
            GameObject.Find("GUI").transform.Find("Canvas").Find("Show_area").Find("Reasoning_table").Find("Result_Sect").Find("Table_area").Find("1_" + i.ToString()).GetComponent<Image>().sprite =
                Resources.Load<Sprite>(path);
            path = "";
        }

        //part_3
        for (int i = 0; i < 8; i++)
        {
            
            if (i == 2) continue;
            //Debug.Log("data_part_3 > " + i + " > " + data.part_3[i]);
            path = data.part_3[i] switch
            {
                "red_1" => "source/img/potion/red_icon_+",
                "red_0" => "source/img/potion/red_icon_-",
                "green_1" => "source/img/potion/green_icon_+",
                "green_0" => "source/img/potion/green_icon_-",
                "blue_1" => "source/img/potion/blue_icon_+",
                "blue_0" => "source/img/potion/blue_icon_-",
                "blank" => "source/img/potion/blank_icon",
                _ => "source/img/background/parchment_6",
            };
            //Debug.Log(path);
            GameObject.Find("GUI").transform.Find("Canvas").Find("Show_area").Find("Reasoning_table").Find("Result_Sect").Find("Table_area").Find("2_" + i.ToString()).GetComponent<Image>().sprite =
                Resources.Load<Sprite>(path);
            path = "";
        }

        //part_4
        for (int i = 0; i < 8; i++)
        {
            if (i == 3) continue;
            //Debug.Log("data_part_4 > " + i + " > " + data.part_4[i]);
            path = data.part_4[i] switch
            {
                "red_1" => "source/img/potion/red_icon_+",
                "red_0" => "source/img/potion/red_icon_-",
                "green_1" => "source/img/potion/green_icon_+",
                "green_0" => "source/img/potion/green_icon_-",
                "blue_1" => "source/img/potion/blue_icon_+",
                "blue_0" => "source/img/potion/blue_icon_-",
                "blank" => "source/img/potion/blank_icon",
                _ => "source/img/background/parchment_6",
            };
            //Debug.Log(path);
            GameObject.Find("GUI").transform.Find("Canvas").Find("Show_area").Find("Reasoning_table").Find("Result_Sect").Find("Table_area").Find("3_" + i.ToString()).GetComponent<Image>().sprite =
                Resources.Load<Sprite>(path);
            path = "";
        }

        //part_5
        for (int i = 0; i < 8; i++)
        {
            if (i == 4) continue;
            //Debug.Log("data_part_5 > " + i + " > " + data.part_5[i]);
            path = data.part_5[i] switch
            {
                "red_1" => "source/img/potion/red_icon_+",
                "red_0" => "source/img/potion/red_icon_-",
                "green_1" => "source/img/potion/green_icon_+",
                "green_0" => "source/img/potion/green_icon_-",
                "blue_1" => "source/img/potion/blue_icon_+",
                "blue_0" => "source/img/potion/blue_icon_-",
                "blank" => "source/img/potion/blank_icon",
                _ => "source/img/background/parchment_6",
            };
            //Debug.Log(path);
            GameObject.Find("GUI").transform.Find("Canvas").Find("Show_area").Find("Reasoning_table").Find("Result_Sect").Find("Table_area").Find("4_" + i.ToString()).GetComponent<Image>().sprite =
                Resources.Load<Sprite>(path);
            path = "";
        }

        //part_6
        for (int i = 0; i < 8; i++)
        {
            if (i == 5) continue;
            //Debug.Log("data_part_6 > " + i + " > " + data.part_6[i]);
            path = data.part_6[i] switch
            {
                "red_1" => "source/img/potion/red_icon_+",
                "red_0" => "source/img/potion/red_icon_-",
                "green_1" => "source/img/potion/green_icon_+",
                "green_0" => "source/img/potion/green_icon_-",
                "blue_1" => "source/img/potion/blue_icon_+",
                "blue_0" => "source/img/potion/blue_icon_-",
                "blank" => "source/img/potion/blank_icon",
                _ => "source/img/background/parchment_6",
            };
            //Debug.Log(path);
            GameObject.Find("GUI").transform.Find("Canvas").Find("Show_area").Find("Reasoning_table").Find("Result_Sect").Find("Table_area").Find("5_" + i.ToString()).GetComponent<Image>().sprite =
                Resources.Load<Sprite>(path);
            path = "";
        }

        //part_7
        for (int i = 0; i < 8; i++)
        {
            if (i == 6) continue;
            //Debug.Log("data_part_7 > " + i + " > " + data.part_7[i]);
            path = data.part_7[i] switch
            {
                "red_1" => "source/img/potion/red_icon_+",
                "red_0" => "source/img/potion/red_icon_-",
                "green_1" => "source/img/potion/green_icon_+",
                "green_0" => "source/img/potion/green_icon_-",
                "blue_1" => "source/img/potion/blue_icon_+",
                "blue_0" => "source/img/potion/blue_icon_-",
                "blank" => "source/img/potion/blank_icon",
                _ => "source/img/background/parchment_6",
            };
            //Debug.Log(path);
            GameObject.Find("GUI").transform.Find("Canvas").Find("Show_area").Find("Reasoning_table").Find("Result_Sect").Find("Table_area").Find("6_" + i.ToString()).GetComponent<Image>().sprite =
                Resources.Load<Sprite>(path);
            path = "";
        }

        //part_8
        for (int i = 0; i < 8; i++)
        {
            if (i == 7) continue;
            //Debug.Log("data_part_8 > " + i + " > " + data.part_8[i]);
            path = data.part_8[i] switch
            {
                "red_1" => "source/img/potion/red_icon_+",
                "red_0" => "source/img/potion/red_icon_-",
                "green_1" => "source/img/potion/green_icon_+",
                "green_0" => "source/img/potion/green_icon_-",
                "blue_1" => "source/img/potion/blue_icon_+",
                "blue_0" => "source/img/potion/blue_icon_-",
                "blank" => "source/img/potion/blank_icon",
                _ => "source/img/background/parchment_6",
            };
            //Debug.Log(path);
            GameObject.Find("GUI").transform.Find("Canvas").Find("Show_area").Find("Reasoning_table").Find("Result_Sect").Find("Table_area").Find("7_" + i.ToString()).GetComponent<Image>().sprite =
                Resources.Load<Sprite>(path);
            path = "";
        }

    }

}
