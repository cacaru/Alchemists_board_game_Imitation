
namespace Alchemists_data
{
    public class User_Data_Array
    {
        private string user_name;
        private string user_key;
        private string user_color;
        private string is_master;
        private string is_ready;
        private string is_ingame;
        private User_Ingame_Data user_ingame_data;

        public User_Data_Array(string user_name, string user_key, string user_color, string is_master, string is_ready, string is_ingame, User_Ingame_Data user_ingame_data)
        {
            this.user_name = user_name;
            this.user_key = user_key;
            this.user_color = user_color;
            this.is_master = is_master;
            this.is_ready = is_ready;
            this.is_ingame = is_ingame;
            this.user_ingame_data = user_ingame_data;
        }

        // getter
        public string User_name { get { return user_name; } }
        public string User_key { get { return user_key; } }
        public string User_color { get { return user_color; } }
        public string Is_master { get { return is_master; } }
        public string Is_ready { get { return is_ready; } }
        public string Is_ingame { get { return is_ingame; } }
        public User_Ingame_Data User_ingame_data { get { return user_ingame_data; } }
    }
    public class User_Ingame_Data
    {
        private int cube_count;
        private int point;
        private int my_gold;
        private Ingredient_Data ingredient;
        private Favor_Card_Data favor_card;
        private Artifacts_Data artifacts;
        private Discount_Adventurer_Data discount_adventurer;
        private Is_Check_Potion_Data is_check_potion;
        private Have_Stamp_Data have_stamp;
        private Get_Extra_Point_Data get_extra_point;

        public User_Ingame_Data(int cube_count, int point, int my_gold,
                                Ingredient_Data ingredient,
                                Favor_Card_Data favor_card,
                                Artifacts_Data artifacts,
                                Discount_Adventurer_Data discount_adventurer,
                                Is_Check_Potion_Data is_check_potion,
                                Have_Stamp_Data have_stamp,
                                Get_Extra_Point_Data get_extra_point
            )
        {
            this.cube_count = cube_count;
            this.point = point;
            this.my_gold = my_gold;
            this.ingredient = ingredient;
            this.favor_card = favor_card;
            this.artifacts = artifacts;
            this.discount_adventurer = discount_adventurer;
            this.is_check_potion = is_check_potion;
            this.have_stamp = have_stamp;
            this.get_extra_point = get_extra_point;
        }

        #region property
        public int Cube_count { get { return cube_count; } }
        public int Point { get { return point; } }
        public int My_gold { get { return my_gold; } }
        public Ingredient_Data Ingredient { get { return ingredient; } }
        public Favor_Card_Data Favor_card { get { return favor_card; } }
        public Artifacts_Data Artifacts { get { return artifacts; } }
        public Discount_Adventurer_Data Discount_adventurer { get { return discount_adventurer; } }
        public Is_Check_Potion_Data Is_check_poition { get { return is_check_potion; } }
        public Have_Stamp_Data Have_stamp { get { return have_stamp; } }
        public Get_Extra_Point_Data Get_extra_point { get { return get_extra_point; } }
        #endregion
    }

    public class Ingredient_Data
    {
        private int card_1;
        private int card_2;
        private int card_3;
        private int card_4;
        private int card_5;
        private int card_6;
        private int card_7;
        private int card_8;
        private int total;

        public Ingredient_Data(int card_1, int card_2, int card_3, int card_4, int card_5, int card_6, int card_7, int card_8, int total)
        {
            this.card_1 = card_1;
            this.card_2 = card_2;
            this.card_3 = card_3;
            this.card_4 = card_4;
            this.card_5 = card_5;
            this.card_6 = card_6;
            this.card_7 = card_7;
            this.card_8 = card_8;
            this.total = total;
        }
        public int Card_1 { get { return card_1; } }
        public int Card_2 { get { return card_2; } }
        public int Card_3 { get { return card_3; } }
        public int Card_4 { get { return card_4; } }
        public int Card_5 { get { return card_5; } }
        public int Card_6 { get { return card_6; } }
        public int Card_7 { get { return card_7; } }
        public int Card_8 { get { return card_8; } }
        public int Total { get { return total; } }
    }
    public class Favor_Card_Data
    {
        private int assistent;
        private int bar_owner;
        private int big_man;
        private int caretaker;
        private int herbalist;
        private int merchant;
        private int shopkeeper;
        private int wise_man;
        private int total;

