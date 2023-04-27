using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alchemists_data
{
    public class Artifacts_Info
    { 
        public int Gold { get; }
        public int Point { get; set; }
        public string Name { get; }
        public string Kor_name { get; }
        public string Comment { get; }

        public Artifacts_Info(int gold, int point, string name, string comment, string kor_name)
        {
            this.Gold = gold;
            this.Point = point;
            this.Name = name;
            this.Comment = comment;
            this.Kor_name = kor_name;
        }
    }

}

