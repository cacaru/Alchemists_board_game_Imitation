using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alchemists_data;
using System;

namespace Self_Converter {

    public class StoD_converter : MonoBehaviour
    {
        // SocketIOResponse to Dictionary<int, Room_List>
        public static Dictionary<int, Room_List> Resp_to_Room_list(object convert)
        {
            // converte �� ������ [[{"key":"value", ...  }]]
            // �̸� dictionary�� �������� �����ϴ� �Լ�
            // key�� int 0~ 
            // value�� Room_list.name, count,max_count�� �̷���� ���� :: private�� Get_name, Get_count, Get_max_count�� ����
            //                      count�� setter ����

            Dictionary<int, Room_List> result = new Dictionary<int, Room_List>();

            // string���� �ѹ� ��ȯ�ϰ�
            var temp = convert.ToString();

            for (var i = 0; i < temp.Length - 2;)
            {
                // {}���� �� ���� ������ string ���ٷ� �̰�
                int x = temp.IndexOf("{", i);
                int y = temp.IndexOf("}", i);

                var room_data_full_str = temp.Substring(x + 1, y - x - 1);

                //Debug.Log(room_data_full_str);

                // ���� str ������ name, count, max_count�� �����Ͽ� Room_List �� ���� Dictionary�� �߰�
                var list = room_data_full_str.Replace("\"", "").Split(",");
                string name = list[0].Trim().Split(":")[1];
                string count = list[1].Trim().Split(":")[1];
                string max_count = list[2].Trim().Split(":")[1];

                Room_List temp_room_list = new Alc_Builder().Room_name(name)
                                                            .Count(int.Parse(count))
                                                            .Max_count(int.Parse(max_count))
                                                            .Room_List_Build();

                int counter = result.Count;
                result.Add(counter, temp_room_list);
                /*                Debug.Log(result[0].Get_name());
                                Debug.Log(result[0].Get_count());
                                Debug.Log(result[0].Get_max_count());*/
                i = y + 1;
            }

            return result;

        }

