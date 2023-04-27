using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alchemists_data
{
    public interface Alc_Data
    {
        public string print();
    }

    #region send data class
    // getter 필수 :: serialize 하는 과정에서 변수에 접근하지 못하면 serialize 불가 => 빈 데이터가 보내짐
    public class Room_List : Alc_Data
    {
        private string room_name;
        private int count;
        private int max_count;

        public Room_List(string room_name, string count, string max_count)
        {
            this.room_name = room_name;
            this.count = int.Parse(count);
            this.max_count = int.Parse(max_count);
        }

        public string Room_name
        {
            get { return room_name; }
        }


        public int Count
        {
            get { return count; }
        }
        public int Max_count
        {
            get { return max_count; }
            set { max_count = value; }
        }

        public string print()
        {
            return "Room_List >> name : " + this.room_name + " / count : " + this.count + " / max_count : " + this.max_count;
        }
    }

    public class Simple_Room_Data : Alc_Data
    {
        public string room_name { get; set; }
        public string room_pw { get; set; }

        public Simple_Room_Data(string name, string pw)
        {
            this.room_name = name;
            this.room_pw = pw;
        }

        public string print()
        {
            return "Simple_Room_Data : " + room_name + " / " + room_pw;
        }

    }

    public class Enter_Room_Data : Alc_Data
    {
        // user_name, room_name, is_master=false,is_ready=false,msg=user_name+"가 방에 참가하였습니다 - by unity"
        public string user_name { get; set; }
        public string room_name { get; set; }
        public string is_master { get; set; }
        public string is_ready { get; set; }
        public bool no_enter { get; set; }
        public string msg { get; set; }

        public Enter_Room_Data(string user_name, string room_name, string is_master, string is_ready, bool no_enter, string msg)
        {
            this.user_name = user_name;
            this.room_name = room_name;
            this.is_master = is_master;
            this.is_ready = is_ready;
            this.no_enter = no_enter;
            this.msg = msg;
        }

        public string print()
        {
            return "Enter_Room_Data >> user_name : " + user_name + " / room_name : " + room_name + " / is_master : " + is_master
                    + " / is_ready : " + is_ready + " / msg : " + msg;
        }
    }

    public class Create_Room : Alc_Data
    {
        public string user_name { get; set; }
        public string room_name { get; set; }
        public string room_pw { get; set; }
        public int count { get; set; }
        public string is_master { get; set; }
        public string is_ready { get; set; }
        public string msg { get; set; }

        public Create_Room(string user_name, string room_name, string room_pw, int count, string is_master, string is_ready, string msg)
        {
            this.user_name = user_name;
            this.room_name = room_name;
            this.room_pw = room_pw;
            this.count = count;
            this.is_master = is_master;
            this.is_ready = is_ready;
            this.msg = msg;
        }
        public string print()
        {
            return "Create_Room >> user_name : " + this.user_name + " / room_name : " +
                    this.room_name + " / room_pw : " +
                    this.room_pw + " / count : " +
                    this.count + " / is_master : " +
                    this.is_master + " / is_ready : " +
                    this.is_ready + " / msg : " +
                    this.msg;

        }
    }

    public class Edit_Color_Data : Alc_Data
    {
        // data :: user_name, user_color, user_key, room_name
        public string user_name { get; }
        public string user_color { get; }
        public string user_key { get; }
        public string room_name { get; }

        public Edit_Color_Data(string user_name, string user_color, string user_key, string room_name)
        {
            this.user_name = user_name;
            this.user_color = user_color;
            this.user_key = user_key;
            this.room_name = room_name;
        }

        public string print()
        {
            return "Edit_Color_Data >> user_name : " + user_name +
                    " / user_color : " + user_color +
                    " / user_key : " + user_key +
                    " / room_name : " + room_name;
        }

    }

    public class Chat_Data : Alc_Data
    {
        // data :: speaker, msg, type(normal, server) , room_name
        public string speaker { get; }
        public string msg { get; }
        public string type { get; }
        public string room_name { get; }

        public Chat_Data(string speaker, string msg, string type, string room_name)
        {
            this.speaker = speaker;
            this.msg = msg;
            this.type = type;
            this.room_name = room_name;
        }

        public string print()
        {
            return "Chat_Data >> speaker : " + speaker
                               + " / msg : " + msg
                              + " / type : " + type
                         + " / room_name : " + room_name;
        }
    }

    public class Common_Data : Alc_Data
    {
        /*
         * 방이름 + 유저키만 보내는 클래스
         */
        public string room_name { get; }
        public string user_key { get; }
        public Common_Data(string room_name, string user_key)
        {
            this.room_name = room_name;
            this.user_key = user_key;
        }

        public string print()
        {
            return "Common_Data >> room_name : " + room_name
                                 + " / user_key : " + user_key;
        }
    }

    public class Ready_Game_Data : Alc_Data 
    {
        public string room_name { get; }
        public string user_key { get; }
        public string is_ready { get; }

        public Ready_Game_Data(string room_name, string user_key, string is_ready)
        {
            this.room_name = room_name;
            this.user_key = user_key;
            this.is_ready = is_ready;
        }
        public string print()
        {
            return "Ready_Game_Data >> room_name : " + room_name
                                  + " / user_key : " + user_key
                                  + " / is_ready : " + is_ready;
        }
    }

    public class Select_Round_Order_Data : Alc_Data
    {
        // data[ 
        //    user_key, 
        //    select_order  
        //    room_name

        public string user_key { get; }
        public string room_name { get; }
        public int order { get; }

        public Select_Round_Order_Data(string user_key, int select_order, string room_name)
        {
            this.room_name = room_name;
            this.user_key = user_key;
            this.order = select_order;
        }

        public string print()
        {
            return "Select_Room_Order_Data >> room_name : " + this.room_name +
                                           " / user_key : " + this.user_key +
                                              " / order : " + this.order;
        }
    }
    
    public class Send_Room_Name_Data : Alc_Data
    {
        public string room_name { get; }
        public Send_Room_Name_Data(string room_name)
        {
            this.room_name = room_name;
        }

        public string print()
        {
            return "Send_Room_Name_Data >> room_name : " + this.room_name;
        }
    }

    public class Select_Cube_Data : Alc_Data
    {
        public string user_key { get; }
        public string room_name { get; }
        public int board_num { get; }
        public int button_order_num { get; }

        public Select_Cube_Data(string user_key, string room_name, int board_num, int button_order_num)
        {
            this.user_key = user_key;
            this.room_name = room_name;
            this.board_num = board_num;
            this.button_order_num = button_order_num;
        }

        public string print()
        {
            return "Select_Cube_Data >> user_key : " + this.user_key +
                                  " /  room_name : " + this.room_name +
                                   " / board_num : " + this.board_num +
                            " / button_order_num : " + this.button_order_num;
        }
    }

    public class Reasoning_Table_Change_Data : Alc_Data
    {
        public string user_key { get; }
        public int x { get; } // index
        public int y { get; } // key
        public int change_val { get; }  // picked :: 0 1 2
        public string room_name { get; }

        public Reasoning_Table_Change_Data(string user_key, string room_name, int x, int y, int change_val)
        {
            this.user_key = user_key;
            this.room_name = room_name;
            this.x = x;
            this.y = y;
            this.change_val = change_val;
        }

        public string print()
        {
            return "Reasoning_Table_Change_Data >> user_key : " + this.user_key +
                                              " / room_name : " + this.room_name +
                                                      " / x : " + this.x +
                                                      " / y : " + this.y +
                                             " / change_val : " + this.change_val;
        }
    }

    public class Pick_Ingre_Data : Alc_Data
    {
        /*
         * 보드1번 재료 선택 행동에서
         * 선택한 재료와 필요정보를 보냄
         */
        public string user_key { get; }
        public int pick_item { get; }
        public int cube_order { get; }
        public int board_order { get; }
        public int ingredient_select_arr_order { get; }
        public string room_name { get; }

        public Pick_Ingre_Data(string user_key, string room_name, int pick_item, int cube_order, int board_order, int ingre_arr_order)
        {
            this.user_key = user_key;
            this.room_name = room_name;
            this.pick_item = pick_item;
            this.cube_order = cube_order;
            this.board_order = board_order;
            this.ingredient_select_arr_order = ingre_arr_order;
        }

        public string print()
        {
            return "Pick_Ingre_Data >> user_key : " + this.user_key +
                                  " / room_name : " + this.room_name +
                                  " / pick_item : " + this.pick_item +
                                 " / cube_order : " + this.cube_order +
                                " / board_order : " + this.board_order +
                " / ingredient_select_arr_order : " + this.ingredient_select_arr_order;
        }

    }

    public class Sell_Item_Confirm_Data : Alc_Data
    {
        /*
         * 보드 2번 재료판매 행동에서 판매하는 재료번호를 보내는 클래스
        */
        public string user_key { get; }
        public int sell_item_num { get; }
        public int board_order { get; }
        public int cube_order { get; }
        public string room_name { get; }

        public Sell_Item_Confirm_Data(string user_key, string room_name, int sell_item_num, int board_order, int cube_order)
        {
            this.user_key = user_key;
            this.room_name = room_name;
            this.sell_item_num = sell_item_num;
            this.board_order = board_order;
            this.cube_order = cube_order;
        }
        
        public string print()
        {
            return "Sell_Item_Confirm_Data >> user_key : " + this.user_key +
                                         " / room_name : " + this.room_name +
                                     " / sell_item_num : " + this.sell_item_num +
                                       " / board_order : " + this.board_order +
                                        " / cube_order : " + this.cube_order;
        }
    }

    public class Cancel_Selling_Ingre_Data : Alc_Data
    {
        /*
         * 재료 판매를 취소할 떄 사용할 클래스
         */
        public string user_key { get; }
        public string room_name { get; }
        public int board_num { get; }
        public int cube_order { get; }

        public Cancel_Selling_Ingre_Data(string user_key, string room_name, int board_num, int cube_order)
        {
            this.user_key = user_key;
            this.room_name = room_name;
            this.board_num = board_num;
            this.cube_order = cube_order;
        }

        public string print()
        {
            return "Cancel_Selling_Ingre_Data >> user_key : " + this.user_key +
                                            " / room_name : " + this.room_name +
                                            " / board_num : " + this.board_num +
                                           " / cube_order : " + this.cube_order;
        }
    }

    public class Adv_Dis_Confirm_Data : Alc_Data
    {
        /* 용병에게 판매할 물약 값을 고르는 클래스
         */
        public string user_key { get; }
        public string color { get; }
        public int dis_coin_num { get; }
        public string room_name { get; }

        public Adv_Dis_Confirm_Data(string user_key, string room_name, string color, int num)
        {
            this.user_key = user_key;
            this.room_name = room_name;
            this.color = color;
            this.dis_coin_num = num;
        }

        public string print()
        {
            return "Adv_Dis_Confirm_Data >> user_key : " + this.user_key +
                                       " / room_name : " + this.room_name +
                                           " / color : " + this.color +
                                    " / dis_coin_num : " + this.dis_coin_num;
        }
    }

    public class Sell_Price_Confirm_Data : Alc_Data
    {
        /* 용병에게 물약을 판매할 판매가를 설정하는 클래스
         */
        public string user_key { get; }
        public string room_name { get; }
        public int sell_price { get; }

        public Sell_Price_Confirm_Data(string user_key, string room_name, int sell_price)
        {
            this.user_key = user_key;
            this.room_name = room_name;
            this.sell_price = sell_price;
        }

        public string print()
        {
            return "Sell_Price_Confirm_Data >> user_key : " + this.user_key + 
                                          " / room_name : " + this.room_name + 
                                         " / sell_price : " + this.sell_price;
        }
    }

    public class Sell_to_Adv_Potion_Data : Alc_Data
    {
        /* 용병에게 물약을 판매 할때 사용할 재료 와 종류 넘기기
         */
        public string user_key { get; }
        public string room_name { get; }
        public string user_color { get; }
        public string[] card_list { get; }
        public string what_kind_sell_potion { get; }
        public int selling_turn { get; }
        
        public Sell_to_Adv_Potion_Data(string user_key, string room_name, string user_color, string[] card_list, string what, int selling_turn)
        {
            this.user_color = user_color;
            this.user_key = user_key;
            this.room_name = room_name;
            this.card_list = card_list;
            this.what_kind_sell_potion = what;
            this.selling_turn = selling_turn;
        }

        public string print()
        {
            return "Sell_to_Adv_Potion_Data >> user_key : " + this.user_key +
                                          " / room_name : " + this.room_name +
                                         " / user_color : " + this.user_color +
                              " / what_kind_sell_potion : " + this.what_kind_sell_potion +
                                       " / selling_turn : " + this.selling_turn;
        }
    }

    public class Buy_Artifact_Confirm_Data : Alc_Data
    {
        /* 유물 구매 정보 넘기기
         */
        public string user_key { get; }
        public string room_name { get; }
        public int board_order { get; }
        public int cube_order { get;}
        public int arti_num { get; }
        public string rank { get; }

        public Buy_Artifact_Confirm_Data(string user_key, string room_name, int board_order, int cube_order, int arti_num, string rank)
        {
            this.user_key = user_key;
            this.room_name = room_name;
            this.board_order = board_order;
            this.cube_order = cube_order;
            this.arti_num = arti_num;
            this.rank = rank;
        }

        public string print()
        {
            return "Buy_Artifact_Confirm_Data >> user_key : " + this.user_key +
                                            " / room_name : " + this.room_name +
                                          " / board_order : " + this.board_order +
                                           " / cube_order : " + this.cube_order +
                                             " / arti_num : " + this.arti_num +
                                                 " / rank : " + this.rank;
        }
    }

    public class Test_Ingredient_Confirm_Data : Alc_Data
    {
        /* 실험하기 정보 넘기기 */
        public string user_key { get; }
        public string room_name { get; }
        public string[] card_list { get; }
        public bool caretaker_used { get; }
        public int board_order { get; }
        public int cube_order { get; }
        public int board_is { get; }    // 학생에게 실험 : 7, 자신에게실험 : 8

        public Test_Ingredient_Confirm_Data(string user_key, string room_name, string[] card_list, bool used, int board_order, int cube_order, int board_is)
        {
            this.user_key = user_key;
            this.room_name = room_name;
            this.card_list = card_list;
            this.caretaker_used = used;
            this.board_is = board_is;
            this.board_order = board_order;
            this.cube_order = cube_order;
        }

        public string print()
        {
            return "Test_Ingredient_Confirm_Data >> user_key : " + this.user_key +
                                               " / room_name : " + this.room_name +
                                               " / card_list : " + this.card_list +
                                          " / caretaker_used : " + this.caretaker_used +
                                             " / board_order : " + this.board_order +
                                              " / cube_order : " + this.cube_order +
                                                " / board_is : " + this.board_is;

        }
    }

    public class Presentation_Theory_Data : Alc_Data
    { 
        /* 논문 발표 자료 보내기 */
        public string user_key { get; }
        public string room_name { get; }
        public string user_color { get; }
        public int ele { get; } // element 번호 ( 원소에 할당된 번호 )
        public int ingre { get; }   // element가 등록될 재료 번호
        public int stamp { get; }   // 사용할 도장 번호
        public int board_order { get; }
        public int cube_order { get; }

        public Presentation_Theory_Data(string user_key, string room_name, string user_color, int ele, int ingre, int stamp, int board_order, int cube_order)
        {
            this.user_key = user_key;
            this.room_name = room_name;
            this.user_color = user_color;
            this.ele = ele;
            this.ingre = ingre;
            this.stamp = stamp;
            this.board_order = board_order;
            this.cube_order = cube_order;
        }

        public string print()
        {
            return "Presentation_Theory_Data >> user_key : " + this.user_key +
                                           " / room_name : " + this.room_name +
                                          " / user_color : " + this.user_color +
                                                 " / ele : " + this.ele +
                                               " / ingre : " + this.ingre +
                                               " / stamp : " + this.stamp +
                                         " / board_order : " + this.board_order +
                                          " / cube_order : " + this.cube_order;
        }
    }

    public class Refuting_Theory_Data : Alc_Data
    {
        /*논문 반박하기 정보 보내기*/
        public string user_key { get; }
        public string room_name { get; }
        public int ingre { get; }
        public int ori { get; }
        public int[] arr { get; }
        public int board_order { get; }
        public int cube_order { get; }

        public Refuting_Theory_Data(string user_key, string room_name, int ingre, int ori, int[] arr, int board_order, int cube_order)
        {
            this.user_key = user_key;
            this.room_name = room_name;
            this.ingre = ingre;
            this.ori = ori;
            this.arr = arr;
            this.board_order = board_order;
            this.cube_order = cube_order;
        }
        public string print()
        {
            return "Refuting_Theroy_Data >> user_key : " + this.user_key +
                                       " / room_name : " + this.room_name +
                                           " / ingre : " + this.ingre +
                                             " / ori : " + this.ori +
                                             " / arr : " + this.arr +
                                     " / board_order : " + this.board_order +
                                      " / cube_order : " + this.cube_order;
        }
    }

    public class Check_Refute_Info_Data : Alc_Data
    {
        /*반박 성공 모달 확인창을 받는 함수*/
        public string room_name { get; }
        public int ingre_num { get; }
        //    stamp :
        //         user_key : 유저 번호
        //         color    : 스탬프 색
        //         point    : 인장 종류 
        // 위 하나의 객체도 전송하긴 하나,
        // 편의를 위해 서버에서 보낸 값을 그대로 다시 보내는 방식을 웹에서 사용하여 나온 불필요 교환이므로 제거
        public Check_Refute_Info_Data(string room_name, int ingre_num)
        {
            this.room_name = room_name;
            this.ingre_num = ingre_num;
        }

        public string print()
        {
            return "Check_Refute_Info_Data >> room_name : " + this.room_name +
                                          " / ingre_num : " + this.ingre_num;
        }
    }

    public class Exhibit_Ingre_Data : Alc_Data
    {
        /*물약 발표회에서 전해질 변수*/
        public string user_key { get; }
        public string room_name { get; }
        public string user_color { get; }
        public string[] card_list { get; }
        public string exhibit_potion { get; }
        public int board_order { get; }
        public int cube_order { get; }

        public Exhibit_Ingre_Data(string user_key, string room_name, string user_color, string[] card_list, string exhibit_potion, int board_order, int cube_order)
        {
            this.user_key = user_key;
            this.room_name = room_name;
            this.user_color = user_color;
            this.card_list = card_list;
            this.exhibit_potion = exhibit_potion;
            this.board_order = board_order;
            this.cube_order = cube_order;
        }
        public string print()
        {
            return "Exhibit_Ingre_Data >> user_key : " + this.user_key +
                                     " / room_name : " + this.room_name +
                                    " / user_color : " + this.user_color +
                                     " / card_list : " + this.card_list +
                                " / exhibit_potion : " + this.exhibit_potion +
                                   " / board_order : " + this.board_order +
                                    " / cube_order : " + this.cube_order;
        }
    }

    public class Favor_Card_Use_Confirm_Data : Alc_Data
    {
        public string user_key { get; }
        public string room_name { get; }
        public string favor_card { get; }
        public int select_board_num { get; }
        public string[] ingre_list { get; }

        public Favor_Card_Use_Confirm_Data(string user_key, string room_name, string favor_card, int select_board_num, string[] ingre_list)
        {
            this.user_key = user_key;
            this.room_name = room_name;
            this.favor_card = favor_card;
            this.select_board_num = select_board_num;
            this.ingre_list = ingre_list;
        }

        public string print()
        {
            return "Favor_Card_Use_Confirm_Data >> user_key : " + this.user_key +
                                              " / room_name : " + this.room_name +
                                             " / favor_card : " + this.favor_card +
                                       " / select_board_num : " + this.select_board_num +
                                             " / ingre_list : " + this.ingre_list;
        }
    }

    public class Board_Passing_Data : Alc_Data
    {
        /*
          user_key    : 유저 번호
          room_name   : 방 이름
          board_num   : 현재 보드 번호
          board_order : 현재 보드 순서
          cube_order : 현재 큐브 순서
        */
        public string user_key { get; set; }
        public string room_name { get; set; }
        public int board_num { get; set; }
        public int board_order { get; set; }
        public int cube_order { get; set; }

        public Board_Passing_Data(string user_key, string room_name, int board_num, int board_order, int cube_order)
        {
            this.user_key = user_key;
            this.room_name = room_name;
            this.board_num = board_num;
            this.board_order = board_order;
            this.cube_order = cube_order;
        }

        public string print()
        {
            return "Board_Passing_Data >> user_key : " + this.user_key +
                                     " / room_name : " + this.room_name +
                                     " / board_num : " + this.board_num +
                                   " / board_order : " + this.board_order +
                                    " / cube_order : " + this.cube_order;
        }
    }

    public class Refute_Check_Info : Alc_Data
    {
        /*
         * user_key : data.user_key    string
           room_name : data.room_name  string
           ingre_num : data.ingre,     int
           ori: data.ori,              int
           success: true,              bool
           stamp: stamp_data,          string,string,string
        */
        public string user_key { get; set; }
        public string room_name { get; set; }
        public int ingre_num { get; set; }
        public int ori { get; set; }
        public bool success { get; set; }
        public Stamp_Info[] stamp { get; set; }

        public Refute_Check_Info(string user_key, string room_name, int ingre_num, int ori, bool success, Stamp_Info[] stamp)
        {
            this.user_key = user_key;
            this.room_name = room_name;
            this.ingre_num = ingre_num;
            this.ori = ori;
            this.success = success;
            this.stamp = stamp;
        }

        public string print()
        {
            return "Refute_Check_Info >> user_key : " + this.user_key +
                                      " / room_name : " + this.room_name +
                                      " / ingre_num : " + this.ingre_num +
                                      " / ori : " + this.ori +
                                      " / success : " + this.success + 
                                      " / stamp : " + this.stamp;
        }
    }


    #endregion

    public enum User_Color_Code
    {
        non,
        Red,
        Blue,
        Black,
        White
    }

    // 오브젝트 컬러 집합
    public class OUTLINE_COLOR
    {
        // highlighted
        public static Color HIGHLIGHT = new Color(194/255f, 255/255f, 105/255f, 1f);
        // selected == default
        public static Color DEFAULT = new Color(160 / 255f, 160 / 255f, 160 / 255f, 1f);
    }
}
