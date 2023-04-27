using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alchemists_data
{
    public class Alc_Builder
    {
        #region values
        private string room_name;
        private string room_pw;
        private string user_name;
        private string is_master;
        private string is_ready;
        private bool no_enter;
        private int count;
        private int max_count;
        private string msg;
        private string user_key;
        private string user_color;
        private string speaker;
        private string type;
        private int select_order;
        private int board_num;
        private int button_order_num;
        private int pick_item;
        private int cube_order;
        private int board_order;
        private int ingredient_select_arr_order;
        private int x;
        private int y;
        private int change_val;
        private int sell_item_num;
        private string color;
        private int dis_coin_num;
        private int sell_price;
        private string[] card_list;
        private string what_kind_sell_potion;
        private int selling_turn;
        private string rank;
        private int arti_num;
        private bool caretaker_used;
        private int board_is;
        private int ele;
        private int ingre;
        private int stamp;
        private int ori;
        private int[] arr;
        private int ingre_num;
        private string exhibit_potion;
        private string favor_card;
        private int select_board_num;
        private string[] ingre_list;
        private bool success;
        private Stamp_Info[] stamp_data;
        #endregion


        #region setter 
        public Alc_Builder Room_name(string room_name)
        {
            this.room_name = room_name;
            return this;
        }
        public Alc_Builder Room_pw(string room_pw)
        {
            this.room_pw = room_pw;
            return this;
        }
        public Alc_Builder User_name(string user_name)
        {
            this.user_name = user_name;
            return this;
        }
        public Alc_Builder Is_master(string is_master)
        {
            this.is_master = is_master;
            return this;
        }
        public Alc_Builder Is_ready(string is_ready)
        {
            this.is_ready = is_ready;
            return this;
        }
        public Alc_Builder No_enter(bool no_enter)
        {
            this.no_enter = no_enter;
            return this;
        }
        public Alc_Builder Count(int count)
        {
            this.count = count;
            return this;
        }
        public Alc_Builder Max_count(int max_count)
        {
            this.max_count = max_count;
            return this;
        }
        public Alc_Builder Msg(string msg)
        {
            this.msg = msg;
            return this;
        }
        public Alc_Builder User_key(string user_key)
        {
            this.user_key = user_key;
            return this;
        }
        public Alc_Builder User_color(string user_color)
        {
            this.user_color = user_color;
            return this;
        }
        public Alc_Builder Speaker(string speaker)
        {
            this.speaker = speaker;
            return this;
        }
        public Alc_Builder Type(string type)
        {
            this.type = type;
            return this;
        }
        public Alc_Builder Select_order(int select_order)
        {
            this.select_order = select_order;
            return this;
        }
        public Alc_Builder Board_num(int board_num)
        {
            this.board_num = board_num;
            return this;
        }
        public Alc_Builder Button_order_num(int button_order_num)
        {
            this.button_order_num = button_order_num;
            return this;
        }
        public Alc_Builder Pick_item(int pick_item)
        {
            this.pick_item = pick_item;
            return this;
        }
        public Alc_Builder Cube_order(int cube_order)
        {
            this.cube_order = cube_order;
            return this;
        }
        public Alc_Builder Board_order(int board_order)
        {
            this.board_order = board_order;
            return this;
        }
        public Alc_Builder Ingredient_select_arr_order(int ingredient_select_arr_order)
        {
            this.ingredient_select_arr_order = ingredient_select_arr_order;
            return this;
        }
        public Alc_Builder X(int x)
        {
            this.x = x;
            return this;
        }
        public Alc_Builder Y(int y)
        {
            this.y = y;
            return this;
        }
        public Alc_Builder Change_val(int change_val)
        {
            this.change_val = change_val;
            return this;
        }
        public Alc_Builder Sell_item_num(int sell_item_num)
        {
            this.sell_item_num = sell_item_num;
            return this;
        }
        public Alc_Builder Color(string color)
        {
            this.color = color;
            return this;
        }
        public Alc_Builder Dis_coin_num(int dis_coin_num)
        {
            this.dis_coin_num = dis_coin_num;
            return this;
        }
        public Alc_Builder Sell_price(int sell_price)
        {
            this.sell_price = sell_price;
            return this;
        }
        public Alc_Builder Card_list(string[] card_list)
        {
            this.card_list = card_list;
            return this;
        }
        public Alc_Builder What_kind_sell_potion(string what)
        {
            this.what_kind_sell_potion = what;
            return this;
        }
        public Alc_Builder Selling_turn(int selling_turn)
        {
            this.selling_turn = selling_turn;
            return this;
        }
        public Alc_Builder Rank(string rank)
        {
            this.rank = rank;
            return this;
        }
        public Alc_Builder Arti_num(int arti_num)
        {
            this.arti_num = arti_num;
            return this;
        }
        public Alc_Builder Caretaker_used(bool caretaker_used)
        {
            this.caretaker_used = caretaker_used;
            return this;
        }
        public Alc_Builder Board_is(int board_is)
        {
            this.board_is = board_is;
            return this;
        }
        public Alc_Builder Ele(int ele)
        {
            this.ele = ele;
            return this;
        }
        public Alc_Builder Ingre(int ingre)
        {
            this.ingre = ingre;
            return this;
        }
        public Alc_Builder Stamp(int stamp)
        {
            this.stamp = stamp;
            return this;
        }
        public Alc_Builder Ori(int ori)
        {
            this.ori = ori;
            return this;
        }
        public Alc_Builder Arr(int[] arr)
        {
            this.arr = arr;
            return this;
        }
        public Alc_Builder Ingre_num(int ingre_num)
        {
            this.ingre_num = ingre_num;
            return this;
        }
        public Alc_Builder Exhibit_potion(string exhibit_potion)
        {
            this.exhibit_potion = exhibit_potion;
            return this;
        }
        public Alc_Builder Favor_card(string favor_card)
        {
            this.favor_card = favor_card;
            return this;
        }
        public Alc_Builder Select_board_num(int select_board_num)
        {
            this.select_board_num = select_board_num;
            return this;
        }
        public Alc_Builder Ingre_list(string[] ingre_list)
        {
            this.ingre_list = ingre_list;
            return this;
        }
        public Alc_Builder Success(bool success)
        {
            this.success = success;
            return this;
        }
        public Alc_Builder Stamp(Stamp_Info[] stamp)
        {
            this.stamp_data = stamp;
            return this;
        }
        #endregion


        #region Builder area

        public Room_List Room_List_Build()
        {
            return new Room_List(room_name, count.ToString(), max_count.ToString());
        }

        public Simple_Room_Data Simple_Room_Data_Build()
        {
            return new Simple_Room_Data(room_name, room_pw);
        }

        public Enter_Room_Data Enter_Room_Data_Build()
        {
            return new Enter_Room_Data(user_name, room_name, is_master, is_ready, no_enter, msg);
        }

        public Create_Room Create_Room_Build()
        {
            return new Create_Room(user_name, room_name, room_pw, count, is_master, is_ready, msg);
        }

        public Edit_Color_Data Edit_Color_Data_Build()
        {
            return new Edit_Color_Data(user_name, user_color, user_key, room_name);
        }

        public Chat_Data Chat_Data_Build()
        {
            return new Chat_Data(speaker, msg, type, room_name);
        }

        public Common_Data Common_Data_Build()
        {
            return new Common_Data(room_name, user_key);
        }
        public Ready_Game_Data Ready_Game_Data_Build()
        {
            return new Ready_Game_Data(room_name, user_key, is_ready);
        }

        public Select_Round_Order_Data Select_Round_Order_Data_Build()
        {
            return new Select_Round_Order_Data(user_key, select_order, room_name);
        }

        public Send_Room_Name_Data Send_Room_Name_Data_Build()
        {
            return new Send_Room_Name_Data(room_name);
        }
        public Select_Cube_Data Select_Cube_Data_Build()
        {
            return new Select_Cube_Data(user_key, room_name, board_num, button_order_num);
        }
        public Reasoning_Table_Change_Data Reasoning_Table_Change_Build()
        {
            return new Reasoning_Table_Change_Data(user_key, room_name, x, y, change_val);
        }
        public Pick_Ingre_Data Pick_Ingre_Data_Build()
        {
            return new Pick_Ingre_Data(user_key, room_name, pick_item, cube_order, board_order, ingredient_select_arr_order);
        }
        public Sell_Item_Confirm_Data Sell_Item_Confirm_Data_Build()
        {
            return new Sell_Item_Confirm_Data(user_key, room_name, sell_item_num, board_order, cube_order);
        }
        public Cancel_Selling_Ingre_Data Cancel_Selling_Ingre_Data_Build()
        {
            return new Cancel_Selling_Ingre_Data(user_key, room_name, board_num, cube_order);
        }
        public Adv_Dis_Confirm_Data Adv_Dis_Confirm_Data_Build()
        {
            return new Adv_Dis_Confirm_Data(user_key, room_name, color, dis_coin_num);
        }
        public Sell_Price_Confirm_Data Sell_Price_Confirm_Data_Build()
        {
            return new Sell_Price_Confirm_Data(user_key, room_name, sell_price);
        }
        public Sell_to_Adv_Potion_Data Sell_To_Adv_Potion_Data_Build()
        {
            return new Sell_to_Adv_Potion_Data(user_key, room_name, user_color, card_list, what_kind_sell_potion, selling_turn);
        }
        public Buy_Artifact_Confirm_Data Buy_Artifact_Confirm_Data_Build()
        {
            return new Buy_Artifact_Confirm_Data(user_key, room_name, board_order, cube_order, arti_num, rank);
        }
        public Test_Ingredient_Confirm_Data Test_Ingredient_Confirm_Data_Build()
        {
            return new Test_Ingredient_Confirm_Data(user_key, room_name, card_list, caretaker_used, board_order, cube_order, board_is);
        }
        public Presentation_Theory_Data Presentation_Theory_Data_Build()
        {
            return new Presentation_Theory_Data(user_key, room_name, user_color, ele, ingre, stamp, board_order, cube_order);
        }
        public Refuting_Theory_Data Refuting_Theory_Data_Build()
        {
            return new Refuting_Theory_Data(user_key, room_name, ingre, ori, arr, board_order, cube_order);
        }
        public Check_Refute_Info_Data Check_Refute_Info_Build()
        {
            return new Check_Refute_Info_Data(room_name, ingre_num);
        }
        public Exhibit_Ingre_Data Exhibit_Ingre_Data_Build()
        {
            return new Exhibit_Ingre_Data(user_key, room_name, user_color, card_list, exhibit_potion, board_order, cube_order);
        }
        public Favor_Card_Use_Confirm_Data Favor_Card_Use_Confirm_Data_Build()
        {
            return new Favor_Card_Use_Confirm_Data(user_key, room_name, favor_card, select_board_num, ingre_list);
        }
        public Board_Passing_Data Board_Passing_Data_Build()
        {
            return new Board_Passing_Data(user_key, room_name, board_num, board_order, cube_order);
        }
        public Refute_Check_Info Refute_Check_Info_Build()
        {
            return new Refute_Check_Info(user_key, room_name, ingre_num, ori, success, stamp_data);
        }

        #endregion

    }
}

