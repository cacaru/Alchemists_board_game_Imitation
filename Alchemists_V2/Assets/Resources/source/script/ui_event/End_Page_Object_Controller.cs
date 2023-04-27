using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alchemists_data;
using TMPro;
using UnityEngine.UI;

public class End_Page_Object_Controller : MonoBehaviour
{

    public GameObject winner_name;
    public GameObject winner_cube;
    public GameObject winner_score;

    public GameObject second_name;
    public GameObject second_cube;
    public GameObject second_score;

    public GameObject third;
    public GameObject third_name;
    public GameObject third_cube;
    public GameObject third_score;

    public GameObject fourth;
    public GameObject fourth_name;
    public GameObject fourth_cube;
    public GameObject fourth_score;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Draw_end_page(Dictionary<int,Game_Result_Data> data)
    {
        // 승자 
        winner_name.GetComponent<TMP_Text>().text = data[1].name;
        winner_cube.GetComponent<Image>().sprite = data[1].color switch
        {
            "red" => Resources.Load<Sprite>("source/img/icon/cube_red"),
            "blue" => Resources.Load<Sprite>("source/img/icon/cube_blue"),
            "black" => Resources.Load<Sprite>("source/img/icon/cube_black"),
            "white" => Resources.Load<Sprite>("source/img/icon/cube_white"),
            _ => Resources.Load<Sprite>("source/img/icon/cube_gray"),
        };
        winner_score.GetComponent<TMP_Text>().text = data[1].score.ToString() + "점";

        // 2위 까지는 반드시 존재
        second_name.GetComponent<TMP_Text>().text = data[2].name;
        second_cube.GetComponent<Image>().sprite = data[2].color switch
        {
            "red" => Resources.Load<Sprite>("source/img/icon/cube_red"),
            "blue" => Resources.Load<Sprite>("source/img/icon/cube_blue"),
            "black" => Resources.Load<Sprite>("source/img/icon/cube_black"),
            "white" => Resources.Load<Sprite>("source/img/icon/cube_white"),
            _ => Resources.Load<Sprite>("source/img/icon/cube_gray"),
        };
        second_score.GetComponent<TMP_Text>().text = data[2].score.ToString() + "점";


        // 3위부터 있는지 검사 후 표시
        if (data.Count > 2)
        {
            third.SetActive(true);
            third_name.GetComponent<TMP_Text>().text = data[3].name;
            third_cube.GetComponent<Image>().sprite = data[3].color switch
            {
                "red" => Resources.Load<Sprite>("source/img/icon/cube_red"),
                "blue" => Resources.Load<Sprite>("source/img/icon/cube_blue"),
                "black" => Resources.Load<Sprite>("source/img/icon/cube_black"),
                "white" => Resources.Load<Sprite>("source/img/icon/cube_white"),
                _ => Resources.Load<Sprite>("source/img/icon/cube_gray"),
            };
            third_score.GetComponent<TMP_Text>().text = data[3].score.ToString() + "점";
            
            if (data.Count > 3)
            {
                fourth.SetActive(true);
                fourth_name.GetComponent<TMP_Text>().text = data[4].name;
                fourth_cube.GetComponent<Image>().sprite = data[4].color switch
                {
                    "red" => Resources.Load<Sprite>("source/img/icon/cube_red"),
                    "blue" => Resources.Load<Sprite>("source/img/icon/cube_blue"),
                    "black" => Resources.Load<Sprite>("source/img/icon/cube_black"),
                    "white" => Resources.Load<Sprite>("source/img/icon/cube_white"),
                    _ => Resources.Load<Sprite>("source/img/icon/cube_gray"),
                };
                fourth_score.GetComponent<TMP_Text>().text = data[4].score.ToString() + "점";
            }
            else
            {
                fourth.SetActive(false);
            }
        }
        else
        {
            third.SetActive(false);
        }
    }
}