        // user_data_array �� Dictionary<int, User_Data_Array>�� �����ϴ� �۾�
        public static Dictionary<int, User_Data_Array> Resp_to_User_Data(object convert)
        {
            Dictionary<int, User_Data_Array> result = new();
            int user_num = 0;
            //Debug.Log(convert);
            var t = convert.ToString().Replace("[", "").Replace("]", "");
            ArrayList a = new ArrayList();

            // �ݴ� ��ȣ ��ġ�� �����ص�
            // �ݴ� ��ȣ�� 9�� ����� ������ ���е� 0~8 : 1������ / 9~17 : 2������ / 18~26 : 3������ / 27~35 : 4�� ����(max)
            for (var i = 0; i < t.Length; i++)
            {
                if (t[i].Equals('}'))
                {
                    a.Add(i - 2);
                }
            }
            int max_list = a.Count;

            int x = 0;
            int y = 0;

            // ���� ���� �ص�
            string user_name, user_color, is_master, is_ready, is_ingame, user_key;
            int my_gold, cube_count, point;
            int card_1, card_2, card_3, card_4, card_5, card_6, card_7, card_8, total;
            int assistent, bar_owner, big_man, caretaker, herbalist, merchant, shopkeeper, wise_man;
            bool arti1, arti2, arti3, arti4, arti5, arti6, arti7, arti8, arti9, arti10, arti11, arti12, arti13, arti14, arti15, arti16, arti17, arti18;
            bool ad0, ad1, ad2, ad3;
            bool potion1, potion2, potion3, potion4, potion5, potion6, potion7, potion8, potion9, potion10;
            bool st1, st2, st3, st4, st5, st6, st7, st8, st9, st10, st11;
            bool red, green, blue;

            int user_counter = 1;

            for (var i = 0; i < max_list;)
            {
                if (i > 0)
                    x = t.IndexOf("{", (int)a[9 * (user_counter - 1) - 1]);

                string temp = t.Substring(x + 1, (int)a[9 * user_counter - 1] - 1 - x);
                //Debug.Log(temp);

                x = 0;

                // user_name, user_color, is_master, is_ready, is_ingame, user_key
                y = temp.IndexOf("{", x + 1) - 19;
                string str_1 = temp.Substring(x, y - x);
                //Debug.Log(str_1);
                var list = str_1.Replace("\"", "").Split(",");
                user_name = list[0].Trim().Split(":")[1];
                user_color = list[1].Trim().Split(":")[1];
                is_master = list[2].Trim().Split(":")[1];
                is_ready = list[3].Trim().Split(":")[1];
                is_ingame = list[4].Trim().Split(":")[1];
                user_key = list[5].Trim().Split(":")[1];

                // user_ingame_data
                // my_gold, cube_count, point
                x = temp.IndexOf("{", y);
                y = temp.IndexOf("{", x + 1);
                str_1 = temp.Substring(x + 1, y - x - 15);
                //Debug.Log(str_1);
                list = str_1.Replace("\"", "").Split(",");
                my_gold = int.Parse(list[0].Trim().Split(":")[1]);
                cube_count = int.Parse(list[1].Trim().Split(":")[1]);
                point = int.Parse(list[2].Trim().Split(":")[1]);

                // ingredient
                x = y;
                if (i > 0)
                    y = (int)a[9 * user_counter - 9] - (int)a[9 * user_counter - 10] - 4;
                else
                    y = (int)a[9 * user_counter - 9];
                str_1 = temp.Substring(x + 1, y - x);
                //Debug.Log(str_1);
                list = str_1.Replace("\"", "").Split(",");
                card_1 = int.Parse(list[0].Trim().Split(":")[1]);
                card_2 = int.Parse(list[1].Trim().Split(":")[1]);
                card_3 = int.Parse(list[2].Trim().Split(":")[1]);
                card_4 = int.Parse(list[3].Trim().Split(":")[1]);
                card_5 = int.Parse(list[4].Trim().Split(":")[1]);
                card_6 = int.Parse(list[5].Trim().Split(":")[1]);
                card_7 = int.Parse(list[6].Trim().Split(":")[1]);
                card_8 = int.Parse(list[7].Trim().Split(":")[1]);
                total = int.Parse(list[8].Trim().Split(":")[1]);

                Ingredient_Data ingre = new Ingredient_Data(card_1, card_2, card_3, card_4, card_5, card_6, card_7, card_8, total);

                // favor_card
                x = temp.IndexOf("{", x + 1);
                if (i > 0)
                    y = (int)a[9 * user_counter - 8] - (int)a[9 * user_counter - 10] - 4;
                else
                    y = (int)a[9 * user_counter - 8];
                str_1 = temp.Substring(x + 1, y - x);
                //Debug.Log(str_1);
                list = str_1.Replace("\"", "").Split(",");
                assistent = int.Parse(list[0].Trim().Split(":")[1]);
                bar_owner = int.Parse(list[1].Trim().Split(":")[1]);
                big_man = int.Parse(list[2].Trim().Split(":")[1]);
                caretaker = int.Parse(list[3].Trim().Split(":")[1]);
                herbalist = int.Parse(list[4].Trim().Split(":")[1]);
                merchant = int.Parse(list[5].Trim().Split(":")[1]);
                shopkeeper = int.Parse(list[6].Trim().Split(":")[1]);
                wise_man = int.Parse(list[7].Trim().Split(":")[1]);
                total = int.Parse(list[8].Trim().Split(":")[1]);

                Favor_Card_Data favor = new Favor_Card_Data(assistent, bar_owner, big_man, caretaker, herbalist, merchant, shopkeeper, wise_man, total);

                // artifacts
                x = temp.IndexOf("{", x + 1);
                if (i > 0)
                    y = (int)a[9 * user_counter - 7] - (int)a[9 * user_counter - 10] - 4;
                else
                    y = (int)a[9 * user_counter - 7];
                str_1 = temp.Substring(x + 1, y - x);
                //Debug.Log(str_1);
                list = str_1.Replace("\"", "").Split(",");
                arti1 = Convert.ToBoolean(list[0].Trim().Split(":")[1]);
                arti2 = Convert.ToBoolean(list[1].Trim().Split(":")[1]);
                arti3 = Convert.ToBoolean(list[2].Trim().Split(":")[1]);
                arti4 = Convert.ToBoolean(list[3].Trim().Split(":")[1]);
                arti5 = Convert.ToBoolean(list[4].Trim().Split(":")[1]);
                arti6 = Convert.ToBoolean(list[5].Trim().Split(":")[1]);
                arti7 = Convert.ToBoolean(list[6].Trim().Split(":")[1]);
                arti8 = Convert.ToBoolean(list[7].Trim().Split(":")[1]);
                arti9 = Convert.ToBoolean(list[8].Trim().Split(":")[1]);
                arti10 = Convert.ToBoolean(list[9].Trim().Split(":")[1]);
                arti11 = Convert.ToBoolean(list[10].Trim().Split(":")[1]);
                arti12 = Convert.ToBoolean(list[11].Trim().Split(":")[1]);
                arti13 = Convert.ToBoolean(list[12].Trim().Split(":")[1]);
                arti14 = Convert.ToBoolean(list[13].Trim().Split(":")[1]);
                arti15 = Convert.ToBoolean(list[14].Trim().Split(":")[1]);
                arti16 = Convert.ToBoolean(list[15].Trim().Split(":")[1]);
                arti17 = Convert.ToBoolean(list[16].Trim().Split(":")[1]);
                arti18 = Convert.ToBoolean(list[17].Trim().Split(":")[1]);

                Artifacts_Data arti = new Artifacts_Data(arti1, arti2, arti3, arti4, arti5, arti6,
                                                         arti7, arti8, arti9, arti10, arti11, arti12,
                                                         arti13, arti14, arti15, arti16, arti17, arti18);


                // discount_adventurer
                x = temp.IndexOf("{", x + 1);
                if (i > 0)
                    y = (int)a[9 * user_counter - 6] - (int)a[9 * user_counter - 10] - 4;
                else
                    y = (int)a[9 * user_counter - 6];
                str_1 = temp.Substring(x + 1, y - x);
                //Debug.Log(str_1);
                list = str_1.Replace("\"", "").Split(",");
                ad0 = Convert.ToBoolean(list[0].Trim().Split(":")[1]);
                ad1 = Convert.ToBoolean(list[1].Trim().Split(":")[1]);
                ad2 = Convert.ToBoolean(list[2].Trim().Split(":")[1]);
                ad3 = Convert.ToBoolean(list[3].Trim().Split(":")[1]);

                Discount_Adventurer_Data disc = new Discount_Adventurer_Data(ad0, ad1, ad2, ad3);

                //is_check_potion
                x = temp.IndexOf("{", x + 1);
                if (i > 0)
                    y = (int)a[9 * user_counter - 5] - (int)a[9 * user_counter - 10] - 4;
                else
                    y = (int)a[9 * user_counter - 5];
                str_1 = temp.Substring(x + 1, y - x);
                //Debug.Log(str_1);
                list = str_1.Replace("\"", "").Split(",");
                potion1 = Convert.ToBoolean(list[0].Trim().Split(":")[1]);
                potion2 = Convert.ToBoolean(list[1].Trim().Split(":")[1]);
                potion3 = Convert.ToBoolean(list[2].Trim().Split(":")[1]);
                potion4 = Convert.ToBoolean(list[3].Trim().Split(":")[1]);
                potion5 = Convert.ToBoolean(list[4].Trim().Split(":")[1]);
                potion6 = Convert.ToBoolean(list[5].Trim().Split(":")[1]);
                potion7 = Convert.ToBoolean(list[6].Trim().Split(":")[1]);
                potion8 = Convert.ToBoolean(list[7].Trim().Split(":")[1]);
                potion9 = Convert.ToBoolean(list[8].Trim().Split(":")[1]);
                potion10 = Convert.ToBoolean(list[9].Trim().Split(":")[1]);

                Is_Check_Potion_Data potion = new Is_Check_Potion_Data(potion1, potion2, potion3, potion4, potion5, potion6, potion7, potion8, potion9, potion10);

                // have_stamp_data
                x = temp.IndexOf("{", x + 1);
                if (i > 0)
                    y = (int)a[9 * user_counter - 4] - (int)a[9 * user_counter - 10] - 4;
                else
                    y = (int)a[9 * user_counter - 4];
                str_1 = temp.Substring(x + 1, y - x);
                //Debug.Log(str_1);
                list = str_1.Replace("\"", "").Split(",");
                st1 = Convert.ToBoolean(list[0].Trim().Split(":")[1]);
                st2 = Convert.ToBoolean(list[1].Trim().Split(":")[1]);
                st3 = Convert.ToBoolean(list[2].Trim().Split(":")[1]);
                st4 = Convert.ToBoolean(list[3].Trim().Split(":")[1]);
                st5 = Convert.ToBoolean(list[4].Trim().Split(":")[1]);
                st6 = Convert.ToBoolean(list[5].Trim().Split(":")[1]);
                st7 = Convert.ToBoolean(list[6].Trim().Split(":")[1]);
                st8 = Convert.ToBoolean(list[7].Trim().Split(":")[1]);
                st9 = Convert.ToBoolean(list[8].Trim().Split(":")[1]);
                st10 = Convert.ToBoolean(list[9].Trim().Split(":")[1]);
                st11 = Convert.ToBoolean(list[10].Trim().Split(":")[1]);
                total = int.Parse(list[11].Trim().Split(":")[1]);

                Have_Stamp_Data stamp = new Have_Stamp_Data(st1, st2, st3, st4, st5, st6, st7, st8, st9, st10, st11, total);

                // get_extra_point_data
                x = temp.IndexOf("{", x + 1);
                if (i > 0)
                    y = (int)a[9 * user_counter - 3] - (int)a[9 * user_counter - 10] - 4;
                else
                    y = (int)a[9 * user_counter - 3];
                str_1 = temp.Substring(x + 1, y - x);
                //Debug.Log(str_1);
                list = str_1.Replace("\"", "").Split(",");
                red = Convert.ToBoolean(list[0].Trim().Split(":")[1]);
                green = Convert.ToBoolean(list[1].Trim().Split(":")[1]);
                blue = Convert.ToBoolean(list[2].Trim().Split(":")[1]);

                Get_Extra_Point_Data extra = new Get_Extra_Point_Data(red, green, blue);

                User_Ingame_Data in_data = new User_Ingame_Data(cube_count, point, my_gold, ingre, favor, arti, disc, potion, stamp, extra);

                User_Data_Array data = new User_Data_Array(user_name, user_key, user_color, is_master, is_ready, is_ingame, in_data);

                result.Add(user_num, data);
                user_num++;

                i = 9 * user_counter;
                user_counter++;
            }

            return result;
        }

