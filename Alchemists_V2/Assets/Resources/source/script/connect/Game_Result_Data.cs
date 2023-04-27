using UnityEditor;
using UnityEngine;

namespace Alchemists_data
{
    public class Game_Result_Data
    {
        public string name { get; set; }
        public float score { get; set; }
        public float grade { get; set; }

        public string color { get; set; }

        public Game_Result_Data(string name, float score, float grade)
        {
            this.name = name;
            this.score = score;
            this.grade = grade;
        }
    }
}