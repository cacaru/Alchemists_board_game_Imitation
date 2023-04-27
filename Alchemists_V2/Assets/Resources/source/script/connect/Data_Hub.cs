using Alchemists_data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 스크립트 내부에서 사용될 변수 통합 저장고
public class Data_Hub : MonoBehaviour
{
    // 모든 scene에서 사용할 수 있도록 고정하는 과정
    public static Data_Hub Instanse;

    private Board_Acting_Checker act_checker;
    

    void Awake()
    {
        if (Instanse != null)
        {
            Destroy(gameObject);
            return;
        }
        Instanse = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
    }

    // scene이 넘어갔을 때 호출되어 act_checer의 변수에 값을 넣어줌
    public void Active_checker()
    {
        act_checker = GameObject.Find("Data_Controller").GetComponent<Board_Acting_Checker>();
    }

    #region value
    // 서버 연결부
    public SocketIOUnity Socket { get; set; }

    private string my_name;             // 내 닉네임
    private string my_key;              // 내 socket.id
    private string room_name;           // 방 이름
    private string room_pw;             // 방 비밀번호
    private int my_color_code;          // 내 색 코드
    private int max_count;              // 방 내부 최대 인원수
    private bool im_master = false;     // 내가 방장인지 여부
    private bool is_in_room = true;     // true 면 방 안 / false 면 게임 보드 안 
    private bool in_round = false;      // 현재 board 진행중 여부
    private bool no_enter = false;      // 게임이 중간에 터져서 다시 입장하는 변수
    private int my_order_cube_select_turn;      // 내가 행동순서큐브를 선택하는 순서
    private int round_cont;                     // 현재 라운드
    private int now_rank;                       // 현재 팔고있는 아티팩트의 등급
    private int decide_round_order_cont;        // 큐브를 선택할 순서번호 받기 ( 순서대로 선택할 수 있도록 가리키는 번호를 받음
    private int now_board_num;          // 현재 행동 번호
    private int board_order;            // 행동 순서 번호 ( 0 :: 첫번째 순서, 1:: 두번째 순서, 2:: 세번째 순서~~유저 
    private int board_cube_order;       // 행동 중인 큐브 번호 ( 1 : 세로 첫줄, 2 : 두번쨰 줄, 3: 세번쨰줄

    private Dictionary<int, User_Data_Array> user_data_array;                       // 전체 유저 정보
    private User_Data_Array my_data;                                                // 내 정보

    private Dictionary<string, Dictionary<int, Artifacts_Info>> artifacts_info;     // 전체 유물 정보를 담아놓을 변수
    private Dictionary<int, Final_Round_Order> final_round_order;                   // 행동순서큐브를 선택하는 과정에서 쓰이는 변수 - 색을 입힐때 사용
    private Dictionary<int, Dictionary<int, User_Cube_Data>> user_cube_data;        // 각 행동순서의 큐브들의 정보를 담을 변수
    private Dictionary<int, Reasoning_Table_Data> result_table;                     // 실험 결과 테이블, 추리 테이블 정보 >> 모든 유저 정보
    private Dictionary<int, Theory_Data> theory_data;                               // 논문 정보들
    private Exhibition_Result exhibition_result;                                    // 마지막 라운드의 물약 시연 보드 정보
    private Dictionary<int, Adv_Dis_Confirm_Data> dis_coin_data;                    // 용병에게 물약판매 행동에서 물약을 판매할 순서를 받아두는 변수

    private int[] ingredient_select_arr = new int[5];                               // 이번 행동에서 선택 가능한 5개의 재료 변수 받기
    private string[] order = new string[4];                                         // 행동순서큐브를 선택하는 순서 - 종합(다른사람분까지 있음)
    private int[] selected_round_order = new int[4];                                // 현재 선택된 행동순서큐브의 번호 집합
    private int[] now_selling_arti_num = new int[3];                                // 현재 팔고있는 아티팩트의 번호

    // 행동 공용 변수
    private string[] select_ingre = new string[2];                                  // 용병에게 물약판매, 실험, 논문, 전시회에서 쓰일 재료2개 선택 변수
    private bool ingre_step_end = false;                                            // 재료 제시 여부

    // 호의카드 사용 관련 변수
    private string favor_card_name;                                                 // 사용할 호의카드 이름
    private int select_board_num;                                                // 호의카드 중 힘센 친구에서 선택할 보드 번호를 저장할 변수

    // 1번 행동 재료 선택
    private int select_ingre_num = 0;                                               // 선택한 재료 번호
    private int select_index = 0;                                                   // 선택한 위치