        // chat data to List<string>
        public static string Resp_to_Chat_Data(object convert)
        {
            // data :: speaker, msg, type
            var t = convert.ToString().Replace("[", "")
                                      .Replace("]", "")
                                      .Replace("{", "")
                                      .Replace("}", "")
                                      .Replace("\"", "")
                                      .Split(",");

            string speaker = t[0].Trim().Split(":")[1];
            string msg = t[1].Trim().Split(":")[1];
            string type = t[2].Trim().Split(":")[1];


            // type : normal :: �Ϲ�ä��
            //      : server :: ���� ����
            string r_type = "";
            if (type.Equals("normal"))
                r_type = "�Ϲ�";
            else if (type.Equals("server") || type.Equals("announce") )
                r_type = "�ȳ�";

            // [type][speaker] : msg
            string r_str = "[" + r_type + "][" + speaker + "] : " + msg + "\n";

            //Debug.Log(r_str);

            return r_str;
        }

        // adventurer_card_data to List<Dictionary<string, bool>>
        public static List<Dictionary<string, bool>> Resp_to_Adventurer_Card_Data(object convert)
        {
            // convert :: ["1" : { "red_1":false, ... "blue_0":false}, 2: ... 6{}]
            List<Dictionary<string, bool>> result = new();
            result.Add(null);
            // �� �� ���ȣ ����
            var t = convert.ToString().Replace("[", "")
                                      .Replace("]", "")
                                      ;
            // �뺴 ��ȣ 1~6��
            int num = 1;
            // x,y 
            int x = 3;
            int y = 3;

            Dictionary<string, bool> ad1 = new();
            Dictionary<string, bool> ad2 = new();
            Dictionary<string, bool> ad3 = new();
            Dictionary<string, bool> ad4 = new();
            Dictionary<string, bool> ad5 = new();
            Dictionary<string, bool> ad6 = new();
            result.Add(ad1);
            result.Add(ad2);
            result.Add(ad3);
            result.Add(ad4);
            result.Add(ad5);
            result.Add(ad6);


            for (int i = 0; i < 6; i++)
            {
                x = t.IndexOf("{", y);
                y = t.IndexOf("}", y + 1);
                // 1 
                string[] temp = t[(x + 1)..y].Replace("\"", "").Split(",");
                foreach (string item in temp)
                {
                    string str = item.Split(":")[0];
                    bool b = item.Split(":")[1].Equals("true");
                    result[num].Add(str, b);
                }
                num++;
            }

            //for( int i = 1; i <= 6; i++) Debug.Log("red_1 : " + result[i]["red_1"]);

            return result;
        }

