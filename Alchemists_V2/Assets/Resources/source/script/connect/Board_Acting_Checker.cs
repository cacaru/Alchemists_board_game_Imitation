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
        // 3 ������ ���� ���������
        // �� �������� Ȯ���� ��
        // �� ���ʶ�� -> 2���� if
        // ���忡 ������ ���ϰ�
        // 3���� ������ 0, -1, -1�� �ʱ�ȭ
        if (board_num > 0 &&                    // �����ȣ
            board_order >= 0 &&                 // ���� ���带 �����ϴ� ���� ��ȣ
            board_cube_order >= 0 &&            // ���� ���带 �����ϴ� ������ ť�� ��ȣ
            board_order == my_board_order       // ���� ���带 �����ϴ� ������ ��ȣ�� �� ������ ���� ��ȣ�� ����!
            )
        {
            // ���� ��ȣ�� ���� �ൿ�� �޶����
            switch (board_num)
            {
                case 1:     // ��� �ޱ�
                    // 6���� ��� ��ϵ鿡 ���� Object_selecter ���̱�
                    Transform[] list = GameObject.Find("Room_Part_1").transform.Find("Show_Ingre_Sect").GetComponentsInChildren<Transform>();
                    for(int i = 0; i < list.Length; i++)
                    {
                        if( list[i] != GameObject.Find("Room_Part_1").transform.Find("Show_Ingre_Sect").transform)
                            list[i].gameObject.AddComponent<Object_Selecter>();
                    }
                    // ���� Ȯ�� ��ư ǥ��
                    GameObject.Find("Switch_Area").transform.Find("Board_act_end").gameObject.SetActive(true);
                    GameObject.Find("Switch_Area").transform.Find("Board_act_pass").gameObject.SetActive(true);

                    // �� ������ �����ȣ�� ������ 3���� ������ �ʱ�ȭ
                    Init_board_val();
                    break;

                case 2:     // ��� �Ǹ�
                    // ������Ʈ�� ���������
                    GameObject.Find("Data_Controller").GetComponent<Ingre_Selecter_for_Play>().Construct_ingre_list(board_num, data_hub.My_data);
                    // ���� Ȯ�� ��ư ǥ��
                    GameObject.Find("Switch_Area").transform.Find("Board_act_end").gameObject.SetActive(true);
                    GameObject.Find("Switch_Area").transform.Find("Board_act_pass").gameObject.SetActive(true);

                    // �� ������ �����ȣ�� ������ 3���� ������ �ʱ�ȭ
                    Init_board_val();
                    break;

                case 4:     // ���� ����
                    // ���� �տ� ���� ��ư Ȱ��ȭ -> ��ư Ŭ���� -> ���� Ȯ��â -> ���� �Ϸ�
                    GameObject.Find("Room_Part_4").transform.Find("Btn_Sect").gameObject.SetActive(true);

                    // �� ������ �����ȣ�� ������ 3���� ������ �ʱ�ȭ
                    Init_board_val();
                    break;

                case 5:     // �� �ݹ�
                    // �ݹ��� ��� ���� -> ���� ���� -> ��� ���� -> �ݹ� ��� â
                    if (data_hub.Core_end_5 == false &&
                        data_hub.Ori_ele_end_5 == false  )
                    {
                        // �ݹ��� ��� ����
                        // ��� ���� script on
                        GameObject.Find("Data_Controller").GetComponent<Data_Controller>().Controller_theory_core_selecter(5, true);
                        // ����Ȯ�� ��ư ǥ��
                        GameObject.Find("Switch_Area").transform.Find("Board_act_end").gameObject.SetActive(true);
                        GameObject.Find("Switch_Area").transform.Find("Board_act_pass").gameObject.SetActive(true);
                    }
                    else if (data_hub.Core_end_5 &&
                             data_hub.Ori_ele_end_5 == false )
                    {
                        // ���� ����
                        GameObject.Find("Data_Controller").GetComponent<Theory_Refute_Obj_for_Play>().Ele_open();
                        // ����Ȯ�� ��ư ǥ��
                        GameObject.Find("Switch_Area").transform.Find("Board_act_end").gameObject.SetActive(true);
                        GameObject.Find("Switch_Area").transform.Find("Board_act_pass").gameObject.SetActive(true);
                    }
                    else if (data_hub.Core_end_5 &&
                             data_hub.Ori_ele_end_5  )
                    {
                        // ��� ���� 
                        GameObject.Find("Data_Controller").GetComponent<Theory_Refute_Obj_for_Play>().Ingre_open();
                        // ����Ȯ�� ��ư ǥ��
                        GameObject.Find("Switch_Area").transform.Find("Board_act_end").gameObject.SetActive(true);
                        GameObject.Find("Switch_Area").transform.Find("Board_act_pass").gameObject.SetActive(true);

                        Init_board_val();
                    }
                    break;

                case 6:     // �� ��ǥ
                    // ��ǥ�� ��� ���� -> ���� ���� -> ���� ���� -> ��ǥ �Ϸ�â
                    if (data_hub.Core_end_6 == false    &&
                        data_hub.Element_end_6 == false  )
                    {
                        //Debug.Log("board_6_checking_1");
                        // ��ǥ�� ��� ���� script true�� ����
                        GameObject.Find("Data_Controller").GetComponent<Data_Controller>().Controller_theory_core_selecter(6, true);
                        // ����Ȯ�� ��ư ǥ��
                        GameObject.Find("Switch_Area").transform.Find("Board_act_end").gameObject.SetActive(true);
                        GameObject.Find("Switch_Area").transform.Find("Board_act_pass").gameObject.SetActive(true);
                    }
                    else if (data_hub.Core_end_6             && 
                             data_hub.Element_end_6 == false  )
                    {
                        //Debug.Log("board_6_checking_2");
                        // ���� ���� - element_end_6
                        GameObject.Find("Data_Controller").GetComponent<Theory_Present_Obj_for_Play>().Ele_open();
                        // ����Ȯ�� ��ư ǥ��
                        GameObject.Find("Switch_Area").transform.Find("Board_act_end").gameObject.SetActive(true);
                        GameObject.Find("Switch_Area").transform.Find("Board_act_pass").gameObject.SetActive(true);
                    }
                    else if (data_hub.Core_end_6 &&
                             data_hub.Element_end_6  )
                    {
                        //Debug.Log("board_6_checking_3");
                        // ���� ���� - stamp_end_6
                        GameObject.Find("Data_Controller").GetComponent<Theory_Present_Obj_for_Play>().Stamp_open();
                        // ����Ȯ�� ��ư ǥ��
                        GameObject.Find("Switch_Area").transform.Find("Board_act_end").gameObject.SetActive(true);
                        GameObject.Find("Switch_Area").transform.Find("Board_act_pass").gameObject.SetActive(true);
                        Init_board_val();
                    }
                    break;

                case 7:     // �л� ����
                    // ��� ���� -> ��� ��ο��� ����
                    // �����ʸ� ��������� : �̰��� ������ ������ �� ��!
                    GameObject.Find("Data_Controller").GetComponent<Ingre_Selecter_for_Play>().Construct_ingre_list(board_num, data_hub.My_data);
                    // ���� Ȯ�� ��ư Ȱ��ȭ
                    GameObject.Find("Switch_Area").transform.Find("Board_act_end").gameObject.SetActive(true);
                    GameObject.Find("Switch_Area").transform.Find("Board_act_pass").gameObject.SetActive(true);

                    // �� ������ �����ȣ�� ������ 3���� ������ �ʱ�ȭ
                    Init_board_val();
                    break;

                case 8:     // ���� ����
                    // ��� ���� -> ��� ��ο��� ����
                    GameObject.Find("Data_Controller").GetComponent<Ingre_Selecter_for_Play>().Construct_ingre_list(board_num, data_hub.My_data);
                    // ���� Ȯ�� ��ư Ȱ��ȭ
                    GameObject.Find("Switch_Area").transform.Find("Board_act_end").gameObject.SetActive(true);
                    GameObject.Find("Switch_Area").transform.Find("Board_act_pass").gameObject.SetActive(true);

                    // �� ������ �����ȣ�� ������ 3���� ������ �ʱ�ȭ
                    Init_board_val();
                    break;

                case 9:     // ����ȸ
                    // ������ ���� ���� -> ��� ǥ�� �� ��� ���� -> ��� ���� ��� Ȯ�� -> �����ϸ� ����â�� �Բ� ť�� �� ����, �����ϸ� ���� ĭ���� ���� �� -1 + ��� �ȳ�
                    // ���� ���� ����, ��� ���� ����, �Ѵ� true �� ��� Ȯ�� �� ť�� ���۽���
                    if (data_hub.Exhibit_potion_step == false)
                    {
                        // ���� ���� ������ ť�� open
                        GameObject.Find("Room_Part_9").transform.Find("Select_Sect").gameObject.SetActive(true);

                        // ���� Ȯ�� ��ư Ȱ��ȭ
                        GameObject.Find("Switch_Area").transform.Find("Board_act_end").gameObject.SetActive(true);
                        GameObject.Find("Switch_Area").transform.Find("Board_act_pass").gameObject.SetActive(true);
                    }
                    else if (data_hub.Exhibit_potion_step)
                    {
                        // ���õ� ������ ��ġ���� ��� open
                        GameObject.Find("Data_Controller").GetComponent<Exhibit_Ingre_for_Play>().Draw_ingre_for_exhibition(data_hub.Exhibit_select_potion);
                    }

                    break;

                case -1:
                    // ���� Ȯ�� ��ư ��Ȱ��ȭ
                    GameObject.Find("Switch_Area").transform.Find("Board_act_end").gameObject.SetActive(false);
                    GameObject.Find("Switch_Area").transform.Find("Board_act_pass").gameObject.SetActive(false);
                    break;
            }


        }
    }
    
    // �뺴���� �����Ǹ� ���ܿ��� ���� Ư�� ������ ���� ����
    private void adv_checking()
    {
        // �� ���ʰ� ����Ǿ��ִٸ� �ٸ� ��� �����Ӹ� �˸��� �ƹ��͵� ���� ���ƾ���
        if (data_hub.My_board_3_end)
        {
            GameObject.Find("Room_Part_3").transform.Find("Ingre_Select_Sect").Find("Waiting_announce").gameObject.SetActive(true);
            return;
        }

        // ����ī�� ���� -> ���� �ߵ�
        // ���� ���� ���� �Ϸ� bool, �ܺ� ���� ���� �Ϸ� bool
        // my_coin_step_end ,, coin_step_end
        if (data_hub.My_coin_step_end == false      &&
            data_hub.Coin_step_end == false         &&
            data_hub.Price_step_end == false        &&
            data_hub.Potion_step_end == false)
        {
            // ������Ʈ�� �̹� ������ �����Ǽ��� �ȵ�
            int obj_count = GameObject.Find("Room_Part_3").transform.Find("Play_Sect").childCount;
            if ( obj_count < 1)
            {
                // ���� ���� ������Ʈ ����
                adv_obj_maker.Discount_obj_show(data_hub.My_data);
            }
            // ���� ��ư ����
            GameObject.Find("Switch_Area").transform.Find("Board_act_end").gameObject.SetActive(true);
            GameObject.Find("Switch_Area").transform.Find("Board_act_pass").gameObject.SetActive(true);
        }

        // �ǸŰ� ����   
        else if (data_hub.My_coin_step_end        &&
                 data_hub.Coin_step_end           &&
                 data_hub.Price_step_end == false &&
                 data_hub.Potion_step_end == false  )
        {
            Debug.Log(data_hub.Selling_turn);
            // �ǸŰ� ������Ʈ ����
            if (data_hub.Dis_coin_data[data_hub.Selling_turn].user_key == data_hub.My_key)
            {
                GameObject.Find("Room_Part_3").transform.Find("Ingre_Select_Sect").Find("Waiting_announce").gameObject.SetActive(false);
                adv_obj_maker.Price_obj_show();
                // ���� ��ư ����
                GameObject.Find("Switch_Area").transform.Find("Board_act_end").gameObject.SetActive(true);
                GameObject.Find("Switch_Area").transform.Find("Board_act_pass").gameObject.SetActive(true);
            }
            // �� ���ʰ� �ƴϸ� ��� ���� �ۼ�
            else
            {
                // �ٸ� ������ �����Դϴ�. ��� ��ٷ��ּ���...
                GameObject.Find("Room_Part_3").transform.Find("Ingre_Select_Sect").Find("Waiting_announce").gameObject.SetActive(true);
            }
        }
        // ���� ����-> �� ���� �϶� 1��
        else if (data_hub.My_coin_step_end        &&
                 data_hub.Coin_step_end           &&
                 data_hub.Price_step_end          &&
                 data_hub.Potion_step_end == false  )
        {
            // ���� ��ư ����
            GameObject.Find("Switch_Area").transform.Find("Board_act_end").gameObject.SetActive(true);
            GameObject.Find("Switch_Area").transform.Find("Board_act_pass").gameObject.SetActive(true);
            adv_obj_maker.Potion_obj_show(data_hub.Adventurer_card_data, data_hub.Random_adv_list[data_hub.Round_cont - 2]);
        }
        // ��� ���� -> �� ������ �� 2�� 
        else if (data_hub.My_coin_step_end &&
                 data_hub.Coin_step_end    &&
                 data_hub.Price_step_end   &&
                 data_hub.Potion_step_end    )
        {
            // ���� ��ư ����
            GameObject.Find("Switch_Area").transform.Find("Board_act_end").gameObject.SetActive(true);
            GameObject.Find("Switch_Area").transform.Find("Board_act_pass").gameObject.SetActive(true);

            // ��� ǥ��
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
        

        // ��ȣ �ֱ�
        // ���� ���� ������ �ʱ�ȭ ����
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

        // �Ѵ� ���ְ�, �Ѵ� �ٸ� ���� ������ ���� ���ڸ� ���� �ڿ� �ֱ�
        else
        {
            data_hub.Select_ingre[0] = data_hub.Select_ingre[1];
            data_hub.Select_ingre[1] = "card_" + ingre_num;
        }

        for (int i = 0; i < list.Length; i++)
        {
            // 0 �� 1 �� ����ִ� ���� �ݵ�� �ٸ��Ƿ� �� �� �ϳ��� ���� ������ �� ���� ī��� �ٲ�����
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
        // ���� ����
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

        // ���õȰ� selected ���� �����
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

        // �Ѵ� ���ְ�, �Ѵ� �ٸ� ���� ������ ���� ���ڸ� ���� �ڿ� �ֱ�
        else
        {
            data_hub.Ingre_arr[0] = data_hub.Ingre_arr[1];
            data_hub.Ingre_arr[1] = num;
        }

        for (int i = 0; i < list.Length; i++)
        {
            // 0 �� 1 �� ����ִ� ���� �ݵ�� �ٸ��Ƿ� �� �� �ϳ��� ���� ������ �� ���� ī��� �ٲ�����
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
        // ���� ����
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

        // ���õȰ� selected ���� �����
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

        // object highlight ����
        for (int i = 0; i < list.Length; i++)
        {
            // 0 �� 1 �� ����ִ� ���� �ݵ�� �ٸ��Ƿ� �� �� �ϳ��� ���� ������ �� ���� ī��� �ٲ�����
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