    // 2번 행동 재료 판매
    private int sell_item_num = 0;                                                  // 선택한 재료 번호
    private List<Dictionary<string, bool>> adventurer_card_data;                    // 용병에게 판매 가능한 물약 정보
    private int[] random_adv_list;                                                  // 이번 게임에서 사용될 용병들과 순서

    // 3번 용병 에게 물약 판매 
    private int dis_coin_num = -1;                                                  // 선택한 할인카드 번호
    private int selling_turn = 0;                                                   // 현재 판매중인 순서의 번호
    private int selling_price;                                                      // 판매가
    private string sell_potion;                                                     // 판매를 선택한 포션
    private bool my_coin_step_end = false;                                          // 내 할인 제시 끝 여부
    private bool coin_step_end = false;                                             // 할인제시 전부 끝 여부
    private bool price_step_end = false;                                            // 판매가 설정 여부
    private bool potion_step_end = false;                                           // 판매 포션 종류 고르기 여부
    private bool my_board_3_end = false;                                            // 3번 행동의 내 차례가 완전히 끝났는지 여부
    private Selling_Potion_End selling_potion_end;                                  // 판매 결과데이터

    // 4번 유물 구매
    private int arti_num = -1;                                                      // 구매할 유물 번호

    // 논문 공용 변수
    private int ele_num;                                                            // 발표할 원소 번호 1~8

    // 5번 논문반박 스텝
    // 반박할 재료 선택 -> 원소 선택 -> 재료 선택 -> 반박 결과 창
    private bool core_end_5 = false;
    private bool ori_ele_end_5 = false;
    private int[] ingre_arr = new int[2];                                                 //재료 번호 - int 버젼

    // 6번 논문 발표 스텝
    //발표할 재료 선택 -> 원소 선택 -> 인장 선택 -> 발표 완료창
    private bool core_end_6 = false;
    private bool element_end_6 = false;
    private int core_num;                                                           // 발표할 재료 번호 1~8
    private int stamp_num;                                                          // 발표할 인장 번호 1~11

    // 전시회 변수
    private bool exhibit_potion_step = false;                                       // 전시할 물약 선택 차례
    private string exhibit_select_potion;                                           // 전시할 물약

    // 게임 종료 관련 변수
    private Dictionary<int, Game_Result_Data> game_result_data;                          // 최종 결과
    #endregion

    #region Property    
    public string My_name { get { return my_name; } set { my_name = value; } }
    public string My_key { get { return my_key; } set { my_key = value; } }
    public string Room_name { get { return room_name; } set { room_name = value; } }
    public string Room_pw { get { return room_pw; } set { room_pw = value; } }
    public int My_color_code { get { return my_color_code; } set { my_color_code = value; } }
    public int Max_count { get { return max_count; } set { max_count = value; } }
    public bool Im_master { get { return im_master; } set { im_master = value; } }
    public bool Is_in_room { get { return is_in_room; } set { is_in_room = value; } }
    public bool In_round { get { return in_round; } set { in_round = value; } }
    public bool No_enter { get { return no_enter; } set { no_enter = value; } }

    public int Now_board_num { 
        get { return now_board_num; } 
        set { 
            now_board_num = value;
            act_checker.Set_board_num(now_board_num);
        } 
    }
    public int Board_order { 
        get { return board_order; } 
        set { 
            board_order = value;
            act_checker.Set_board_order(board_order);
        }
    }
    public int Board_cube_order { 
        get { return board_cube_order; } 
        set { 
            act_checker.Set_board_cube_order(value); 
            board_cube_order = value; 
        } 
    }
    public Dictionary<string, Dictionary<int, Artifacts_Info>> Artifacts_info { get { return artifacts_info; } set { artifacts_info = value; } }
    public int Decide_round_order_cont { get { return decide_round_order_cont; } set { decide_round_order_cont = value; } }
    public int[] Ingredient_select_arr { get { return ingredient_select_arr; } set { ingredient_select_arr = value; } }
    public string[] Order { 
        get { return order; } 
        set {
            order = value;
            for (int i = 0; i < max_count; i++)
            {
                if (order[i].Equals(my_key))
                {
                    my_order_cube_select_turn = i;
                    break;
                }
            }
        } 
    }
    public int My_order_cube_select_turn { get { return my_order_cube_select_turn; } set { my_order_cube_select_turn = value; } }
    public Dictionary<int, Final_Round_Order> Final_round_order { 
        get { return final_round_order; } 
        set { 
            final_round_order = value;
            for(int i =0;i<final_round_order.Count; i++)
            {
                // 지정된 행동 순서 번호 저장
                selected_round_order[i] = final_round_order[i].order;

                if (final_round_order[i].user_key.Equals(my_key))
                {
                    act_checker.Set_my_board_order(i);
                }
            }
            
        } 
    }
    public Dictionary<int, Dictionary<int, User_Cube_Data>> User_cube_data { get { return user_cube_data; } set { user_cube_data = value; } }
    public int[] Selected_round_order { get { return selected_round_order; } set { selected_round_order = value; } }
    public int Round_cont { get { return round_cont; } set { round_cont = value; } }
    public int Now_rank { get { return now_rank; } set { now_rank = value; } }
    public int[] Now_selling_arti_num { get { return now_selling_arti_num; } set { now_selling_arti_num = value; } }
    public Dictionary<int, Reasoning_Table_Data> Result_table { get { return result_table; } set { result_table = value; } }
    public Dictionary<int, Theory_Data> Theory_data { get { return theory_data; } set { theory_data = value; } }
    public Exhibition_Result Exhibition_result { get { return exhibition_result; } set { exhibition_result = value; } }