        // resp to result_table 
        public static Dictionary<int, Reasoning_Table_Data> Resp_to_Reasoning_Table_Data(object convert)
        {
            List<string> data = new();
            Dictionary<int, Reasoning_Table_Data> result = new();
            string t = convert.ToString();
            //Debug.Log(t);
            int x = 2;
            int y = 2;

            // �� �ο��� ������ �и�
            while (true)
            {
                x = t.IndexOf("{", y);
                y = t.IndexOf("}", y + 1);
                if (x > 0)
                {
                    string tmp = t[(x + 1)..(y + 1)];
                    data.Add(tmp);
                }
                else
                {
                    break;
                }
            }

            for (int i = 0; i < data.Count; i++)
            {
                // user_key �и�
                x = data[i].IndexOf(",", 0);
                string user_key = data[i][0..x].Replace("\"", "").Trim().Split(":")[1];
                //Debug.Log(user_key);

                // ingredient_reasoning == reasoning_table �и�
                x = data[i].IndexOf(":", x);
                y = data[i].IndexOf("\"", x);
                string reason = data[i][(x + 1)..(y - 1)];
                //Debug.Log(reason);

                // ingredient_result �и�
                x = data[i].IndexOf(":", y);
                y = data[i].IndexOf("}", y);
                string ingre_result = data[i][(x + 1)..y];
                //Debug.Log(ingre_result);
                List<string[]> reason_data = new();
                List<string[]> ingre_result_data = new();

                // ingredient_reasoning, ingre_result �� line�� ������ �и�
                y = 1;
                int a = 1, b = 1;
                for (int j = 0; j < 8; j++)
                {
                    x = reason.IndexOf("[", y);
                    y = reason.IndexOf("]", y + 1);
                    reason_data.Add(reason[(x + 1)..y].Replace("\"", "").Split(","));

                    a = ingre_result.IndexOf("[", b);
                    b = ingre_result.IndexOf("]", b + 1);
                    ingre_result_data.Add(ingre_result[(a + 1)..b].Replace("\"","").Split(","));
                }

                // �и��� �����͵��� ��� result�� �߰�
                Reasoning_Table reasoning = new(reason_data[0], reason_data[1],
                                                reason_data[2], reason_data[3],
                                                reason_data[4], reason_data[5],
                                                reason_data[6], reason_data[7]);
                Ingredient_Result ingredient = new(ingre_result_data[0],
                                                   ingre_result_data[1],
                                                   ingre_result_data[2],
                                                   ingre_result_data[3],
                                                   ingre_result_data[4],
                                                   ingre_result_data[5],
                                                   ingre_result_data[6],
                                                   ingre_result_data[7]);

                result.Add(i, new(user_key, reasoning, ingredient));
            }

            return result;
        }

