using UnityEditor;
using UnityEngine;

namespace Alchemists_data
{
    public class Final_Round_Order
    {
        // final_round_order
        // user_key     :: 사용자 고유키
        // order        :: 현재 고른 순서
        // before_order :: 이전에 고른 순서 :: 색을 뺴기 위해 필요
        // user_color   :: 클라이언트에서 불필요한 연산을 줄이기 위해

        public string user_key { get; set; }
        public int order { get; set; }
        public int before_order { get; set; }
        public string user_color { get; set; }

        public Final_Round_Order(string user_key, int before_order, int order, string user_color)
        {
            this.user_color = user_color;
            this.user_key = user_key;
            this.order = order;
            this.before_order = before_order;
        }
    }
}