    public Dictionary<int, User_Data_Array> User_data_array
    {  // 전체 유저 정보
        get
        {
            return user_data_array;
        }
        set
        {
            user_data_array = value;

            for (int i = 0; i < user_data_array.Count; i++)
            {
                if (user_data_array[i].User_key.Equals(my_key))
                {
                    my_data = user_data_array[i];
                    break;
                }
            }
            

        }
    }
    public User_Data_Array My_data { get { return my_data; } }

    public int Select_ingre_num { get { return select_ingre_num; } set { select_ingre_num = value; } }
    public int Select_index { get { return select_index; } set { select_index = value; } }
    public int Sell_item_num { get { return sell_item_num; } set { sell_item_num = value; } }
    public List<Dictionary<string, bool>> Adventurer_card_data { get { return adventurer_card_data; } set { adventurer_card_data = value; } }
    public int[] Random_adv_list { get { return random_adv_list; } set { random_adv_list = value; } }

    public int Dis_coin_num { get { return dis_coin_num; } set { dis_coin_num = value; } }
    public int Selling_price { get { return selling_price; } set { selling_price = value; } }
    public string Sell_potion { get { return sell_potion; } set { sell_potion = value; } }
    public string[] Select_ingre { get { return select_ingre; } set { select_ingre = value; } }

    public string Favor_card_name { get { return favor_card_name; } set { favor_card_name = value; } }
    public int Select_board_num { get { return select_board_num; } set { select_board_num = value; } }

    public bool My_coin_step_end { get { return my_coin_step_end; } set { my_coin_step_end = value; } }
    public bool Coin_step_end { get { return coin_step_end; } set { coin_step_end = value; } }
    public bool Price_step_end { get { return price_step_end; } set { price_step_end = value; } }
    public bool Potion_step_end { get { return potion_step_end; } set { potion_step_end = value; } }
    public bool Ingre_step_end { get { return ingre_step_end; } set { ingre_step_end = value; } }
    public bool My_board_3_end { get { return my_board_3_end; } set { my_board_3_end = value; } }
    public int Selling_turn { 
        get { return selling_turn; } 
        set { 
            selling_turn = value;
            if (now_board_num > 0)
                act_checker.Set_board_num(3);
        } 
    }
    public Selling_Potion_End Selling_potion_end { get { return selling_potion_end; } set { selling_potion_end = value; } }
    public Dictionary<int, Adv_Dis_Confirm_Data> Dis_coin_data { get { return dis_coin_data; } set { dis_coin_data = value; } }

    public int Arti_num { get { return arti_num; } set { arti_num = value; } }

    public bool Core_end_5 { get { return core_end_5; } set { core_end_5 = value; } }
    public bool Ori_ele_end_5 { get { return ori_ele_end_5; } set { ori_ele_end_5 = value; } }

    public bool Core_end_6 { get { return core_end_6; } set { core_end_6 = value; } }
    public bool Element_end_6 { get { return element_end_6; } set { element_end_6 = value; } }

    public int Core_num { get { return core_num; } set { core_num = value; } }
    public int Ele_num { get { return ele_num; } set { ele_num = value; } }
    public int Stamp_num { get { return stamp_num; } set { stamp_num = value; } }
    public int[] Ingre_arr { get { return ingre_arr; } set { ingre_arr = value; } }

    public bool Exhibit_potion_step { get { return exhibit_potion_step; } set { exhibit_potion_step = value; } }
    public string Exhibit_select_potion { get { return exhibit_select_potion; } set { exhibit_select_potion = value; } }

    public Dictionary<int, Game_Result_Data> Game_result_data { get { return game_result_data; } set { game_result_data = value; } }
    #endregion


}