        // resp to theory_data 
        public static Dictionary<int, Theory_Data> Resp_to_Theory_Data(object convert)
        {
            Dictionary<int, Theory_Data> result = new();

            string t = convert.ToString().Replace("[", "").Replace("]", "");
            List<string> data = new();

            int x = 2;
            int y = 2;
            // 8���� ��� ���� �ɰ���
            while (true)
            {
                x = t.IndexOf("{", y);
                y = t.IndexOf("}}", y + 3);
                if( x > 0)
                {
                    data.Add(t[(x+1)..(y+1)]);
                }
                else
                {
                    break;
                }
            }
            
            for (int i = 1; i <= data.Count; i++)
            {
                //Debug.Log(data[i]);
                y = 2;
                x = data[i-1].IndexOf(":", y);
                y = data[i-1].IndexOf(",", y + 1);
                int ele = String.IsNullOrWhiteSpace(data[i - 1][(x + 1)..y].Replace("\"", "")) ? 0 : int.Parse(data[i-1][(x + 1)..y].Replace("\"", ""));   // 1~8

                x = data[i-1].IndexOf("{\"u", y);
                y = data[i-1].IndexOf("}", y + 1);
                string[] tmp = data[i-1][(x + 1)..y].Split(",");
                //Debug.Log(tmp[1]);
                Stamp_Info st_1 = new Stamp_Info(tmp[0].Replace("\"", "").Split(":")[1],
                                                 tmp[1].Replace("\"", "").Split(":")[1],
                                                 tmp[2].Replace("\"", "").Split(":")[1]);

                //Debug.Log(tmp[0].Replace("\"", "").Split(":")[1]);
                //Debug.Log(tmp[1].Replace("\"", "").Split(":")[1]);
                //Debug.Log(tmp[2].Replace("\"", "").Split(":")[1]);

                x = data[i-1].IndexOf("{\"u", y);
                y = data[i-1].IndexOf("}", y + 1);
                tmp = data[i-1][(x + 1)..y].Split(",");
                Stamp_Info st_2 = new Stamp_Info(tmp[0].Replace("\"", "").Split(":")[1],
                                                 tmp[1].Replace("\"", "").Split(":")[1],
                                                 tmp[2].Replace("\"", "").Split(":")[1]);
                //Debug.Log(tmp[0].Replace("\"", "").Split(":")[1]);
                //Debug.Log(tmp[1].Replace("\"", "").Split(":")[1]);
                //Debug.Log(tmp[2].Replace("\"", "").Split(":")[1]);
                
                x = data[i-1].IndexOf("{\"u", y);
                y = data[i-1].IndexOf("}", y + 1);
                tmp = data[i-1][(x + 1)..y].Split(",");
                Stamp_Info st_3 = new Stamp_Info(tmp[0].Replace("\"", "").Split(":")[1],
                                                 tmp[1].Replace("\"", "").Split(":")[1],
                                                 tmp[2].Replace("\"", "").Split(":")[1]);
                //Debug.Log(tmp[0].Replace("\"", "").Split(":")[1]);
                //Debug.Log(tmp[1].Replace("\"", "").Split(":")[1]);
                //Debug.Log(tmp[2].Replace("\"", "").Split(":")[1]);

                result.Add(i, new(ele, st_1, st_2, st_3));
            }

            return result;
        }

