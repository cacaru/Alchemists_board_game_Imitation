using Alchemists_data;
using Self_Converter;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Socket_In_Board : MonoBehaviour
{
    private GameObject show_area;   // �����Ǵ� �����͸� �����ϱ� ���� �����ص�
    private GameObject side_menu;   // ���̵�޴� 

    // ���� ����
    private Color ORDER_CUBE_DEFAULT = new(82 / 255f, 82 / 255f, 82 / 255f, 1);
    private Color ORDER_TEXT_HILIGHT = new(134 / 255f, 255 / 255f, 235 / 255f, 1);
    private Color ORDER_TEXT_DEFAULT = Color.white;
    private readonly string DEFAULT_ANNOUNCE_IN_BOARD = "wasd - �̵� \n ����alt - �̵��Ұ�/ȭ�������ϱ� \n ��Ŭ�� - ������Ʈ ����";
    private readonly string DEFAULT_ANNOUNCE_IN_CUBE = "������� å�� ���� \n�̹� ������ ť�� ������ �����մϴ�.";
    private readonly string DEFAULT_ANNOUNCE_IN_ACT = "tŰ Ȥ�� ����Ȯ�� ��ư���� �ൿ�� Ȯ���մϴ�.";
    private readonly string WAITING_ANNOUNCE = "�ٸ� ����� �ൿ Ȯ���� ��ٸ��� ���Դϴ�...";

    private Data_Hub data_hub;
    private Board_Acting_Checker act_checker;
    private GameObject order_cube_selecter;
    private GameObject result_sect;

    public TMP_Text chat_area_obj;
    public TMP_InputField my_chat_input_obj;

    private bool round_end_checker = false;
    private bool my_act_end = false;
    private void Awake()
    {
        // �� ������ ������ Ʋ �����صα�
        show_area = GameObject.Find("GUI").transform.Find("Canvas").Find("Show_area").gameObject;
        side_menu = GameObject.Find("GUI").transform.Find("Canvas").Find("Show_area").Find("Side_menu").gameObject;
        result_sect = GameObject.Find("GUI").transform.Find("Canvas").Find("Switch_Area").Find("Result_Sect").gameObject;

        data_hub = GameObject.Find("Data_Hub").GetComponent<Data_Hub>();
        act_checker = GameObject.Find("Data_Controller").GetComponent<Board_Acting_Checker>();
        order_cube_selecter = GameObject.Find("Select_order_obj").transform.Find("Order_Cube_Selecter").gameObject;

        data_hub.Active_checker();
        data_hub.Select_ingre[0] = "";
        data_hub.Select_ingre[1] = "";
    }

    // Start is called before the first frame update
    void Start()
    {
        // ù ���� ������ ���� ����
        for( int i = 0; i < data_hub.User_data_array.Count; i++)
        {
            if(data_hub.User_data_array[i].User_key.Equals(data_hub.My_key))
            {
                switch (data_hub.User_data_array[i].User_color)
                {
                    case "red":
                        data_hub.My_color_code = (int)User_Color_Code.Red;
                        break;
                    case "blue":
                        data_hub.My_color_code = (int)User_Color_Code.Blue;
                        break;
                    case "black":
                        data_hub.My_color_code = (int)User_Color_Code.Black;
                        break;
                    case "white":
                        data_hub.My_color_code = (int)User_Color_Code.White;
                        break;
                }
            }
            // ���ϴ� ���� ���� ����
            // ���� ���� ������ ť�긦 �����ϴ� ���� < > ���尡 �����ϸ� ������ ����
            // start �� �ʱ�ȭ �̹Ƿ� ť�긦 �����ϴ� ���� -> ó���� ������ �������
            string order_text = "order_" + (i+1).ToString();
            //Debug.Log(order_text);
            GameObject.Find("GUI").transform.Find("Canvas").Find("Show_area").Find("Order_panel").Find(order_text).GetComponent<TMP_Text>().text = data_hub.User_data_array[i].User_name;
        }

        // �ൿ����ť�긦 ������ ������Ʈ�� Ȱ��ȭ
        /*
        if (max_count == 4)
            GameObject.Find("Select_order_obj").transform.Find("Over_4").gameObject.SetActive(true);
        else
            GameObject.Find("Select_order_obj").transform.Find("Under_3").gameObject.SetActive(true);*/
        // �������� �ο��� �߰��� ���� �̱���
        GameObject.Find("Select_order_obj").transform.Find("Under_3").gameObject.SetActive(true);

        // ��ġ �ʴ� ������ ����
        // ������ �̸� ����
        side_menu.transform.Find("My_nick").GetComponent<TMP_Text>().text = data_hub.My_name;
        // �� �� ť�� �̹��� ����
        string path = "";
        if(data_hub.My_data.User_color.Equals("black"))  path = "source/img/icon/cube_black";
        else if (data_hub.My_data.User_color.Equals("red")) path = "source/img/icon/cube_red";
        else if (data_hub.My_data.User_color.Equals("white")) path = "source/img/icon/cube_white";
        else if (data_hub.My_data.User_color.Equals("blue")) path = "source/img/icon/cube_blue";
        side_menu.transform.Find("My_color").GetComponent<Image>().sprite = Resources.Load<Sprite>(path);
        // ���� ���� ����
        data_hub.Artifacts_info = GameObject.Find("Data_Controller").GetComponent<Data_Controller>().Init_artifacts_info();
        
        // �����Ǵ� �����ʹ� �Լ��� ����
        Board_Data_Control.Control(show_area, data_hub.My_data);

        // socket.emit �⺻ ���� �ޱ� ���� ħ
        data_hub.Socket.Emit("created_data_announce", data_hub.Room_name);
        // socket.on start
        Listen_socket();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        
    }

    /// <summary>
    ///  socket.on container
    /// </summary>
    private void Listen_socket()
    {
        // ä�� �ޱ�
        data_hub.Socket.OnUnityThread("chat", (data) =>
        {
            //Debug.Log(data);

            // data :: speaker , msg, type
            chat_area_obj.text += StoD_converter.Resp_to_Chat_Data(data);
            Scroll_to_Bottom.Scroll_to_bottom(chat_area_obj.transform.parent.parent.parent.gameObject.GetComponent<ScrollRect>());

        });

        // �뺴���� �Ǹ� ������ ���� �ޱ�
        data_hub.Socket.OnUnityThread("adv_sell_potion_list", (resp) => {
            data_hub.Adventurer_card_data = StoD_converter.Resp_to_Adventurer_Card_Data(resp);
            //Debug.Log(resp);
        });

        // �̹� ���ӿ� ���� �뺴�� ��ȣ���� �ޱ�
        data_hub.Socket.OnUnityThread("adv_list_setting", (resp) => {
            // resp to int[]             // [[6,2,1,4,3]]
            //Debug.Log(resp);
            int[] random_adv_list = new int[5];
            string[] t = resp.ToString().Replace("[", "").Replace("]", "").Trim().Split(",");
            random_adv_list[0] = int.Parse(t[0]);
            random_adv_list[1] = int.Parse(t[1]);
            random_adv_list[2] = int.Parse(t[2]);
            random_adv_list[3] = int.Parse(t[3]);
            random_adv_list[4] = int.Parse(t[4]);

            // data_hub�� �α�
            data_hub.Random_adv_list = random_adv_list;
            /*
            foreach (var item in random_adv_list)
            {
                Debug.Log(item);
            }
            */
        });

        // ���� ���� ���� ���� �ޱ�
        data_hub.Socket.OnUnityThread("round_order_setting_before", (resp) => {
            // resp to string[]            //[["-NOR9jrIce9Nui-yAAAB","GIc-xRSrZMwDBWvpAAAE"]]
            //Debug.Log(resp);
            string[] t = resp.ToString().Replace("[", "").Replace("]", "").Replace("\"", "").Trim().Split(",");
            string[] order = new string[4];
            for (int i = 0; i < t.Length; i++)
            {
                order[i] = t[i];
            }
            data_hub.Order = order;
            // �� ���� �˾Ƴ���
            // ���� â�� �̸� ���� -> �ִ� 16�� ���� �ڵ�...

            for (int i = 0; i < data_hub.Max_count; i++)
            {
                for (int j = 0; j < data_hub.User_data_array.Count; j++)
                {
                    if (order[i].Equals(data_hub.User_data_array[j].User_key))
                    {
                        string order_text = "order_" + (i + 1).ToString();
                        show_area.transform.Find("Order_panel").Find(order_text).GetComponent<TMP_Text>().text = data_hub.User_data_array[j].User_name;
                        break;
                    }
                }
            }


            if (data_hub.Decide_round_order_cont == data_hub.My_order_cube_select_turn)
            {
                //Debug.Log("in_point");
                // ť�� Ŭ���� �����ϰ� ����
                order_cube_selecter.SetActive(true);

                // �غ� �Ϸ� ��ư Ȱ��ȭ
                GameObject.Find("Switch_Area").transform.Find("Round_order_ready").gameObject.SetActive(true);
                data_hub.Decide_round_order_cont = -1;
            }
        });

        // ������ �� �ִ� ��� ī�� ����
        data_hub.Socket.OnUnityThread("ingredient_select_card_open", (resp) => {
            // resp to int[]  // [[3,8,1,3,4]]
            //Debug.Log(resp);
            string[] t = resp.ToString().Replace("[", "").Replace("]", "").Trim().Split(",");
            int[] temp = new int[5];
            temp[0] = int.Parse(t[0]);
            temp[1] = int.Parse(t[1]);
            temp[2] = int.Parse(t[2]);
            temp[3] = int.Parse(t[3]);
            temp[4] = int.Parse(t[4]);
            data_hub.Ingredient_select_arr = temp;
            /*
            foreach(var item in ingredient_select_arr)
            {
                Debug.Log(item);
            }
            */

            // ȭ�鿡 ����
            GameObject.Find("Data_Controller").GetComponent<Data_Controller>().Controller_select_ingre(data_hub.Ingredient_select_arr);
        });

        // ���� ���� �ޱ�
        data_hub.Socket.OnUnityThread("get_ingame_data", (resp) => {
            data_hub.User_data_array = StoD_converter.Resp_to_User_Data(resp);

            // GUI ǥ�� ���� ����
            Board_Data_Control.Control(show_area, data_hub.My_data);
            Board_Data_Control.Other_data_setting(show_area.transform, data_hub.User_data_array, data_hub.My_key);
            // ������Ʈ�� ǥ�õ� ���� ����
            GameObject.Find("Data_Controller").GetComponent<Data_Controller>().Controller_ingre_count_sign(data_hub.My_data);
            // ��밡�� ť�� ���� ǥ��
            GameObject.Find("Data_Controller").GetComponent<Data_Controller>().Controller_showing_cube(data_hub.My_data);

            // �濡 ���������� ������ ���̹Ƿ�, chat�� ���� �ȳ��޽����� �Ѹ�
            Alc_Data send_data = new Alc_Builder().Speaker(data_hub.My_name)
                                                  .Msg("�濡 �����Ͽ����ϴ�.")
                                                  .Type("announce")
                                                  .Room_name(data_hub.Room_name)
                                                  .Chat_Data_Build();
            //Debug.Log(send_data.print());
            data_hub.Socket.Emit("chat", send_data);
        });

        // ���� ���� ����
        data_hub.Socket.OnUnityThread("change_user_data", (resp) => {
            data_hub.User_data_array = StoD_converter.Resp_to_User_Data(resp);

            // GUI ǥ�� ���� ����
            Board_Data_Control.Control(show_area, data_hub.My_data);
            Board_Data_Control.Other_data_setting(show_area.transform, data_hub.User_data_array, data_hub.My_key);
            // ������Ʈ�� ǥ�õ� ���� ����
            GameObject.Find("Data_Controller").GetComponent<Data_Controller>().Controller_ingre_count_sign(data_hub.My_data);
            // ��� ���� ť�� ���� ǥ��
            GameObject.Find("Data_Controller").GetComponent<Data_Controller>().Controller_showing_cube(data_hub.My_data);

            // ���� �غ� �Ϸ� ���¶�� ���̻� btn�� ǥ������ ����.
            if (data_hub.My_data.Is_ready.Equals("true"))
                show_area.transform.Find("GUI_btn_area").Find("Ready_btn").gameObject.SetActive(false);
            // �ٸ� ���� �غ�Ϸ� ���µ� Ȯ��
            Board_Data_Control.Ready_player_shower(show_area, data_hub.User_data_array);
        });

        // �ൿ ������ ������ ť�긦 ������ ������ ��ȣ�� ���� 
        // 1�� ������ ù��° ������ ť�� ������ ���� , 2~4 ������ ���� ���ϰ� ���� ���� �뵵 ( ������ ���� �����޾� Ȯ����)
        data_hub.Socket.OnUnityThread("decide_round_setting_order_counter_send", (resp) => {
            // resp to int // [[0]]
            string t = resp.ToString().Replace("[", "").Replace("]", "").Trim();
            data_hub.Decide_round_order_cont = int.Parse(t);
            //Debug.Log("decide_round_order_cont");
            //Debug.Log(decide_round_order_cont);

            // ��� ������ �� ����
            show_area.transform.Find("Order_panel").Find("order_1").GetComponent<TMP_Text>().fontStyle = (FontStyles)FontStyle.Normal;
            show_area.transform.Find("Order_panel").Find("order_1").GetComponent<TMP_Text>().color = ORDER_TEXT_DEFAULT;
            show_area.transform.Find("Order_panel").Find("order_2").GetComponent<TMP_Text>().fontStyle = (FontStyles)FontStyle.Normal;
            show_area.transform.Find("Order_panel").Find("order_2").GetComponent<TMP_Text>().color = ORDER_TEXT_DEFAULT;
            show_area.transform.Find("Order_panel").Find("order_3").GetComponent<TMP_Text>().fontStyle = (FontStyles)FontStyle.Normal;
            show_area.transform.Find("Order_panel").Find("order_3").GetComponent<TMP_Text>().color = ORDER_TEXT_DEFAULT;
            show_area.transform.Find("Order_panel").Find("order_4").GetComponent<TMP_Text>().fontStyle = (FontStyles)FontStyle.Normal;
            show_area.transform.Find("Order_panel").Find("order_4").GetComponent<TMP_Text>().color = ORDER_TEXT_DEFAULT;
            // ���� ������ �� ĥ�ϱ�
            switch (data_hub.Decide_round_order_cont)
            {
                case 0:
                    show_area.transform.Find("Order_panel").Find("order_1").GetComponent<TMP_Text>().fontStyle = (FontStyles)FontStyle.Bold;
                    show_area.transform.Find("Order_panel").Find("order_1").GetComponent<TMP_Text>().color = ORDER_TEXT_HILIGHT;
                    break;
                case 1:
                    show_area.transform.Find("Order_panel").Find("order_2").GetComponent<TMP_Text>().fontStyle = (FontStyles)FontStyle.Bold;
                    show_area.transform.Find("Order_panel").Find("order_2").GetComponent<TMP_Text>().color = ORDER_TEXT_HILIGHT;
                    break;
                case 2:
                    show_area.transform.Find("Order_panel").Find("order_3").GetComponent<TMP_Text>().fontStyle = (FontStyles)FontStyle.Bold;
                    show_area.transform.Find("Order_panel").Find("order_3").GetComponent<TMP_Text>().color = ORDER_TEXT_HILIGHT;
                    break;
                case 3:
                    show_area.transform.Find("Order_panel").Find("order_4").GetComponent<TMP_Text>().fontStyle = (FontStyles)FontStyle.Bold;
                    show_area.transform.Find("Order_panel").Find("order_4").GetComponent<TMP_Text>().color = ORDER_TEXT_HILIGHT;
                    break;
            }
            //Debug.Log(my_order_cube_select_turn);
            if (data_hub.Decide_round_order_cont == data_hub.My_order_cube_select_turn)
            {
                // ť�� Ŭ���� �����ϰ� ����
                order_cube_selecter.SetActive(true);

                // �غ� �Ϸ� ��ư Ȱ��ȭ
                GameObject.Find("Switch_Area").transform.Find("Round_order_ready").gameObject.SetActive(true);
            }
        });

        // �Ǹ��� ���� ����
        data_hub.Socket.OnUnityThread("can_buy_artifacts_list", (resp) => {
            // resp to show_artifacts
            //Debug.Log(resp);
            string t = resp.ToString().Replace("[", "").Replace("]", "").Trim();
            int x = t.IndexOf("rank", 0);
            data_hub.Now_rank = int.Parse(t[(x + 6)..(x + 7)]);

            // �����ʹ� 0���� 5���� �����µ� ������ 1~6������
            string tmp = t[0..(x - 2)];
            tmp = tmp.Split(":")[1];            
            int[] temp = new int[3];
            temp[0] = int.Parse(tmp.Split(",")[0]) + 1;
            temp[1] = int.Parse(tmp.Split(",")[1]) + 1;
            temp[2] = int.Parse(tmp.Split(",")[2]) + 1;
            data_hub.Now_selling_arti_num = temp;

            GameObject.Find("Data_Controller").GetComponent<Data_Controller>().Controller_selling_arti(data_hub.Now_rank, data_hub.Now_selling_arti_num, data_hub.Artifacts_info);
            /*
            Debug.Log(now_rank);
            Debug.Log(now_selling_arti_num[0]);
            Debug.Log(now_selling_arti_num[1]);
            Debug.Log(now_selling_arti_num[2]);
            */

        });

        // ���� ���� ����
        // round_cont : int -> ���� ������ Ŭ�� ���ο� ����
        //socket.emit("change_round", room_data[room_name].round_cont);
        data_hub.Socket.OnUnityThread("change_round", (resp) => {
            // resp to int [0]
            //Debug.Log(resp);
            string t = resp.ToString().Replace("[", "").Replace("]", "").Trim();
            data_hub.Round_cont = int.Parse(t);
            /*
            Debug.Log("round_couter");
            Debug.Log(round_cont);
            */

            // ���� ���� ���� ����
            GameObject.Find("Round_announce").transform.Find("round_val").GetComponent<TMP_Text>().text = data_hub.Round_cont.ToString();

            // ���� ������ ���� �뺴ǥ�� ����
            if (data_hub.Round_cont == 2)
            {
                // 2���� ���� ������ ���Ƶ� 3���� ������
                GameObject.Find("Room_Part_3").transform.Find("Gate").gameObject.SetActive(false);
                GameObject.Find("Room_Part_5").transform.Find("Gate").gameObject.SetActive(false);
                GameObject.Find("Room_Part_6").transform.Find("Gate").gameObject.SetActive(false);
            }

            // �뺴 ǥ��
            if (data_hub.Round_cont >= 2)
            {
                int[] random_adv_list = data_hub.Random_adv_list;
                GameObject.Find("Data_Controller").GetComponent<Data_Controller>().Controller_adventurer_marking(data_hub.Adventurer_card_data, random_adv_list[data_hub.Round_cont - 2]);
            }

            // ������ ���� ����ȸ ����
            if (data_hub.Round_cont == 6)
            {
                Vector3 tmp = GameObject.Find("Objects").transform.Find("Round_Room").Find("Wall").Find("End_Gate").localPosition;
                tmp.y = -16f;
                GameObject.Find("Objects").transform.Find("Round_Room").Find("Wall").Find("End_Gate").localPosition = tmp;
            }
        });

        // ���� �߸� �� ��� ���̺� ����
        // result_table[ { user_key, ingredient_table, ingredient_reasoning } , {}]
        data_hub.Socket.OnUnityThread("change_result_table", (resp) => {
            // resp to result_table // [[{ "user_key":"-NOR9jrIce9Nui-yAAAB","ingredient_reasoning":[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]],"ingredient_result":[["","","","","","","",""],["","","","","","","",""],["","","","","","","",""],["","","","","","","",""],["","","","","","","",""],["","","","","","","",""],["","","","","","","",""],["","","","","","","",""]]},{"user_key":"GIc-xRSrZMwDBWvpAAAE","ingredient_reasoning":[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]],"ingredient_result":[["","","","","","","",""],["","","","","","","",""],["","","","","","","",""],["","","","","","","",""],["","","","","","","",""],["","","","","","","",""],["","","","","","","",""],["","","","","","","",""]]}]]
            data_hub.Result_table = StoD_converter.Resp_to_Reasoning_Table_Data(resp);
            //Debug.Log(resp);

            for(int i = 0; i < data_hub.Result_table.Count; i++)
            {
                if (data_hub.Result_table[i].user_key.Equals(data_hub.My_key))
                {
                    Result_Table_Checker.Result_table_chekcing(data_hub.Result_table[i].ingredient_result);
                    break;
                }
            }
        });

        // �ʱ� �� ������ ����
        // theory_data[{1:element, stamp{1:user_key,color,point ,2 ,3}}, {2:element~ }, ... {8}]
        data_hub.Socket.OnUnityThread("change_theory_data", (resp) => {
            // [{"1":{"element":"","stamp":{"1":{"user_key":"","color":"","point":""},"2":{"user_key":"","color":"","point":""},"3":{"user_key":"","color":"","point":""}}},"2":{"element":"","stamp":{"1":{"user_key":"","color":"","point":""},"2":{"user_key":"","color":"","point":""},"3":{"user_key":"","color":"","point":""}}},"3":{"element":"","stamp":{"1":{"user_key":"","color":"","point":""},"2":{"user_key":"","color":"","point":""},"3":{"user_key":"","color":"","point":""}}},"4":{"element":"","stamp":{"1":{"user_key":"","color":"","point":""},"2":{"user_key":"","color":"","point":""},"3":{"user_key":"","color":"","point":""}}},"5":{"element":"","stamp":{"1":{"user_key":"","color":"","point":""},"2":{"user_key":"","color":"","point":""},"3":{"user_key":"","color":"","point":""}}},"6":{"element":"","stamp":{"1":{"user_key":"","color":"","point":""},"2":{"user_key":"","color":"","point":""},"3":{"user_key":"","color":"","point":""}}},"7":{"element":"","stamp":{"1":{"user_key":"","color":"","point":""},"2":{"user_key":"","color":"","point":""},"3":{"user_key":"","color":"","point":""}}},"8":{"element":"","stamp":{"1":{"user_key":"","color":"","point":""},"2":{"user_key":"","color":"","point":""},"3":{"user_key":"","color":"","point":""}}}}]
            data_hub.Theory_data = StoD_converter.Resp_to_Theory_Data(resp);
            //Debug.Log(resp);

            // ������Ʈ ȭ�鿡 ǥ��
            GameObject.Find("Data_Controller").GetComponent<Data_Controller>().Controller_theory_field(data_hub.Theory_data, data_hub.My_key);
        });


        // ���� ����ȸ �⺻ ���� ����
        // exhibition_result[first{1{user_key,color},2,3,~,6}, secound{7~12}]
        data_hub.Socket.OnUnityThread("change_exhibition_data", (resp) => {
            //{"first":{"1":{"user_key":"","color":""},"2":{"user_key":"","color":""},"3":{"user_key":"","color":""},"4":{"user_key":"","color":""},"5":{"user_key":"","color":""},"6":{"user_key":"","color":""}},"second":{"7":{"user_key":"","color":""},"8":{"user_key":"","color":""},"9":{"user_key":"","color":""},"10":{"user_key":"","color":""},"11":{"user_key":"","color":""},"12":{"user_key":"","color":""}}}]
            data_hub.Exhibition_result = StoD_converter.Resp_to_Exhibition_Result(resp);
            // Debug.Log(resp);

            if (data_hub.Now_board_num == 9)
            {
                Debug.Log("in");
                GameObject.Find("Data_Controller").GetComponent<Data_Controller>().Controller_exhibit_cube_draw(data_hub.Exhibition_result);
            }
        });

        // �ൿ ������ �����ϴ� ť�긦 ������ �� ����ϴ� final_round_order �ޱ�
        data_hub.Socket.OnUnityThread("select_round_order_recive", (resp) => {
            // [[{"user_key":"upo5enxz0PW_laDMAAAI","before_order":0,"order":7,"user_color":"white"},{"user_key":"pzpvRGvqaKwmbTTeAAAG","before_order":0,"order":5,"user_color":"blue"}]]
            data_hub.Final_round_order = StoD_converter.Resp_to_Final_Round_Order_Data(resp);

            // final_round_order�� ���� ���õǾ� �ִ� ť����� ���� ĥ�ϰ�, �ٸ� �̰� ������ �� ������ ���ƾ���
            for (int i = 0; i < data_hub.Final_round_order.Count; i++)
            {
                // ������ ������ ���� �־��ٸ� ���� ��
                if (data_hub.Final_round_order[i].before_order > 0)
                {
                    string pre_cube_name;
                    if (data_hub.Max_count != 4 && data_hub.Final_round_order[i].before_order == 7)
                        pre_cube_name = "Cube_8";
                    else
                        pre_cube_name = "Cube_" + data_hub.Final_round_order[i].before_order.ToString();

                    GameObject.Find(pre_cube_name).GetComponent<Renderer>().material.color = ORDER_CUBE_DEFAULT;
                }

                // ���� ������ ���� ����
                // ť�� ��ȣ�� 7���� �� �ִ� �ο����� ���� 8�� ��������
                string cube_name;
                if (data_hub.Max_count != 4 && data_hub.Final_round_order[i].order == 7)
                    cube_name = "Cube_8";
                else
                    cube_name = "Cube_" + data_hub.Final_round_order[i].order.ToString();

                //Debug.Log(cube_name);
                Color color = Color.gray;
                switch (data_hub.Final_round_order[i].user_color)
                {
                    case "red":
                        color = Color.red;
                        break;
                    case "blue":
                        color = Color.blue;
                        break;
                    case "black":
                        color = Color.black;
                        break;
                    case "white":
                        color = Color.white;
                        break;
                }
                GameObject.Find(cube_name).GetComponent<Renderer>().material.color = color;

            }

            // Order_Cube_Selecter.selected_round_order�� ���� �־��ֱ�
            GameObject.Find("Select_order_obj").transform.GetChild(0).transform.GetComponent<Order_Cube_Select_Event>().selected_round_order = data_hub.Selected_round_order;
        });

        // �ൿ ���� ������ �Ϸ���� �˷���
        data_hub.Socket.OnUnityThread("decide_round_setting_order_end", (resp) => {
            data_hub.Final_round_order = StoD_converter.Resp_to_Final_Round_Order_Data(resp);

            // ť�� Ŭ�� ������ �ʱ�ȭ
            order_cube_selecter.GetComponent<Order_Cube_Select_Event>().cube_num = 0;
            data_hub.Selected_round_order[0] = 0; data_hub.Selected_round_order[1] = 0; data_hub.Selected_round_order[2] = 0; data_hub.Selected_round_order[3] = 0;
            order_cube_selecter.GetComponent<Order_Cube_Select_Event>().selected_round_order = data_hub.Selected_round_order;
            // ť�� Ŭ���� �Ұ����ϰ� ����
            order_cube_selecter.SetActive(false);

            data_hub.My_order_cube_select_turn = -1;
            data_hub.Decide_round_order_cont = -1;

            // fade -in
            GameObject.Find("UI_Event_Obj").transform.GetComponent<Black_Out>().enabled = true;
            GameObject.Find("UI_Event_Obj").transform.GetComponent<Black_Out>().Start_fade(true);

            // �� ������Ʈ �� Ȱ��ȭ
            /*
            if (max_count == 4)
                GameObject.Find("Over_4").SetActive(false);
            else
                GameObject.Find("Under_3").SetActive(false);
            ������ 4�� ������ �Ǿ����� �����Ƿ� 3�� ����
            */
            // �� ������Ʈ�� ť��� �ʱ�ȭ
            GameObject.Find("Data_Controller").GetComponent<Data_Controller>().Init_round_order_cube();
            GameObject.Find("Under_3").SetActive(false);

            // �غ� �Ϸ� ��ư ��Ȱ��ȭ
            GameObject.Find("Switch_Area").transform.Find("Round_order_ready").gameObject.SetActive(false);
            // �Ʒ� ���� ����
            GameObject.Find("Help_announce").transform.Find("text_area").GetComponent<TMP_Text>().text = DEFAULT_ANNOUNCE_IN_BOARD;

            // ���� â�� ���� �غ� �Ϸ� â���� ����
            show_area.transform.Find("Order_panel").gameObject.SetActive(false);
            show_area.transform.Find("GUI_btn_area").gameObject.SetActive(true);
            show_area.transform.Find("Ready_area").gameObject.SetActive(true);

            // ��� ������ ť�� ǥ��
            show_area.transform.Find("Can_use_cube_area").gameObject.SetActive(true);
            GameObject.Find("Data_Controller").GetComponent<Data_Controller>().Controller_showing_cube(data_hub.My_data);

            // �� ������Ʈ ǥ��
            GameObject.Find("Data_Controller").GetComponent<Data_Controller>().Controller_theory_core_init();
        });

        // �� ť�� ������ �޾ƿ�
        data_hub.Socket.OnUnityThread("change_user_cube_data", (resp) => {
            //Debug.Log(resp); //[[2,[{"user_key":"CB1JWEm1Ps08MABqAAAE","user_color":"blue","order":6,"length":3,"cube":{"1":{"num":1,"cnt":1,"is_select":false},"2":{"num":2,"cnt":1,"is_select":false},"3":{"num":3,"cnt":1,"is_select":false}}},{"user_key":"eYiBdllRJVxHdk6MAAAB","user_color":"white","order":7,"length":3,"cube":{"1":{"num":1,"cnt":1,"is_select":false},"2":{"num":2,"cnt":1,"is_select":false},"3":{"num":3,"cnt":1,"is_select":false}}}],[{"user_key":"CB1JWEm1Ps08MABqAAAE","user_color":"blue","order":6,"length":2,"cube":{"1":{"num":1,"cnt":1,"is_select":false},"2":{"num":2,"cnt":2,"is_select":false}}},{"user_key":"eYiBdllRJVxHdk6MAAAB","user_color":"white","order":7,"length":2,"cube":{"1":{"num":1,"cnt":1,"is_select":false},"2":{"num":2,"cnt":2,"is_select":false}}}],[{"user_key":"CB1JWEm1Ps08MABqAAAE","user_color":"blue","order":6,"length":1,"cube":{"1":{"num":1,"cnt":2,"is_select":false}}},{"user_key":"eYiBdllRJVxHdk6MAAAB","user_color":"white","order":7,"length":1,"cube":{"1":{"num":1,"cnt":2,"is_select":false}}}],[{"user_key":"CB1JWEm1Ps08MABqAAAE","user_color":"blue","order":6,"length":2,"cube":{"1":{"num":1,"cnt":1,"is_select":false},"2":{"num":2,"cnt":2,"is_select":false}}},{"user_key":"eYiBdllRJVxHdk6MAAAB","user_color":"white","order":7,"length":2,"cube":{"1":{"num":1,"cnt":1,"is_select":false},"2":{"num":2,"cnt":2,"is_select":false}}}],[{"user_key":"CB1JWEm1Ps08MABqAAAE","user_color":"blue","order":6,"length":2,"cube":{"1":{"num":1,"cnt":1,"is_select":false},"2":{"num":2,"cnt":1,"is_select":false}}},{"user_key":"eYiBdllRJVxHdk6MAAAB","user_color":"white","order":7,"length":2,"cube":{"1":{"num":1,"cnt":1,"is_select":false},"2":{"num":2,"cnt":1,"is_select":false}}}],[{"user_key":"CB1JWEm1Ps08MABqAAAE","user_color":"blue","order":6,"length":2,"cube":{"1":{"num":1,"cnt":1,"is_select":false},"2":{"num":2,"cnt":2,"is_select":false}}},{"user_key":"eYiBdllRJVxHdk6MAAAB","user_color":"white","order":7,"length":2,"cube":{"1":{"num":1,"cnt":1,"is_select":false},"2":{"num":2,"cnt":2,"is_select":false}}}],[{"user_key":"CB1JWEm1Ps08MABqAAAE","user_color":"blue","order":6,"length":2,"cube":{"1":{"num":1,"cnt":1,"is_select":false},"2":{"num":2,"cnt":1,"is_select":false}}},{"user_key":"eYiBdllRJVxHdk6MAAAB","user_color":"white","order":7,"length":2,"cube":{"1":{"num":1,"cnt":1,"is_select":false},"2":{"num":2,"cnt":1,"is_select":false}}}],[{"user_key":"CB1JWEm1Ps08MABqAAAE","user_color":"blue","order":6,"length":2,"cube":{"1":{"num":1,"cnt":1,"is_select":false},"2":{"num":2,"cnt":1,"is_select":false}}},{"user_key":"eYiBdllRJVxHdk6MAAAB","user_color":"white","order":7,"length":2,"cube":{"1":{"num":1,"cnt":1,"is_select":false},"2":{"num":2,"cnt":1,"is_select":false}}}],[{"user_key":"CB1JWEm1Ps08MABqAAAE","user_color":"blue","order":6,"length":4,"cube":{"1":{"num":1,"cnt":1,"is_select":false},"2":{"num":2,"cnt":1,"is_select":false},"3":{"num":3,"cnt":1,"is_select":false},"4":{"num":4,"cnt":1,"is_select":false}}},{"user_key":"eYiBdllRJVxHdk6MAAAB","user_color":"white","order":7,"length":4,"cube":{"1":{"num":1,"cnt":1,"is_select":false},"2":{"num":2,"cnt":1,"is_select":false},"3":{"num":3,"cnt":1,"is_select":false},"4":{"num":4,"cnt":1,"is_select":false}}}]]]
            data_hub.User_cube_data = StoD_converter.Resp_to_User_Cube_Data(resp);
            //Debug.Log(user_cube_data.Count);
            if (data_hub.User_cube_data.Count > 0)
                GameObject.Find("Board_Cube_Controller").GetComponent<Board_Cube_Constructor>().Draw_cubes(data_hub.User_cube_data, data_hub.Round_cont, data_hub.My_key, data_hub.In_round);
        });

        // final_round_order ���� ���� ����
        data_hub.Socket.OnUnityThread("change_final_round_order", (resp) => {
            data_hub.Final_round_order = StoD_converter.Resp_to_Final_Round_Order_Data(resp);
        });

        // �ൿ �������� ť�� ������ �Ұ������� �˸��� �Լ�
        data_hub.Socket.OnUnityThread("cant_use_cube", (resp) => {
            // ["���� ��ġ���� ť�긦 �ξ���մϴ�"] or ["��� ������ ť�갡 �����ϴ�."]
            // �����͸� ������ �ٷ� Help_announce.text_area�� �Ѹ��� -> �ڷ�ƾ���� 5�ʵ� �⺻�ȳ��� ����
            string say = resp.ToString().Replace("[", "").Replace("]", "").Replace("{", "").Replace("}", "").Replace("\"", "").Replace("\\n", "\n").Split(":")[1];

            // �ڷ�ƾ �ۼ� start
            StartCoroutine(Announce_pannel(say, 1));
            //Debug.Log(resp);
            //Debug.Log(say);
        });



        #region ���� �ൿ
        // ������ ���� ��ȣ �ޱ�
        data_hub.Socket.OnUnityThread("board_start", (resp) =>
        {
            round_end_checker = false;
            data_hub.In_round = true;
            // ť�� ������ ���ϰ� redraw
            GameObject.Find("Board_Cube_Controller").GetComponent<Board_Cube_Constructor>().Draw_cubes(data_hub.User_cube_data, data_hub.Round_cont, data_hub.My_key, data_hub.In_round);
            // ī�޶��� �̵��� ������Ŵ 
            GameObject.Find("Moving_obj").GetComponent<Camera_Moving>().enabled = false;

            GameObject.Find("Camera_Controller").GetComponent<Mouse_Switch>().in_round = true;
            // ���� ��ȣ�� ���� subī�޶��� pos�� ��� �̵� ��Ŵ
            //Debug.Log(resp.ToString());
            data_hub.Now_board_num = int.Parse(resp.ToString().Replace("[", "").Replace("]", ""));
            //Debug.Log(now_board_num);

            Vector3 pos = new();
            Vector3 rot = new();
            switch (data_hub.Now_board_num)
            {
                case 1:
                    pos = new Vector3(14f, 7.5f, 53f);
                    rot = new Vector3(0f, 90f, 0f);
                    break;
                case 2:
                    pos = new Vector3(14f, 7.5f, 81f);
                    rot = new Vector3(0f, 90f, 0f);
                    break;
                case 3:
                    pos = new Vector3(14f, 7.5f, 110f);
                    rot = new Vector3(0f, 90f, 0f);
                    break;
                case 4:
                    pos = new Vector3(14f, 7.5f, 137.5f);
                    rot = new Vector3(0f, 90f, 0f);
                    break;
                case 5:
                    pos = new Vector3(-35f, 7.5f, 49f);
                    rot = new Vector3(0f, 0f, 0f);
                    break;
                case 6:
                    pos = new Vector3(-35f, 7.5f, 78f);
                    rot = new Vector3(0f, 0f, 0f);
                    break;
                case 7:
                    pos = new Vector3(-22f, 7.5f, 109f);
                    rot = new Vector3(0f, -90f, 0f);
                    break;
                case 8:
                    pos = new Vector3(-22f, 7.5f, 137.5f);
                    rot = new Vector3(0f, -90f, 0f);
                    break;
                case 9:
                    // �̵������ϰ� �ٽ� �ϰ� ��ġ�� �����
                    pos = new Vector3(-5.4f, 7.5f, 160f);
                    rot = new Vector3(0f, 0f, 0f);
                    break;
            }

            GameObject.Find("Moving_obj").transform.SetPositionAndRotation(pos, Quaternion.Euler(rot));

            // ���ʿ� ������Ʈ ��Ȱ��ȭ
            show_area.transform.Find("GUI_btn_area").Find("Ready_btn").gameObject.SetActive(true);
            show_area.transform.Find("GUI_btn_area").gameObject.SetActive(false);
            show_area.transform.Find("Ready_area").gameObject.SetActive(false);
        });

        // �ൿ�� ���� ��ȣ �ޱ�
        data_hub.Socket.OnUnityThread("change_board_order_val", (resp) => {
            /*
             * board_order : start_board_order,
             * board_cube_order : 1,
             * [{"board_order":0,"board_cube_order":1}]
             */
            //Debug.Log(resp);
            string t = resp.ToString();
            // board_order :: ���� ���� ����� ��ȣ : 0 : ù���� �������� ����, 1 : �ι��� �������� ���� etc
            // board_cube_order :: ���� ���� ������� ť�� ���� ��ȣ , �ּ� 1 �ִ� 3����
            data_hub.Board_order = int.Parse(t[t.IndexOf("board_order") + 13].ToString());
            data_hub.Board_cube_order = int.Parse(t[t.IndexOf("_cube_order") + 13].ToString());

            //Debug.Log(board_order);
            //Debug.Log(board_cube_order);
        });

        #region �뺴 �ŷ� ����
        // ����ī�� ���� ������ ������ ������ �� �޴� �Լ�
        data_hub.Socket.OnUnityThread("end_adv_dis_step", (resp) => {
            //[[{"user_key":"knVwUzIJ4NguKyOLAAAk","color":"white","dis_coin_num":1,"room_name":"qwer"},{"user_key":"6_eiwJi5PWudyOfqAAAn","room_name":"qwer","color":"blue","dis_coin_num":1}]]
            //Debug.Log(resp);
            data_hub.Dis_coin_data = StoD_converter.Resp_to_Discount_Coin_List_Data(resp);
            // ���� ī�� ���� �˸�
            data_hub.Coin_step_end = true;
            // ������ ���� ������Ʈ ����
            GameObject.Find("Data_Controller").GetComponent<Adv_Obj_Construct_for_Play>().Dis_order_show(data_hub.Dis_coin_data);
            data_hub.Now_board_num = 3;
        });

        // �뺴���� �����Ǹſ��� �Ǹűݾ��� ������ �����ϰ� ���� ������ ������ �˸� �Լ�
        data_hub.Socket.OnUnityThread("selling_start", (resp) => {
            // resp == true :: ȣ��ī�� ���
            //      == false :: ȣ��ī�� �̻��
            //Debug.Log(resp);
            data_hub.Now_board_num = 3;
        });

        // �뺴���� ���� �Ǹ� ���ܿ��� ���� ������ �˸�
        data_hub.Socket.OnUnityThread("change_selling_turn", (resp) => {
            //Debug.Log(resp);
            data_hub.Selling_turn = int.Parse(resp.ToString().Replace("[", "").Replace("]", ""));
        });

        // �뺴���� ���� �Ǹſ��� �� ������ ���� �ǸŰ� ����Ǿ����� �˸�
        data_hub.Socket.OnUnityThread("selling_potion_end", (resp) =>
        {
            /*
             *  selling_success : selling_success,
                potion : data.what_kind_sell_potion,
                user_key : data.user_key,
                user_color : data.user_color,
             */
            data_hub.Selling_potion_end = StoD_converter.Resp_to_Selling_Potion_End(resp);

            // �Ǹ� ��� ��� ����
            Sprite tmp = data_hub.Selling_potion_end.what_kind_sell_potion switch
            {
                "red_1" => Resources.Load<Sprite>("source/img/potion/red_+"),
                "red_0" => Resources.Load<Sprite>("source/img/potion/red_-"),
                "green_1" => Resources.Load<Sprite>("source/img/potion/green_+"),
                "green_0" => Resources.Load<Sprite>("source/img/potion/green_-"),
                "blue_1" => Resources.Load<Sprite>("source/img/potion/blue_+"),
                "blue_0" => Resources.Load<Sprite>("source/img/potion/blue_-"),
                _ => Resources.Load<Sprite>("source/img/potion/blank"),
            };

            result_sect.transform.Find("Normal_Form").Find("Potion").gameObject.GetComponent<Image>().sprite = tmp;

            tmp = data_hub.Selling_potion_end.user_color switch
            {
                "red" => Resources.Load<Sprite>("source/img/icon/cube_red"),
                "blue" => Resources.Load<Sprite>("source/img/icon/cube_blue"),
                "black" => Resources.Load<Sprite>("source/img/icon/cube_black"),
                "white" => Resources.Load<Sprite>("source/img/icon/cube_white"),
                _ => Resources.Load<Sprite>("source/img/icon/cube_gray")
            };

            result_sect.transform.Find("Normal_Form").Find("Color").gameObject.GetComponent<Image>().sprite = tmp;
            string presenter = "";
            for(int i = 0; i < data_hub.User_data_array.Count; i++)
            {
                if (data_hub.User_data_array[i].User_key.Equals(data_hub.Selling_potion_end.user_key))
                {
                    presenter = data_hub.User_data_array[i].User_name;
                    break;
                }
            }
            result_sect.transform.Find("Normal_Form").Find("Name").GetComponent<TMP_Text>().text = presenter;

            result_sect.transform.Find("Normal_Form").Find("Result").GetComponent<TMP_Text>().text =
                data_hub.Selling_potion_end.selling_success ? "����" : "����";

            result_sect.transform.Find("Normal_Form").gameObject.SetActive(true);
            result_sect.transform.Find("Refute_Form").gameObject.SetActive(false);
            result_sect.GetComponent<Result_Sect_Open>().Open();
        });
        #endregion

        #region �� ��ǥ/�ݹ� ����
        // �� ��ǥ ��� or ����� �ȳ�
        data_hub.Socket.OnUnityThread("theory_presentation_success", (resp) => {
            // resp : [[{"user_key":"","restart":false,"success":true}]]
            //Debug.Log(resp);
            string t = resp.ToString().Replace("[", "").Replace("]", "").Replace("{","").Replace("}", "").Replace("\"", "");
            string user_key = t.Split(",")[0].Split(":")[1];
            bool restart = t.Split(",")[1].Split(":")[1].Equals("true");
            bool success = t.Split(",")[2].Split(":")[1].Equals("true");

            // restart �� true �� �� ��ǥ�� �ٽ� ������
            if (restart)
            {
                // �� �Լ��� �������� data_hub�� �ڷ���� �ʱ�ȭ �Ǿ� ó������ �ٽý����� �� ����
                data_hub.Now_board_num = 6;
            }
            // restart�� false�� success�� true �� ��ǥ ����
            else if (!restart && success)
            {
                // ��ǥ ���� â�� ������ ����
                for(int i = 0; i < data_hub.User_data_array.Count; i++) {
                    if (data_hub.User_data_array[i].User_key.Equals(user_key))
                    {
                        StartCoroutine(Announce_pannel(data_hub.User_data_array[i].User_name + "��(��) �� ��ǥ�� �����Ͽ����ϴ�!", 2));
                        break;
                    }
                }
            }
        });

        // �� �ݹ� ����/���� �˸�
        data_hub.Socket.OnUnityThread("open_stamp_modal", (resp) => {
            /*
            user_key : data.user_key    string
            room_name : data.room_name  string
            ingre_num : data.ingre,     int
            ori: data.ori,              int
            success: true,              bool
            stamp: stamp_data,          string,string,string
            [[{
            "ingre_num":1,
            "ori":1,
            "success":true,
            "stamp":{"user_key":"string",
            "color":"red", 
            "point":"point_5_1"}, 
            {"user_key":"string",
            "color":"red",
            "point":"point_5_2"},
            {"user_key":"string",
            "color":"red",
            "point":"point_3_1"}}]]
            */
            // resp to useful data
            string t = resp.ToString().Replace("[", "").Replace("]", "").Replace("{", "").Replace("}", "").Replace("\"", "");
            //Debug.Log(t);
            string[] list = t.Split(",");
            string refute_user_key = list[0].Split(":")[1];
            string room_name = list[1].Split(":")[1];
            int ingre_num = int.Parse(list[2].Split(":")[1]);
            int ori = int.Parse(list[3].Split(":")[1]);
            bool success = list[4].Split(":")[1].Equals("true");

            //stamp
            Stamp_Info[] stamp_data = new Stamp_Info[3];
            string[] user_key = new string[3];
            string[] color = new string[3];
            string[] point = new string[3];
            // stamp�� ���� ������ �ƴѰ��
            if (list[5].Split(":")[1].Length > 1)
            {
                // 1
                user_key[0] = list[5].Split(":")[2];
                color[0] = list[6].Split(":")[1];
                point[0] = list[7].Split(":")[1];

                Stamp_Info tmp = new(user_key[0], color[0], point[0]);
                stamp_data[0] = tmp;

                //Debug.Log(user_key[0]);
                //Debug.Log(color[0]);
                //Debug.Log(point[0]);
                // 2 ���ʹ� ���θ� Ȯ���ؾ���
                if (list.Length > 8)
                {
                    user_key[1] = list[8].Split(":")[2];
                    color[1] = list[9].Split(":")[1];
                    point[1] = list[10].Split(":")[1];

                    tmp = new(user_key[1], color[1], point[1]);
                    stamp_data[1] = tmp;

                    if (list.Length > 11)
                    {
                        user_key[2] = list[11].Split(":")[2];
                        color[2] = list[12].Split(":")[1];
                        point[2] = list[13].Split(":")[1];

                        tmp = new(user_key[2], color[2], point[2]);
                        stamp_data[2] = tmp;
                    }
                }
            }
            // else�� ��� ���� ���°� == success�� false �̹Ƿ� ���� �ʿ� ����

            // �� �������� �ݹ� ����/���� ����� ���
            if (success)
            {
                // Ȯ�� �ٷ� emit �ϱ� -> resp �״�� �ٽ� ����
                Alc_Data send_data = new Alc_Builder().User_key(refute_user_key)
                                                      .Room_name(room_name)
                                                      .Ingre_num(ingre_num)
                                                      .Ori(ori)
                                                      .Success(success)
                                                      .Stamp(stamp_data)
                                                      .Refute_Check_Info_Build();

                data_hub.Socket.Emit("check_refute_info", send_data);

                // result, color, name, ori, stamp
                result_sect.transform.Find("Refute_Form").Find("Result").GetComponent<TMP_Text>().text = "����";
                string path = "";
                string name = "";
                string ingre_path = "";
                ingre_path = ingre_num switch
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

                for (int i =0; i < data_hub.User_data_array.Count; i++)
                {
                    if (data_hub.User_data_array[i].User_key.Equals(refute_user_key))
                    {
                        path = data_hub.User_data_array[i].User_color switch
                        {
                            "red" => "source/img/icon/cube_red",
                            "blue" => "source/img/icon/cube_blue",
                            "black" => "source/img/icon/cube_black",
                            "white" => "source/img/icon/cube_white",
                            _ => "source/img/icon/cube_gray",
                        };
                        name = data_hub.User_data_array[i].User_name;
                        break;
                    }
                }
                //Debug.Log(path);
                result_sect.transform.Find("Refute_Form").Find("Color").GetComponent<Image>().sprite = Resources.Load<Sprite>(path);
                result_sect.transform.Find("Refute_Form").Find("Name").GetComponent<TMP_Text>().text = name;
                result_sect.transform.Find("Refute_Form").Find("Ori").GetComponent<Image>().sprite = Resources.Load<Sprite>(ingre_path);

                // stamp ���� ���̰��ϱ�
                result_sect.transform.Find("Refute_Form").Find("Success_Area").gameObject.SetActive(true);

                // stamp 
                // user_key[0][1][2] �� �˻��� ������ stamp �ֱ� , ��ǥ�Ǿ��ٸ� 0�� ������ �־�����
                if (!string.IsNullOrEmpty(user_key[0]))
                {
                    //Debug.Log("in stamp_");
                    path = color[0] switch
                    {
                        "red" => "source/img/stamp/stamp_red_",
                        "blue" => "source/img/stamp/stamp_blue_",
                        "black" => "source/img/stamp/stamp_black_",
                        "white" => "source/img/stamp/stamp_white_",
                        _ => "source/img/stamp/stamp_red_",
                    };

                    switch (point[0])
                    {
                        case "point_5_1":
                        case "point_5_2":
                            path += "5";
                            break;
                        case "point_3_1":
                        case "point_3_2":
                        case "point_3_3":
                            path += "3";
                            break;
                        case "question_red_1":
                        case "question_red_2":
                            path += "red";
                            break;
                        case "question_green_1":
                        case "question_green_2":
                            path += "green";
                            break;
                        case "question_blue_1":
                        case "question_blue_2":
                            path += "blue";
                            break;
                    }
                    //Debug.Log(path);
                    result_sect.transform.Find("Refute_Form").Find("Success_Area").Find("Stamp_1").gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(path);
                }
                else
                {
                    result_sect.transform.Find("Refute_Form").Find("Success_Area").Find("Stamp_1").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/stamp/stamp_gray");
                }

                if (!string.IsNullOrEmpty(user_key[1]))
                {
                    path = color[1] switch
                    {
                        "red" => "source/img/stamp/Materials/stamp_red_",
                        "blue" => "source/img/stamp/Materials/stamp_blue_",
                        "black" => "source/img/stamp/Materials/stamp_black_",
                        "white" => "source/img/stamp/Materials/stamp_white_",
                        _ => "source/img/stamp/Materials/stamp_red_",
                    };

                    switch (point[1])
                    {
                        case "point_5_1":
                        case "point_5_2":
                            path += "5";
                            break;
                        case "point_3_1":
                        case "point_3_2":
                        case "point_3_3":
                            path += "3";
                            break;
                        case "question_red_1":
                        case "question_red_2":
                            path += "red";
                            break;
                        case "question_green_1":
                        case "question_green_2":
                            path += "green";
                            break;
                        case "question_blue_1":
                        case "question_blue_2":
                            path += "blue";
                            break;
                    }
                    result_sect.transform.Find("Refute_Form").Find("Success_Area").Find("Stamp_2").GetComponent<Image>().sprite = Resources.Load<Sprite>(path);
                }
                else
                {
                    result_sect.transform.Find("Refute_Form").Find("Success_Area").Find("Stamp_2").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/stamp/stamp_gray");
                }

                if (!string.IsNullOrEmpty(user_key[2]))
                {
                    path = color[2] switch
                    {
                        "red" => "source/img/stamp/Materials/stamp_red_",
                        "blue" => "source/img/stamp/Materials/stamp_blue_",
                        "black" => "source/img/stamp/Materials/stamp_black_",
                        "white" => "source/img/stamp/Materials/stamp_white_",
                        _ => "source/img/stamp/Materials/stamp_red_",
                    };

                    switch (point[2])
                    {
                        case "point_5_1":
                        case "point_5_2":
                            path += "5";
                            break;
                        case "point_3_1":
                        case "point_3_2":
                        case "point_3_3":
                            path += "3";
                            break;
                        case "question_red_1":
                        case "question_red_2":
                            path += "red";
                            break;
                        case "question_green_1":
                        case "question_green_2":
                            path += "green";
                            break;
                        case "question_blue_1":
                        case "question_blue_2":
                            path += "blue";
                            break;
                    }
                    result_sect.transform.Find("Refute_Form").Find("Success_Area").Find("Stamp_3").GetComponent<Image>().sprite = Resources.Load<Sprite>(path);
                }
                else
                {
                    result_sect.transform.Find("Refute_Form").Find("Success_Area").Find("Stamp_3").GetComponent<Image>().sprite = Resources.Load<Sprite>("source/img/stamp/stamp_gray");
                }
            }
            else
            {
                result_sect.transform.Find("Refute_Form").Find("Result").GetComponent<TMP_Text>().text = "����";
                string path = "";
                string name = "";
                string ingre_path = "";
                ingre_path = ingre_num switch
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

                for (int i = 0; i < data_hub.User_data_array.Count; i++)
                {
                    if (data_hub.User_data_array[i].User_key.Equals(refute_user_key))
                    {
                        path = data_hub.User_data_array[i].User_color switch
                        {
                            "red" => "source/img/icon/cube_red",
                            "blue" => "source/img/icon/cube_blue",
                            "black" => "source/img/icon/cube_black",
                            "white" => "source/img/icon/cube_white",
                            _ => "source/img/icon/cube_gray",
                        };
                        name = data_hub.User_data_array[i].User_name;
                        break;
                    }
                }

                result_sect.transform.Find("Refute_Form").Find("Color").GetComponent<Image>().sprite = Resources.Load<Sprite>(path);
                result_sect.transform.Find("Refute_Form").Find("Name").GetComponent<TMP_Text>().text = name;
            }

            result_sect.transform.Find("Normal_Form").gameObject.SetActive(false);
            result_sect.transform.Find("Refute_Form").gameObject.SetActive(true);
            result_sect.GetComponent<Result_Sect_Open>().Open();
        });

        #endregion

        #region ���� ����
        data_hub.Socket.OnUnityThread("test_ingredient_result", (resp) => {
            /*
                user_key : data.user_key,
                test_result : result_test => ���� ����,
                [[{"user_key":"123124151515","test_result":"red_1"}]]
            */
            //Debug.Log(resp);
            string t = resp.ToString().Replace("[", "").Replace("]", "").Replace("{", "").Replace("}", "");
            string user_key = t.Split(",")[0].Replace("\"", "").Split(":")[1];
            string test_result = t.Split(",")[1].Replace("\"", "").Split(":")[1];

            // ���� ��� ���̱�
            Sprite potion = test_result switch
            {
                "red_1" => Resources.Load<Sprite>("source/img/potion/red_+"),
                "red_0" => Resources.Load<Sprite>("source/img/potion/red_-"),
                "green_1" => Resources.Load<Sprite>("source/img/potion/green_+"),
                "green_0" => Resources.Load<Sprite>("source/img/potion/green_-"),
                "blue_1" => Resources.Load<Sprite>("source/img/potion/blue_+"),
                "blue_0" => Resources.Load<Sprite>("source/img/potion/blue_-"),
                _ => Resources.Load<Sprite>("source/img/potion/blank"),
            };
            string color_str = "";
            string presenter = "";
            for (int i = 0; i < data_hub.User_data_array.Count; i++)
            {
                if (data_hub.User_data_array[i].User_key.Equals(user_key))
                {
                    color_str = data_hub.User_data_array[i].User_color;
                    presenter = data_hub.User_data_array[i].User_name;
                    break;
                }
            }
            Sprite color;
            switch (color_str)
            {
                case "red":
                    color = Resources.Load<Sprite>("source/img/icon/cube_red");
                    break;
                case "blue":
                    color = Resources.Load<Sprite>("source/img/icon/cube_blue");
                    break;
                case "black":
                    color = Resources.Load<Sprite>("source/img/icon/cube_black");
                    break;
                case "white":
                    color = Resources.Load<Sprite>("source/img/icon/cube_white");
                    break;
                default:
                    color = Resources.Load<Sprite>("source/img/icon/cube_gray");
                    break;
            }

            result_sect.transform.Find("Normal_Form").Find("Name").GetComponent<TMP_Text>().text = presenter;

            if (data_hub.Now_board_num == 7)
            {
                result_sect.transform.Find("Normal_Form").Find("Potion").GetComponent<Image>().sprite = potion;
                result_sect.transform.Find("Normal_Form").Find("Color").GetComponent<Image>().sprite = color;
                result_sect.transform.Find("Normal_Form").Find("Result").GetComponent<TMP_Text>().text = "���";
            }
            else if (data_hub.Now_board_num == 8)
            {
                result_sect.transform.Find("Normal_Form").Find("Potion").GetComponent<Image>().sprite = potion;
                result_sect.transform.Find("Normal_Form").Find("Color").GetComponent<Image>().sprite = color;
                result_sect.transform.Find("Normal_Form").Find("Result").GetComponent<TMP_Text>().text = "���";
            }

            result_sect.transform.Find("Normal_Form").gameObject.SetActive(true);
            result_sect.transform.Find("Refute_Form").gameObject.SetActive(false);
            result_sect.GetComponent<Result_Sect_Open>().Open();

        });
        #endregion

        #region ����ȸ
        data_hub.Socket.OnUnityThread("show_exhibition", (resp) => {
            /*
             * user_name : room_data[data.room_name].user_data_array[user_num].user_name,  string
               user_key : data.user_key,    string
               get_cube_success,         bool
               result : true,            bool
             * */
            string t = resp.ToString().Replace("[", "").Replace("]", "").Replace("{", "").Replace("}", "").Replace("\"", "");
            string[] list = t.Split(",");

            //������ ����
            string exhibit_user_name = list[0].Split(":")[1];
            string exhibit_user_key = list[1].Split(":")[1];
            bool exhibit_success = list[2].Split(":")[1].Equals("true");
            bool exhibit_result = list[3].Split(":")[1].Equals("true");

            // ���� �����̶�� announce�� � ����
            if (exhibit_result && exhibit_success)
            {
                StartCoroutine(Announce_pannel(exhibit_user_name + "��(��) ���ÿ� �����Ͽ����ϴ�!\n���� 1�� �����մϴ�.", 2));
                // ���ô����� ť��� ����
                // -> change_exhibition_data ���� ����
            }
            else if (exhibit_result && !exhibit_success)
            {
                StartCoroutine(Announce_pannel(exhibit_user_name + "��(��) ���ÿ� ���� �������� �����Ͽ�����\n�������� �� �� ���ÿ��� �����Ͽ����ϴ�.", 2));
            }
            else if (!exhibit_result)
            {
                StartCoroutine(Announce_pannel(exhibit_user_name + "��(��) ���ÿ� ���� ������ �����Ͽ����ϴ�.\n���� 1�� �����˴ϴ�.", 2));
            }
            
        });

        #endregion

        data_hub.Socket.OnUnityThread("round_end", (resp) => {
            // ī�޶� ����
            // ���� ��� ���� ���� 
            // ù��° ���� �������� ����

            round_end_checker = true;
            my_act_end = false;
            data_hub.In_round = false;
            // fade -in
            GameObject.Find("UI_Event_Obj").transform.GetComponent<Black_Out>().enabled = true;
            GameObject.Find("UI_Event_Obj").transform.GetComponent<Black_Out>().Start_fade(false);

            // �� ������Ʈ Ȱ��ȭ
            /*
            if (max_count == 4)
                GameObject.Find("Over_4").SetActive(false);
            else
                GameObject.Find("Under_3").SetActive(false);
            ������ 4�� ������ �Ǿ����� �����Ƿ� 3�� ����
            */
            GameObject.Find("Front").transform.Find("Select_order_obj").Find("Under_3").gameObject.SetActive(true);

            // �Ʒ� ���� ��ȯ
            GameObject.Find("Help_announce").transform.Find("text_area").GetComponent<TMP_Text>().text = DEFAULT_ANNOUNCE_IN_CUBE;

            // �غ�Ϸ� â�� ����â���� ����
            show_area.transform.Find("Order_panel").gameObject.SetActive(true);
            show_area.transform.Find("GUI_btn_area").Find("Ready_btn").gameObject.SetActive(true);
            show_area.transform.Find("GUI_btn_area").gameObject.SetActive(false);
            show_area.transform.Find("Ready_area").gameObject.SetActive(false);

            // ��� ������ ť�� ǥ�� ����
            show_area.transform.Find("Can_use_cube_area").gameObject.SetActive(false);

            // ���콺 alt�� ������ ���¶�� ����
            GameObject.Find("Camera_Controller").GetComponent<Mouse_Switch>().Outside_mouse_controll(true);
            // �̵� �Ұ� ���� ���� ����
            GameObject.Find("Camera_Controller").GetComponent<Mouse_Switch>().in_round = false;

            // 3�� �뺴 ���� ���۵� �ʱ�ȭ
            data_hub.Now_board_num = -1;
            data_hub.My_board_3_end = false;
            data_hub.Selling_turn = 0;
            GameObject.Find("Room_Part_3").transform.Find("Ingre_Select_Sect").Find("Waiting_announce").gameObject.SetActive(false);
        });

        #endregion

        #region ���� ����

        data_hub.Socket.OnUnityThread("game_end_event", (resp) => {
            /*
             * resp :: 
             * [[{1:{"name":"string","score":12,"grade":1},2:{"name":string,"score":12","grade":2},~~ �ִ� 4�� ����{}}]]
             */
            // connection�� �ѱ��// �̸��� ���� �� ������ �߰��ؼ� �Ѱܾ���
            Dictionary<int, Game_Result_Data> tmp = StoD_converter.Resp_to_Game_Result_Data(resp);

            // tmp�� �� ����ֱ�
            for(int i = 1; i <= tmp.Count; i++)
            {
                for(int j = 0; j < data_hub.User_data_array.Count; j++)
                {
                    if (tmp[i].name.Equals(data_hub.User_data_array[j].User_name))
                    {
                        tmp[i].color = data_hub.User_data_array[j].User_color;
                        break;
                    }
                }
            }

            data_hub.Game_result_data = tmp;

            // ���� ���� �˶��̹Ƿ�
            // ������ ����Ǿ��ٰ� �ȳ�â�� ��� -> Ȯ�ι�ư���� ���â���� �̵�
            GameObject.Find("Show_area").transform.Find("Game_end_announce").gameObject.SetActive(true);
            // �̵�, ���콺 ������ ����
            //Destroy(GameObject.Find("Camera_Controller").GetComponent<Mouse_Switch>());
            //Destroy(GameObject.Find("Moving_obj").GetComponent<Camera_Moving>());
        });

        #endregion

        data_hub.Socket.OnUnityThread("move_room_page_check", (resp) => {
            // �� �̵� Ȯ�� �ȳ�â
            // Ȯ�� ������ scene �̵�
            //SceneManager.LoadScene("Room_page");
            GameObject.Find("GUI").transform.Find("Canvas").Find("Show_area").Find("Forced_termination_announce").gameObject.SetActive(true);
        });
    }

    #region �Ϲ� �Լ���

    private void Board_deactive(int board_num)
    {
        bool off = false;
        switch (board_num)
        {
            case 1:
                // ���� �����ϰ��ϴ� object_selecter ����
                Transform[] list = GameObject.Find("Room_Part_1").transform.Find("Show_Ingre_Sect").GetComponentsInChildren<Transform>();
                for(int i =0; i < list.Length; i++)
                {
                    if (list[i] != GameObject.Find("Room_Part_1").transform.Find("Show_Ingre_Sect").GetComponent<Transform>())
                    {
                        Destroy(list[i].gameObject.GetComponent<Object_Selecter>());
                        list[i].GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.0f);
                    }
                }
                off = true;
                break;
            case 2:
                // �����ߴ� ��� ���� ��ü�� �ı�
                GameObject.Find("Data_Controller").GetComponent<Ingre_Selecter_for_Play>().Destory_ingre_obj();
                off = true;
                break;
            // �뺴���� �����Ǹ�
            case 3:
                if (data_hub.My_coin_step_end == false &&
                    data_hub.Coin_step_end == false &&
                    data_hub.Price_step_end == false &&
                    data_hub.Potion_step_end == false)
                {
                    // ���� �ݾ� ���� �Ϸ� 
                    data_hub.My_coin_step_end = true;
                    GameObject.Find("Data_Controller").GetComponent<Adv_Obj_Construct_for_Play>().Discount_obj_destroy();
                    off = true;
                }
                else if (data_hub.My_coin_step_end &&
                         data_hub.Coin_step_end &&
                         data_hub.Price_step_end == false &&
                         data_hub.Potion_step_end == false)
                {
                    // �ǸŰ� ���� �Ϸ�
                    data_hub.Price_step_end = true;
                    GameObject.Find("Data_Controller").GetComponent<Adv_Obj_Construct_for_Play>().Price_obj_Destroy();
                    off = true;
                }
                else if (data_hub.My_coin_step_end        &&
                         data_hub.Coin_step_end           &&
                         data_hub.Price_step_end          &&
                         data_hub.Potion_step_end == false)
                {
                    // ���� ���� ��
                    data_hub.Potion_step_end = true;
                    GameObject.Find("Data_Controller").GetComponent<Adv_Obj_Construct_for_Play>().Potion_obj_destroy();
                    // �Ǹ� ��� ��������
                    // board_act_checker ȣ��� ���� ����
                    data_hub.Now_board_num = 3;
                }
                else if (data_hub.My_coin_step_end &&
                         data_hub.Coin_step_end &&
                         data_hub.Price_step_end &&
                         data_hub.Potion_step_end )
                {
                    // �Ǹ� �Ϸ�
                    // ���� ǥ���� ��� ����
                    GameObject.Find("Data_Controller").GetComponent<Adv_Obj_Construct_for_Play>().Dis_order_destroy();
                    GameObject.Find("Data_Controller").GetComponent<Ingre_Selecter_for_Play>().Destory_ingre_obj();
                    // ��� ������ �������Ƿ� ���� �Լ��� ���� false�� �ʱ�ȭ
                    data_hub.My_coin_step_end = false;
                    data_hub.Coin_step_end = false;
                    data_hub.Price_step_end = false;
                    data_hub.Potion_step_end = false;

                    // �� ���ʰ� �������Ƿ� �� ������ ���� ���δ� true�� ����
                    data_hub.My_board_3_end = true;
                    // ���� ��� ��ȣ�� �ʱ�ȭ
                    data_hub.Select_ingre[0] = "card_0";
                    data_hub.Select_ingre[1] = "card_0";
                    off = true;
                }
                break;
            case 4:
                // ���� ��ư ��Ȱ��ȭ
                GameObject.Find("Room_Part_4").transform.Find("Btn_Sect").gameObject.SetActive(false);
                // ���� Ȯ�� â ����
                GameObject.Find("Show_area").transform.Find("Buy_announce").gameObject.SetActive(false);
                off = true;
                break;
            case 5:
                // �ݹ��� ��� ���� -> ���� ���� -> ��� ���� -> �ݹ� ��� â
                if (data_hub.Core_end_5 == false &&
                    data_hub.Ori_ele_end_5 == false )
                {
                    // �ݹ��� ��� ���� ��
                    data_hub.Core_end_5 = true;
                    data_hub.Now_board_num = 5;
                    // ��� ���� script off
                    GameObject.Find("Data_Controller").GetComponent<Data_Controller>().Controller_theory_core_selecter(5, false);
                }
                else if (data_hub.Core_end_5 &&
                         data_hub.Ori_ele_end_5 == false )
                {
                    // ���� ���� ��
                    data_hub.Ori_ele_end_5 = true;
                    data_hub.Now_board_num = 5;
                    // ���� ������
                    GameObject.Find("Data_Controller").GetComponent<Theory_Refute_Obj_for_Play>().Destroy_ele();
                }
                else if (data_hub.Core_end_5 &&
                         data_hub.Ori_ele_end_5  )
                {
                    // ��� ������
                    GameObject.Find("Data_Controller").GetComponent<Theory_Refute_Obj_for_Play>().Destroy_ingre();

                    // ���� ��
                    // ��� ���� �ʱ�ȭ
                    data_hub.Core_end_5 = false;
                    data_hub.Ori_ele_end_5 = false;

                    data_hub.Ingre_arr[0] = 0;
                    data_hub.Ingre_arr[1] = 0;
                    data_hub.Core_num = -1;
                    data_hub.Ele_num = -1;

                    off = true;
                }
                break;
                // �� ��ǥ
            case 6:
                // ��ǥ�� ��� ���� -> ���� ���� -> ���� ���� -> ��ǥ �Ϸ�â
                if (data_hub.Core_end_6 == false &&
                    data_hub.Element_end_6 == false )
                {
                    // ��ǥ�� ��� ���� - core_end_6
                    data_hub.Core_end_6 = true;
                    data_hub.Now_board_num = 6;
                    // ��� ���� script off
                    GameObject.Find("Data_Controller").GetComponent<Data_Controller>().Controller_theory_core_selecter(6, false);
                }
                else if (data_hub.Core_end_6 &&
                         data_hub.Element_end_6 == false )
                {
                    // ���� ���� - element_end_6
                    data_hub.Element_end_6 = true;
                    data_hub.Now_board_num = 6;
                    // ���� ������Ʈ ������
                    GameObject.Find("Data_Controller").GetComponent<Theory_Present_Obj_for_Play>().Destroy_ele();
                }
                else if (data_hub.Core_end_6 &&
                         data_hub.Element_end_6 )
                {
                    // ���� ������
                    GameObject.Find("Data_Controller").GetComponent<Theory_Present_Obj_for_Play>().Destroy_stamp();

                    // ���� �ʱ�ȭ
                    data_hub.Core_end_6 = false;
                    data_hub.Element_end_6 = false;
                    data_hub.Core_num = -1;
                    data_hub.Stamp_num = -1;
                    data_hub.Ele_num = -1;

                    // ��ǥ�� �����ߴٰ� �ȳ����ڸ� ��������
                    off = true;
                }
                break;
            case 7:
                // �����ߴ� ��� ���� ��ü�� �ı�
                GameObject.Find("Data_Controller").GetComponent<Ingre_Selecter_for_Play>().Destory_ingre_obj();
                // ���� ��� ��ȣ�� �ʱ�ȭ
                data_hub.Select_ingre[0] = "card_0";
                data_hub.Select_ingre[1] = "card_0";
                off = true;
                break;
            case 8:
                // �����ߴ� ��� ���� ��ü�� �ı�
                GameObject.Find("Data_Controller").GetComponent<Ingre_Selecter_for_Play>().Destory_ingre_obj();
                // ���� ��� ��ȣ�� �ʱ�ȭ
                data_hub.Select_ingre[0] = "card_0";
                data_hub.Select_ingre[1] = "card_0";
                off = true;
                break;
            case 9:
                if (data_hub.Exhibit_potion_step == false)
                {
                    // ���� ���� ������ ť�� end
                    // select_cube ����
                    GameObject.Find("Room_Part_9").transform.Find("Select_Sect").gameObject.SetActive(false);
                    // potion_step true
                    data_hub.Exhibit_potion_step = true;
                    data_hub.Now_board_num = 9;
                }
                else if (data_hub.Exhibit_potion_step)
                {
                    // ���õ� ������ ��ġ���� ��� end
                    // ��� ������Ʈ ����
                    GameObject.Find("Data_Controller").GetComponent<Exhibit_Ingre_for_Play>().Destroy_ingre_for_exhibition();
                    // ���� ���� �ʱ�ȭ
                    data_hub.Exhibit_potion_step = false;
                    data_hub.Exhibit_select_potion = "";
                    data_hub.Select_ingre[0] = "";
                    data_hub.Select_ingre[1] = "";

                    off = true;
                }
                break;
        }

        if (off)
        {
            // �ൿ �Ϸ� ��ư ��Ȱ��ȭ
            GameObject.Find("Switch_Area").transform.Find("Board_act_end").gameObject.SetActive(false);
            GameObject.Find("Switch_Area").transform.Find("Board_act_pass").gameObject.SetActive(false);

            my_act_end = true;
        }
    }

    // cant_user_cube���� ���� �ڷ�ƾ �Լ�
    IEnumerator Announce_pannel(string say, int state)
    {
        GameObject.Find("Help_announce").transform.Find("text_area").GetComponent<TMP_Text>().text = say;
        yield return new WaitForSeconds(3f);
        if (state == 1) // board
            GameObject.Find("Help_announce").transform.Find("text_area").GetComponent<TMP_Text>().text = DEFAULT_ANNOUNCE_IN_BOARD;
        else if (state == 2 && round_end_checker == false) // act && in round
            GameObject.Find("Help_announce").transform.Find("text_area").GetComponent<TMP_Text>().text = DEFAULT_ANNOUNCE_IN_ACT;
        else if (state == 2 && round_end_checker == false && my_act_end)
            GameObject.Find("Help_announce").transform.Find("text_area").GetComponent<TMP_Text>().text = WAITING_ANNOUNCE;
        else if (state == 2 && round_end_checker)
            GameObject.Find("Help_announce").transform.Find("text_area").GetComponent<TMP_Text>().text = DEFAULT_ANNOUNCE_IN_BOARD;

        yield break;
    }

    // 5�ʵ� ����� �� �ڷ�ƾ �Լ�
    IEnumerator Behavior_result_show(GameObject _target, float sec)
    {
        yield return new WaitForSeconds(sec);
        _target.SetActive(false);
        yield break;
    }

    IEnumerator Wait_1f_for_adv_selling()
    {
        yield return new WaitForSeconds(1f);
        data_hub.Now_board_num = 3;
        yield break;
    }
    #endregion

    #region emit area

    public void emit_chat()
    {
        // �� ���� ���� x
        if (my_chat_input_obj.text.Equals("")) return;
        Alc_Data send_data = new Alc_Builder().Speaker(data_hub.My_name)
                                              .Msg(my_chat_input_obj.text)
                                              .Type("normal")
                                              .Room_name(data_hub.Room_name)
                                              .Chat_Data_Build();
        //Debug.Log(send_data.print());

        data_hub.Socket.Emit("chat", send_data);

        my_chat_input_obj.text = "";

        // observer ����
        GameObject.Find("Data_Controller").GetComponent<Chat_Observe_in_Board>().enabled = false;
        // focus ����
        //my_chat_input_obj.Select();
    }

    public void Chat_observe_start()
    {
        GameObject.Find("Data_Controller").GetComponent<Chat_Observe_in_Board>().enabled = true;
    }

    public void Chat_observe_end()
    {
        GameObject.Find("Data_Controller").GetComponent<Chat_Observe_in_Board>().enabled = false;
    }

    // �ൿ����ť�긦 ������ ������ ���� �Լ� -> �ٸ� �����鵵 �˱� ���� ������ ������ ������� ����
    public void Round_order_cube_select(int cube_num)
    {
        // 3�� �����϶� cube_num�� 8�̶��
        if(data_hub.Max_count <= 3 && cube_num == 8)
        {
            cube_num -= 1;
        }

        // ������ ������ ����
        Alc_Data send_data = new Alc_Builder().Room_name(data_hub.Room_name)
                                              .Select_order(cube_num)
                                              .User_key(data_hub.My_key)
                                              .Select_Round_Order_Data_Build();

        //Debug.Log(send_data.print());

        data_hub.Socket.Emit("select_round_order", send_data);
    }

    // �ൿ����ť�긦 ���� �� �غ�ϷḦ ������ �� ���� �Լ�
    public void Round_order_ready()
    {
        // emit �ϱ� ���� ������ �޾ƿ���
        int cube_num = GameObject.Find("Order_Cube_Selecter").GetComponent<Order_Cube_Select_Event>().cube_num;

        // cube_num�� 0���� ������ ���ۺҰ�
        if (cube_num <= 0 || cube_num > 9)
        {
            // �ȳ�â�� �ұ�
            return;
        }

        Alc_Data send_data = new Alc_Builder().Room_name(data_hub.Room_name).Send_Room_Name_Data_Build();

        data_hub.Socket.Emit("decide_round_setting_order_counter_incre", send_data);

        // ť�� Ŭ�� ������ �ʱ�ȭ
        order_cube_selecter.GetComponent<Order_Cube_Select_Event>().cube_num = 0;
        data_hub.Selected_round_order[0] = 0; data_hub.Selected_round_order[1] = 0; data_hub.Selected_round_order[2] = 0; data_hub.Selected_round_order[3] = 0;
        order_cube_selecter.GetComponent<Order_Cube_Select_Event>().selected_round_order = data_hub.Selected_round_order;
        // ť�� Ŭ���� �Ұ����ϰ� ����
        order_cube_selecter.SetActive(false);
        // �غ� �Ϸ� ��ư ��Ȱ��ȭ
        GameObject.Find("Switch_Area").transform.Find("Round_order_ready").gameObject.SetActive(false);
        
    }

    // �ൿ�� ť�긦 �������� �� ������ �����͸� �ѱ� �Լ�
    public void Select_board_cube(int[] info)
    {
        /*
            user_key,
            board_num : ���� ��ȣ, info[0]
            button_order_num : ��ư ����, -> cube_num info[1]
            room_name : �� �̸�
        */
        Alc_Data send_data = new Alc_Builder().User_key(data_hub.My_key)
                                              .Room_name(data_hub.Room_name)
                                              .Board_num(info[0])
                                              .Button_order_num(info[1])
                                              .Select_Cube_Data_Build();
        //Debug.Log(send_data.print());

        data_hub.Socket.Emit("select_cube", send_data);
    }

    // ���� �غ� �Ϸ� ��ư�� ������ ��
    public void Round_ready_prepare()
    {
        show_area.transform.Find("Round_ready_announce").gameObject.SetActive(true);
    }

    public void Round_ready_prepare_close()
    {
        show_area.transform.Find("Round_ready_announce").gameObject.SetActive(false);
    }

    public void Round_ready()
    {
        // �ʿ� ������
        // room_name, user_key
        Alc_Data send_data = new Alc_Builder().Room_name(data_hub.Room_name)
                                              .User_key(data_hub.My_key)
                                              .Common_Data_Build();

        data_hub.Socket.Emit("round_ready_on", send_data);

        show_area.transform.Find("Round_ready_announce").gameObject.SetActive(false);

        // �غ� �Ϸ� ê�� �Ѹ�
        send_data = new Alc_Builder().Speaker(data_hub.My_name)
                                     .Msg("�غ� �Ϸ��Ͽ����ϴ�.")
                                     .Type("announce")
                                     .Room_name(data_hub.Room_name)
                                     .Chat_Data_Build();
        //Debug.Log(send_data.print());
        data_hub.Socket.Emit("chat", send_data);
    }

    // ȣ��ī�� ����ϱ�
    public void Favor_card_use_confirm()
    {
        // ���� ���õ� ȣ��ī�忡 ���� �ʿ��� �����Ͱ� �ִ��� Ȯ��
        // ���� �����Ͷ�� ���� �־� ����
        // �����Ͱ� �ùٸ��� Ȯ���� ����
        bool check_result = false;
        switch (data_hub.Favor_card_name)
        {
            case "assistent":
            case "bar_owner":
            case "caretaker":
            case "merchant":
            case "shopkeeper":
            case "wise_man":
                data_hub.Select_ingre[0] = "";
                data_hub.Select_ingre[1] = "";
                data_hub.Select_board_num = 0;
                check_result = true;
                break;
            case "big_man":
                // select_board_num ���� 0���� ũ�� 9���� ������ Ȯ��
                if (data_hub.Select_board_num > 0 && data_hub.Select_board_num < 9)
                    check_result = true;

                break;
            case "herbalist":
                // select_ingre�� card_1~card_8 ������ �� ���� �ִ��� Ȯ��
                if (
                    (data_hub.Select_ingre[0].Equals("card_1") ||
                     data_hub.Select_ingre[0].Equals("card_2") ||
                     data_hub.Select_ingre[0].Equals("card_3") ||
                     data_hub.Select_ingre[0].Equals("card_4") ||
                     data_hub.Select_ingre[0].Equals("card_5") ||
                     data_hub.Select_ingre[0].Equals("card_6") ||
                     data_hub.Select_ingre[0].Equals("card_7") ||
                     data_hub.Select_ingre[0].Equals("card_8") 
                     ) &&
                   (data_hub.Select_ingre[1].Equals("card_1") ||
                    data_hub.Select_ingre[1].Equals("card_2") ||
                    data_hub.Select_ingre[1].Equals("card_3") ||
                    data_hub.Select_ingre[1].Equals("card_4") ||
                    data_hub.Select_ingre[1].Equals("card_5") ||
                    data_hub.Select_ingre[1].Equals("card_6") ||
                    data_hub.Select_ingre[1].Equals("card_7") ||
                    data_hub.Select_ingre[1].Equals("card_8")  )
                    ) {
                    check_result = true;
                }
                    
                break;
        }

        // �� Ȯ���� �����̶�� emit
        if (check_result)
        {
            // data :
            //    user_key : ���� ��ȣ
            //    room_name : �� �̸�
            //    favor_card : ����ϴ� ȣ��ī��
            //    select_board_num : ����ģ�� ī�尡 ���� �� �ʿ��� ����
            //    ingre_list : �������� ī�尡 ���� �� �ʿ��� ����
            Alc_Data send_data = new Alc_Builder().User_key(data_hub.My_key)
                                                  .Room_name(data_hub.Room_name)
                                                  .Favor_card(data_hub.Favor_card_name)
                                                  .Select_board_num(data_hub.Select_board_num)
                                                  .Ingre_list(data_hub.Select_ingre)
                                                  .Favor_Card_Use_Confirm_Data_Build();

            data_hub.Socket.Emit("favor_card_use_confirm", send_data);

            //���� �� favor_card�� use window ����
            GameObject.Find("Data_Controller").GetComponent<Favor_Card_Use_Window_Control>().Using_window_close();
        }
    }

    // �� ������ �ൿ�� Ȯ���� ��
    public void Board_act_end_confirm()
    {
        Alc_Data send_data;
        //Debug.Log(now_board_num);
        // ���� ��ȣ�� ���� �ൿ�� �޶����
        switch (data_hub.Now_board_num)
        {
            case 1:     // ��� �ޱ�

                //Debug.Log(select_ingre_num);
                //Debug.Log(index);
                // pick_ingredient �� ������
                /*
                  user_key : this.my_key,
                  pick_item : ������ ī��
                  cube_order : ť�� ����
                  board_order : this.board_order,
                  ingredient_select_arr_order : index,
                  room_name : �� �̸�
                */
                // ������ ī�尡 ������ �����϶�� �ȳ��ϰ� �����ؾ���
                if (data_hub.Select_ingre_num < 0 || data_hub.Select_ingre_num > 8)
                {
                    StartCoroutine(Announce_pannel("ī�带 �����ؾ� �ൿ�� Ȯ���� �� �ֽ��ϴ�.", 2));
                    return;
                }
                send_data = new Alc_Builder().User_key(data_hub.My_key)
                                             .Room_name(data_hub.Room_name)
                                             .Pick_item(data_hub.Select_ingre_num)
                                             .Cube_order(data_hub.Board_cube_order)
                                             .Board_order(data_hub.Board_order)
                                             .Ingredient_select_arr_order(data_hub.Select_index)
                                             .Pick_Ingre_Data_Build();

                data_hub.Socket.Emit("pick_ingredient", send_data);

                break;
            case 2:     // ��� �Ǹ�
                //Board_deactive(now_board_num);
                /*
                  user_key : this.my_key,
                  sell_item_num : sell_item_num,
                  board_order : this.board_order,
                  cube_order : this.board_cube_order,
                  room_name : this.room_name,
                */
                // ������ ī�尡 ������ �����϶�� �ȳ��ϰ� �����ؾ���
                if (data_hub.Sell_item_num < 1 || data_hub.Sell_item_num > 8)
                {
                    StartCoroutine(Announce_pannel("ī�带 �����ؾ� �ൿ�� Ȯ���� �� �ֽ��ϴ�.", 2));
                    return;
                }
                send_data = new Alc_Builder().User_key(data_hub.My_key)
                                             .Room_name(data_hub.Room_name)
                                             .Sell_item_num(data_hub.Sell_item_num)
                                             .Board_order(data_hub.Board_order)
                                             .Cube_order(data_hub.Board_cube_order)
                                             .Sell_Item_Confirm_Data_Build();

                data_hub.Socket.Emit("sell_item_confirm", send_data);
                
                break;
            case 3:     // �뺴���� ���� �Ǹ�
                // ����ī�� ����
                if (data_hub.My_coin_step_end == false &&
                    data_hub.Coin_step_end == false    &&
                    data_hub.Price_step_end == false   &&
                    data_hub.Potion_step_end == false     )
                {
                    // �뺴���� ����ī�� ���� �Լ�
                    // ������ ����ī��� �̹� ���ӿ��� ��� �Ұ�
                    /*
                       data :
                        user_key      :: ����Ű
                        color         :: ������
                        dis_coin_num  :: ������ ���� ���� ī��
                        room_name     :: �� �̸�
                    */// ������ ī�尡 ������ �����϶�� �ȳ��ϰ� �����ؾ���
                    if (data_hub.Dis_coin_num < 0 || data_hub.Dis_coin_num > 3)
                    {
                        StartCoroutine(Announce_pannel("ī�带 �����ؾ� ������ Ȯ���� �� �ֽ��ϴ�.", 2));
                        return;
                    }
                    send_data = new Alc_Builder().User_key(data_hub.My_key)
                                                 .Room_name(data_hub.Room_name)
                                                 .Color(data_hub.My_data.User_color)
                                                 .Dis_coin_num(data_hub.Dis_coin_num)
                                                 .Adv_Dis_Confirm_Data_Build();
                    data_hub.Socket.Emit("adv_dis_confirm", send_data);
                    
                }
                // �ǸŰ� ����
                else if (data_hub.My_coin_step_end        &&
                         data_hub.Coin_step_end           &&
                         data_hub.Price_step_end == false &&
                         data_hub.Potion_step_end == false )
                {
                    if (data_hub.Selling_price < 1 || data_hub.Selling_price > 4)
                    {
                        StartCoroutine(Announce_pannel("�ǸŰ��� �����ؾ� Ȯ���� �� �ֽ��ϴ�.", 2));
                        return;
                    }
                    // �Ǹ� �ݾ��� Ȧ���ϱ� ���ؼ��� �� ���̾����
                    if (data_hub.Dis_coin_data[data_hub.Selling_turn].user_key == data_hub.My_key)
                    {
                        // �뺴���� ������ �Ǹ� �ݾ� Ȧ��
                        /*
                          data
                            user_key  :: ����Ű
                            sell_price :: �ǸŰ�
                            room_name  :: �� �̸�
                        */
                        send_data = new Alc_Builder().User_key(data_hub.My_key)
                                                     .Room_name(data_hub.Room_name)
                                                     .Sell_price(data_hub.Selling_price)
                                                     .Sell_Price_Confirm_Data_Build();

                        data_hub.Socket.Emit("sell_price_confirm", send_data);
                    }
                    else
                    {
                        StartCoroutine(Announce_pannel("���� ���ʰ� �ƴ϶� Ȯ���� �� �����ϴ�.", 2));
                    }
                }
                // �Ǹ� ���� ����
                else if (data_hub.My_coin_step_end        &&
                         data_hub.Coin_step_end           &&
                         data_hub.Price_step_end          &&
                         data_hub.Potion_step_end == false   )
                {
                    // ��ἳ������ �ѱ��
                    // emit ���� ����
                    if (data_hub.Sell_potion.Length < 1)
                    {
                        StartCoroutine(Announce_pannel("������ ������ �����ؾ� ������ Ȯ���� �� �ֽ��ϴ�.", 2));
                        return;
                    }

                }
                // �Ǹ�
                else if (data_hub.My_coin_step_end      &&
                         data_hub.Coin_step_end         &&
                         data_hub.Price_step_end        &&
                         data_hub.Potion_step_end )
                {
                    // �Ǹ� ���� emit
                    /*
                        data:
                        user_key  :: ����Ű
                        user_color:: ������
                        card_list :: ������ ���� 2���� ��� (card_1~8)
                        what_kind_sell_potion :: ���� ���� ������ ������ ����(red_1,0/ green_1,0 / blue_1,0)\
                        selling_turn :: �����Ǹ� ����
                        room_name :: �� �̸�
                    */
                    send_data = new Alc_Builder().User_key(data_hub.My_key)
                                                 .User_color(data_hub.My_data.User_color)
                                                 .Card_list(data_hub.Select_ingre)
                                                 .What_kind_sell_potion(data_hub.Sell_potion)
                                                 .Selling_turn(data_hub.Selling_turn)
                                                 .Room_name(data_hub.Room_name)
                                                 .Sell_To_Adv_Potion_Data_Build();

                    data_hub.Socket.Emit("sell_to_adv_potion", send_data);
                }
                break;
            case 4:     // ���� ����
                /*
                  board_order : this.board_order,
                  cube_order : this.board_cube_order,
                  rank : data.rank,
                  arti_num : data.num,
                  user_key : this.my_key,
                  room_name : this.room_name,
                 */
                // arti_num�� -1 �̰ų� 8�̸� �۵��ؼ��� �ȵ�
                if (data_hub.Arti_num == -1 || data_hub.Arti_num == 8)
                {
                    StartCoroutine(Announce_pannel("���� ���� ��ư�� ������ Ȯ���˴ϴ�.", 2));
                    return;
                }
                string rank = data_hub.Now_rank switch
                {
                    1 => "rank_1",
                    2 => "rank_2",
                    3 => "rank_3",
                    _ => "rank_err",
                };
                send_data = new Alc_Builder().User_key(data_hub.My_key)
                                             .Room_name(data_hub.Room_name)
                                             .Rank(rank)
                                             .Board_order(data_hub.Board_order)
                                             .Cube_order(data_hub.Board_cube_order)
                                             .Arti_num(data_hub.Arti_num)
                                             .Buy_Artifact_Confirm_Data_Build();

                data_hub.Socket.Emit("buy_artifact_confirm", send_data);
                break;

            case 5:     // �� �ݹ�
                // �ݹ��� ��� ���� -> ���� ���� -> ��� ���� -> �ݹ� ��� â
                if (data_hub.Core_end_5 == false &&
                    data_hub.Ori_ele_end_5 == false )
                {
                    // �ݹ��� ��� ����
                    if (data_hub.Core_num < 0 || data_hub.Core_num > 8)
                    {
                        StartCoroutine(Announce_pannel("�ݹ��� ��Ḧ �����ؾ� ���� �������� �Ѿ �� �ֽ��ϴ�.\n��Ḧ �������ּ���!.", 2));
                        return;
                    }
                }
                else if (data_hub.Core_end_5 &&
                         data_hub.Ori_ele_end_5 == false  )
                {
                    // ���� ����
                    if (data_hub.Ele_num < 0 || data_hub.Ele_num > 3)
                    {
                        StartCoroutine(Announce_pannel("�ݹ��� ������ ���� �����ؾ� ���� �������� �Ѿ �� �ֽ��ϴ�.\n���Ҹ� ����ּ���.", 2));
                        return;
                    }
                }
                else if (data_hub.Core_end_5 &&
                         data_hub.Ori_ele_end_5  )
                {
                    // ��� ���� 
                    // ���� ���� �Ϸ��̹Ƿ� emit
                    /*
                      ingre    : ���õ� ����          1~8 num
                      ori      : �ݹ��� ������ ����    1 red  2 green 3 blue num
                      user_key : �ݹ��� ���          string
                      arr      : ������ ������ Ʋ�ȴٴ� ���� �����ϱ� ���� 2���� ��� 1~8 *2
                      cube_order : ť�� ���� 
                      board_order: ���� ����
                      room_name : ���̸�
                    */
                    // ��ᰡ 2���� ��� ������ �ȵǾ��ֵ��� �����϶�� �˸�
                    if ((data_hub.Ingre_arr[0] < 0 || data_hub.Ingre_arr[0] > 8) || (data_hub.Ingre_arr[1] < 0 || data_hub.Ingre_arr[1] > 8))
                    {
                        StartCoroutine(Announce_pannel("��� 2������ ��� �����ؾ� �մϴ�.\n2 ������ ��Ḧ ����ּ���.", 2));
                        return;
                    }
                    send_data = new Alc_Builder().User_key(data_hub.My_key)
                                                 .Room_name(data_hub.Room_name)
                                                 .Board_order(data_hub.Board_order)
                                                 .Cube_order(data_hub.Board_cube_order)
                                                 .Ingre(data_hub.Core_num)
                                                 .Ori(data_hub.Ele_num)
                                                 .Arr(data_hub.Ingre_arr)
                                                 .Refuting_Theory_Data_Build();

                    data_hub.Socket.Emit("refuting_theory_data", send_data);
                }
                break;

            case 6:     // �� ��ǥ
                // ��ǥ�� ��� ���� -> ���� ���� -> ���� ���� -> ��ǥ �Ϸ�â
                if (data_hub.Core_end_6 == false &&
                    data_hub.Element_end_6 == false )
                {
                    // ��ǥ�� ��� ������ ����
                    if (data_hub.Core_num < 0 || data_hub.Core_num > 8)
                    {
                        StartCoroutine(Announce_pannel("��ǥ�� ��Ḧ �����ؾ� ���� �������� �Ѿ �� �ֽ��ϴ�.\n��Ḧ �������ּ���!.", 2));
                        return;
                    }
                }
                else if (data_hub.Core_end_6 &&
                         data_hub.Element_end_6 == false )
                {
                    // ���� ������ ����
                    if (data_hub.Ele_num < 0 || data_hub.Ele_num > 8)
                    {
                        StartCoroutine(Announce_pannel("��ǥ�� ��ᰡ ���� ���Ҹ� �����ؾ� ���� �������� �Ѿ �� �ֽ��ϴ�.\n���Ҹ� ����ּ���.", 2));
                        return;
                    }
                }
                else if (data_hub.Core_end_6 &&
                         data_hub.Element_end_6  )
                {
                    // ���� ������ ����
                    // ���� ���ð� ���ÿ� ��ǥ�� �� -> �� ������ ���� ����
                    // emit�� ����
                    //  ele :: element ��ȣ
                    //  ingre :: ���� ���õ� ��� ��ȣ
                    //  stamp :: ��ǥ�ڰ� ����� stamp
                    //  user_key , user_color
                    //  cube_order : ť�� ���� 
                    //  board_order: ���� ����
                    //  room_name :: �� �̸�
                    if (data_hub.Stamp_num < 0 || data_hub.Stamp_num > 11)
                    {
                        StartCoroutine(Announce_pannel("���� Ȥ�� '���� �ǹ�'�� ���� ������ �����ؾ� ������ Ȯ���� �� �ֽ��ϴ�.\n������ �������ּ���", 2));
                    }
                    send_data = new Alc_Builder().User_key(data_hub.My_key)
                                                 .User_color(data_hub.My_data.User_color)
                                                 .Room_name(data_hub.Room_name)
                                                 .Board_order(data_hub.Board_order)
                                                 .Cube_order(data_hub.Board_cube_order)
                                                 .Ingre(data_hub.Core_num)
                                                 .Ele(data_hub.Ele_num)
                                                 .Stamp(data_hub.Stamp_num)
                                                 .Presentation_Theory_Data_Build();

                    data_hub.Socket.Emit("presentation_theory", send_data);
                }
                break;
            case 7:     // �л� ����
                if (data_hub.Select_ingre[0].Length < 0 || data_hub.Select_ingre[1].Length < 0)
                {
                    StartCoroutine(Announce_pannel("��� 2������ ��� �����ؾ� �մϴ�.\n2 ������ ��Ḧ ����ּ���.", 2));
                    return;
                }
                send_data = new Alc_Builder().User_key(data_hub.My_key)
                                             .Room_name(data_hub.Room_name)
                                             .Board_is(7)
                                             .Board_order(data_hub.Board_order)
                                             .Cube_order(data_hub.Board_cube_order)
                                             .Caretaker_used(false)
                                             .Card_list(data_hub.Select_ingre)
                                             .Test_Ingredient_Confirm_Data_Build();

                data_hub.Socket.Emit("test_ingredient_confirm", send_data);
                break;
            case 8:     // ���� ����
                if (data_hub.Select_ingre[0].Length < 0 || data_hub.Select_ingre[1].Length < 0)
                {
                    StartCoroutine(Announce_pannel("��� 2������ ��� �����ؾ� �մϴ�.\n2 ������ ��Ḧ ����ּ���.", 2));
                    return;
                }
                send_data = new Alc_Builder().User_key(data_hub.My_key)
                                             .Room_name(data_hub.Room_name)
                                             .Board_is(8)
                                             .Board_order(data_hub.Board_order)
                                             .Cube_order(data_hub.Board_cube_order)
                                             .Caretaker_used(false)
                                             .Card_list(data_hub.Select_ingre)
                                             .Test_Ingredient_Confirm_Data_Build();

                data_hub.Socket.Emit("test_ingredient_confirm", send_data);
                break;
            case 9:     // ����ȸ
                        // ������ ���� ���� -> ��� ǥ�� �� ��� ���� -> ��� ���� ��� Ȯ�� -> �����ϸ� ����â�� �Բ� ť�� �� ����, �����ϸ� ���� ĭ���� ���� �� -1
                if (data_hub.Exhibit_potion_step == false)
                {
                    // ���� ���� ������ ť�� open
                    // ���⼱ �ƹ��͵� ����
                    if (data_hub.Exhibit_select_potion.Length < 0 )
                    {
                        StartCoroutine(Announce_pannel("������ ������ ����ּ���!", 2));
                        return;
                    }
                }
                else if (data_hub.Exhibit_potion_step)
                {
                    // ���õ� ������ ��ġ���� ��� open
                    // ��� ������ �Ϸ� �� ���̹Ƿ� emit ����
                    /* data
                     *    user_key : ������ȣ
                     *    user_color : ��ǥ�� ���� ��
                     *    card_list : ������ ��ᰡ ����� �迭 :: card_1~8
                     *    exhibit_potion : ��ǥ�� ���� 
                     *          red_1,0 / green_1,0 / blue_1,0
                     *    board_order : ���� ����
                     *    cube_order  : ť�� ����
                     *    room_name   : �� �̸�
                     */
                    if (data_hub.Select_ingre[0].Length < 0 || data_hub.Select_ingre[1].Length < 0)
                    {
                        StartCoroutine(Announce_pannel("��� 2������ ��� �����ؾ� �մϴ�.\n2 ������ ��Ḧ ����ּ���.", 2));
                        return;
                    }
                    send_data = new Alc_Builder().User_key(data_hub.My_key)
                                                 .Room_name(data_hub.Room_name)
                                                 .User_color(data_hub.My_data.User_color)
                                                 .Card_list(data_hub.Select_ingre)
                                                 .Exhibit_potion(data_hub.Exhibit_select_potion)
                                                 .Board_order(data_hub.Board_order)
                                                 .Cube_order(data_hub.Board_cube_order)
                                                 .Exhibit_Ingre_Data_Build();

                    data_hub.Socket.Emit("exhibit_ingre", send_data);
                }
                break;
        }

        Board_deactive(data_hub.Now_board_num);
    }

    // �� ������ �ൿ�� �ѱ� ��
    public void Board_act_passing_confirm()
    {
        /*
          user_key    : ���� ��ȣ
          room_name   : �� �̸�
          board_num   : ���� ���� ��ȣ
          board_order : ���� ���� ����
          cube_order : ���� ť�� ����
        */
        Alc_Data send_data = new Alc_Builder().User_key(data_hub.My_key)
                                              .Room_name(data_hub.Room_name)
                                              .Board_num(data_hub.Now_board_num)
                                              .Board_order(data_hub.Board_order)
                                              .Cube_order(data_hub.Board_cube_order)
                                              .Board_Passing_Data_Build();

        data_hub.Socket.Emit("board_passing", send_data);

        // ����Ȯ��/ �ѱ�� ��ư �Ⱥ��̰��ϱ�
        GameObject.Find("Switch_Area").transform.Find("Board_act_end").gameObject.SetActive(false);
        GameObject.Find("Switch_Area").transform.Find("Board_act_pass").gameObject.SetActive(false);
    }

    // ���� ���� �ȳ� Ȯ�� ��ư �ൿ
    public void Game_end_confirm()
    {
        Alc_Data send_data = new Alc_Builder().Room_name(data_hub.Room_name)
                                              .Send_Room_Name_Data_Build();

        data_hub.Socket.Emit("game_end_confirm", send_data);
        SceneManager.LoadScene("End_page");
    }

    // �� ������ Ȯ��
    public void Forced_termination_confirm()
    {
        data_hub.No_enter = true;
        SceneManager.LoadScene("Room_page");
    }
    
    public void Quit_room_in_board()
    {
        Alc_Data send_data = new Alc_Builder().Room_name(data_hub.Room_name)
                                              .User_key(data_hub.My_key)
                                              .Common_Data_Build();

        data_hub.Socket.Emit("quit_room_in_board", send_data);
        SceneManager.LoadScene("Lobby");
    }

    #endregion
}
