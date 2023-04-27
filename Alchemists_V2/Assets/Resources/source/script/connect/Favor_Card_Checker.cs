using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Favor_Card_Checker : MonoBehaviour
{
    private Data_Hub data_hub;

    // Use this for initialization
    void Start()
    {
        data_hub = GameObject.Find("Data_Hub").GetComponent<Data_Hub>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Ingre_card_checker(string name)
    {
        // 번호 넣기
        // 같은 수가 있으면 초기화 해줌
        if (data_hub.Select_ingre[0].Equals("card_0"))
            data_hub.Select_ingre[0] = name;

        else if (!data_hub.Select_ingre[0].Equals("card_0") && data_hub.Select_ingre[0].Equals(name))
            data_hub.Select_ingre[0] = name;

        else if (!data_hub.Select_ingre[0].Equals("card_0") && !data_hub.Select_ingre[0].Equals(name) && data_hub.Select_ingre[1].Equals("card_0"))
            data_hub.Select_ingre[1] = name;

        else if (!data_hub.Select_ingre[0].Equals("card_0") &&
                 !data_hub.Select_ingre[0].Equals(name) &&
                 !data_hub.Select_ingre[1].Equals("card_0") &&
                  data_hub.Select_ingre[1].Equals(name))
            data_hub.Select_ingre[1] = "card_0";

        // 둘다 차있고, 둘다 다른 수가 있으면 앞의 숫자를 뺴고 뒤에 넣기
        else
        {
            data_hub.Select_ingre[0] = data_hub.Select_ingre[1];
            data_hub.Select_ingre[1] = name;
        }

        // object check
        Transform[] list = GameObject.Find("Favor_window").transform.Find("Use_check").Find("ingre_sect").GetComponentsInChildren<Transform>();
        for (int i = 0; i < list.Length; i++)
        {
            // 0 과 1 에 들어있는 수가 반드시 다르므로 둘 중 하나의 수가 같으면 그 수의 카드는 바뀌어야함
            if (list[i] != GameObject.Find("Favor_window").transform.Find("Use_check").Find("ingre_sect").GetComponent<Transform>() &&
                list[i].gameObject.name.StartsWith("card_")                                                                           )
            {
                if (data_hub.Select_ingre[0].Equals(list[i].gameObject.name) ||
                    data_hub.Select_ingre[1].Equals(list[i].gameObject.name)   )
                {
                    list[i].GetComponent<Favor_Card_Herbalist_Selecter>().selected = true;
                    gameObject.GetComponent<Outline>().enabled = true;
                }
                else
                {
                    list[i].GetComponent<Favor_Card_Herbalist_Selecter>().selected = false;
                }
                list[i].GetComponent<Favor_Card_Herbalist_Selecter>().Init_pos();
                gameObject.GetComponent<Shadow>().enabled = false;
            }
        }
    }

    public void Board_checker(string board_num)
    {
        Transform[] list = GameObject.Find("Favor_window").transform.Find("Use_check").Find("board_num_sect").GetComponentsInChildren<Transform>();

        for(int i = 0; i < list.Length; i++)
        {
            if (list[i] != GameObject.Find("Favor_window").transform.Find("Use_check").Find("board_num_sect").GetComponent<Transform>() &&
                list[i].gameObject.name.StartsWith("board_")                                                                               )
            {
                if (list[i].gameObject.name.Equals(board_num)) 
                {
                    list[i].GetComponent<Favor_Card_Big_Man_Selecter>().selected = true;
                    gameObject.GetComponent<Outline>().enabled = true;
                }
                else
                {
                    list[i].GetComponent<Favor_Card_Big_Man_Selecter>().selected = false;
                }
                list[i].GetComponent<Favor_Card_Big_Man_Selecter>().Init_pos();
                gameObject.GetComponent<Shadow>().enabled = false;
            }
        }
    }
}