        // resp to exhibition_data
        public static Exhibition_Result Resp_to_Exhibition_Result(object convert)
        {
            string t = convert.ToString().Replace("[", "").Replace("]", "");

            int x = 2;
            int y = 2;
            // �����͸� tmp, tmp_2 �� 2���� �������� ����
            x = t.IndexOf("first", y);
            y = t.IndexOf("}}", y + 1);

            string tmp = t[(x + 7)..(y + 1)];
            //Debug.Log(tmp);

            x = t.IndexOf("second", y);
            y = t.IndexOf("}}", y + 3);

            string tmp_2 = t[(x + 8)..(y + 1)];
            //Debug.Log(tmp_2);

            List<string[]> first_data = new();
            List<string[]> second_data = new();

            // ������ ����
            y = 2;
            int a = 2, b = 2;
            for(int i = 0; i < 6; i++)
            {
                x = tmp.IndexOf(":", y);
                y = tmp.IndexOf("},", y + 2);
                a = tmp_2.IndexOf(":", b);
                b = tmp_2.IndexOf("},", b + 2);
                if (y > 0 || b > 0)
                {
                    first_data.Add(tmp[(x + 2)..y].Split(","));
                    second_data.Add(tmp_2[(a + 2)..b].Split(","));
                }
                else
                {
                    first_data.Add(tmp[(x + 2)..(tmp.Length - 1)].Split(","));
                    second_data.Add(tmp_2[(a + 2)..(tmp_2.Length - 1)].Split(","));
                }
                
            }

            Exhibit_Potion_Data f_red_1   = new(first_data[0][0].Split(":")[1].Replace("\"", "").Trim(), first_data[0][1].Split(":")[1].Replace("\"", "").Trim());
            Exhibit_Potion_Data f_red_0   = new(first_data[1][0].Split(":")[1].Replace("\"", "").Trim(), first_data[1][1].Split(":")[1].Replace("\"", "").Trim());
            Exhibit_Potion_Data f_green_1 = new(first_data[2][0].Split(":")[1].Replace("\"", "").Trim(), first_data[2][1].Split(":")[1].Replace("\"", "").Trim());
            Exhibit_Potion_Data f_green_0 = new(first_data[3][0].Split(":")[1].Replace("\"", "").Trim(), first_data[3][1].Split(":")[1].Replace("\"", "").Trim());
            Exhibit_Potion_Data f_blue_1  = new(first_data[4][0].Split(":")[1].Replace("\"", "").Trim(), first_data[4][1].Split(":")[1].Replace("\"", "").Trim());
            Exhibit_Potion_Data f_blue_0  = new(first_data[5][0].Split(":")[1].Replace("\"", "").Trim(), first_data[5][1].Split(":")[1].Replace("\"", "").Trim());

            Exhibit_Potion_Data s_red_1   = new(second_data[0][0].Split(":")[1].Replace("\"", "").Trim(), second_data[0][1].Split(":")[1].Replace("\"", "").Trim());
            Exhibit_Potion_Data s_red_0   = new(second_data[1][0].Split(":")[1].Replace("\"", "").Trim(), second_data[1][1].Split(":")[1].Replace("\"", "").Trim());
            Exhibit_Potion_Data s_green_1 = new(second_data[2][0].Split(":")[1].Replace("\"", "").Trim(), second_data[2][1].Split(":")[1].Replace("\"", "").Trim());
            Exhibit_Potion_Data s_green_0 = new(second_data[3][0].Split(":")[1].Replace("\"", "").Trim(), second_data[3][1].Split(":")[1].Replace("\"", "").Trim());
            Exhibit_Potion_Data s_blue_1  = new(second_data[4][0].Split(":")[1].Replace("\"", "").Trim(), second_data[4][1].Split(":")[1].Replace("\"", "").Trim());
            Exhibit_Potion_Data s_blue_0  = new(second_data[5][0].Split(":")[1].Replace("\"", "").Trim(), second_data[5][1].Split(":")[1].Replace("\"", "").Trim());

            Exhibition_Result result = new(new(f_red_1, f_red_0, f_green_1, f_green_0, f_blue_1, f_blue_0), 
                                           new(s_red_1, s_red_0, s_green_1, s_green_0, s_blue_1, s_blue_0));
            return result;
        }

        // resp to final_round_order_data
        public static Dictionary<int, Final_Round_Order> Resp_to_Final_Round_Order_Data(object convert)
        {
            Dictionary<int, Final_Round_Order> result = new();

            string t = convert.ToString().Replace("[", "").Replace("]", "");
            //Debug.Log(t);
            if (t.Length > 1)
            {
                // �߰�ȣ�� �������� ����
                int x = 0;
                int y = 0;
                int i = 0;
                while (true)
                {
                    x = t.IndexOf("{", y);
                    y = t.IndexOf("}", y + 1);

                    if (x < 0)
                        break;

                    string tmp = t[(x + 1)..y];
                    //Debug.Log(tmp);
                    string[] tmp2 = tmp.Replace("\"", "").Trim().Split(",");

                    Final_Round_Order f_data = new Final_Round_Order(tmp2[0].Split(":")[1],
                                                                     int.Parse(tmp2[1].Split(":")[1]),
                                                                     int.Parse(tmp2[2].Split(":")[1]),
                                                                     tmp2[3].Split(":")[1]);
                    result.Add(i++, f_data);
                }
            }
            

            return result;
        }