        public Favor_Card_Data(int assistent, int bar_owner, int big_man, int caretaker, int herbalist, int merchant, int shopkeeper, int wise_man, int total)
        {
            this.assistent = assistent;
            this.bar_owner = bar_owner;
            this.big_man = big_man;
            this.caretaker = caretaker;
            this.herbalist = herbalist;
            this.merchant = merchant;
            this.shopkeeper = shopkeeper;
            this.wise_man = wise_man;
            this.total = total;
        }
        public int Assistent { get { return assistent; } }
        public int Bar_owner { get { return bar_owner; } }
        public int Big_man { get { return big_man; } }
        public int Caretaker { get { return caretaker; } }
        public int Herbalist { get { return herbalist; } }
        public int Merchant { get { return merchant; } }
        public int Shopkeeper { get { return shopkeeper; } }
        public int Wise_man { get { return wise_man; } }
        public int Total { get { return total; } }
    }
    public class Artifacts_Data
    {
        // rank1
        private bool discount_card;
        private bool haste_boots;
        private bool magic_mortar;
        private bool night_vision;
        private bool printing_machine;
        private bool robe_of_respect;

        //rank2
        private bool chest_of_witch;
        private bool eloquent_necklace;
        private bool hypnotic_necklace;
        private bool seal_of_authority;
        private bool silver_glass;
        private bool thinking_hat;

        //rank3
        private bool bronze_cup;
        private bool feather_hat;
        private bool glass_cabinet;
        private bool golden_alter;
        private bool magic_mirror;
        private bool statue_of_wisdom;

        public Artifacts_Data(bool discount_card, bool haste_boots, bool magic_mortar, bool night_vision, bool printing_machine, bool robe_of_respect,
                         bool chest_of_witch, bool eloquent_necklace, bool hypnotic_necklace, bool seal_of_authority, bool silver_glass, bool thinking_hat,
                         bool bronze_cup, bool feather_hat, bool glass_cabinet, bool golden_alter, bool magic_mirror, bool statue_of_wisdom)
        {
            this.discount_card = discount_card;
            this.haste_boots = haste_boots;
            this.magic_mortar = magic_mortar;
            this.night_vision = night_vision;
            this.printing_machine = printing_machine;
            this.robe_of_respect = robe_of_respect;

            this.chest_of_witch = chest_of_witch;
            this.eloquent_necklace = eloquent_necklace;
            this.hypnotic_necklace = hypnotic_necklace;
            this.seal_of_authority = seal_of_authority;
            this.silver_glass = silver_glass;
            this.thinking_hat = thinking_hat;

            this.bronze_cup = bronze_cup;
            this.feather_hat = feather_hat;
            this.glass_cabinet = glass_cabinet;
            this.golden_alter = golden_alter;
            this.magic_mirror = magic_mirror;
            this.statue_of_wisdom = statue_of_wisdom;
        }
        #region getter area
        public bool Discount_card { get { return discount_card; } }
        public bool Haste_boots { get { return haste_boots; } }
        public bool Magic_mortar { get { return magic_mortar; } }
        public bool Night_vision { get { return night_vision; } }
        public bool Printing_machine { get { return printing_machine; } }
        public bool Robe_of_respect { get { return robe_of_respect; } }
        public bool Chest_of_witch { get { return chest_of_witch; } }
        public bool Eloquent_necklace { get { return eloquent_necklace; } }
        public bool Hypnotic_necklace { get { return hypnotic_necklace; } }
        public bool Seal_of_authority { get { return seal_of_authority; } }
        public bool Silver_glass { get { return silver_glass; } }
        public bool Thinking_hat { get { return thinking_hat; } }
        public bool Bronze_cup { get { return bronze_cup; } }
        public bool Feather_hat { get { return feather_hat; } }
        public bool Glass_cabinet { get { return glass_cabinet; } }
        public bool Golden_alter { get { return golden_alter; } }
        public bool Magic_mirror { get { return magic_mirror; } }
        public bool Statue_of_wisdom { get { return statue_of_wisdom; } }
        #endregion
    }
    public class Discount_Adventurer_Data
    {
        private bool ad_0;
        private bool ad_1;
        private bool ad_2;
        private bool ad_3;

