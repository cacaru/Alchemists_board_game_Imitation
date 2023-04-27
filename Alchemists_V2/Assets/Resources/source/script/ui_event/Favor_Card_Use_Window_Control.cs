using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Favor_Card_Use_Window_Control : MonoBehaviour
{
    public GameObject favor_use_window;
    public GameObject effect;

    public GameObject ingre_sect;
    public GameObject board_num_sect;

    private Data_Hub data_hub;
    // Start is called before the first frame update
    void Start()
    {
        data_hub = GameObject.Find("Data_Hub").GetComponent<Data_Hub>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Using_window_close()
    {
        // 세부 구성요소 안보이게 하기
        ingre_sect.SetActive(false);
        board_num_sect.SetActive(false);
        favor_use_window.transform.Find("Use").gameObject.SetActive(false);

        // 재료 전부 끄기
        ingre_sect.transform.Find("card_1").gameObject.SetActive(true);
        ingre_sect.transform.Find("card_2").gameObject.SetActive(true);
        ingre_sect.transform.Find("card_3").gameObject.SetActive(true);
        ingre_sect.transform.Find("card_4").gameObject.SetActive(true);
        ingre_sect.transform.Find("card_5").gameObject.SetActive(true);
        ingre_sect.transform.Find("card_6").gameObject.SetActive(true);
        ingre_sect.transform.Find("card_7").gameObject.SetActive(true);
        ingre_sect.transform.Find("card_8").gameObject.SetActive(true);

        // 윈도우 안보이게
        favor_use_window.SetActive(false);
    }

    public void Using_window_open(string name)
    {
        data_hub.Favor_card_name = name;

        // 사용하기 버튼 활성화 여부 체크 및 문구 변경
        switch (name)
        {
            case "assistent":
                effect.GetComponent<TMP_Text>().text = "이번 라운드에 행동큐브 1개가 추가됩니다.";
                if (data_hub.My_data.User_ingame_data.Favor_card.Assistent > 0)
                {
                    favor_use_window.transform.Find("Use").gameObject.SetActive(true);
                }
                break;
            case "bar_owner":
                effect.GetComponent<TMP_Text>().text = "용병에게 물약 판매 행동에서 정확히 일치한 물약을 만들면 명성을 1점 추가로 얻습니다.\n일치하지 않아도 판매에 성공한다면 금화를 1장 더 받습니다.";
                if (data_hub.My_data.User_ingame_data.Favor_card.Bar_owner > 0)
                {
                    favor_use_window.transform.Find("Use").gameObject.SetActive(true);
                }
                break;
            case "big_man":
                effect.GetComponent<TMP_Text>().text = "행동칸을 하나 선택해서 자신의 큐브를 가장 윗 줄로 옮깁니다.";
                board_num_sect.SetActive(true);
                if (data_hub.My_data.User_ingame_data.Favor_card.Big_man > 0)
                {
                    favor_use_window.transform.Find("Use").gameObject.SetActive(true);
                }
                break;
            case "caretaker":
                effect.GetComponent<TMP_Text>().text = "이번 라운드에 용병에게 물약 판매 전, 자신에게 물약 실험 행동을 할 수 있습니다.";
                if (data_hub.My_data.User_ingame_data.Favor_card.Caretaker > 0)
                {
                    favor_use_window.transform.Find("Use").gameObject.SetActive(true);
                }
                break;
            case "herbalist":
                effect.GetComponent<TMP_Text>().text = "재료카드 3장을 받고, 2장을 버립니다.";
                if (data_hub.My_data.User_ingame_data.Favor_card.Herbalist > 0)
                {
                    favor_use_window.transform.Find("Use").gameObject.SetActive(true);
                }
                // 있는 재료중 골라야하므로 있는 재료 목록만 true 나머진 false
                if (data_hub.My_data.User_ingame_data.Ingredient.Card_1 > 0)
                    ingre_sect.transform.Find("card_1").gameObject.SetActive(true);
                if (data_hub.My_data.User_ingame_data.Ingredient.Card_2 > 0)
                    ingre_sect.transform.Find("card_2").gameObject.SetActive(true);
                if (data_hub.My_data.User_ingame_data.Ingredient.Card_3 > 0)
                    ingre_sect.transform.Find("card_3").gameObject.SetActive(true);
                if (data_hub.My_data.User_ingame_data.Ingredient.Card_4 > 0)
                    ingre_sect.transform.Find("card_4").gameObject.SetActive(true);
                if (data_hub.My_data.User_ingame_data.Ingredient.Card_5 > 0)
                    ingre_sect.transform.Find("card_5").gameObject.SetActive(true);
                if (data_hub.My_data.User_ingame_data.Ingredient.Card_6 > 0)
                    ingre_sect.transform.Find("card_6").gameObject.SetActive(true);
                if (data_hub.My_data.User_ingame_data.Ingredient.Card_7 > 0)
                    ingre_sect.transform.Find("card_7").gameObject.SetActive(true);
                if (data_hub.My_data.User_ingame_data.Ingredient.Card_8 > 0)
                    ingre_sect.transform.Find("card_8").gameObject.SetActive(true);

                ingre_sect.SetActive(true);
                break;
            case "merchant":
                effect.GetComponent<TMP_Text>().text = "자신의 용병에게 물약 판매 행동 순서가 첫 번쨰라면 판매 성공시 금화 1개를 추가로 얻습니다.\n첫 번쨰가 아니라도 판매 가능한 3가지 물약 중 원하는 것을 선택할 수 있습니다.";
                if (data_hub.My_data.User_ingame_data.Favor_card.Merchant > 0)
                {
                    favor_use_window.transform.Find("Use").gameObject.SetActive(true);
                }
                break;
            case "shopkeeper":
                effect.GetComponent<TMP_Text>().text = "아이템을 살 때 금화 1개를 적게 냅니다.";
                if (data_hub.My_data.User_ingame_data.Favor_card.Shopkeeper > 0)
                {
                    favor_use_window.transform.Find("Use").gameObject.SetActive(true);
                }
                break;
            case "wise_man":
                effect.GetComponent<TMP_Text>().text = "재료를 판매할 때, 금화 1개를 추가로 얻습니다.";
                if (data_hub.My_data.User_ingame_data.Favor_card.Wise_man > 0)
                {
                    favor_use_window.transform.Find("Use").gameObject.SetActive(true);
                }
                break;
        }

        favor_use_window.SetActive(true);

    }
}