        // resp to User_Cube_Data
        public static Dictionary<int, Dictionary<int, User_Cube_Data>> Resp_to_User_Cube_Data(object convert)
        {
            Dictionary<int, Dictionary<int, User_Cube_Data>> result = new();
            // int :: board_num
            // User_Cube_Data :: board ���� �����Ͱ� ��ȣ������ �������...
            /*
             *    user_key : data.user_key,
                  user_color : user_color,
                  order : '',
                  length : 3,
                  cube : {
                    1 : {
                      num : 1,
                      cnt : 1,
                      is_select : false,
                    },
                    2 : {
                      num : 2,
                      cnt : 1,
                      is_select : false,
                    },
                    3 : {
                      num : 3,
                      cnt : 1,
                      is_select : false,
                    },
                  }
             * 
             * */
            string t = convert.ToString();
            // �����Ͱ� ����ִ��� �Ǵ�
            int check_point = t.IndexOf("{", 0);    // �����Ͱ� ���� ���ȣ�ۿ� ���� [[null,[],[],[],[],[],[],[],[],[]]]

            // �����Ͱ� ����ִ� ���
            if (check_point > 0)
            {
                // ���ȣ�� �� 9������ �ɰ�����
                int x = 0;
                int y = 0;
                // ù �ο����� ���� �����͸� ������ �̱�
                x = t.IndexOf("[[", y);
                y = t.IndexOf(",", x);

                // �ൿ���� ǥ�� ����
                int board_num = 1;  // 1�� ������� �����Ƿ�
                                    // 9��
                while (true)
                {
                    x = t.IndexOf("[", y);
                    y = t.IndexOf("]", y + 1);

                    if (x < 0)
                        break;

                    string tmp = t[(x + 1)..y];
                    int a = 0;
                    int b = 0;
                    //Debug.Log(tmp);
                    Dictionary<int, User_Cube_Data> tmp_data_dic = new();

                    // �������� ���� �ִ� �����͸� �ٽ� ���� 
                    // �ִ� 4��
                    int user_num = 0;
                    while (true)
                    {
                        // ����� ���� ������
                        Dictionary<int, Cube_On_Board> tmp_cube_data_dic = new();
                        // ��� ���� ������ ���������� ����
                        a = tmp.IndexOf("{", b);
                        b = tmp.IndexOf("}}}", b + 1);
                        if (a < 0) break;
                        //Debug.Log(tmp[(a + 1)..b]);
                        // ������ ���� 
                        string part = tmp[(a + 1)..b];

                        // cube ���ķ� ���ڿ��� �ɰ��� ������ ���ϰ� �ٲ�
                        int cube_finder = part.IndexOf("\"cube\"");
                        // cube �չ��忡�� �ʿ��� ������ ����
                        string[] part_data = part[0..cube_finder].Replace("\"", "").Split(",");

                        // �� ����(cube)�����͸� �ٽ� ������ �غ�
                        string part_data2 = part[(cube_finder + 7)..part.Length];
                        //Debug.Log(part_data2);
                        int c = 0;
                        int d = 3;

                        // cube_numbering
                        int cube_num = 1;
                        while (true) // �ִ� 4��
                        {
                            c = part_data2.IndexOf("{", d);
                            if (c < 0) break;
                            d = part_data2.IndexOf("}", d + 1);
                            if (d < 0) d = part_data2.Length;
                            /*
                                Debug.Log(part_data2[(c + 1)..d]);
                                Debug.Log(part_data2[(c + 1)..d].Replace("\"", "").Split(",")[0].Split(":")[1]);
                                Debug.Log(part_data2[(c + 1)..d].Replace("\"", "").Split(",")[1].Split(":")[1]);
                                Debug.Log(part_data2[(c + 1)..d].Replace("\"", "").Split(",")[2].Split(":")[1].Equals("true"));
                            */
                            Cube_On_Board tmp_cube_data = new(int.Parse(part_data2[(c + 1)..d].Replace("\"", "").Split(",")[0].Split(":")[1]),
                                                              int.Parse(part_data2[(c + 1)..d].Replace("\"", "").Split(",")[1].Split(":")[1]),
                                                              part_data2[(c + 1)..d].Replace("\"", "").Split(",")[2].Split(":")[1].Equals("true"));

                            tmp_cube_data_dic.Add(cube_num++, tmp_cube_data);
                        }

                        // cube_data�� ���� ����� tmp_cube_data_dic�� ������ �����͸� ������ ������ ������ �ϼ���
                        // 0 : user_key, 1:user_color, 2:order, 3:length
                        User_Cube_Data tmp_data = new User_Cube_Data(part_data[0].Split(":")[1],
                                                                     part_data[1].Split(":")[1],
                                                                     int.Parse(part_data[2].Split(":")[1]),
                                                                     int.Parse(part_data[3].Split(":")[1]),
                                                                     tmp_cube_data_dic);

                        tmp_data_dic.Add(user_num++, tmp_data);
                    }

                    // 2�� �̻��� ������ ���� ������ ������ ������� ������
                    result.Add(board_num++, tmp_data_dic);
                }
            }

            // ������� ���� ��� -> �����͸� ������ �ʿ䰡 ����, �׳� �� �����͸� ������
            // ���� �ƹ��͵� ���� �����Ƿ� else ������ �ʿ� ����

            return result;
        }

