using Alchemists_data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��ũ��Ʈ ���ο��� ���� ���� ���� �����
public class Data_Hub : MonoBehaviour
{
    // ��� scene���� ����� �� �ֵ��� �����ϴ� ����
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

    // scene�� �Ѿ�� �� ȣ��Ǿ� act_checer�� ������ ���� �־���
    public void Active_checker()
    {
        act_checker = GameObject.Find("Data_Controller").GetComponent<Board_Acting_Checker>();
    }

    #region value
    // ���� �����
    public SocketIOUnity Socket { get; set; }

    private string my_name;             // �� �г���
    private string my_key;              // �� socket.id
    private string room_name;           // �� �̸�
    private string room_pw;             // �� ��й�ȣ
    private int my_color_code;          // �� �� �ڵ�
    private int max_count;              // �� ���� �ִ� �ο���
    private bool im_master = false;     // ���� �������� ����
    private bool is_in_room = true;     // true �� �� �� / false �� ���� ���� �� 
    private bool in_round = false;      // ���� board ������ ����
    private bool no_enter = false;      // ������ �߰��� ������ �ٽ� �����ϴ� ����
    private int my_order_cube_select_turn;      // ���� �ൿ����ť�긦 �����ϴ� ����
    private int round_cont;                     // ���� ����
    private int now_rank;                       // ���� �Ȱ��ִ� ��Ƽ��Ʈ�� ���
    private int decide_round_order_cont;        // ť�긦 ������ ������ȣ �ޱ� ( ������� ������ �� �ֵ��� ����Ű�� ��ȣ�� ����
    private int now_board_num;          // ���� �ൿ ��ȣ
    private int board_order;            // �ൿ ���� ��ȣ ( 0 :: ù��° ����, 1:: �ι�° ����, 2:: ����° ����~~���� 
    private int board_cube_order;       // �ൿ ���� ť�� ��ȣ ( 1 : ���� ù��, 2 : �ι��� ��, 3: ��������

    private Dictionary<int, User_Data_Array> user_data_array;                       // ��ü ���� ����
    private User_Data_Array my_data;                                                // �� ����

    private Dictionary<string, Dictionary<int, Artifacts_Info>> artifacts_info;     // ��ü ���� ������ ��Ƴ��� ����
    private Dictionary<int, Final_Round_Order> final_round_order;                   // �ൿ����ť�긦 �����ϴ� �������� ���̴� ���� - ���� ������ ���
    private Dictionary<int, Dictionary<int, User_Cube_Data>> user_cube_data;        // �� �ൿ������ ť����� ������ ���� ����
    private Dictionary<int, Reasoning_Table_Data> result_table;                     // ���� ��� ���̺�, �߸� ���̺� ���� >> ��� ���� ����
    private Dictionary<int, Theory_Data> theory_data;                               // �� ������
    private Exhibition_Result exhibition_result;                                    // ������ ������ ���� �ÿ� ���� ����
    private Dictionary<int, Adv_Dis_Confirm_Data> dis_coin_data;                    // �뺴���� �����Ǹ� �ൿ���� ������ �Ǹ��� ������ �޾Ƶδ� ����

    private int[] ingredient_select_arr = new int[5];                               // �̹� �ൿ���� ���� ������ 5���� ��� ���� �ޱ�
    private string[] order = new string[4];                                         // �ൿ����ť�긦 �����ϴ� ���� - ����(�ٸ�����б��� ����)
    private int[] selected_round_order = new int[4];                                // ���� ���õ� �ൿ����ť���� ��ȣ ����
    private int[] now_selling_arti_num = new int[3];                                // ���� �Ȱ��ִ� ��Ƽ��Ʈ�� ��ȣ

    // �ൿ ���� ����
    private string[] select_ingre = new string[2];                                  // �뺴���� �����Ǹ�, ����, ��, ����ȸ���� ���� ���2�� ���� ����
    private bool ingre_step_end = false;                                            // ��� ���� ����

    // ȣ��ī�� ��� ���� ����
    private string favor_card_name;                                                 // ����� ȣ��ī�� �̸�
    private int select_board_num;                                                // ȣ��ī�� �� ���� ģ������ ������ ���� ��ȣ�� ������ ����

    // 1�� �ൿ ��� ����
    private int select_ingre_num = 0;                                               // ������ ��� ��ȣ
    private int select_index = 0;                                                   // ������ ��ġ

    // 2�� �ൿ ��� �Ǹ�
    private int sell_item_num = 0;                                                  // ������ ��� ��ȣ
    private List<Dictionary<string, bool>> adventurer_card_data;                    // �뺴���� �Ǹ� ������ ���� ����
    private int[] random_adv_list;                                                  // �̹� ���ӿ��� ���� �뺴��� ����

    // 3�� �뺴 ���� ���� �Ǹ� 
    private int dis_coin_num = -1;                                                  // ������ ����ī�� ��ȣ
    private int selling_turn = 0;                                                   // ���� �Ǹ����� ������ ��ȣ
    private int selling_price;                                                      // �ǸŰ�
    private string sell_potion;                                                     // �ǸŸ� ������ ����
    private bool my_coin_step_end = false;                                          // �� ���� ���� �� ����
    private bool coin_step_end = false;                                             // �������� ���� �� ����
    private bool price_step_end = false;                                            // �ǸŰ� ���� ����
    private bool potion_step_end = false;                                           // �Ǹ� ���� ���� ���� ����
    private bool my_board_3_end = false;                                            // 3�� �ൿ�� �� ���ʰ� ������ �������� ����
    private Selling_Potion_End selling_potion_end;                                  // �Ǹ� ���������

    // 4�� ���� ����
    private int arti_num = -1;                                                      // ������ ���� ��ȣ

    // �� ���� ����
    private int ele_num;                                                            // ��ǥ�� ���� ��ȣ 1~8

    // 5�� ���ݹ� ����
    // �ݹ��� ��� ���� -> ���� ���� -> ��� ���� -> �ݹ� ��� â
    private bool core_end_5 = false;
    private bool ori_ele_end_5 = false;
    private int[] ingre_arr = new int[2];                                                 //��� ��ȣ - int ����

    // 6�� �� ��ǥ ����
    //��ǥ�� ��� ���� -> ���� ���� -> ���� ���� -> ��ǥ �Ϸ�â
    private bool core_end_6 = false;
    private bool element_end_6 = false;
    private int core_num;                                                           // ��ǥ�� ��� ��ȣ 1~8
    private int stamp_num;                                                          // ��ǥ�� ���� ��ȣ 1~11

    // ����ȸ ����
    private bool exhibit_potion_step = false;                                       // ������ ���� ���� ����
    private string exhibit_select_potion;                                           // ������ ����

    // ���� ���� ���� ����
    private Dictionary<int, Game_Result_Data> game_result_data;                          // ���� ���
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
                // ������ �ൿ ���� ��ȣ ����
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
    {  // ��ü ���� ����
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