        public Discount_Adventurer_Data(bool ad_0, bool ad_1, bool ad_2, bool ad_3)
        {
            this.ad_0 = ad_0;
            this.ad_1 = ad_1;
            this.ad_2 = ad_2;
            this.ad_3 = ad_3;
        }
        public bool Ad_0 { get { return ad_0; } }
        public bool Ad_1 { get { return ad_1; } }
        public bool Ad_2 { get { return ad_2; } }
        public bool Ad_3 { get { return ad_3; } }

    }
    public class Is_Check_Potion_Data
    {
        private bool red_1;
        private bool red_0;
        private bool blue_1;
        private bool blue_0;
        private bool green_1;
        private bool green_0;
        private bool blank;
        private bool this_round_red_0;
        private bool this_round_green_0;
        private bool this_round_blue_0;

        public Is_Check_Potion_Data(bool red_1, bool red_0, bool blue_1, bool blue_0, bool green_1, bool green_0, bool blank,
                               bool this_round_red_0, bool this_round_green_0, bool this_round_blue_0)
        {
            this.red_1 = red_1;
            this.red_0 = red_0;
            this.blue_1 = blue_1;
            this.blue_0 = blue_0;
            this.green_1 = green_1;
            this.green_0 = green_0;
            this.blank = blank;
            this.this_round_red_0 = this_round_red_0;
            this.this_round_green_0 = this_round_green_0;
            this.this_round_blue_0 = this_round_blue_0;
        }

        public bool Red_1 { get { return red_1; } }
        public bool Red_0 { get { return red_0; } }
        public bool Blue_1 { get { return blue_1; } }
        public bool Blue_0 { get { return blue_0; } }
        public bool Green_1 { get { return green_1; } }
        public bool Green_0 { get { return green_0; } }
        public bool Blank { get { return blank; } }
        public bool This_round_red_0 { get { return this_round_red_0; } }
        public bool This_round_green_0 { get { return this_round_green_0; } }
        public bool This_round_blue_0 { get { return this_round_blue_0; } }
    }
    public class Have_Stamp_Data
    {
        private bool point_5_1;
        private bool point_5_2;
        private bool point_3_1;
        private bool point_3_2;
        private bool point_3_3;
        private bool question_red_1;
        private bool question_red_2;
        private bool question_blue_1;
        private bool question_blue_2;
        private bool question_green_1;
        private bool question_green_2;
        private int total;

        public Have_Stamp_Data(bool point_5_1, bool point_5_2, bool point_3_1, bool point_3_2, bool point_3_3,
                          bool question_red_1, bool question_red_2,
                          bool question_blue_1, bool question_blue_2,
                          bool question_green_1, bool question_green_2,
                          int total)
        {
            this.point_3_1 = point_3_1;
            this.point_3_2 = point_3_2;
            this.point_3_3 = point_3_3;
            this.point_5_1 = point_5_1;
            this.point_5_2 = point_5_2;
            this.question_blue_1 = question_blue_1;
            this.question_blue_2 = question_blue_2;
            this.question_green_1 = question_green_1;
            this.question_green_2 = question_green_2;
            this.question_red_1 = question_red_1;
            this.question_red_2 = question_red_2;

            this.total = total;
        }
        public bool Point_5_1 { get { return point_5_1; } }
        public bool Point_5_2 { get { return point_5_2; } }
        public bool Point_3_1 { get { return point_3_1; } }
        public bool Point_3_2 { get { return point_3_2; } }
        public bool Point_3_3 { get { return point_3_3; } }
        public bool Question_red_1 { get { return question_red_1; } }
        public bool Question_red_2 { get { return question_red_2; } }
        public bool Question_blue_1 { get { return question_blue_1; } }
        public bool Question_blue_2 { get { return question_blue_2; } }
        public bool Question_green_1 { get { return question_green_1; } }
        public bool Question_green_2 { get { return question_green_2; } }
        public int Total { get { return total; } }
    }
    public class Get_Extra_Point_Data
    {
        private bool red;
        private bool green;
        private bool blue;

        public Get_Extra_Point_Data(bool red, bool green, bool blue)
        {
            this.red = red;
            this.green = green;
            this.blue = blue;
        }
        public bool Red { get { return red; } }
        public bool Green { get { return green; } }
        public bool Blue { get { return blue; } }
    }

}