        // resp to discount_coin_list
        public static Dictionary<int, Adv_Dis_Confirm_Data> Resp_to_Discount_Coin_List_Data(object convert)
        {
            Dictionary<int, Adv_Dis_Confirm_Data> result = new();
            string t = convert.ToString().Replace("[", "").Replace("]","");
            //Debug.Log(t);
            // Adv_Dis_Confirm_Data ::
            /* string user_key, room_name, color, dis_coin_num
             * [[{"user_key":"knVwUzIJ4NguKyOLAAAk","color":"white","dis_coin_num":1,"room_name":"qwer"},
             * {"user_key":"6_eiwJi5PWudyOfqAAAn","color":"blue","dis_coin_num":1,"room_name":"qwer"}]]
             */
            int x = 0, y = 0, cnt = 0;

            while(true)
            {
                x = t.IndexOf("{", y);
                if (x < 0) break;
                y = t.IndexOf("}", x);
                string tmp = t[(x + 1)..y];
                string[] list = tmp.Split(",");

                Adv_Dis_Confirm_Data tmp_adv = new Adv_Dis_Confirm_Data(list[0].Replace("\"", "").Split(":")[1],
                                                                        list[3].Replace("\"", "").Split(":")[1],
                                                                        list[1].Replace("\"", "").Split(":")[1],
                                                                        int.Parse(list[2].Replace("\"", "").Split(":")[1])
                                                                        );
                //Debug.Log(tmp_adv.print());
                result.Add(cnt++, tmp_adv);
            }


            return result;
        }

        public static Selling_Potion_End Resp_to_Selling_Potion_End(object convert)
        {
            Selling_Potion_End result;
            string t = convert.ToString().Replace("[", "").Replace("]", "").Replace("{", "").Replace("}", "").Replace("\"", "");
            /*    selling_success : selling_success,
                  potion : data.what_kind_sell_potion,
                  user_key : data.user_key,
                  user_color : data.user_color,
             */
            string[] list = t.Split(",");
            result = new Selling_Potion_End( list[0].Split(":")[1].Equals("true"),
                                             list[1].Split(":")[1],
                                             list[2].Split(":")[1],
                                             list[3].Split(":")[1]               );
            //Debug.Log(result.print());
            return result;
        }

        public static Dictionary<int, Game_Result_Data> Resp_to_Game_Result_Data(object convert)
        {
            string t = convert.ToString().Replace("[", "").Replace("]", "").Replace("\"", "").Replace("{", "").Replace("}", "");
            string[] list = t.Split(",");

            Dictionary<int, Game_Result_Data> result = new();

            string name;
            float score;
            float grade;
            
            // 1�� ����
            name = list[0].Split(":")[2];
            score = float.Parse(list[1].Split(":")[1]);
            grade = float.Parse(list[2].Split(":")[1]);

            Game_Result_Data tmp = new(name, score, grade);
            result.Add(1, tmp);

            // 2�� ����
            name = list[3].Split(":")[2];
            score = float.Parse(list[4].Split(":")[1]);
            grade = float.Parse(list[5].Split(":")[1]);

            tmp = new(name, score, grade);
            result.Add(2, tmp);

            ///////////////�� ������ ������ ����///////////////////////////

            // 3�� ������ �ִ��� �˻�
            if (list[6].Split(":")[2].Length > 1)
            {
                // ������ �߰��ϱ�
                name = list[6].Split(":")[2];
                score = float.Parse(list[7].Split(":")[1]);
                grade = float.Parse(list[8].Split(":")[1]);

                tmp = new(name, score, grade);
                result.Add(3, tmp);

                // 4�� ������ �ִ��� �˻�
                if (list[9].Split(":")[2].Length > 1)
                {
                    // ������ �߰��ϱ�
                    name = list[9].Split(":")[2];
                    score = float.Parse(list[10].Split(":")[1]);
                    grade = float.Parse(list[11].Split(":")[1]);

                    tmp = new(name, score, grade);
                    result.Add(4, tmp);
                }
            }

            return result;
        }
    }

}
