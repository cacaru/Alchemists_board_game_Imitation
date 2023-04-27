using Alchemists_data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board_Acting_Checker : MonoBehaviour
{
    private int board_num = 0;
    private int board_order = -1;
    private int board_cube_order = -1;

    private int my_board_order = -1;

    private Data_Hub data_hub;
    private Adv_Obj_Construct_for_Play adv_obj_maker;
    // Start is called before the first frame update
    void Start()
    {
        data_hub = GameObject.Find("Data_Hub").GetComponent<Data_Hub>();
        adv_obj_maker = GameObject.Find("Data_Controller").GetComponent<Adv_Obj_Construct_for_Play>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Set_board_num(int board_num)
    {
        this.board_num = board_num;
        if (board_num == 3)
            adv_checking();
        else
            checking();
    }
    public void Set_board_order(int board_order)
    {
        this.board_order = board_order;
    }
    public void Set_board_cube_order(int board_cube_order)
    {
        this.board_cube_order = board_cube_order;
        if (board_num == 3)
            adv_checking();
        else
            checking();
    }
    public void Set_my_board_order(int my_board_order)
    {
        this.my_board_order = my_board_order;
    }

    public void Set_coin_step_end(bool check)
    {
        data_hub.Coin_step_end = check;
    }

    public void Init_board_val()
    {
        this.board_order = -1;
        this.board_cube_order = -1;
    }

    private void checking()
    {
       /* Debug.Log("----------- checking-------------");
        Debug.Log("board_num : "+board_num);
        Debug.Log("board_order : " + board_order);
        Debug.Log("board_cube_order : " + board_cube_order);
        Debug.Log("my_num : " + my_board_order);
        Debug.Log("---------------------------------");*/
        // 3 변수에 값이 들어있으면
        // 내 차례인지 확인한 뒤
        // 내 차례라면 -> 2가지 if
        // 보드에 동작을 가하고
        // 3가지 변수를 0, -1, -1로 초기화
        if (board_num > 0 &&                    // 보드번호
            board_order >= 0 &&                 // 현재 보드를 진행하는 유저 번호
            board_cube_order >= 0 &&            // 현재 보드를 진행하는 유저의 큐브 번호
            board_order == my_board_order       // 현재 보드를 진행하는 유저의 번호와 내 차례의 보드 번호가 같다!
            )
        {
            // 보드 번호에 따라 행동이 달라야함
            switch (board_num)
            {
                case 1:     // 재료 받기
                    // 6가지 재료 목록들에 대해 Object_selecter 붙이기
                    Transform[] list = GameObject.Find("Room_Part_1").transform.Find("Show_Ingre_Sect").GetComponentsInChildren<Transform>();
                    for(int i = 0; i < list.Length; i++)
                    {
                        if( list[i] != GameObject.Find("Room_Part_1").transform.Find("Show_Ingre_Sect").transform)
                            list[i].gameObject.AddComponent<Object_Selecter>();
                    }
                    // 선택 확정 버튼 표시
                    GameObject.Find("Switch_Area").transform.Find("Board_act_end").gameObject.SetActive(true);
                    GameObject.Find("Switch_Area").transform.Find("Board_act_pass").gameObject.SetActive(true);

                    // 내 차례의 보드번호를 제외한 3가지 변수를 초기화
                    Init_board_val();
                    break;

                case 2:     // 재료 판매
                    // 오브젝트를 보여줘야함
                    GameObject.Find("Data_Controller").GetComponent<Ingre_Selecter_for_Play>().Construct_ingre_list(board_num, data_hub.My_data);
                    // 선택 확정 버튼 표시
                    GameObject.Find("Switch_Area").transform.Find("Board_act_end").gameObject.SetActive(true);
                    GameObject.Find("Switch_Area").transform.Find("Board_act_pass").gameObject.SetActive(true);

                    // 내 차례의 보드번호를 제외한 3가지 변수를 초기화
                    Init_board_val();
                    break;

                case 4:     // 유물 구매
                    // 유물 앞에 구매 버튼 활성화 -> 버튼 클릭됨 -> 구매 확인창 -> 구매 완료
                    GameObject.Find("Room_Part_4").transform.Find("Btn_Sect").gameObject.SetActive(true);

                    // 내 차례의 보드번호를 제외한 3가지 변수를 초기화
                    Init_board_val();
                    break;

                case 5:     // 논문 반박
                    // 반박할 재료 선택 -> 원소 선택 -> 재료 선택 -> 반박 결과 창
                    if (data_hub.Core_end_5 == false &&
                        data_hub.Ori_ele_end_5 == false  )
                    {
                        // 반박할 재료 선택
                        // 재료 선택 script on
                        GameObject.Find("Data_Controller").GetComponent<Data_Controller>().Controller_theory_core_selecter(5, true);
                        // 선택확정 버튼 표시
                        GameObject.Find("Switch_Area").transform.Find("Board_act_end").gameObject.SetActive(true);
                        GameObject.Find("Switch_Area").transform.Find("Board_act_pass").gameObject.SetActive(true);
                    }
                    else if (data_hub.Core_end_5 &&
                             data_hub.Ori_ele_end_5 == false )
                    {
                        // 원소 선택
                        GameObject.Find("Data_Controller").GetComponent<Theory_Refute_Obj_for_Play>().Ele_open();
                        // 선택확정 버튼 표시
                        GameObject.Find("Switch_Area").transform.Find("Board_act_end").gameObject.SetActive(true);
                        GameObject.Find("Switch_Area").transform.Find("Board_act_pass").gameObject.SetActive(true);
                    }
                    else if (data_hub.Core_end_5 &&
                             data_hub.Ori_ele_end_5  )
                    {
                        // 재료 선택 
                        GameObject.Find("Data_Controller").GetComponent<Theory_Refute_Obj_for_Play>().Ingre_open();
                        // 선택확정 버튼 표시
                        GameObject.Find("Switch_Area").transform.Find("Board_act_end").gameObject.SetActive(true);
                        GameObject.Find("Switch_Area").transform.Find("Board_act_pass").gameObject.SetActive(true);

                        Init_board_val();
                    }
                    break;

                case 6:     // 논문 발표
                    // 발표할 재료 선택 -> 원소 선택 -> 인장 선택 -> 발표 완료창
                    if (data_hub.Core_end_6 == false    &&
                        data_hub.Element_end_6 == false  )
                    {
                        //Debug.Log("board_6_checking_1");
                        // 발표할 재료 선택 script true로 변경
                        GameObject.Find("Data_Controller").GetComponent<Data_Controller>().Controller_theory_core_selecter(6, true);
                        // 선택확정 버튼 표시
                        GameObject.Find("Switch_Area").transform.Find("Board_act_end").gameObject.SetActive(true);
                        GameObject.Find("Switch_Area").transform.Find("Board_act_pass").gameObject.SetActive(true);
                    }
                    else if (data_hub.Core_end_6             && 
                             data_hub.Element_end_6 == false  )
                    {
                        //Debug.Log("board_6_checking_2");
                        // 원소 선택 - element_end_6
                        GameObject.Find("Data_Controller").GetComponent<Theory_Present_Obj_for_Play>().Ele_open();
                        // 선택확정 버튼 표시
                        GameObject.Find("Switch_Area").transform.Find("Board_act_end").gameObject.SetActive(true);
                        GameObject.Find("Switch_Area").transform.Find("Board_act_pass").gameObject.SetActive(true);
                    }
                    else if (data_hub.Core_end_6 &&
                             data_hub.Element_end_6  )
                    {
                        //Debug.Log("board_6_checking_3");
                        // 인장 선택 - stamp_end_6
                        GameObject.Find("Data_Controller").GetComponent<Theory_Present_Obj_for_Play>().Stamp_open();
                        // 선택확정 버튼 표시
                        GameObject.Find("Switch_Area").transform.Find("Board_act_end").gameObject.SetActive(true);
                        GameObject.Find("Switch_Area").transform.Find("Board_act_pass").gameObject.SetActive(true);
                        Init_board_val();
                    }
                    break;

                case 7:     // 학생 실험
                    // 재료 선택 -> 결과 모두에게 공유
                    // 내차례면 보여줘야함 : 이곳에 들어오면 내차례 인 것!
                    GameObject.Find("Data_Controller").GetComponent<Ingre_Selecter_for_Play>().Construct_ingre_list(board_num, data_hub.My_data);
                    // 선택 확정 버튼 활성화
                    GameObject.Find("Switch_Area").transform.Find("Board_act_end").gameObject.SetActive(true);
                    GameObject.Find("Switch_Area").transform.Find("Board_act_pass").gameObject.SetActive(true);

                    // 내 차례의 보드번호를 제외한 3가지 변수를 초기화
                    Init_board_val();
                    break;

                case 8:     // 본인 실험
                    // 재료 선택 -> 결과 모두에게 공유
                    GameObject.Find("Data_Controller").GetComponent<Ingre_Selecter_for_Play>().Construct_ingre_list(board_num, data_hub.My_data);
                    // 선택 확정 버튼 활성화
                    GameObject.Find("Switch_Area").transform.Find("Board_act_end").gameObject.SetActive(true);
                    GameObject.Find("Switch_Area").transform.Find("Board_act_pass").gameObject.SetActive(true);

                    // 내 차례의 보드번호를 제외한 3가지 변수를 초기화
                    Init_board_val();
                    break;

                case 9:     // 전시회
                    // 전시할 물약 선택 -> 재료 표시 및 재료 선택 -> 재료 실험 결과 확인 -> 성공하면 성공창과 함께 큐브 색 변경, 실패하면 실패 칸으로 가고 명성 -1 + 결과 안내
                    // 물약 선택 스탭, 재료 선택 스탭, 둘다 true 면 결과 확인 및 큐브 조작스탭
                    if (data_hub.Exhibit_potion_step == false)
                    {
                        // 물약 선택 가능한 큐브 open
                        GameObject.Find("Room_Part_9").transform.Find("Select_Sect").gameObject.SetActive(true);

                        // 선택 확정 버튼 활성화
                        GameObject.Find("Switch_Area").transform.Find("Board_act_end").gameObject.SetActive(true);
                        GameObject.Find("Switch_Area").transform.Find("Board_act_pass").gameObject.SetActive(true);
                    }
                    else if (data_hub.Exhibit_potion_step)
                    {
                        // 선택된 포션의 위치에서 재료 open
                        GameObject.Find("Data_Controller").GetComponent<Exhibit_Ingre_for_Play>().Draw_ingre_for_exhibition(data_hub.Exhibit_select_potion);
                    }

                    break;

                case -1:
                    // 선택 확정 버튼 비활성화
                    GameObject.Find("Switch_Area").transform.Find("Board_act_end").gameObject.SetActive(false);
                    GameObject.Find("Switch_Area").transform.Find("Board_act_pass").gameObject.SetActive(false);
                    break;
            }


        }
    }
    
    // 용병에게 물약판매 스텝에서 사용될 특수 순서에 따라 진행
    private void adv_checking()
    {
        // 내 차례가 종료되어있다면 다른 사람 차례임만 알리고 아무것도 하지 말아야함
        if (data_hub.My_board_3_end)
        {
            GameObject.Find("Room_Part_3").transform.Find("Ingre_Select_Sect").Find("Waiting_announce").gameObject.SetActive(true);
            return;
        }

        // 할인카드 제시 -> 최초 발동
        // 내부 할인 제시 완료 bool, 외부 전원 제시 완료 bool
        // my_coin_step_end ,, coin_step_end
        if (data_hub.My_coin_step_end == false      &&
            data_hub.Coin_step_end == false         &&
            data_hub.Price_step_end == false        &&
            data_hub.Potion_step_end == false)
        {
            // 오브젝트가 이미 있으면 생성되서는 안됨
            int obj_count = GameObject.Find("Room_Part_3").transform.Find("Play_Sect").childCount;
            if ( obj_count < 1)
            {
                // 할인 제시 오브젝트 생성
                adv_obj_maker.Discount_obj_show(data_hub.My_data);
            }
            // 결정 버튼 생성
            GameObject.Find("Switch_Area").transform.Find("Board_act_end").gameObject.SetActive(true);
            GameObject.Find("Switch_Area").transform.Find("Board_act_pass").gameObject.SetActive(true);
        }

        // 판매가 설정   
        else if (data_hub.My_coin_step_end        &&
                 data_hub.Coin_step_end           &&
                 data_hub.Price_step_end == false &&
                 data_hub.Potion_step_end == false  )
        {
            Debug.Log(data_hub.Selling_turn);
            // 판매가 오브젝트 설정
            if (data_hub.Dis_coin_data[data_hub.Selling_turn].user_key == data_hub.My_key)
            {
                GameObject.Find("Room_Part_3").transform.Find("Ingre_Select_Sect").Find("Waiting_announce").gameObject.SetActive(false);
                adv_obj_maker.Price_obj_show();
                // 결정 버튼 생성
                GameObject.Find("Switch_Area").transform.Find("Board_act_end").gameObject.SetActive(true);
                GameObject.Find("Switch_Area").transform.Find("Board_act_pass").gameObject.SetActive(true);
            }
            // 내 차례가 아니면 대기 문구 작성
            else
            {
                // 다른 유저의 차례입니다. 잠시 기다려주세요...
                GameObject.Find("Room_Part_3").transform.Find("Ingre_Select_Sect").Find("Waiting_announce").gameObject.SetActive(true);
            }
        }
        // 물약 제시-> 내 차례 일때 1차
        else if (data_hub.My_coin_step_end        &&
                 data_hub.Coin_step_end           &&
                 data_hub.Price_step_end          &&
                 data_hub.Potion_step_end == false  )
        {
            // 결정 버튼 생성
            GameObject.Find("Switch_Area").transform.Find("Board_act_end").gameObject.SetActive(true);
            GameObject.Find("Switch_Area").transform.Find("Board_act_pass").gameObject.SetActive(true);
            adv_obj_maker.Potion_obj_show(data_hub.Adventurer_card_data, data_hub.Random_adv_list[data_hub.Round_cont - 2]);
        }
        // 재료 제시 -> 내 차례일 때 2차 
        else if (data_hub.My_coin_step_end &&
                 data_hub.Coin_step_end    &&
                 data_hub.Price_step_end   &&
                 data_hub.Potion_step_end    )
        {
            // 결정 버튼 생성
            GameObject.Find("Switch_Area").transform.Find("Board_act_end").gameObject.SetActive(true);
            GameObject.Find("Switch_Area").transform.Find("Board_act_pass").gameObject.SetActive(true);

            // 재료 표시
            GameObject.Find("Data_Controller").GetComponent<Ingre_Selecter_for_Play>().Construct_ingre_list(board_num, data_hub.My_data);
        }
    }

    public void Board_1_selecting_obj_checker(int index)
    {
        Transform[] list = GameObject.Find("Room_Part_1").transform.Find("Show_Ingre_Sect").GetComponentsInChildren<Transform>();
        for(int i = 0; i < list.Length; i++)
        {
            if (list[i] != GameObject.Find("Room_Part_1").transform.Find("Show_Ingre_Sect").transform)
            {
                list[i].GetComponent<Object_Selecter>().selected = false;
                list[i].gameObject.GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.0f);
            }
        }
        list[index+1].GetComponent<Object_Selecter>().selected = true;
        list[index+1].gameObject.GetComponent<Renderer>().material.SetColor("_OutlineColor", OUTLINE_COLOR.DEFAULT);
        list[index+1].gameObject.GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.2f);
    }

    public void Board_2_selecting_obj_checker(int num)
    {
        Transform[] list = GameObject.Find("Room_Part_2").transform.Find("Play_Sect").GetComponentsInChildren<Transform>();
        string t = "2_ingre_" + num.ToString();
        for ( int i = 0; i < list.Length; i++)
        {
            if (list[i].gameObject.name.Equals(t))
            {
                list[i].GetComponent<Object_Selecter>().selected = true;
                list[i].gameObject.GetComponent<Renderer>().material.SetColor("_OutlineColor", OUTLINE_COLOR.DEFAULT);
                list[i].gameObject.GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.2f);
            }
            else if (list[i] != GameObject.Find("Room_Part_2").transform.Find("Play_Sect").GetComponent<Transform>())
            {
                list[i].GetComponent<Object_Selecter>().selected = false;
                list[i].gameObject.GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.0f);
            }
        }

    }

    public void Board_3_coin_checker(int num)
    {
        Transform[] list = GameObject.Find("Room_Part_3").transform.Find("Play_Sect").GetComponentsInChildren<Transform>();
        string t = "dis_coin_" + num.ToString();

        for (int i = 0; i < list.Length; i++)
        {
            if (list[i].gameObject.name.Equals(t))
            {
                list[i].GetComponent<Coin_Selecter>().selected = true;
                list[i].gameObject.GetComponent<Renderer>().material.SetColor("_OutlineColor", OUTLINE_COLOR.DEFAULT);
                list[i].gameObject.GetComponent<Renderer>().material.SetFloat("_Ountline", 0.2f);
            }
            else if (list[i] != GameObject.Find("Room_Part_3").transform.Find("Play_Sect").GetComponent<Transform>())
            {
                list[i].GetComponent<Coin_Selecter>().selected = false;
                list[i].gameObject.GetComponent<Renderer>().material.SetFloat("_Outline", 0.0f);
            }
        }
    }

    public void Board_3_price_checker(int num)
    {
        Transform[] list = GameObject.Find("Room_Part_3").transform.Find("Play_Sect").GetComponentsInChildren<Transform>();
        string t = "sell_potion_" + num.ToString();

        for(int i = 0; i < list.Length; i++)
        {
            if (list[i].gameObject.name.Equals(t))
            {
                list[i].GetComponent<Price_Selecter>().selected = true;
                list[i].gameObject.GetComponent<Renderer>().material.SetColor("_OutlineColor", OUTLINE_COLOR.DEFAULT);
                list[i].gameObject.GetComponent<Renderer>().material.SetFloat("_Outline", 0.2f);
            }
            else if(list[i] != GameObject.Find("Room_Part_3").transform.Find("Play_Sect").GetComponent<Transform>())
            {
                list[i].GetComponent<Price_Selecter>().selected = false;
                list[i].gameObject.GetComponent<Renderer>().material.SetFloat("_Outline", 0.0f);
            }
        }
    }

    public void Board_3_potion_checker(string name)
    {
        Transform[] list = GameObject.Find("Room_Part_3").transform.Find("Play_Sect").GetComponentsInChildren<Transform>();

        for (int i = 0; i < list.Length; i++)
        {
            if (list[i].gameObject.name.Equals(name))
            {
                list[i].GetComponent<Potion_Selecter>().selected = true;
                list[i].gameObject.GetComponent<Renderer>().material.SetColor("_OutlineColor", OUTLINE_COLOR.DEFAULT);
                list[i].gameObject.GetComponent<Renderer>().material.SetFloat("_Ountline", 0.2f);
            }
            else if (list[i] != GameObject.Find("Room_Part_3").transform.Find("Play_Sect").GetComponent<Transform>())
            {
                list[i].GetComponent<Potion_Selecter>().selected = false;
                list[i].gameObject.GetComponent<Renderer>().material.SetFloat("_Outline", 0.0f);
            }
        }
    }

    public void Board_ingre_checker(int ingre_num)
    {
        Transform[] list = null;
        Transform ori = null;
        if (data_hub.Now_board_num == 3)
        {
            list = GameObject.Find("Room_Part_3").transform.Find("Play_Sect").GetComponentsInChildren<Transform>();
            ori = GameObject.Find("Room_Part_3").transform.Find("Play_Sect").GetComponent<Transform>();
        }
        else if (data_hub.Now_board_num == 7)
        {
            list = GameObject.Find("Room_Part_7").transform.Find("Play_Sect").GetComponentsInChildren<Transform>();
            ori = GameObject.Find("Room_Part_7").transform.Find("Play_Sect").GetComponent<Transform>();
        }
        else if (data_hub.Now_board_num == 8)
        {
            list = GameObject.Find("Room_Part_8").transform.Find("Play_Sect").GetComponentsInChildren<Transform>();
            ori = GameObject.Find("Room_Part_8").transform.Find("Play_Sect").GetComponent<Transform>();
        }
        

        // 번호 넣기
        // 같은 수가 있으면 초기화 해줌
        if (data_hub.Select_ingre[0].Equals("card_0"))
            data_hub.Select_ingre[0] = "card_" + ingre_num;

        else if (!data_hub.Select_ingre[0].Equals("card_0") && data_hub.Select_ingre[0].Equals("card_"+ingre_num))
            data_hub.Select_ingre[0] = "card_0";

        else if (!data_hub.Select_ingre[0].Equals("card_0") && !data_hub.Select_ingre[0].Equals("card_"+ingre_num) && data_hub.Select_ingre[1].Equals("card_0"))
            data_hub.Select_ingre[1] = "card_" + ingre_num;

        else if (!data_hub.Select_ingre[0].Equals("card_0")            && 
                 !data_hub.Select_ingre[0].Equals("card_" + ingre_num) && 
                 !data_hub.Select_ingre[1].Equals("card_0")            && 
                  data_hub.Select_ingre[1].Equals("card_" + ingre_num)    )
            data_hub.Select_ingre[1] = "card_0";

        // 둘다 차있고, 둘다 다른 수가 있으면 앞의 숫자를 뺴고 뒤에 넣기
        else
        {
            data_hub.Select_ingre[0] = data_hub.Select_ingre[1];
            data_hub.Select_ingre[1] = "card_" + ingre_num;
        }

        for (int i = 0; i < list.Length; i++)
        {
            // 0 과 1 에 들어있는 수가 반드시 다르므로 둘 중 하나의 수가 같으면 그 수의 카드는 바뀌어야함
            if (list[i] != ori)
            {
                if (data_hub.Select_ingre[0].Equals("card_" + list[i].gameObject.name[^1].ToString()) ||
                    data_hub.Select_ingre[1].Equals("card_" + list[i].gameObject.name[^1].ToString())   )
                {
                    list[i].GetComponent<Object_Selecter>().selected = true;
                    list[i].gameObject.GetComponent<Renderer>().material.SetColor("_OutlineColor", OUTLINE_COLOR.DEFAULT);
                    list[i].gameObject.GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.2f);
                }
                else
                {
                    list[i].GetComponent<Object_Selecter>().selected = false; 
                    list[i].gameObject.GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.0f);
                }
            }
        }
    }

    public void Board_5_core_checker(int num)
    {
        // 전부 끄고
        // outline off
        GameObject.Find("Room_Part_5").transform.Find("Contents").Find("Ingre_1").Find("Ingre").GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.0f);
        GameObject.Find("Room_Part_5").transform.Find("Contents").Find("Ingre_2").Find("Ingre").GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.0f);
        GameObject.Find("Room_Part_5").transform.Find("Contents").Find("Ingre_3").Find("Ingre").GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.0f);
        GameObject.Find("Room_Part_5").transform.Find("Contents").Find("Ingre_4").Find("Ingre").GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.0f);
        GameObject.Find("Room_Part_5").transform.Find("Contents").Find("Ingre_5").Find("Ingre").GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.0f);
        GameObject.Find("Room_Part_5").transform.Find("Contents").Find("Ingre_6").Find("Ingre").GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.0f);
        GameObject.Find("Room_Part_5").transform.Find("Contents").Find("Ingre_7").Find("Ingre").GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.0f);
        GameObject.Find("Room_Part_5").transform.Find("Contents").Find("Ingre_8").Find("Ingre").GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.0f);

        // selected off
        GameObject.Find("Room_Part_5").transform.Find("Contents").Find("Ingre_1").Find("Ingre").GetComponent<Theory_Core_Selecter>().selected = false;
        GameObject.Find("Room_Part_5").transform.Find("Contents").Find("Ingre_2").Find("Ingre").GetComponent<Theory_Core_Selecter>().selected = false;
        GameObject.Find("Room_Part_5").transform.Find("Contents").Find("Ingre_3").Find("Ingre").GetComponent<Theory_Core_Selecter>().selected = false;
        GameObject.Find("Room_Part_5").transform.Find("Contents").Find("Ingre_4").Find("Ingre").GetComponent<Theory_Core_Selecter>().selected = false;
        GameObject.Find("Room_Part_5").transform.Find("Contents").Find("Ingre_5").Find("Ingre").GetComponent<Theory_Core_Selecter>().selected = false;
        GameObject.Find("Room_Part_5").transform.Find("Contents").Find("Ingre_6").Find("Ingre").GetComponent<Theory_Core_Selecter>().selected = false;
        GameObject.Find("Room_Part_5").transform.Find("Contents").Find("Ingre_7").Find("Ingre").GetComponent<Theory_Core_Selecter>().selected = false;
        GameObject.Find("Room_Part_5").transform.Find("Contents").Find("Ingre_8").Find("Ingre").GetComponent<Theory_Core_Selecter>().selected = false;

        // 선택된것 selected 상태 만들기
        switch (num)
        {
            case 1:
                GameObject.Find("Room_Part_5").transform.Find("Contents").Find("Ingre_1").Find("Ingre").GetComponent<Renderer>().material.SetColor("_OutlineColor", OUTLINE_COLOR.DEFAULT);
                GameObject.Find("Room_Part_5").transform.Find("Contents").Find("Ingre_1").Find("Ingre").GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.2f);
                GameObject.Find("Room_Part_5").transform.Find("Contents").Find("Ingre_1").Find("Ingre").GetComponent<Theory_Core_Selecter>().selected = true;
                break;
            case 2:
                GameObject.Find("Room_Part_5").transform.Find("Contents").Find("Ingre_2").Find("Ingre").GetComponent<Renderer>().material.SetColor("_OutlineColor", OUTLINE_COLOR.DEFAULT);
                GameObject.Find("Room_Part_5").transform.Find("Contents").Find("Ingre_2").Find("Ingre").GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.2f);
                GameObject.Find("Room_Part_5").transform.Find("Contents").Find("Ingre_2").Find("Ingre").GetComponent<Theory_Core_Selecter>().selected = true;
                break;
            case 3:
                GameObject.Find("Room_Part_5").transform.Find("Contents").Find("Ingre_3").Find("Ingre").GetComponent<Renderer>().material.SetColor("_OutlineColor", OUTLINE_COLOR.DEFAULT);
                GameObject.Find("Room_Part_5").transform.Find("Contents").Find("Ingre_3").Find("Ingre").GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.2f);
                GameObject.Find("Room_Part_5").transform.Find("Contents").Find("Ingre_3").Find("Ingre").GetComponent<Theory_Core_Selecter>().selected = true;
                break;
            case 4:
                GameObject.Find("Room_Part_5").transform.Find("Contents").Find("Ingre_4").Find("Ingre").GetComponent<Renderer>().material.SetColor("_OutlineColor", OUTLINE_COLOR.DEFAULT);
                GameObject.Find("Room_Part_5").transform.Find("Contents").Find("Ingre_4").Find("Ingre").GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.2f);
                GameObject.Find("Room_Part_5").transform.Find("Contents").Find("Ingre_4").Find("Ingre").GetComponent<Theory_Core_Selecter>().selected = true;
                break;
            case 5:
                GameObject.Find("Room_Part_5").transform.Find("Contents").Find("Ingre_5").Find("Ingre").GetComponent<Renderer>().material.SetColor("_OutlineColor", OUTLINE_COLOR.DEFAULT);
                GameObject.Find("Room_Part_5").transform.Find("Contents").Find("Ingre_5").Find("Ingre").GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.2f);
                GameObject.Find("Room_Part_5").transform.Find("Contents").Find("Ingre_5").Find("Ingre").GetComponent<Theory_Core_Selecter>().selected = true;
                break;
            case 6:
                GameObject.Find("Room_Part_5").transform.Find("Contents").Find("Ingre_6").Find("Ingre").GetComponent<Renderer>().material.SetColor("_OutlineColor", OUTLINE_COLOR.DEFAULT);
                GameObject.Find("Room_Part_5").transform.Find("Contents").Find("Ingre_6").Find("Ingre").GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.2f);
                GameObject.Find("Room_Part_5").transform.Find("Contents").Find("Ingre_6").Find("Ingre").GetComponent<Theory_Core_Selecter>().selected = true;
                break;
            case 7:
                GameObject.Find("Room_Part_5").transform.Find("Contents").Find("Ingre_7").Find("Ingre").GetComponent<Renderer>().material.SetColor("_OutlineColor", OUTLINE_COLOR.DEFAULT);
                GameObject.Find("Room_Part_5").transform.Find("Contents").Find("Ingre_7").Find("Ingre").GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.2f);
                GameObject.Find("Room_Part_5").transform.Find("Contents").Find("Ingre_7").Find("Ingre").GetComponent<Theory_Core_Selecter>().selected = true;
                break;
            case 8:
                GameObject.Find("Room_Part_5").transform.Find("Contents").Find("Ingre_8").Find("Ingre").GetComponent<Renderer>().material.SetColor("_OutlineColor", OUTLINE_COLOR.DEFAULT);
                GameObject.Find("Room_Part_5").transform.Find("Contents").Find("Ingre_8").Find("Ingre").GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.2f);
                GameObject.Find("Room_Part_5").transform.Find("Contents").Find("Ingre_8").Find("Ingre").GetComponent<Theory_Core_Selecter>().selected = true;
                break;
        }
    }

    public void Board_5_ori_ele_checker(int num)
    {
        Transform[] list = GameObject.Find("Room_Part_5").transform.Find("Play_Sect").GetComponentsInChildren<Transform>();

        for (int i = 0; i < list.Length; i++)
        {
            if (list[i].gameObject.name.Equals(num.ToString()))
            {
                list[i].GetComponent<Theory_Origin_Ele_Selecter>().selected = true;
                list[i].gameObject.GetComponent<Renderer>().material.SetColor("_OutlineColor", OUTLINE_COLOR.DEFAULT);
                list[i].gameObject.GetComponent<Renderer>().material.SetFloat("_Outline", 0.2f);
            }
            else if (list[i] != GameObject.Find("Room_Part_5").transform.Find("Play_Sect").GetComponent<Transform>())
            {
                list[i].GetComponent<Theory_Origin_Ele_Selecter>().selected = false;
                list[i].gameObject.GetComponent<Renderer>().material.SetFloat("_Outline", 0.0f);
            }
        }
    }

    public void Board_5_ingre_checker(int num)
    {
        Transform[] list = GameObject.Find("Room_Part_5").transform.Find("Play_Sect").GetComponentsInChildren<Transform>();

        if (data_hub.Ingre_arr[0] == 0)
            data_hub.Ingre_arr[0] = num;

        else if (data_hub.Ingre_arr[0] != 0 && data_hub.Ingre_arr[0] == num)
            data_hub.Ingre_arr[0] = 0;

        else if (data_hub.Ingre_arr[0] != 0 && data_hub.Ingre_arr[0] != num && data_hub.Ingre_arr[1] == 0)
            data_hub.Ingre_arr[1] = num;

        else if (data_hub.Ingre_arr[0] != 0   &&
                 data_hub.Ingre_arr[0] != num &&
                 data_hub.Ingre_arr[1] != 0   &&
                 data_hub.Ingre_arr[1] == num )
            data_hub.Ingre_arr[1] = 0;

        // 둘다 차있고, 둘다 다른 수가 있으면 앞의 숫자를 뺴고 뒤에 넣기
        else
        {
            data_hub.Ingre_arr[0] = data_hub.Ingre_arr[1];
            data_hub.Ingre_arr[1] = num;
        }

        for (int i = 0; i < list.Length; i++)
        {
            // 0 과 1 에 들어있는 수가 반드시 다르므로 둘 중 하나의 수가 같으면 그 수의 카드는 바뀌어야함
            if (list[i] != GameObject.Find("Room_Part_5").transform.Find("Play_Sect").GetComponent<Transform>())
            {
                if (data_hub.Ingre_arr[0] == int.Parse(list[i].gameObject.name.ToString()) ||
                    data_hub.Ingre_arr[1] == int.Parse(list[i].gameObject.name.ToString())   )
                {
                    list[i].GetComponent<Theory_Ingre_Selecter>().selected = true;
                    list[i].gameObject.GetComponent<Renderer>().material.SetColor("_OutlineColor", OUTLINE_COLOR.DEFAULT);
                    list[i].gameObject.GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.2f);
                }
                else
                {
                    list[i].GetComponent<Theory_Ingre_Selecter>().selected = false;
                    list[i].gameObject.GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.0f);
                }
            }
        }
    }

    public void Board_6_core_checker(int num)
    {
        // 전부 끄고
        // outline off
        GameObject.Find("Room_Part_6").transform.Find("Contents").Find("Ingre_1").Find("Ingre").GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.0f);
        GameObject.Find("Room_Part_6").transform.Find("Contents").Find("Ingre_2").Find("Ingre").GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.0f);
        GameObject.Find("Room_Part_6").transform.Find("Contents").Find("Ingre_3").Find("Ingre").GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.0f);
        GameObject.Find("Room_Part_6").transform.Find("Contents").Find("Ingre_4").Find("Ingre").GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.0f);
        GameObject.Find("Room_Part_6").transform.Find("Contents").Find("Ingre_5").Find("Ingre").GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.0f);
        GameObject.Find("Room_Part_6").transform.Find("Contents").Find("Ingre_6").Find("Ingre").GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.0f);
        GameObject.Find("Room_Part_6").transform.Find("Contents").Find("Ingre_7").Find("Ingre").GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.0f);
        GameObject.Find("Room_Part_6").transform.Find("Contents").Find("Ingre_8").Find("Ingre").GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.0f);

        // selected off
        GameObject.Find("Room_Part_6").transform.Find("Contents").Find("Ingre_1").Find("Ingre").GetComponent<Theory_Core_Selecter>().selected = false;
        GameObject.Find("Room_Part_6").transform.Find("Contents").Find("Ingre_2").Find("Ingre").GetComponent<Theory_Core_Selecter>().selected = false;
        GameObject.Find("Room_Part_6").transform.Find("Contents").Find("Ingre_3").Find("Ingre").GetComponent<Theory_Core_Selecter>().selected = false;
        GameObject.Find("Room_Part_6").transform.Find("Contents").Find("Ingre_4").Find("Ingre").GetComponent<Theory_Core_Selecter>().selected = false;
        GameObject.Find("Room_Part_6").transform.Find("Contents").Find("Ingre_5").Find("Ingre").GetComponent<Theory_Core_Selecter>().selected = false;
        GameObject.Find("Room_Part_6").transform.Find("Contents").Find("Ingre_6").Find("Ingre").GetComponent<Theory_Core_Selecter>().selected = false;
        GameObject.Find("Room_Part_6").transform.Find("Contents").Find("Ingre_7").Find("Ingre").GetComponent<Theory_Core_Selecter>().selected = false;
        GameObject.Find("Room_Part_6").transform.Find("Contents").Find("Ingre_8").Find("Ingre").GetComponent<Theory_Core_Selecter>().selected = false;

        // 선택된것 selected 상태 만들기
        switch (num)
        {
            case 1:
                GameObject.Find("Room_Part_6").transform.Find("Contents").Find("Ingre_1").Find("Ingre").GetComponent<Renderer>().material.SetColor("_OutlineColor", OUTLINE_COLOR.DEFAULT);
                GameObject.Find("Room_Part_6").transform.Find("Contents").Find("Ingre_1").Find("Ingre").GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.2f);
                GameObject.Find("Room_Part_6").transform.Find("Contents").Find("Ingre_1").Find("Ingre").GetComponent<Theory_Core_Selecter>().selected = true;
                break;
            case 2:
                GameObject.Find("Room_Part_6").transform.Find("Contents").Find("Ingre_2").Find("Ingre").GetComponent<Renderer>().material.SetColor("_OutlineColor", OUTLINE_COLOR.DEFAULT);
                GameObject.Find("Room_Part_6").transform.Find("Contents").Find("Ingre_2").Find("Ingre").GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.2f);
                GameObject.Find("Room_Part_6").transform.Find("Contents").Find("Ingre_2").Find("Ingre").GetComponent<Theory_Core_Selecter>().selected = true;
                break;
            case 3:
                GameObject.Find("Room_Part_6").transform.Find("Contents").Find("Ingre_3").Find("Ingre").GetComponent<Renderer>().material.SetColor("_OutlineColor", OUTLINE_COLOR.DEFAULT);
                GameObject.Find("Room_Part_6").transform.Find("Contents").Find("Ingre_3").Find("Ingre").GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.2f);
                GameObject.Find("Room_Part_6").transform.Find("Contents").Find("Ingre_3").Find("Ingre").GetComponent<Theory_Core_Selecter>().selected = true;
                break;
            case 4:
                GameObject.Find("Room_Part_6").transform.Find("Contents").Find("Ingre_4").Find("Ingre").GetComponent<Renderer>().material.SetColor("_OutlineColor", OUTLINE_COLOR.DEFAULT);
                GameObject.Find("Room_Part_6").transform.Find("Contents").Find("Ingre_4").Find("Ingre").GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.2f);
                GameObject.Find("Room_Part_6").transform.Find("Contents").Find("Ingre_4").Find("Ingre").GetComponent<Theory_Core_Selecter>().selected = true;
                break;
            case 5:
                GameObject.Find("Room_Part_6").transform.Find("Contents").Find("Ingre_5").Find("Ingre").GetComponent<Renderer>().material.SetColor("_OutlineColor", OUTLINE_COLOR.DEFAULT);
                GameObject.Find("Room_Part_6").transform.Find("Contents").Find("Ingre_5").Find("Ingre").GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.2f);
                GameObject.Find("Room_Part_6").transform.Find("Contents").Find("Ingre_5").Find("Ingre").GetComponent<Theory_Core_Selecter>().selected = true;
                break;
            case 6:
                GameObject.Find("Room_Part_6").transform.Find("Contents").Find("Ingre_6").Find("Ingre").GetComponent<Renderer>().material.SetColor("_OutlineColor", OUTLINE_COLOR.DEFAULT);
                GameObject.Find("Room_Part_6").transform.Find("Contents").Find("Ingre_6").Find("Ingre").GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.2f);
                GameObject.Find("Room_Part_6").transform.Find("Contents").Find("Ingre_6").Find("Ingre").GetComponent<Theory_Core_Selecter>().selected = true;
                break;
            case 7:
                GameObject.Find("Room_Part_6").transform.Find("Contents").Find("Ingre_7").Find("Ingre").GetComponent<Renderer>().material.SetColor("_OutlineColor", OUTLINE_COLOR.DEFAULT);
                GameObject.Find("Room_Part_6").transform.Find("Contents").Find("Ingre_7").Find("Ingre").GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.2f);
                GameObject.Find("Room_Part_6").transform.Find("Contents").Find("Ingre_7").Find("Ingre").GetComponent<Theory_Core_Selecter>().selected = true;
                break;
            case 8:
                GameObject.Find("Room_Part_6").transform.Find("Contents").Find("Ingre_8").Find("Ingre").GetComponent<Renderer>().material.SetColor("_OutlineColor", OUTLINE_COLOR.DEFAULT);
                GameObject.Find("Room_Part_6").transform.Find("Contents").Find("Ingre_8").Find("Ingre").GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.2f);
                GameObject.Find("Room_Part_6").transform.Find("Contents").Find("Ingre_8").Find("Ingre").GetComponent<Theory_Core_Selecter>().selected = true;
                break;
        }
    }

    public void Board_6_ele_checker(int num)
    {
        Transform[] list = GameObject.Find("Room_Part_6").transform.Find("Play_Sect").GetComponentsInChildren<Transform>();
        
        for (int i = 0; i < list.Length; i++)
        {
            if (list[i].gameObject.name.Equals(num.ToString()))
            {
                list[i].GetComponent<Theory_Ele_Selecter>().selected = true;
                list[i].gameObject.GetComponent<Renderer>().material.SetColor("_OutlineColor", OUTLINE_COLOR.DEFAULT);
                list[i].gameObject.GetComponent<Renderer>().material.SetFloat("_Outline", 0.2f);
            }
            else if (list[i] != GameObject.Find("Room_Part_6").transform.Find("Play_Sect").GetComponent<Transform>())
            {
                list[i].GetComponent<Theory_Ele_Selecter>().selected = false;
                list[i].gameObject.GetComponent<Renderer>().material.SetFloat("_Outline", 0.0f);
            }
        }
    }

    public void Board_6_stamp_checker(int num)
    {
        Transform[] list = GameObject.Find("Room_Part_6").transform.Find("Play_Sect").GetComponentsInChildren<Transform>();

        for (int i = 0; i < list.Length; i++)
        {
            if (list[i].gameObject.name.Equals(num.ToString()))
            {
                list[i].GetComponent<Theory_Stamp_Selecter>().selected = true;
                list[i].gameObject.GetComponent<Renderer>().material.SetColor("_OutlineColor", OUTLINE_COLOR.DEFAULT);
                list[i].gameObject.GetComponent<Renderer>().material.SetFloat("_Outline", 0.2f);
            }
            else if (list[i] != GameObject.Find("Room_Part_6").transform.Find("Play_Sect").GetComponent<Transform>())
            {
                list[i].GetComponent<Theory_Stamp_Selecter>().selected = false;
                list[i].gameObject.GetComponent<Renderer>().material.SetFloat("_Outline", 0.0f);
            }
        }
    }

    public void Board_9_potion_checker(string name)
    {
        Transform[] list = GameObject.Find("Room_Part_9").transform.Find("Select_Sect").GetComponentsInChildren<Transform>();

        for(int i = 0; i < list.Length; i++)
        {
            if (list[i].gameObject.name.Equals(name))
            {
                list[i].GetComponent<Exhibit_Potion_Selecter>().selected = true;
                list[i].gameObject.GetComponent<Renderer>().material.SetColor("_OutlineColor", OUTLINE_COLOR.HIGHLIGHT);
                list[i].gameObject.GetComponent<Renderer>().material.SetFloat("_Outline", .2f);
            }
            else if (list[i] != GameObject.Find("Room_Part_9").transform.Find("Select_Sect").GetComponent<Transform>())
            {
                list[i].GetComponent<Exhibit_Potion_Selecter>().selected = false;
                list[i].gameObject.GetComponent<Renderer>().material.SetFloat("_Outline", 0.0f);
            }
        }
    }

    public void Board_9_ingre_checker(string name)
    {
        Transform[] list = GameObject.Find("Room_Part_9").transform.Find("Ingre_Sect").GetComponentsInChildren<Transform>();
        if (data_hub.Select_ingre[0].Equals("card_0"))
            data_hub.Select_ingre[0] = name;

        else if (!data_hub.Select_ingre[0].Equals("card_0") && data_hub.Select_ingre[0].Equals(name))
            data_hub.Select_ingre[0] = "card_0";

        else if (!data_hub.Select_ingre[0].Equals("card_0") && !data_hub.Select_ingre[0].Equals(name) && data_hub.Select_ingre[1].Equals("card_0"))
            data_hub.Select_ingre[1] = name;

        else if (!data_hub.Select_ingre[0].Equals("card_0") &&
                 !data_hub.Select_ingre[0].Equals(name) &&
                 !data_hub.Select_ingre[1].Equals("card_0") &&
                  data_hub.Select_ingre[1].Equals(name))
            data_hub.Select_ingre[1] = "card_0";
        else
        {
            data_hub.Select_ingre[0] = data_hub.Select_ingre[1];
            data_hub.Select_ingre[1] = name;
        }

        // object highlight 조절
        for (int i = 0; i < list.Length; i++)
        {
            // 0 과 1 에 들어있는 수가 반드시 다르므로 둘 중 하나의 수가 같으면 그 수의 카드는 바뀌어야함
            if (list[i] != GameObject.Find("Room_Part_9").transform.Find("Ingre_Sect").transform)
            {
                if (data_hub.Select_ingre[0].Equals(list[i].gameObject.name) ||
                    data_hub.Select_ingre[1].Equals(list[i].gameObject.name)   )
                {
                    list[i].GetComponent<Exhibit_Ingre_Selecter>().selected = true;
                    list[i].gameObject.GetComponent<Renderer>().material.SetColor("_OutlineColor", OUTLINE_COLOR.DEFAULT);
                    list[i].gameObject.GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.2f);
                }
                else
                {
                    list[i].GetComponent<Exhibit_Ingre_Selecter>().selected = false;
                    list[i].gameObject.GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.0f);
                }
            }
        }
    }
}
