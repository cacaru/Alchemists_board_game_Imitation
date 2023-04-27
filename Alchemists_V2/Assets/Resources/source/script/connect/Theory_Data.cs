using UnityEditor;
using UnityEngine;

namespace Alchemists_data
{
    public class Theory_Data
    {
        public int element { get; set; }
        public Stamp_Info stamp_1 { get; set; }
        public Stamp_Info stamp_2 { get; set; }
        public Stamp_Info stamp_3 { get; set; }

        public Theory_Data(int element, Stamp_Info stamp_1, Stamp_Info stamp_2, Stamp_Info stamp_3)
        {
            this.element = element;
            this.stamp_1 = stamp_1;
            this.stamp_2 = stamp_2;
            this.stamp_3 = stamp_3;
        }
    }

    public class Stamp_Info
    {
        public string user_key { get; }
        public string color { get; }
        public string point { get; }

        public Stamp_Info(string user_key, string color, string point)
        {
            this.user_key = user_key;
            this.color = color;
            this.point = point;
        }
    }
}