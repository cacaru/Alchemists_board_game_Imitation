using UnityEditor;
using UnityEngine;

namespace Alchemists_data
{
    public class Exhibition_Result
    {
        public Exhibit_Potion_Container first { get; }
        public Exhibit_Potion_Container second { get; }

        public Exhibition_Result(Exhibit_Potion_Container first, Exhibit_Potion_Container second)
        {
            this.first = first;
            this.second = second;
        }
    }
    public class Exhibit_Potion_Container
    {
        public Exhibit_Potion_Data red_1 { get; }
        public Exhibit_Potion_Data red_0 { get; }
        public Exhibit_Potion_Data green_1 { get; }
        public Exhibit_Potion_Data green_0 { get; }
        public Exhibit_Potion_Data blue_1 { get; }
        public Exhibit_Potion_Data blue_0 { get; }

        public Exhibit_Potion_Container(Exhibit_Potion_Data red_1, Exhibit_Potion_Data red_0,
                                        Exhibit_Potion_Data green_1, Exhibit_Potion_Data green_0,
                                        Exhibit_Potion_Data blue_1, Exhibit_Potion_Data blue_0)
        {
            this.red_1 = red_1;
            this.red_0 = red_0;
            this.green_1 = green_1;
            this.green_0 = green_0;
            this.blue_1 = blue_1;
            this.blue_0 = blue_0;
        }
    }

    public class Exhibit_Potion_Data
    {
        public string user_key { get; }
        public string color { get; }
        public Exhibit_Potion_Data(string user_key, string color)
        {
            this.user_key = user_key;
            this.color = color;
        }
    }
}