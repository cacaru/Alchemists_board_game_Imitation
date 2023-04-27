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
    private GameObject show_area;   // 변동되는 데이터를 연동하기 위해 저장해둠
    private GameObject side_menu;   // 사이드메뉴 

    // 고정 변수
    private Color ORDER_CUBE_DEFAULT = new(82 / 255f, 82 / 255f, 82 / 255f, 1);
    private Color ORDER_TEXT_HILIGHT = new(134 / 255f, 255 / 255f, 235 / 255f, 1);
    private Color ORDER_TEXT_DEFAULT = Color.white;
    private readonly string DEFAULT_ANNOUNCE_IN_BOARD = "wasd - 이동 \n 좌측alt - 이동불가/화면조작하기 \n 좌클릭 - 오브젝트 선택";
    private readonly string DEFAULT_ANNOUNCE_IN_CUBE = "순서대로 책을 눌러 \n이번 라운드의 큐브 순서를 결정합니다.";
    private readonly string DEFAULT_ANNOUNCE_IN_ACT = "t키 혹은 선택확정 버튼으로 행동을 확정합니다.";
    private readonly string WAITING_ANNOUNCE = "다른 사람의 행동 확정을 기다리는 중입니다...";

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
        // 내 정보를 설정할 틀 저장해두기
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
        // 첫 번쨰 라운드의 순서 설정
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
            // 좌하단 순서 변수 설정
            // 라운드 시작 전에는 큐브를 선택하는 순서 < > 라운드가 시작하면 라운드의 순서
            // start 는 초기화 이므로 큐브를 선택하는 순서 -> 처음은 무조건 방장부터
            string order_text = "order_" + (i+1).ToString();
            //Debug.Log(order_text);
            GameObject.Find("GUI").transform.Find("Canvas").Find("Show_area").Find("Order_panel").Find(order_text).GetComponent<TMP_Text>().text = data_hub.User_data_array[i].User_name;
        }

        // 행동순서큐브를 결정할 오브젝트를 활성화
        /*
        if (max_count == 4)
            GameObject.Find("Select_order_obj").transform.Find("Over_4").gameObject.SetActive(true);
        else
            GameObject.Find("Select_order_obj").transform.Find("Under_3").gameObject.SetActive(true);*/
        // 웹에서는 인원수 추가가 아직 미구현
        GameObject.Find("Select_order_obj").transform.Find("Under_3").gameObject.SetActive(true);

        // 변치 않는 데이터 설정
        // 내정보 이름 설정
        side_menu.transform.Find("My_nick").GetComponent<TMP_Text>().text = data_hub.My_name;
        // 내 색 큐브 이미지 설정
        string path = "";
        if(data_hub.My_data.User_color.Equals("black"))  path = "source/img/icon/cube_black";
        else if (data_hub.My_data.User_color.Equals("red")) path = "source/img/icon/cube_red";
        else if (data_hub.My_data.User_color.Equals("white")) path = "source/img/icon/cube_white";
        else if (data_hub.My_data.User_color.Equals("blue")) path = "source/img/icon/cube_blue";
        side_menu.transform.Find("My_color").GetComponent<Image>().sprite = Resources.Load<Sprite>(path);
        // 유물 정보 생성
        data_hub.Artifacts_info = GameObject.Find("Data_Controller").GetComponent<Data_Controller>().Init_artifacts_info();
        
        // 변동되는 데이터는 함수로 변경
        Board_Data_Control.Control(show_area, data_hub.My_data);

        // socket.emit 기본 정보 받기 위해 침
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
        // 채팅 받기
        data_hub.Socket.OnUnityThread("chat", (data) =>
        {
            //Debug.Log(data);

            // data :: speaker , msg, type
            chat_area_obj.text += StoD_converter.Resp_to_Chat_Data(data);
            Scroll_to_Bottom.Scroll_to_bottom(chat_area_obj.transform.parent.parent.parent.gameObject.GetComponent<ScrollRect>());

        });

        // 용병에게 판매 가능한 물약 받기
        data_hub.Socket.OnUnityThread("adv_sell_potion_list", (resp) => {
            data_hub.Adventurer_card_data = StoD_converter.Resp_to_Adventurer_Card_Data(resp);
            //Debug.Log(resp);
        });

        // 이번 게임에 사용될 용병들 번호순서 받기
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

            // data_hub에 두기
            data_hub.Random_adv_list = random_adv_list;
            /*
            foreach (var item in random_adv_list)
            {
                Debug.Log(item);
            }
            */
        });

        // 게임 순서 결정 순서 받기
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
            // 내 순서 알아내기
            // 순서 창의 이름 변경 -> 최대 16번 도는 코드...

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
                // 큐브 클릭이 가능하게 변경
                order_cube_selecter.SetActive(true);

                // 준비 완료 버튼 활성화
                GameObject.Find("Switch_Area").transform.Find("Round_order_ready").gameObject.SetActive(true);
                data_hub.Decide_round_order_cont = -1;
            }
        });

        // 선택할 수 있는 재료 카드 공개
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

            // 화면에 공유
            GameObject.Find("Data_Controller").GetComponent<Data_Controller>().Controller_select_ingre(data_hub.Ingredient_select_arr);
        });

        // 유저 정보 받기
        data_hub.Socket.OnUnityThread("get_ingame_data", (resp) => {
            data_hub.User_data_array = StoD_converter.Resp_to_User_Data(resp);

            // GUI 표시 정보 변경
            Board_Data_Control.Control(show_area, data_hub.My_data);
            Board_Data_Control.Other_data_setting(show_area.transform, data_hub.User_data_array, data_hub.My_key);
            // 오브젝트에 표시될 정보 변경
            GameObject.Find("Data_Controller").GetComponent<Data_Controller>().Controller_ingre_count_sign(data_hub.My_data);
            // 사용가능 큐브 갯수 표시
            GameObject.Find("Data_Controller").GetComponent<Data_Controller>().Controller_showing_cube(data_hub.My_data);

            // 방에 성공적으로 입장한 것이므로, chat에 입장 안내메시지를 뿌림
            Alc_Data send_data = new Alc_Builder().Speaker(data_hub.My_name)
                                                  .Msg("방에 입장하였습니다.")
                                                  .Type("announce")
                                                  .Room_name(data_hub.Room_name)
                                                  .Chat_Data_Build();
            //Debug.Log(send_data.print());
            data_hub.Socket.Emit("chat", send_data);
        });

        // 유저 정보 갱신
        data_hub.Socket.OnUnityThread("change_user_data", (resp) => {
            data_hub.User_data_array = StoD_converter.Resp_to_User_Data(resp);

            // GUI 표시 정보 변경
            Board_Data_Control.Control(show_area, data_hub.My_data);
            Board_Data_Control.Other_data_setting(show_area.transform, data_hub.User_data_array, data_hub.My_key);
            // 오브젝트에 표시될 정보 변경
            GameObject.Find("Data_Controller").GetComponent<Data_Controller>().Controller_ingre_count_sign(data_hub.My_data);
            // 사용 가능 큐브 갯수 표시
            GameObject.Find("Data_Controller").GetComponent<Data_Controller>().Controller_showing_cube(data_hub.My_data);

            // 게임 준비 완료 상태라면 더이상 btn을 표기하지 않음.
            if (data_hub.My_data.Is_ready.Equals("true"))
                show_area.transform.Find("GUI_btn_area").Find("Ready_btn").gameObject.SetActive(false);
            // 다른 유저 준비완료 상태도 확인
            Board_Data_Control.Ready_player_shower(show_area, data_hub.User_data_array);
        });

        // 행동 순서를 결정할 큐브를 선택할 순서의 번호를 받음 
        // 1을 받으면 첫번째 유저만 큐브 선택이 가능 , 2~4 순서는 선택 못하게 막기 위한 용도 ( 서버를 통해 공유받아 확인함)
        data_hub.Socket.OnUnityThread("decide_round_setting_order_counter_send", (resp) => {
            // resp to int // [[0]]
            string t = resp.ToString().Replace("[", "").Replace("]", "").Trim();
            data_hub.Decide_round_order_cont = int.Parse(t);
            //Debug.Log("decide_round_order_cont");
            //Debug.Log(decide_round_order_cont);

            // 모든 순서의 색 빼기
            show_area.transform.Find("Order_panel").Find("order_1").GetComponent<TMP_Text>().fontStyle = (FontStyles)FontStyle.Normal;
            show_area.transform.Find("Order_panel").Find("order_1").GetComponent<TMP_Text>().color = ORDER_TEXT_DEFAULT;
            show_area.transform.Find("Order_panel").Find("order_2").GetComponent<TMP_Text>().fontStyle = (FontStyles)FontStyle.Normal;
            show_area.transform.Find("Order_panel").Find("order_2").GetComponent<TMP_Text>().color = ORDER_TEXT_DEFAULT;
            show_area.transform.Find("Order_panel").Find("order_3").GetComponent<TMP_Text>().fontStyle = (FontStyles)FontStyle.Normal;
            show_area.transform.Find("Order_panel").Find("order_3").GetComponent<TMP_Text>().color = ORDER_TEXT_DEFAULT;
            show_area.transform.Find("Order_panel").Find("order_4").GetComponent<TMP_Text>().fontStyle = (FontStyles)FontStyle.Normal;
            show_area.transform.Find("Order_panel").Find("order_4").GetComponent<TMP_Text>().color = ORDER_TEXT_DEFAULT;
            // 현재 순서의 색 칠하기
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
                // 큐브 클릭이 가능하게 변경
                order_cube_selecter.SetActive(true);

                // 준비 완료 버튼 활성화
                GameObject.Find("Switch_Area").transform.Find("Round_order_ready").gameObject.SetActive(true);
            }
        });

        // 판매할 유물 정보
        data_hub.Socket.OnUnityThread("can_buy_artifacts_list", (resp) => {
            // resp to show_artifacts
            //Debug.Log(resp);
            string t = resp.ToString().Replace("[", "").Replace("]", "").Trim();
            int x = t.IndexOf("rank", 0);
            data_hub.Now_rank = int.Parse(t[(x + 6)..(x + 7)]);

            // 데이터는 0부터 5까지 들어오는데 저장은 1~6으로함
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

        // 현재 라운드 전송
        // round_cont : int -> 라운드 변수를 클라 내부에 저장
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

            // 현재 라운드 변수 설정
            GameObject.Find("Round_announce").transform.Find("round_val").GetComponent<TMP_Text>().text = data_hub.Round_cont.ToString();

            // 라운드 변수에 따라 용병표기 변경
            if (data_hub.Round_cont == 2)
            {
                // 2라운드 부터 못가게 막아둔 3곳을 오픈함
                GameObject.Find("Room_Part_3").transform.Find("Gate").gameObject.SetActive(false);
                GameObject.Find("Room_Part_5").transform.Find("Gate").gameObject.SetActive(false);
                GameObject.Find("Room_Part_6").transform.Find("Gate").gameObject.SetActive(false);
            }

            // 용병 표기
            if (data_hub.Round_cont >= 2)
            {
                int[] random_adv_list = data_hub.Random_adv_list;
                GameObject.Find("Data_Controller").GetComponent<Data_Controller>().Controller_adventurer_marking(data_hub.Adventurer_card_data, random_adv_list[data_hub.Round_cont - 2]);
            }

            // 마지막 라운드 전시회 열기
            if (data_hub.Round_cont == 6)
            {
                Vector3 tmp = GameObject.Find("Objects").transform.Find("Round_Room").Find("Wall").Find("End_Gate").localPosition;
                tmp.y = -16f;
                GameObject.Find("Objects").transform.Find("Round_Room").Find("Wall").Find("End_Gate").localPosition = tmp;
            }
        });

        // 게임 추리 및 결과 테이블 전송
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

        // 초기 논문 데이터 전송
        // theory_data[{1:element, stamp{1:user_key,color,point ,2 ,3}}, {2:element~ }, ... {8}]
        data_hub.Socket.OnUnityThread("change_theory_data", (resp) => {
            // [{"1":{"element":"","stamp":{"1":{"user_key":"","color":"","point":""},"2":{"user_key":"","color":"","point":""},"3":{"user_key":"","color":"","point":""}}},"2":{"element":"","stamp":{"1":{"user_key":"","color":"","point":""},"2":{"user_key":"","color":"","point":""},"3":{"user_key":"","color":"","point":""}}},"3":{"element":"","stamp":{"1":{"user_key":"","color":"","point":""},"2":{"user_key":"","color":"","point":""},"3":{"user_key":"","color":"","point":""}}},"4":{"element":"","stamp":{"1":{"user_key":"","color":"","point":""},"2":{"user_key":"","color":"","point":""},"3":{"user_key":"","color":"","point":""}}},"5":{"element":"","stamp":{"1":{"user_key":"","color":"","point":""},"2":{"user_key":"","color":"","point":""},"3":{"user_key":"","color":"","point":""}}},"6":{"element":"","stamp":{"1":{"user_key":"","color":"","point":""},"2":{"user_key":"","color":"","point":""},"3":{"user_key":"","color":"","point":""}}},"7":{"element":"","stamp":{"1":{"user_key":"","color":"","point":""},"2":{"user_key":"","color":"","point":""},"3":{"user_key":"","color":"","point":""}}},"8":{"element":"","stamp":{"1":{"user_key":"","color":"","point":""},"2":{"user_key":"","color":"","point":""},"3":{"user_key":"","color":"","point":""}}}}]
            data_hub.Theory_data = StoD_converter.Resp_to_Theory_Data(resp);
            //Debug.Log(resp);

            // 오브젝트 화면에 표시
            GameObject.Find("Data_Controller").GetComponent<Data_Controller>().Controller_theory_field(data_hub.Theory_data, data_hub.My_key);
        });


        // 물약 전시회 기본 변수 전송
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

        // 행동 순서를 결정하는 큐브를 선택할 떄 사용하는 final_round_order 받기
        data_hub.Socket.OnUnityThread("select_round_order_recive", (resp) => {
            // [[{"user_key":"upo5enxz0PW_laDMAAAI","before_order":0,"order":7,"user_color":"white"},{"user_key":"pzpvRGvqaKwmbTTeAAAG","before_order":0,"order":5,"user_color":"blue"}]]
            data_hub.Final_round_order = StoD_converter.Resp_to_Final_Round_Order_Data(resp);

            // final_round_order에 따라 선택되어 있는 큐브들은 색을 칠하고, 다른 이가 선택할 수 없도록 막아야함
            for (int i = 0; i < data_hub.Final_round_order.Count; i++)
            {
                // 이전에 선택한 것이 있었다면 색을 뺌
                if (data_hub.Final_round_order[i].before_order > 0)
                {
                    string pre_cube_name;
                    if (data_hub.Max_count != 4 && data_hub.Final_round_order[i].before_order == 7)
                        pre_cube_name = "Cube_8";
                    else
                        pre_cube_name = "Cube_" + data_hub.Final_round_order[i].before_order.ToString();

                    GameObject.Find(pre_cube_name).GetComponent<Renderer>().material.color = ORDER_CUBE_DEFAULT;
                }

                // 현재 선택한 것을 조절
                // 큐브 번호가 7번일 때 최대 인원수에 따라 8로 조절해줌
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

            // Order_Cube_Selecter.selected_round_order에 변수 넣어주기
            GameObject.Find("Select_order_obj").transform.GetChild(0).transform.GetComponent<Order_Cube_Select_Event>().selected_round_order = data_hub.Selected_round_order;
        });

        // 행동 순서 결정이 완료됨을 알려옴
        data_hub.Socket.OnUnityThread("decide_round_setting_order_end", (resp) => {
            data_hub.Final_round_order = StoD_converter.Resp_to_Final_Round_Order_Data(resp);

            // 큐브 클릭 데이터 초기화
            order_cube_selecter.GetComponent<Order_Cube_Select_Event>().cube_num = 0;
            data_hub.Selected_round_order[0] = 0; data_hub.Selected_round_order[1] = 0; data_hub.Selected_round_order[2] = 0; data_hub.Selected_round_order[3] = 0;
            order_cube_selecter.GetComponent<Order_Cube_Select_Event>().selected_round_order = data_hub.Selected_round_order;
            // 큐브 클릭이 불가능하게 변경
            order_cube_selecter.SetActive(false);

            data_hub.My_order_cube_select_turn = -1;
            data_hub.Decide_round_order_cont = -1;

            // fade -in
            GameObject.Find("UI_Event_Obj").transform.GetComponent<Black_Out>().enabled = true;
            GameObject.Find("UI_Event_Obj").transform.GetComponent<Black_Out>().Start_fade(true);

            // 벽 오브젝트 비 활성화
            /*
            if (max_count == 4)
                GameObject.Find("Over_4").SetActive(false);
            else
                GameObject.Find("Under_3").SetActive(false);
            웹에서 4인 구현이 되어있지 않으므로 3인 기준
            */
            // 벽 오브젝트의 큐브들 초기화
            GameObject.Find("Data_Controller").GetComponent<Data_Controller>().Init_round_order_cube();
            GameObject.Find("Under_3").SetActive(false);

            // 준비 완료 버튼 비활성화
            GameObject.Find("Switch_Area").transform.Find("Round_order_ready").gameObject.SetActive(false);
            // 아래 도움말 변경
            GameObject.Find("Help_announce").transform.Find("text_area").GetComponent<TMP_Text>().text = DEFAULT_ANNOUNCE_IN_BOARD;

            // 순서 창을 게임 준비 완료 창으로 변경
            show_area.transform.Find("Order_panel").gameObject.SetActive(false);
            show_area.transform.Find("GUI_btn_area").gameObject.SetActive(true);
            show_area.transform.Find("Ready_area").gameObject.SetActive(true);

            // 사용 가능한 큐브 표시
            show_area.transform.Find("Can_use_cube_area").gameObject.SetActive(true);
            GameObject.Find("Data_Controller").GetComponent<Data_Controller>().Controller_showing_cube(data_hub.My_data);

            // 논문 오브젝트 표시
            GameObject.Find("Data_Controller").GetComponent<Data_Controller>().Controller_theory_core_init();
        });

        // 각 큐브 정보를 받아옴
        data_hub.Socket.OnUnityThread("change_user_cube_data", (resp) => {
            //Debug.Log(resp); //[[2,[{"user_key":"CB1JWEm1Ps08MABqAAAE","user_color":"blue","order":6,"length":3,"cube":{"1":{"num":1,"cnt":1,"is_select":false},"2":{"num":2,"cnt":1,"is_select":false},"3":{"num":3,"cnt":1,"is_select":false}}},{"user_key":"eYiBdllRJVxHdk6MAAAB","user_color":"white","order":7,"length":3,"cube":{"1":{"num":1,"cnt":1,"is_select":false},"2":{"num":2,"cnt":1,"is_select":false},"3":{"num":3,"cnt":1,"is_select":false}}}],[{"user_key":"CB1JWEm1Ps08MABqAAAE","user_color":"blue","order":6,"length":2,"cube":{"1":{"num":1,"cnt":1,"is_select":false},"2":{"num":2,"cnt":2,"is_select":false}}},{"user_key":"eYiBdllRJVxHdk6MAAAB","user_color":"white","order":7,"length":2,"cube":{"1":{"num":1,"cnt":1,"is_select":false},"2":{"num":2,"cnt":2,"is_select":false}}}],[{"user_key":"CB1JWEm1Ps08MABqAAAE","user_color":"blue","order":6,"length":1,"cube":{"1":{"num":1,"cnt":2,"is_select":false}}},{"user_key":"eYiBdllRJVxHdk6MAAAB","user_color":"white","order":7,"length":1,"cube":{"1":{"num":1,"cnt":2,"is_select":false}}}],[{"user_key":"CB1JWEm1Ps08MABqAAAE","user_color":"blue","order":6,"length":2,"cube":{"1":{"num":1,"cnt":1,"is_select":false},"2":{"num":2,"cnt":2,"is_select":false}}},{"user_key":"eYiBdllRJVxHdk6MAAAB","user_color":"white","order":7,"length":2,"cube":{"1":{"num":1,"cnt":1,"is_select":false},"2":{"num":2,"cnt":2,"is_select":false}}}],[{"user_key":"CB1JWEm1Ps08MABqAAAE","user_color":"blue","order":6,"length":2,"cube":{"1":{"num":1,"cnt":1,"is_select":false},"2":{"num":2,"cnt":1,"is_select":false}}},{"user_key":"eYiBdllRJVxHdk6MAAAB","user_color":"white","order":7,"length":2,"cube":{"1":{"num":1,"cnt":1,"is_select":false},"2":{"num":2,"cnt":1,"is_select":false}}}],[{"user_key":"CB1JWEm1Ps08MABqAAAE","user_color":"blue","order":6,"length":2,"cube":{"1":{"num":1,"cnt":1,"is_select":false},"2":{"num":2,"cnt":2,"is_select":false}}},{"user_key":"eYiBdllRJVxHdk6MAAAB","user_color":"white","order":7,"length":2,"cube":{"1":{"num":1,"cnt":1,"is_select":false},"2":{"num":2,"cnt":2,"is_select":false}}}],[{"user_key":"CB1JWEm1Ps08MABqAAAE","user_color":"blue","order":6,"length":2,"cube":{"1":{"num":1,"cnt":1,"is_select":false},"2":{"num":2,"cnt":1,"is_select":false}}},{"user_key":"eYiBdllRJVxHdk6MAAAB","user_color":"white","order":7,"length":2,"cube":{"1":{"num":1,"cnt":1,"is_select":false},"2":{"num":2,"cnt":1,"is_select":false}}}],[{"user_key":"CB1JWEm1Ps08MABqAAAE","user_color":"blue","order":6,"length":2,"cube":{"1":{"num":1,"cnt":1,"is_select":false},"2":{"num":2,"cnt":1,"is_select":false}}},{"user_key":"eYiBdllRJVxHdk6MAAAB","user_color":"white","order":7,"length":2,"cube":{"1":{"num":1,"cnt":1,"is_select":false},"2":{"num":2,"cnt":1,"is_select":false}}}],[{"user_key":"CB1JWEm1Ps08MABqAAAE","user_color":"blue","order":6,"length":4,"cube":{"1":{"num":1,"cnt":1,"is_select":false},"2":{"num":2,"cnt":1,"is_select":false},"3":{"num":3,"cnt":1,"is_select":false},"4":{"num":4,"cnt":1,"is_select":false}}},{"user_key":"eYiBdllRJVxHdk6MAAAB","user_color":"white","order":7,"length":4,"cube":{"1":{"num":1,"cnt":1,"is_select":false},"2":{"num":2,"cnt":1,"is_select":false},"3":{"num":3,"cnt":1,"is_select":false},"4":{"num":4,"cnt":1,"is_select":false}}}]]]
            data_hub.User_cube_data = StoD_converter.Resp_to_User_Cube_Data(resp);
            //Debug.Log(user_cube_data.Count);
            if (data_hub.User_cube_data.Count > 0)
                GameObject.Find("Board_Cube_Controller").GetComponent<Board_Cube_Constructor>().Draw_cubes(data_hub.User_cube_data, data_hub.Round_cont, data_hub.My_key, data_hub.In_round);
        });

        // final_round_order 변수 변경 감지
        data_hub.Socket.OnUnityThread("change_final_round_order", (resp) => {
            data_hub.Final_round_order = StoD_converter.Resp_to_Final_Round_Order_Data(resp);
        });

        // 행동 순서에서 큐브 선택이 불가능함을 알리는 함수
        data_hub.Socket.OnUnityThread("cant_use_cube", (resp) => {
            // ["이전 위치부터 큐브를 두어야합니다"] or ["사용 가능한 큐브가 없습니다."]
            // 데이터를 받으면 바로 Help_announce.text_area에 뿌리기 -> 코루틴으로 5초뒤 기본안내로 변경
            string say = resp.ToString().Replace("[", "").Replace("]", "").Replace("{", "").Replace("}", "").Replace("\"", "").Replace("\\n", "\n").Split(":")[1];

            // 코루틴 작성 start
            StartCoroutine(Announce_pannel(say, 1));
            //Debug.Log(resp);
            //Debug.Log(say);
        });



        #region 라운드 행동
        // 시작할 보드 번호 받기
        data_hub.Socket.OnUnityThread("board_start", (resp) =>
        {
            round_end_checker = false;
            data_hub.In_round = true;
            // 큐브 선택을 못하게 redraw
            GameObject.Find("Board_Cube_Controller").GetComponent<Board_Cube_Constructor>().Draw_cubes(data_hub.User_cube_data, data_hub.Round_cont, data_hub.My_key, data_hub.In_round);
            // 카메라의 이동을 금지시킴 
            GameObject.Find("Moving_obj").GetComponent<Camera_Moving>().enabled = false;

            GameObject.Find("Camera_Controller").GetComponent<Mouse_Switch>().in_round = true;
            // 보드 번호에 따라 sub카메라의 pos를 즉시 이동 시킴
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
                    // 이동가능하게 다시 하고 위치만 잡아줌
                    pos = new Vector3(-5.4f, 7.5f, 160f);
                    rot = new Vector3(0f, 0f, 0f);
                    break;
            }

            GameObject.Find("Moving_obj").transform.SetPositionAndRotation(pos, Quaternion.Euler(rot));

            // 불필요 오브젝트 비활성화
            show_area.transform.Find("GUI_btn_area").Find("Ready_btn").gameObject.SetActive(true);
            show_area.transform.Find("GUI_btn_area").gameObject.SetActive(false);
            show_area.transform.Find("Ready_area").gameObject.SetActive(false);
        });

        // 행동의 순서 번호 받기
        data_hub.Socket.OnUnityThread("change_board_order_val", (resp) => {
            /*
             * board_order : start_board_order,
             * board_cube_order : 1,
             * [{"board_order":0,"board_cube_order":1}]
             */
            //Debug.Log(resp);
            string t = resp.ToString();
            // board_order :: 현재 보드 사용자 번호 : 0 : 첫번쨰 순서색의 유저, 1 : 두번쨰 순서색의 유저 etc
            // board_cube_order :: 현재 보드 사용자의 큐브 순서 번호 , 최소 1 최대 3까지
            data_hub.Board_order = int.Parse(t[t.IndexOf("board_order") + 13].ToString());
            data_hub.Board_cube_order = int.Parse(t[t.IndexOf("_cube_order") + 13].ToString());

            //Debug.Log(board_order);
            //Debug.Log(board_cube_order);
        });

        #region 용병 거래 구역
        // 할인카드 제시 스텝이 완전히 끝났을 때 받는 함수
        data_hub.Socket.OnUnityThread("end_adv_dis_step", (resp) => {
            //[[{"user_key":"knVwUzIJ4NguKyOLAAAk","color":"white","dis_coin_num":1,"room_name":"qwer"},{"user_key":"6_eiwJi5PWudyOfqAAAn","room_name":"qwer","color":"blue","dis_coin_num":1}]]
            //Debug.Log(resp);
            data_hub.Dis_coin_data = StoD_converter.Resp_to_Discount_Coin_List_Data(resp);
            // 할인 카드 엔드 알림
            data_hub.Coin_step_end = true;
            // 순서에 따라 오브젝트 생성
            GameObject.Find("Data_Controller").GetComponent<Adv_Obj_Construct_for_Play>().Dis_order_show(data_hub.Dis_coin_data);
            data_hub.Now_board_num = 3;
        });

        // 용병에게 물약판매에서 판매금액을 서버에 전송하고 전송 성공시 시작을 알릴 함수
        data_hub.Socket.OnUnityThread("selling_start", (resp) => {
            // resp == true :: 호의카드 사용
            //      == false :: 호의카드 미사용
            //Debug.Log(resp);
            data_hub.Now_board_num = 3;
        });

        // 용병에게 물약 판매 스텝에서 다음 순서를 알림
        data_hub.Socket.OnUnityThread("change_selling_turn", (resp) => {
            //Debug.Log(resp);
            data_hub.Selling_turn = int.Parse(resp.ToString().Replace("[", "").Replace("]", ""));
        });

        // 용병에게 물약 판매에서 한 유저의 물약 판매가 종료되었음을 알림
        data_hub.Socket.OnUnityThread("selling_potion_end", (resp) =>
        {
            /*
             *  selling_success : selling_success,
                potion : data.what_kind_sell_potion,
                user_key : data.user_key,
                user_color : data.user_color,
             */
            data_hub.Selling_potion_end = StoD_converter.Resp_to_Selling_Potion_End(resp);

            // 판매 결과 모달 띄우기
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
                data_hub.Selling_potion_end.selling_success ? "성공" : "실패";

            result_sect.transform.Find("Normal_Form").gameObject.SetActive(true);
            result_sect.transform.Find("Refute_Form").gameObject.SetActive(false);
            result_sect.GetComponent<Result_Sect_Open>().Open();
        });
        #endregion

        #region 논문 발표/반박 영역
        // 논문 발표 결과 or 재시작 안내
        data_hub.Socket.OnUnityThread("theory_presentation_success", (resp) => {
            // resp : [[{"user_key":"","restart":false,"success":true}]]
            //Debug.Log(resp);
            string t = resp.ToString().Replace("[", "").Replace("]", "").Replace("{","").Replace("}", "").Replace("\"", "");
            string user_key = t.Split(",")[0].Split(":")[1];
            bool restart = t.Split(",")[1].Split(":")[1].Equals("true");
            bool success = t.Split(",")[2].Split(":")[1].Equals("true");

            // restart 가 true 면 논문 발표를 다시 시작함
            if (restart)
            {
                // 이 함수의 시점에는 data_hub의 자료들이 초기화 되어 처음부터 다시시작할 수 있음
                data_hub.Now_board_num = 6;
            }
            // restart가 false고 success가 true 면 발표 성공
            else if (!restart && success)
            {
                // 발표 성공 창의 정보를 수정
                for(int i = 0; i < data_hub.User_data_array.Count; i++) {
                    if (data_hub.User_data_array[i].User_key.Equals(user_key))
                    {
                        StartCoroutine(Announce_pannel(data_hub.User_data_array[i].User_name + "이(가) 논문 발표에 성공하였습니다!", 2));
                        break;
                    }
                }
            }
        });

        // 논문 반박 성공/실패 알림
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
            // stamp의 값이 공란이 아닌경우
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
                // 2 부터는 여부를 확인해야함
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
            // else의 경우 값이 없는것 == success가 false 이므로 굳이 필요 없음

            // 위 내용으로 반박 성공/실패 모달을 띄움
            if (success)
            {
                // 확인 바로 emit 하기 -> resp 그대로 다시 전송
                Alc_Data send_data = new Alc_Builder().User_key(refute_user_key)
                                                      .Room_name(room_name)
                                                      .Ingre_num(ingre_num)
                                                      .Ori(ori)
                                                      .Success(success)
                                                      .Stamp(stamp_data)
                                                      .Refute_Check_Info_Build();

                data_hub.Socket.Emit("check_refute_info", send_data);

                // result, color, name, ori, stamp
                result_sect.transform.Find("Refute_Form").Find("Result").GetComponent<TMP_Text>().text = "성공";
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

                // stamp 영역 보이게하기
                result_sect.transform.Find("Refute_Form").Find("Success_Area").gameObject.SetActive(true);

                // stamp 
                // user_key[0][1][2] 를 검사해 있으면 stamp 넣기 , 발표되었다면 0은 무조건 있었을것
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
                result_sect.transform.Find("Refute_Form").Find("Result").GetComponent<TMP_Text>().text = "실패";
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

        #region 실험 구역
        data_hub.Socket.OnUnityThread("test_ingredient_result", (resp) => {
            /*
                user_key : data.user_key,
                test_result : result_test => 포션 종류,
                [[{"user_key":"123124151515","test_result":"red_1"}]]
            */
            //Debug.Log(resp);
            string t = resp.ToString().Replace("[", "").Replace("]", "").Replace("{", "").Replace("}", "");
            string user_key = t.Split(",")[0].Replace("\"", "").Split(":")[1];
            string test_result = t.Split(",")[1].Replace("\"", "").Split(":")[1];

            // 실험 결과 보이기
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
                result_sect.transform.Find("Normal_Form").Find("Result").GetComponent<TMP_Text>().text = "결과";
            }
            else if (data_hub.Now_board_num == 8)
            {
                result_sect.transform.Find("Normal_Form").Find("Potion").GetComponent<Image>().sprite = potion;
                result_sect.transform.Find("Normal_Form").Find("Color").GetComponent<Image>().sprite = color;
                result_sect.transform.Find("Normal_Form").Find("Result").GetComponent<TMP_Text>().text = "결과";
            }

            result_sect.transform.Find("Normal_Form").gameObject.SetActive(true);
            result_sect.transform.Find("Refute_Form").gameObject.SetActive(false);
            result_sect.GetComponent<Result_Sect_Open>().Open();

        });
        #endregion

        #region 전시회
        data_hub.Socket.OnUnityThread("show_exhibition", (resp) => {
            /*
             * user_name : room_data[data.room_name].user_data_array[user_num].user_name,  string
               user_key : data.user_key,    string
               get_cube_success,         bool
               result : true,            bool
             * */
            string t = resp.ToString().Replace("[", "").Replace("]", "").Replace("{", "").Replace("}", "").Replace("\"", "");
            string[] list = t.Split(",");

            //데이터 가공
            string exhibit_user_name = list[0].Split(":")[1];
            string exhibit_user_key = list[1].Split(":")[1];
            bool exhibit_success = list[2].Split(":")[1].Equals("true");
            bool exhibit_result = list[3].Split(":")[1].Equals("true");

            // 전시 성공이라면 announce에 어떤 물약
            if (exhibit_result && exhibit_success)
            {
                StartCoroutine(Announce_pannel(exhibit_user_name + "이(가) 전시에 성공하였습니다!\n명성이 1점 증가합니다.", 2));
                // 전시대위의 큐브색 조절
                // -> change_exhibition_data 에서 진행
            }
            else if (exhibit_result && !exhibit_success)
            {
                StartCoroutine(Announce_pannel(exhibit_user_name + "이(가) 전시용 물약 생성에는 성공하였으나\n전시장이 꽉 차 전시에는 실패하였습니다.", 2));
            }
            else if (!exhibit_result)
            {
                StartCoroutine(Announce_pannel(exhibit_user_name + "이(가) 전시용 물약 생성에 실패하였습니다.\n명성이 1점 감점됩니다.", 2));
            }
            
        });

        #endregion

        data_hub.Socket.OnUnityThread("round_end", (resp) => {
            // 카메라 변경
            // 켜진 모든 것을 끄고 
            // 첫번째 보드 선택으로 변경

            round_end_checker = true;
            my_act_end = false;
            data_hub.In_round = false;
            // fade -in
            GameObject.Find("UI_Event_Obj").transform.GetComponent<Black_Out>().enabled = true;
            GameObject.Find("UI_Event_Obj").transform.GetComponent<Black_Out>().Start_fade(false);

            // 벽 오브젝트 활성화
            /*
            if (max_count == 4)
                GameObject.Find("Over_4").SetActive(false);
            else
                GameObject.Find("Under_3").SetActive(false);
            웹에서 4인 구현이 되어있지 않으므로 3인 기준
            */
            GameObject.Find("Front").transform.Find("Select_order_obj").Find("Under_3").gameObject.SetActive(true);

            // 아래 도움말 전환
            GameObject.Find("Help_announce").transform.Find("text_area").GetComponent<TMP_Text>().text = DEFAULT_ANNOUNCE_IN_CUBE;

            // 준비완료 창을 순서창으로 변경
            show_area.transform.Find("Order_panel").gameObject.SetActive(true);
            show_area.transform.Find("GUI_btn_area").Find("Ready_btn").gameObject.SetActive(true);
            show_area.transform.Find("GUI_btn_area").gameObject.SetActive(false);
            show_area.transform.Find("Ready_area").gameObject.SetActive(false);

            // 사용 가능한 큐브 표시 해제
            show_area.transform.Find("Can_use_cube_area").gameObject.SetActive(false);

            // 마우스 alt가 눌러진 상태라면 해제
            GameObject.Find("Camera_Controller").GetComponent<Mouse_Switch>().Outside_mouse_controll(true);
            // 이동 불가 고정 변수 해제
            GameObject.Find("Camera_Controller").GetComponent<Mouse_Switch>().in_round = false;

            // 3번 용병 관련 조작들 초기화
            data_hub.Now_board_num = -1;
            data_hub.My_board_3_end = false;
            data_hub.Selling_turn = 0;
            GameObject.Find("Room_Part_3").transform.Find("Ingre_Select_Sect").Find("Waiting_announce").gameObject.SetActive(false);
        });

        #endregion

        #region 게임 종료

        data_hub.Socket.OnUnityThread("game_end_event", (resp) => {
            /*
             * resp :: 
             * [[{1:{"name":"string","score":12,"grade":1},2:{"name":string,"score":12","grade":2},~~ 최대 4번 까지{}}]]
             */
            // connection에 넘기기// 이름에 따른 색 정보도 추가해서 넘겨야함
            Dictionary<int, Game_Result_Data> tmp = StoD_converter.Resp_to_Game_Result_Data(resp);

            // tmp에 색 집어넣기
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

            // 게임 종료 알람이므로
            // 게임이 종료되었다고 안내창을 띄움 -> 확인버튼으로 결과창으로 이동
            GameObject.Find("Show_area").transform.Find("Game_end_announce").gameObject.SetActive(true);
            // 이동, 마우스 움직임 막기
            //Destroy(GameObject.Find("Camera_Controller").GetComponent<Mouse_Switch>());
            //Destroy(GameObject.Find("Moving_obj").GetComponent<Camera_Moving>());
        });

        #endregion

        data_hub.Socket.OnUnityThread("move_room_page_check", (resp) => {
            // 방 이동 확인 안내창
            // 확인 누르면 scene 이동
            //SceneManager.LoadScene("Room_page");
            GameObject.Find("GUI").transform.Find("Canvas").Find("Show_area").Find("Forced_termination_announce").gameObject.SetActive(true);
        });
    }

    #region 일반 함수부

    private void Board_deactive(int board_num)
    {
        bool off = false;
        switch (board_num)
        {
            case 1:
                // 선택 가능하게하는 object_selecter 제거
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
                // 생성했던 재료 선택 객체들 파괴
                GameObject.Find("Data_Controller").GetComponent<Ingre_Selecter_for_Play>().Destory_ingre_obj();
                off = true;
                break;
            // 용병에게 물약판매
            case 3:
                if (data_hub.My_coin_step_end == false &&
                    data_hub.Coin_step_end == false &&
                    data_hub.Price_step_end == false &&
                    data_hub.Potion_step_end == false)
                {
                    // 할인 금액 제시 완료 
                    data_hub.My_coin_step_end = true;
                    GameObject.Find("Data_Controller").GetComponent<Adv_Obj_Construct_for_Play>().Discount_obj_destroy();
                    off = true;
                }
                else if (data_hub.My_coin_step_end &&
                         data_hub.Coin_step_end &&
                         data_hub.Price_step_end == false &&
                         data_hub.Potion_step_end == false)
                {
                    // 판매가 설정 완료
                    data_hub.Price_step_end = true;
                    GameObject.Find("Data_Controller").GetComponent<Adv_Obj_Construct_for_Play>().Price_obj_Destroy();
                    off = true;
                }
                else if (data_hub.My_coin_step_end        &&
                         data_hub.Coin_step_end           &&
                         data_hub.Price_step_end          &&
                         data_hub.Potion_step_end == false)
                {
                    // 물약 설정 끝
                    data_hub.Potion_step_end = true;
                    GameObject.Find("Data_Controller").GetComponent<Adv_Obj_Construct_for_Play>().Potion_obj_destroy();
                    // 판매 재료 띄워줘야함
                    // board_act_checker 호출용 변수 제어
                    data_hub.Now_board_num = 3;
                }
                else if (data_hub.My_coin_step_end &&
                         data_hub.Coin_step_end &&
                         data_hub.Price_step_end &&
                         data_hub.Potion_step_end )
                {
                    // 판매 완료
                    // 순서 표지와 재료 제거
                    GameObject.Find("Data_Controller").GetComponent<Adv_Obj_Construct_for_Play>().Dis_order_destroy();
                    GameObject.Find("Data_Controller").GetComponent<Ingre_Selecter_for_Play>().Destory_ingre_obj();
                    // 모든 스텝이 끝났으므로 스텝 함수를 전부 false로 초기화
                    data_hub.My_coin_step_end = false;
                    data_hub.Coin_step_end = false;
                    data_hub.Price_step_end = false;
                    data_hub.Potion_step_end = false;

                    // 내 차례가 끝났으므로 내 차례의 종료 여부는 true로 변경
                    data_hub.My_board_3_end = true;
                    // 보낸 재료 번호도 초기화
                    data_hub.Select_ingre[0] = "card_0";
                    data_hub.Select_ingre[1] = "card_0";
                    off = true;
                }
                break;
            case 4:
                // 구매 버튼 비활성화
                GameObject.Find("Room_Part_4").transform.Find("Btn_Sect").gameObject.SetActive(false);
                // 구매 확인 창 끄기
                GameObject.Find("Show_area").transform.Find("Buy_announce").gameObject.SetActive(false);
                off = true;
                break;
            case 5:
                // 반박할 재료 선택 -> 원소 선택 -> 재료 선택 -> 반박 결과 창
                if (data_hub.Core_end_5 == false &&
                    data_hub.Ori_ele_end_5 == false )
                {
                    // 반박할 재료 선택 끝
                    data_hub.Core_end_5 = true;
                    data_hub.Now_board_num = 5;
                    // 재료 선택 script off
                    GameObject.Find("Data_Controller").GetComponent<Data_Controller>().Controller_theory_core_selecter(5, false);
                }
                else if (data_hub.Core_end_5 &&
                         data_hub.Ori_ele_end_5 == false )
                {
                    // 원소 선택 끝
                    data_hub.Ori_ele_end_5 = true;
                    data_hub.Now_board_num = 5;
                    // 원소 내리기
                    GameObject.Find("Data_Controller").GetComponent<Theory_Refute_Obj_for_Play>().Destroy_ele();
                }
                else if (data_hub.Core_end_5 &&
                         data_hub.Ori_ele_end_5  )
                {
                    // 재료 내리기
                    GameObject.Find("Data_Controller").GetComponent<Theory_Refute_Obj_for_Play>().Destroy_ingre();

                    // 완전 끝
                    // 사용 변수 초기화
                    data_hub.Core_end_5 = false;
                    data_hub.Ori_ele_end_5 = false;

                    data_hub.Ingre_arr[0] = 0;
                    data_hub.Ingre_arr[1] = 0;
                    data_hub.Core_num = -1;
                    data_hub.Ele_num = -1;

                    off = true;
                }
                break;
                // 논문 발표
            case 6:
                // 발표할 재료 선택 -> 원소 선택 -> 인장 선택 -> 발표 완료창
                if (data_hub.Core_end_6 == false &&
                    data_hub.Element_end_6 == false )
                {
                    // 발표할 재료 선택 - core_end_6
                    data_hub.Core_end_6 = true;
                    data_hub.Now_board_num = 6;
                    // 재료 선택 script off
                    GameObject.Find("Data_Controller").GetComponent<Data_Controller>().Controller_theory_core_selecter(6, false);
                }
                else if (data_hub.Core_end_6 &&
                         data_hub.Element_end_6 == false )
                {
                    // 원소 선택 - element_end_6
                    data_hub.Element_end_6 = true;
                    data_hub.Now_board_num = 6;
                    // 원소 오브젝트 내리기
                    GameObject.Find("Data_Controller").GetComponent<Theory_Present_Obj_for_Play>().Destroy_ele();
                }
                else if (data_hub.Core_end_6 &&
                         data_hub.Element_end_6 )
                {
                    // 인장 내리기
                    GameObject.Find("Data_Controller").GetComponent<Theory_Present_Obj_for_Play>().Destroy_stamp();

                    // 변수 초기화
                    data_hub.Core_end_6 = false;
                    data_hub.Element_end_6 = false;
                    data_hub.Core_num = -1;
                    data_hub.Stamp_num = -1;
                    data_hub.Ele_num = -1;

                    // 발표에 성공했다고 안내문자를 보내야함
                    off = true;
                }
                break;
            case 7:
                // 생성했던 재료 선택 객체들 파괴
                GameObject.Find("Data_Controller").GetComponent<Ingre_Selecter_for_Play>().Destory_ingre_obj();
                // 보낸 재료 번호도 초기화
                data_hub.Select_ingre[0] = "card_0";
                data_hub.Select_ingre[1] = "card_0";
                off = true;
                break;
            case 8:
                // 생성했던 재료 선택 객체들 파괴
                GameObject.Find("Data_Controller").GetComponent<Ingre_Selecter_for_Play>().Destory_ingre_obj();
                // 보낸 재료 번호도 초기화
                data_hub.Select_ingre[0] = "card_0";
                data_hub.Select_ingre[1] = "card_0";
                off = true;
                break;
            case 9:
                if (data_hub.Exhibit_potion_step == false)
                {
                    // 물약 선택 가능한 큐브 end
                    // select_cube 끄기
                    GameObject.Find("Room_Part_9").transform.Find("Select_Sect").gameObject.SetActive(false);
                    // potion_step true
                    data_hub.Exhibit_potion_step = true;
                    data_hub.Now_board_num = 9;
                }
                else if (data_hub.Exhibit_potion_step)
                {
                    // 선택된 포션의 위치에서 재료 end
                    // 재료 오브젝트 끄기
                    GameObject.Find("Data_Controller").GetComponent<Exhibit_Ingre_for_Play>().Destroy_ingre_for_exhibition();
                    // 사용된 변수 초기화
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
            // 행동 완료 버튼 비활성화
            GameObject.Find("Switch_Area").transform.Find("Board_act_end").gameObject.SetActive(false);
            GameObject.Find("Switch_Area").transform.Find("Board_act_pass").gameObject.SetActive(false);

            my_act_end = true;
        }
    }

    // cant_user_cube에서 사용될 코루틴 함수
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

    // 5초뒤 결과를 끌 코루틴 함수
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
        // 빈 값은 전송 x
        if (my_chat_input_obj.text.Equals("")) return;
        Alc_Data send_data = new Alc_Builder().Speaker(data_hub.My_name)
                                              .Msg(my_chat_input_obj.text)
                                              .Type("normal")
                                              .Room_name(data_hub.Room_name)
                                              .Chat_Data_Build();
        //Debug.Log(send_data.print());

        data_hub.Socket.Emit("chat", send_data);

        my_chat_input_obj.text = "";

        // observer 끄기
        GameObject.Find("Data_Controller").GetComponent<Chat_Observe_in_Board>().enabled = false;
        // focus 해제
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

    // 행동순서큐브를 선택할 때마다 눌릴 함수 -> 다른 유저들도 알기 위해 서버에 무엇을 골랐는지 전송
    public void Round_order_cube_select(int cube_num)
    {
        // 3인 이하일때 cube_num이 8이라면
        if(data_hub.Max_count <= 3 && cube_num == 8)
        {
            cube_num -= 1;
        }

        // 전송할 데이터 빌딩
        Alc_Data send_data = new Alc_Builder().Room_name(data_hub.Room_name)
                                              .Select_order(cube_num)
                                              .User_key(data_hub.My_key)
                                              .Select_Round_Order_Data_Build();

        //Debug.Log(send_data.print());

        data_hub.Socket.Emit("select_round_order", send_data);
    }

    // 행동순서큐브를 선택 후 준비완료를 결정할 때 사용될 함수
    public void Round_order_ready()
    {
        // emit 하기 위해 데이터 받아오기
        int cube_num = GameObject.Find("Order_Cube_Selecter").GetComponent<Order_Cube_Select_Event>().cube_num;

        // cube_num이 0보다 작으면 전송불가
        if (cube_num <= 0 || cube_num > 9)
        {
            // 안내창을 할까
            return;
        }

        Alc_Data send_data = new Alc_Builder().Room_name(data_hub.Room_name).Send_Room_Name_Data_Build();

        data_hub.Socket.Emit("decide_round_setting_order_counter_incre", send_data);

        // 큐브 클릭 데이터 초기화
        order_cube_selecter.GetComponent<Order_Cube_Select_Event>().cube_num = 0;
        data_hub.Selected_round_order[0] = 0; data_hub.Selected_round_order[1] = 0; data_hub.Selected_round_order[2] = 0; data_hub.Selected_round_order[3] = 0;
        order_cube_selecter.GetComponent<Order_Cube_Select_Event>().selected_round_order = data_hub.Selected_round_order;
        // 큐브 클릭이 불가능하게 변경
        order_cube_selecter.SetActive(false);
        // 준비 완료 버튼 비활성화
        GameObject.Find("Switch_Area").transform.Find("Round_order_ready").gameObject.SetActive(false);
        
    }

    // 행동의 큐브를 선택했을 때 서버에 데이터를 넘길 함수
    public void Select_board_cube(int[] info)
    {
        /*
            user_key,
            board_num : 보드 번호, info[0]
            button_order_num : 버튼 순서, -> cube_num info[1]
            room_name : 방 이름
        */
        Alc_Data send_data = new Alc_Builder().User_key(data_hub.My_key)
                                              .Room_name(data_hub.Room_name)
                                              .Board_num(info[0])
                                              .Button_order_num(info[1])
                                              .Select_Cube_Data_Build();
        //Debug.Log(send_data.print());

        data_hub.Socket.Emit("select_cube", send_data);
    }

    // 라운드 준비 완료 버튼을 눌렀을 때
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
        // 필요 데이터
        // room_name, user_key
        Alc_Data send_data = new Alc_Builder().Room_name(data_hub.Room_name)
                                              .User_key(data_hub.My_key)
                                              .Common_Data_Build();

        data_hub.Socket.Emit("round_ready_on", send_data);

        show_area.transform.Find("Round_ready_announce").gameObject.SetActive(false);

        // 준비 완료 챗을 뿌림
        send_data = new Alc_Builder().Speaker(data_hub.My_name)
                                     .Msg("준비 완료하였습니다.")
                                     .Type("announce")
                                     .Room_name(data_hub.Room_name)
                                     .Chat_Data_Build();
        //Debug.Log(send_data.print());
        data_hub.Socket.Emit("chat", send_data);
    }

    // 호의카드 사용하기
    public void Favor_card_use_confirm()
    {
        // 현재 선택된 호의카드에 따라 필요한 데이터가 있는지 확인
        // 공백 데이터라면 빈값을 넣어 보냄
        // 데이터가 올바른지 확인할 변수
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
                // select_board_num 값이 0보다 크고 9보다 작은지 확인
                if (data_hub.Select_board_num > 0 && data_hub.Select_board_num < 9)
                    check_result = true;

                break;
            case "herbalist":
                // select_ingre에 card_1~card_8 사이의 두 값이 있는지 확인
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

        // 값 확인이 정상이라면 emit
        if (check_result)
        {
            // data :
            //    user_key : 유저 번호
            //    room_name : 방 이름
            //    favor_card : 사용하는 호의카드
            //    select_board_num : 힘센친구 카드가 사용될 때 필요한 변수
            //    ingre_list : 약초학자 카드가 사용될 떄 필요한 변수
            Alc_Data send_data = new Alc_Builder().User_key(data_hub.My_key)
                                                  .Room_name(data_hub.Room_name)
                                                  .Favor_card(data_hub.Favor_card_name)
                                                  .Select_board_num(data_hub.Select_board_num)
                                                  .Ingre_list(data_hub.Select_ingre)
                                                  .Favor_Card_Use_Confirm_Data_Build();

            data_hub.Socket.Emit("favor_card_use_confirm", send_data);

            //전송 후 favor_card의 use window 끄기
            GameObject.Find("Data_Controller").GetComponent<Favor_Card_Use_Window_Control>().Using_window_close();
        }
    }

    // 한 보드의 행동을 확정할 때
    public void Board_act_end_confirm()
    {
        Alc_Data send_data;
        //Debug.Log(now_board_num);
        // 보드 번호에 따라 행동이 달라야함
        switch (data_hub.Now_board_num)
        {
            case 1:     // 재료 받기

                //Debug.Log(select_ingre_num);
                //Debug.Log(index);
                // pick_ingredient 로 보내기
                /*
                  user_key : this.my_key,
                  pick_item : 선택한 카드
                  cube_order : 큐브 순서
                  board_order : this.board_order,
                  ingredient_select_arr_order : index,
                  room_name : 방 이름
                */
                // 선택한 카드가 없으면 선택하라고 안내하고 종료해야함
                if (data_hub.Select_ingre_num < 0 || data_hub.Select_ingre_num > 8)
                {
                    StartCoroutine(Announce_pannel("카드를 선택해야 행동을 확정할 수 있습니다.", 2));
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
            case 2:     // 재료 판매
                //Board_deactive(now_board_num);
                /*
                  user_key : this.my_key,
                  sell_item_num : sell_item_num,
                  board_order : this.board_order,
                  cube_order : this.board_cube_order,
                  room_name : this.room_name,
                */
                // 선택한 카드가 없으면 선택하라고 안내하고 종료해야함
                if (data_hub.Sell_item_num < 1 || data_hub.Sell_item_num > 8)
                {
                    StartCoroutine(Announce_pannel("카드를 선택해야 행동을 확정할 수 있습니다.", 2));
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
            case 3:     // 용병에게 물약 판매
                // 할인카드 제시
                if (data_hub.My_coin_step_end == false &&
                    data_hub.Coin_step_end == false    &&
                    data_hub.Price_step_end == false   &&
                    data_hub.Potion_step_end == false     )
                {
                    // 용병에게 할인카드 제시 함수
                    // 제시한 할인카드는 이번 게임에서 사용 불가
                    /*
                       data :
                        user_key      :: 유저키
                        color         :: 유저색
                        dis_coin_num  :: 선택한 할인 제시 카드
                        room_name     :: 방 이름
                    */// 선택한 카드가 없으면 선택하라고 안내하고 종료해야함
                    if (data_hub.Dis_coin_num < 0 || data_hub.Dis_coin_num > 3)
                    {
                        StartCoroutine(Announce_pannel("카드를 선택해야 순서를 확정할 수 있습니다.", 2));
                        return;
                    }
                    send_data = new Alc_Builder().User_key(data_hub.My_key)
                                                 .Room_name(data_hub.Room_name)
                                                 .Color(data_hub.My_data.User_color)
                                                 .Dis_coin_num(data_hub.Dis_coin_num)
                                                 .Adv_Dis_Confirm_Data_Build();
                    data_hub.Socket.Emit("adv_dis_confirm", send_data);
                    
                }
                // 판매가 설정
                else if (data_hub.My_coin_step_end        &&
                         data_hub.Coin_step_end           &&
                         data_hub.Price_step_end == false &&
                         data_hub.Potion_step_end == false )
                {
                    if (data_hub.Selling_price < 1 || data_hub.Selling_price > 4)
                    {
                        StartCoroutine(Announce_pannel("판매가를 설정해야 확정할 수 있습니다.", 2));
                        return;
                    }
                    // 판매 금액을 홀드하기 위해서는 내 턴이어야함
                    if (data_hub.Dis_coin_data[data_hub.Selling_turn].user_key == data_hub.My_key)
                    {
                        // 용병에게 제시한 판매 금액 홀드
                        /*
                          data
                            user_key  :: 유저키
                            sell_price :: 판매가
                            room_name  :: 방 이름
                        */
                        send_data = new Alc_Builder().User_key(data_hub.My_key)
                                                     .Room_name(data_hub.Room_name)
                                                     .Sell_price(data_hub.Selling_price)
                                                     .Sell_Price_Confirm_Data_Build();

                        data_hub.Socket.Emit("sell_price_confirm", send_data);
                    }
                    else
                    {
                        StartCoroutine(Announce_pannel("아직 차례가 아니라 확정할 수 없습니다.", 2));
                    }
                }
                // 판매 물약 설정
                else if (data_hub.My_coin_step_end        &&
                         data_hub.Coin_step_end           &&
                         data_hub.Price_step_end          &&
                         data_hub.Potion_step_end == false   )
                {
                    // 재료설정으로 넘기기
                    // emit 하지 않음
                    if (data_hub.Sell_potion.Length < 1)
                    {
                        StartCoroutine(Announce_pannel("제조할 물약을 선택해야 순서를 확정할 수 있습니다.", 2));
                        return;
                    }

                }
                // 판매
                else if (data_hub.My_coin_step_end      &&
                         data_hub.Coin_step_end         &&
                         data_hub.Price_step_end        &&
                         data_hub.Potion_step_end )
                {
                    // 판매 개시 emit
                    /*
                        data:
                        user_key  :: 유저키
                        user_color:: 유저색
                        card_list :: 물약을 만들 2가지 재료 (card_1~8)
                        what_kind_sell_potion :: 만들 것을 선택한 물약의 종류(red_1,0/ green_1,0 / blue_1,0)\
                        selling_turn :: 현재판매 순서
                        room_name :: 방 이름
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
            case 4:     // 유물 구매
                /*
                  board_order : this.board_order,
                  cube_order : this.board_cube_order,
                  rank : data.rank,
                  arti_num : data.num,
                  user_key : this.my_key,
                  room_name : this.room_name,
                 */
                // arti_num이 -1 이거나 8이면 작동해서는 안됨
                if (data_hub.Arti_num == -1 || data_hub.Arti_num == 8)
                {
                    StartCoroutine(Announce_pannel("유물 구매 버튼을 눌러야 확정됩니다.", 2));
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

            case 5:     // 논문 반박
                // 반박할 재료 선택 -> 원소 선택 -> 재료 선택 -> 반박 결과 창
                if (data_hub.Core_end_5 == false &&
                    data_hub.Ori_ele_end_5 == false )
                {
                    // 반박할 재료 선택
                    if (data_hub.Core_num < 0 || data_hub.Core_num > 8)
                    {
                        StartCoroutine(Announce_pannel("반박할 재료를 선택해야 다음 과정으로 넘어갈 수 있습니다.\n재료를 선택해주세요!.", 2));
                        return;
                    }
                }
                else if (data_hub.Core_end_5 &&
                         data_hub.Ori_ele_end_5 == false  )
                {
                    // 원소 선택
                    if (data_hub.Ele_num < 0 || data_hub.Ele_num > 3)
                    {
                        StartCoroutine(Announce_pannel("반박할 원소의 색을 선택해야 다음 과정으로 넘어갈 수 있습니다.\n원소를 골라주세요.", 2));
                        return;
                    }
                }
                else if (data_hub.Core_end_5 &&
                         data_hub.Ori_ele_end_5  )
                {
                    // 재료 선택 
                    // 최종 선택 완료이므로 emit
                    /*
                      ingre    : 선택된 원소          1~8 num
                      ori      : 반박할 원소의 색깔    1 red  2 green 3 blue num
                      user_key : 반박한 사람          string
                      arr      : 원소의 색깔이 틀렸다는 것을 주장하기 위한 2가지 재료 1~8 *2
                      cube_order : 큐브 순서 
                      board_order: 보드 순서
                      room_name : 방이름
                    */
                    // 재료가 2가지 모두 선택이 안되어있따면 선택하라고 알림
                    if ((data_hub.Ingre_arr[0] < 0 || data_hub.Ingre_arr[0] > 8) || (data_hub.Ingre_arr[1] < 0 || data_hub.Ingre_arr[1] > 8))
                    {
                        StartCoroutine(Announce_pannel("재료 2가지를 모두 선택해야 합니다.\n2 가지의 재료를 골라주세요.", 2));
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

            case 6:     // 논문 발표
                // 발표할 재료 선택 -> 원소 선택 -> 인장 선택 -> 발표 완료창
                if (data_hub.Core_end_6 == false &&
                    data_hub.Element_end_6 == false )
                {
                    // 발표할 재료 선택의 순간
                    if (data_hub.Core_num < 0 || data_hub.Core_num > 8)
                    {
                        StartCoroutine(Announce_pannel("발표할 재료를 선택해야 다음 과정으로 넘어갈 수 있습니다.\n재료를 선택해주세요!.", 2));
                        return;
                    }
                }
                else if (data_hub.Core_end_6 &&
                         data_hub.Element_end_6 == false )
                {
                    // 원소 선택의 순간
                    if (data_hub.Ele_num < 0 || data_hub.Ele_num > 8)
                    {
                        StartCoroutine(Announce_pannel("발표할 재료가 가진 원소를 선택해야 다음 과정으로 넘어갈 수 있습니다.\n원소를 골라주세요.", 2));
                        return;
                    }
                }
                else if (data_hub.Core_end_6 &&
                         data_hub.Element_end_6  )
                {
                    // 인장 선택의 순간
                    // 인장 선택과 동시에 발표가 됨 -> 더 선택할 것이 없음
                    // emit의 순간
                    //  ele :: element 번호
                    //  ingre :: 현재 선택된 재료 번호
                    //  stamp :: 발표자가 사용한 stamp
                    //  user_key , user_color
                    //  cube_order : 큐브 순서 
                    //  board_order: 보드 순서
                    //  room_name :: 방 이름
                    if (data_hub.Stamp_num < 0 || data_hub.Stamp_num > 11)
                    {
                        StartCoroutine(Announce_pannel("점수 혹은 '원소 의문'을 가질 인장을 선택해야 순서를 확정할 수 있습니다.\n인장을 선택해주세요", 2));
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
            case 7:     // 학생 실험
                if (data_hub.Select_ingre[0].Length < 0 || data_hub.Select_ingre[1].Length < 0)
                {
                    StartCoroutine(Announce_pannel("재료 2가지를 모두 선택해야 합니다.\n2 가지의 재료를 골라주세요.", 2));
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
            case 8:     // 본인 실험
                if (data_hub.Select_ingre[0].Length < 0 || data_hub.Select_ingre[1].Length < 0)
                {
                    StartCoroutine(Announce_pannel("재료 2가지를 모두 선택해야 합니다.\n2 가지의 재료를 골라주세요.", 2));
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
            case 9:     // 전시회
                        // 전시할 물약 선택 -> 재료 표시 및 재료 선택 -> 재료 실험 결과 확인 -> 성공하면 성공창과 함께 큐브 색 변경, 실패하면 실패 칸으로 가고 명성 -1
                if (data_hub.Exhibit_potion_step == false)
                {
                    // 물약 선택 가능한 큐브 open
                    // 여기선 아무것도 안함
                    if (data_hub.Exhibit_select_potion.Length < 0 )
                    {
                        StartCoroutine(Announce_pannel("전시할 물약을 골라주세요!", 2));
                        return;
                    }
                }
                else if (data_hub.Exhibit_potion_step)
                {
                    // 선택된 포션의 위치에서 재료 open
                    // 재료 선택이 완료 된 것이므로 emit 실행
                    /* data
                     *    user_key : 유저번호
                     *    user_color : 발표한 유저 색
                     *    card_list : 선택한 재료가 저장된 배열 :: card_1~8
                     *    exhibit_potion : 발표할 물약 
                     *          red_1,0 / green_1,0 / blue_1,0
                     *    board_order : 보드 순서
                     *    cube_order  : 큐브 순서
                     *    room_name   : 방 이름
                     */
                    if (data_hub.Select_ingre[0].Length < 0 || data_hub.Select_ingre[1].Length < 0)
                    {
                        StartCoroutine(Announce_pannel("재료 2가지를 모두 선택해야 합니다.\n2 가지의 재료를 골라주세요.", 2));
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

    // 한 보드의 행동을 넘길 때
    public void Board_act_passing_confirm()
    {
        /*
          user_key    : 유저 번호
          room_name   : 방 이름
          board_num   : 현재 보드 번호
          board_order : 현재 보드 순서
          cube_order : 현재 큐브 순서
        */
        Alc_Data send_data = new Alc_Builder().User_key(data_hub.My_key)
                                              .Room_name(data_hub.Room_name)
                                              .Board_num(data_hub.Now_board_num)
                                              .Board_order(data_hub.Board_order)
                                              .Cube_order(data_hub.Board_cube_order)
                                              .Board_Passing_Data_Build();

        data_hub.Socket.Emit("board_passing", send_data);

        // 선택확정/ 넘기기 버튼 안보이게하기
        GameObject.Find("Switch_Area").transform.Find("Board_act_end").gameObject.SetActive(false);
        GameObject.Find("Switch_Area").transform.Find("Board_act_pass").gameObject.SetActive(false);
    }

    // 게임 종료 안내 확인 버튼 행동
    public void Game_end_confirm()
    {
        Alc_Data send_data = new Alc_Builder().Room_name(data_hub.Room_name)
                                              .Send_Room_Name_Data_Build();

        data_hub.Socket.Emit("game_end_confirm", send_data);
        SceneManager.LoadScene("End_page");
    }

    // 방 나가기 확인
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
