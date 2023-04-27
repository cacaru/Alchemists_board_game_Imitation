using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alchemists_data
{
    public class Reasoning_Table_Data
    {
        public string user_key { get; set; }
        public Reasoning_Table reasoning_table { get; set; } // o x null
        public Ingredient_Result ingredient_result { get; set; } // red_1,0, green_1,0, blue_1,0, blank

        public Reasoning_Table_Data(string user_key, Reasoning_Table reasoning_table, Ingredient_Result ingredient_result)
        {
            this.user_key = user_key;
            this.reasoning_table = reasoning_table;
            this.ingredient_result = ingredient_result;
        }
    }

    public class Reasoning_Table
    {
        public string[] part_1 { get; set; }
        public string[] part_2 { get; set; }
        public string[] part_3 { get; set; }
        public string[] part_4 { get; set; }
        public string[] part_5 { get; set; }
        public string[] part_6 { get; set; }
        public string[] part_7 { get; set; }
        public string[] part_8 { get; set; }

        public Reasoning_Table(string[] part_1, string[] part_2, string[] part_3, string[] part_4, string[] part_5, string[] part_6, string[] part_7, string[] part_8)
        {
            this.part_1 = part_1;
            this.part_2 = part_2;
            this.part_3 = part_3;
            this.part_4 = part_4;
            this.part_5 = part_5;
            this.part_6 = part_6;
            this.part_7 = part_7;
            this.part_8 = part_8;
        }
    }

    public class Ingredient_Result
    {
        public string[] part_1 { get; set; }
        public string[] part_2 { get; set; }
        public string[] part_3 { get; set; }
        public string[] part_4 { get; set; }
        public string[] part_5 { get; set; }
        public string[] part_6 { get; set; }
        public string[] part_7 { get; set; }
        public string[] part_8 { get; set; }

        public Ingredient_Result(string[] part_1, string[] part_2, string[] part_3, string[] part_4, string[] part_5, string[] part_6, string[] part_7, string[] part_8)
        {
            this.part_1 = part_1;
            this.part_2 = part_2;
            this.part_3 = part_3;
            this.part_4 = part_4;
            this.part_5 = part_5;
            this.part_6 = part_6;
            this.part_7 = part_7;
            this.part_8 = part_8;
        }
    }
}